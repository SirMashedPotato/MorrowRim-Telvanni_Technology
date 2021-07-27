using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;
using Verse.AI.Group;
using Verse.Sound;
using RimWorld;

namespace MorrowRim_Telvanni
{
    public class AlbinoSpiderNestSpawner : ThingWithComps
	{
		/* get thingDefs that aren't included in this mod*/
		private static readonly PawnKindDef albinoSpider = DefDatabase<PawnKindDef>.GetNamed("MorrowRim_AlbinoSpider");

		public override void ExposeData()
		{
			base.ExposeData();
			Scribe_Values.Look<int>(ref this.secondarySpawnTick, "secondarySpawnTick", 0, false);
			Scribe_Values.Look<bool>(ref this.spawnHive, "spawnHive", true, false);
			Scribe_Values.Look<float>(ref this.insectsPoints, "insectsPoints", 0f, false);
			Scribe_Values.Look<bool>(ref this.spawnedByInfestationThingComp, "spawnedByInfestationThingComp", false, false);
		}

		public override void SpawnSetup(Map map, bool respawningAfterLoad)
		{
			base.SpawnSetup(map, respawningAfterLoad);
			if (!respawningAfterLoad)
			{
				this.secondarySpawnTick = Find.TickManager.TicksGame + this.ResultSpawnDelay.RandomInRange.SecondsToTicks();
			}
			this.CreateSustainer();
		}

		public override void Tick()
		{
			if (base.Spawned)
			{
				this.sustainer.Maintain();
				Vector3 vector = base.Position.ToVector3Shifted();
				IntVec3 c;
				if (Rand.MTBEventOccurs(DustMoteSpawnMTB, 1f, 1.TicksToSeconds()))
				{
					FleckMaker.ThrowDustPuffThick(new Vector3(vector.x, 0f, vector.z)
					{
						y = AltitudeLayer.MoteOverhead.AltitudeFor()
					}, base.Map, Rand.Range(1.5f, 3f), new Color(1f, 1f, 1f, 2.5f));
				}
				if (this.secondarySpawnTick <= Find.TickManager.TicksGame)
				{
					this.sustainer.End();
					Map map = base.Map;
					IntVec3 position = base.Position;
					this.Destroy(DestroyMode.Vanish);
					/* spawn spider */
					List<Pawn> list = new List<Pawn>();
					Pawn pawn = PawnGenerator.GeneratePawn(albinoSpider, Faction.OfInsects);
					GenSpawn.Spawn(pawn, CellFinder.RandomClosewalkCellNear(position, map, 2, null), map, WipeMode.Vanish);
					pawn.mindState.spawnedByInfestationThingComp = true;
					list.Add(pawn);
					LordMaker.MakeNewLord(Faction.OfInsects, new LordJob_AssaultColony(Faction.OfInsects, true, false, false, false, true), map, list);
				}
			}
		}

		public override void Draw()
		{
			Rand.PushState();
			Rand.Seed = this.thingIDNumber;
			for (int i = 0; i < 6; i++)
			{
				this.DrawDustPart(Rand.Range(0f, 360f), Rand.Range(0.9f, 1.1f) * (float)Rand.Sign * 4f, Rand.Range(1f, 1.5f));
			}
			Rand.PopState();
		}

		private void DrawDustPart(float initialAngle, float speedMultiplier, float scale)
		{
			float num = (Find.TickManager.TicksGame - this.secondarySpawnTick).TicksToSeconds();
			Vector3 pos = base.Position.ToVector3ShiftedWithAltitude(AltitudeLayer.Filth);
			pos.y += 0.0454545468f * Rand.Range(0f, 1f);
			Color value = new Color(0.470588237f, 0.384313732f, 0.3254902f, 0.7f);
			matPropertyBlock.SetColor(ShaderPropertyIDs.Color, value);
			Matrix4x4 matrix = Matrix4x4.TRS(pos, Quaternion.Euler(0f, initialAngle + speedMultiplier * num, 0f), Vector3.one * scale);
			Graphics.DrawMesh(MeshPool.plane10, matrix, TunnelMaterial, 0, null, 0, matPropertyBlock);
		}

		private void CreateSustainer()
		{
			LongEventHandler.ExecuteWhenFinished(delegate
			{
				SoundDef tunnel = RimWorld.SoundDefOf.Tunnel;
				this.sustainer = tunnel.TrySpawnSustainer(SoundInfo.InMap(this, MaintenanceType.PerTick));
			});
		}

		// Token: 0x04002DEB RID: 11755
		private int secondarySpawnTick;

		// Token: 0x04002B0E RID: 11022
		public bool spawnHive = true;

		// Token: 0x04002B0F RID: 11023
		public float insectsPoints;

		// Token: 0x04002B10 RID: 11024
		public bool spawnedByInfestationThingComp;

		// Token: 0x04002DEF RID: 11759
		private Sustainer sustainer;

		// Token: 0x04002DF0 RID: 11760
		private static MaterialPropertyBlock matPropertyBlock = new MaterialPropertyBlock();

		// Token: 0x04002DF1 RID: 11761
		private readonly FloatRange ResultSpawnDelay = new FloatRange(26f, 30f);

		// Token: 0x04002DF2 RID: 11762
		[TweakValue("Gameplay", 0f, 1f)]
		private static float DustMoteSpawnMTB = 0.2f;

		// Token: 0x04002DF3 RID: 11763
		[TweakValue("Gameplay", 0f, 1f)]
		private static float FilthSpawnMTB = 0.3f;

		// Token: 0x04002DF4 RID: 11764
		[TweakValue("Gameplay", 0f, 10f)]
		private static float FilthSpawnRadius = 3f;

		// Token: 0x04002DF5 RID: 11765
		private static readonly Material TunnelMaterial = MaterialPool.MatFrom("Things/Filth/Grainy/GrainyA", ShaderDatabase.Transparent);

		// Token: 0x04002DF6 RID: 11766
		private static List<ThingDef> filthTypes = new List<ThingDef>();
	}
}
