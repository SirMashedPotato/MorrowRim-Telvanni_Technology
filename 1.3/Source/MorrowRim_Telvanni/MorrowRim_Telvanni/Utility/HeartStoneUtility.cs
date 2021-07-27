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
            Log.Message("Base chance is: " + (baseSpawnChance + GetWorkerSkill(worker) + GetAdditionalChances()));
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
            if (ResearchProjectDefOf.MorrowRim_HeartStoneStudiesI.IsFinished)
            {
                addChance += additionalSpawnChance;
            }
            if (ResearchProjectDefOf.MorrowRim_HeartStoneStudiesII.IsFinished)
            {
                addChance += additionalSpawnChance;
            }
            return addChance;
        }
    }
}
