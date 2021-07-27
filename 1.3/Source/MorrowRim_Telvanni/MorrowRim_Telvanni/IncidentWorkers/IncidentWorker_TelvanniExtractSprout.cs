using System;
using System.Collections.Generic;
using Verse;
using RimWorld;


namespace MorrowRim_Telvanni
{
    class IncidentWorker_TelvanniExtractSprout : IncidentWorker
	{
		private static readonly List<ThingDef> possibleFlowers = new List<ThingDef>
		{
			ThingDefOf.Telvanni_BoltVine, ThingDefOf.Telvanni_GraveNut, ThingDefOf.Telvanni_NightPitcher, ThingDefOf.Telvanni_SpectralBell, ThingDefOf.Telvanni_VampireGrass
		};

		protected override bool CanFireNowSub(IncidentParms parms)
		{
			if (!base.CanFireNowSub(parms))
			{
				return false;
			}
			Map map = (Map)parms.target;
			IntVec3 intVec;
			return map.weatherManager.growthSeasonMemory.GrowthSeasonOutdoorsNow && this.TryFindRootCell(map, out intVec, possibleFlowers.RandomElement());
		}

		protected override bool TryExecuteWorker(IncidentParms parms)
		{
			Map map = (Map)parms.target;
			IntVec3 intVec;
			ThingDef extractPlant = possibleFlowers.RandomElement();
			if (!this.TryFindRootCell(map, out intVec, extractPlant))
			{
				return false;
			}
			Thing thing = null;
			int randomInRange = CountRange.RandomInRange;
			for (int i = 0; i < randomInRange; i++)
			{
				IntVec3 root = intVec;
				Map map2 = map;
				int radius = SpawnRadius;
				IntVec3 intVec2;
				if (!CellFinder.TryRandomClosewalkCellNear(root, map2, radius, out intVec2, (IntVec3 x) => this.CanSpawnAt(x, map, extractPlant)))
				{
					break;
				}
				Plant plant = intVec2.GetPlant(map);
				if (plant != null)
				{
					plant.Destroy(DestroyMode.Vanish);
				}
				Thing thing2 = GenSpawn.Spawn(extractPlant, intVec2, map, WipeMode.Vanish);
				if (thing == null)
				{
					thing = thing2;
				}
			}
			if (thing == null)
			{
				return false;
			}
			base.SendStandardLetter(parms, thing, Array.Empty<NamedArgument>());
			return true;
		}

		private bool TryFindRootCell(Map map, out IntVec3 cell, ThingDef extractPlant)
		{
			return CellFinderLoose.TryFindRandomNotEdgeCellWith(10, (IntVec3 x) => this.CanSpawnAt(x, map, extractPlant) && x.GetRoom(map).CellCount >= MinRoomCells, map, out cell);
		}

		private bool CanSpawnAt(IntVec3 c, Map map, ThingDef extractPlant)
		{
			if (!c.Standable(map) || c.Fogged(map) || map.fertilityGrid.FertilityAt(c) < extractPlant.plant.fertilityMin || !c.GetRoom(map).PsychologicallyOutdoors || c.GetEdifice(map) != null || !PlantUtility.GrowthSeasonNow(c, map, false))
			{
				return false;
			}
			Plant plant = c.GetPlant(map);
			if (plant != null && plant.def.plant.growDays > 10f)
			{
				return false;
			}
			List<Thing> thingList = c.GetThingList(map);
			for (int i = 0; i < thingList.Count; i++)
			{
				if (extractPlant == thingList[i].def)
				{
					return false;
				}
			}
			return true;
		}

		private static readonly IntRange CountRange = new IntRange(10, 20);
		private const int MinRoomCells = 64;
		private const int SpawnRadius = 6;
	}
}
