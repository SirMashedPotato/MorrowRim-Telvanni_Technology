using RimWorld;
using System.Reflection;
using Verse;
using System;
using System.Collections.Generic;
using Verse.Sound;

namespace MorrowRim_Telvanni
{
    public static class AlbinoSpiderNestUtility
    {
        /* get thingDefs that aren't included in this mod*/
        private static readonly ThingDef albinoSpiderNestSpawner = DefDatabase<ThingDef>.GetNamed("MorrowRim_AlbinoSpiderNestSpawner");

        /* For Nest Incident */

        public static Thing SpawnTunnels(int hiveCount, Map map, bool spawnAnywhereIfNoGoodCell = false, bool ignoreRoofedRequirement = false, string questTag = null)
        {
            IntVec3 loc;

            if (!RCellFinder.TryFindRandomCellNearTheCenterOfTheMapWith(delegate (IntVec3 x)
            {
                if (!x.Standable(map) || x.Fogged(map))
                {
                    return false;
                }
                bool result = false;
                int num = GenRadial.NumCellsInRadius(3f);
                for (int j = 0; j < num; j++)
                {
                    IntVec3 c = x + GenRadial.RadialPattern[j];
                    if (c.InBounds(map))
                    {
                        TerrainDef terrain = c.GetTerrain(map);
                        if (terrain != null && terrain.affordances.Contains(TerrainAffordanceDefOf.Diggable))
                        {
                            result = true;
                            break;
                        }
                    }
                }
                return result;
            }, map, out loc))
            {
                return null;
            }
            
            Thing thing = GenSpawn.Spawn(ThingMaker.MakeThing(albinoSpiderNestSpawner, null), loc, map, WipeMode.FullRefund);
            QuestUtility.AddQuestTag(thing, questTag);
            for (int i = 0; i < hiveCount - 1; i++)
            {
                loc = loc.RandomAdjacentCell8Way().RandomAdjacentCell8Way();
                if (loc.IsValid)
                {
                    thing = GenSpawn.Spawn(ThingMaker.MakeThing(albinoSpiderNestSpawner, null), loc, map, WipeMode.FullRefund);
                    QuestUtility.AddQuestTag(thing, questTag);
                }
            }
            return thing;
        }
    }
}
