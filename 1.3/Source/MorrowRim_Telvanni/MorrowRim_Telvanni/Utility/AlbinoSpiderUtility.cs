using RimWorld;
using System.Reflection;
using Verse;
using System;
using System.Collections.Generic;
using Verse.Sound;

namespace MorrowRim_Telvanni
{
    public static class AlbinoSpiderUtility
    {
        private static readonly float baseSpawnChance = 0.50f;
        private static readonly float additionalSpawnChance = 0.10f;

        /* get thingDefs that aren't included in this mod*/
        private static readonly ThingDef albinoSpiderPod = DefDatabase<ThingDef>.GetNamed("MorrowRim_AlbinoSpiderPod");
        private static readonly List<ResearchProjectDef> researchProjects = new List<ResearchProjectDef>
        {
            DefDatabase<ResearchProjectDef>.GetNamed("MorrowRim_SpiderForge"),
            DefDatabase<ResearchProjectDef>.GetNamed("MorrowRim_SpiderTraps"),
            DefDatabase<ResearchProjectDef>.GetNamed("MorrowRim_SpiderAdvanced")
        };

        /* For Spider Pawns */

        public static void SpawnAlbinoSpiderPod(Corpse spider)
        {
            if (CheckChance())
            {
                Thing thing = ThingMaker.MakeThing(albinoSpiderPod);
                thing.stackCount = 1;
                GenPlace.TryPlaceThing(thing, spider.Position, spider.Map, ThingPlaceMode.Near);
                Messages.Message("MorrowRim_ObtainedAlbinoSpiderPod".Translate(), thing, MessageTypeDefOf.PositiveEvent);
            }
        }

        public static bool CheckChance()
        {
            Log.Message("Base chance is: " + (baseSpawnChance + GetAdditionalChances()));
            return Rand.Chance(baseSpawnChance + GetAdditionalChances());
        }

        private static float GetAdditionalChances()
        {
            float addChance = 0f;
            foreach (ResearchProjectDef research in researchProjects)
            {
                if (research.IsFinished)
                {
                    addChance += additionalSpawnChance;
                }
            }
            return addChance;
        }

        public static void PlaySpiderSound(Thing target)
        {
            SoundDef sound = SoundDefOf.MorrowRim_Impact_TelvanniSpider;
            sound.PlayOneShot(new TargetInfo(target.Position, target.Map, false));
        }
    }
}
