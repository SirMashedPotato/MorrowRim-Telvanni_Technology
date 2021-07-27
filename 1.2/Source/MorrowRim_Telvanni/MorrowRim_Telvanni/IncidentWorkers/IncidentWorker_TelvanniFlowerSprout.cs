using System;
using System.Collections.Generic;
using Verse;
using RimWorld;

namespace MorrowRim_Telvanni
{
    class IncidentWorker_TelvanniFlowerSprout : IncidentWorker
    {
        private static readonly List<ThingDef> possibleFlowers = new List<ThingDef>
        { 
            ThingDefOf.Telvanni_AshCap, ThingDefOf.Telvanni_FirePetal, 
            ThingDefOf.Telvanni_FrostLeaf, ThingDefOf.Telvanni_StoneAnther, 
            ThingDefOf.Telvanni_VoidBloom, ThingDefOf.Telvanni_WaterRoot,
            ThingDefOf.Telvanni_WindBulb
        };

        protected override bool CanFireNowSub(IncidentParms parms)
        {
            if (!base.CanFireNowSub(parms))
            {
                return false;
            }
            Map map = (Map)parms.target;
            IntVec3 intVec;
            return map.weatherManager.growthSeasonMemory.GrowthSeasonOutdoorsNow && this.TryFindRootCell(map, out intVec);
        }

		protected override bool TryExecuteWorker(IncidentParms parms)
		{
			Map map = (Map)parms.target;
			IntVec3 intVec;
			if (!this.TryFindRootCell(map, out intVec))
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
				if (!CellFinder.TryRandomClosewalkCellNear(root, map2, radius, out intVec2, (IntVec3 x) => this.CanSpawnAt(x, map)))
				{
					break;
				}
				Plant plant = intVec2.GetPlant(map);
				if (plant != null)
				{
					plant.Destroy(DestroyMode.Vanish);
				}
				Thing thing2 = GenSpawn.Spawn(possibleFlowers.RandomElement(), intVec2, map, WipeMode.Vanish);
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

		private bool TryFindRootCell(Map map, out IntVec3 cell)
		{
			return CellFinderLoose.TryFindRandomNotEdgeCellWith(10, (IntVec3 x) => this.CanSpawnAt(x, map) && x.GetRoom(map, RegionType.Set_Passable).CellCount >= MinRoomCells, map, out cell);
		}

		private bool CanSpawnAt(IntVec3 c, Map map)
		{
			if (!c.Standable(map) || c.Fogged(map) || map.fertilityGrid.FertilityAt(c) < ThingDefOf.Telvanni_AshCap.plant.fertilityMin || !c.GetRoom(map, RegionType.Set_Passable).PsychologicallyOutdoors || c.GetEdifice(map) != null || !PlantUtility.GrowthSeasonNow(c, map, false))
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
				if (possibleFlowers.Contains(thingList[i].def))
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
