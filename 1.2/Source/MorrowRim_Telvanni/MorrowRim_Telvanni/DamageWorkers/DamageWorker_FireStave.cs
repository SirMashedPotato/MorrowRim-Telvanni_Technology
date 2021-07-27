using System;
using System.Linq;
using RimWorld;
using UnityEngine;
using Verse;

namespace MorrowRim_Telvanni
{
    class DamageWorker_FireStave : DamageWorker_AddInjury
    {
		//copied from DamageWorker_Flame
		public override DamageWorker.DamageResult Apply(DamageInfo dinfo, Thing victim)
		{
			Pawn pawn = victim as Pawn;
			if (pawn != null && pawn.Faction == Faction.OfPlayer)
			{
				Find.TickManager.slower.SignalForceNormalSpeedShort();
			}
			Map map = victim.Map;
			DamageWorker.DamageResult damageResult = base.Apply(dinfo, victim);
			//Start the fire engines
			if (Rand.Chance(0.15f))
			{
				if (!damageResult.deflected && !dinfo.InstantPermanentInjury)
				{
					if (victim.HasAttachment(RimWorld.ThingDefOf.Fire))
					{
						Fire fire = AttachmentUtility.GetAttachment(victim, RimWorld.ThingDefOf.Fire) as Fire;
						fire.fireSize = fire.CurrentSize() + Rand.Range(0.15f, 0.25f);
					} 
					else
					{
						victim.TryAttachFire(Rand.Range(0.15f, 0.25f));
					}
				}
				if (victim.Destroyed && map != null && pawn == null)
				{
					foreach (IntVec3 c in victim.OccupiedRect())
					{
						FilthMaker.TryMakeFilth(c, map, RimWorld.ThingDefOf.Filth_Ash, 1, FilthSourceFlags.None);
					}
					Plant plant = victim as Plant;
					if (plant != null && victim.def.plant.IsTree && plant.LifeStage != PlantLifeStage.Sowing && victim.def != RimWorld.ThingDefOf.BurnedTree)
					{
						((DeadPlant)GenSpawn.Spawn(RimWorld.ThingDefOf.BurnedTree, victim.Position, map, WipeMode.Vanish)).Growth = plant.Growth;
					}
				}
			}
			return damageResult;
		}
	}
}
