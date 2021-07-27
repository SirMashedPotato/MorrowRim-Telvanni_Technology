using RimWorld;
using System.Reflection;
using Verse;
using System;
using System.Collections.Generic;

namespace MorrowRim_Telvanni
{
    public static class HeartStoneUtility
    {

        private static readonly float baseSpawnChance = 0.05f;
        private static readonly float additionalSpawnChance = 0.05f;

        public static void SpawnHeartStone(Pawn worker)
        {
            if (CheckChance(worker))
            {
                Thing thing = ThingMaker.MakeThing(ThingDefOf.MorrowRim_HeartStoneDepleted);
                thing.stackCount = 1;
                GenPlace.TryPlaceThing(thing, worker.Position, worker.Map, ThingPlaceMode.Near);
                Messages.Message("MorrowRim_ObrtainedDepletedHeartStone".Translate(worker.Name), worker, MessageTypeDefOf.PositiveEvent);
            }
        }

        public static bool CheckChance(Pawn worker)
        {
            return Rand.Chance(baseSpawnChance + GetWorkerSkill(worker) + GetAdditionalChances());
        }

        private static float GetWorkerSkill(Pawn worker)
        {
            float temp = worker.skills.GetSkill(SkillDefOf.Intellectual).levelInt / 50f;
            return temp;
        }

        private static float GetAdditionalChances()
        {
            float addChance = 0f;
            ResearchManager researchManager = new ResearchManager();
            if (researchManager.GetProgress(ResearchProjectDefOf.MorrowRim_HeartStoneStudiesI) == ResearchProjectDefOf.MorrowRim_HeartStoneStudiesI.CostApparent)
            {
                addChance += additionalSpawnChance;
            }
            if (researchManager.GetProgress(ResearchProjectDefOf.MorrowRim_HeartStoneStudiesII) == ResearchProjectDefOf.MorrowRim_HeartStoneStudiesII.CostApparent)
            {
                addChance += additionalSpawnChance;
            }
            return addChance;
        }
    }
}
