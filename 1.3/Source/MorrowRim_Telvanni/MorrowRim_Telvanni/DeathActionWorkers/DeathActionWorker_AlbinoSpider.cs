using System;
using Verse;
using RimWorld;

namespace MorrowRim_Telvanni
{
    class DeathActionWorker_AlbinoSpider : DeathActionWorker
    {
        public override void PawnDied(Corpse corpse)
        {
            FilthMaker.TryMakeFilth(corpse.Position, corpse.Map, ThingDefOf.Filth_BloodInsect, Rand.RangeInclusive(1, 3));
            //see if a pod is spawned
            if (LoadedModManager.RunningModsListForReading.Any(x => x.Name == "MorrowRim - Telvanni Spiders"))
            {
                AlbinoSpiderUtility.SpawnAlbinoSpiderPod(corpse);
                AlbinoSpiderUtility.PlaySpiderSound(corpse);
            }
            corpse.Destroy();
        }
    }
}
