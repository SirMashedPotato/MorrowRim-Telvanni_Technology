using System;
using Verse;
using RimWorld;

namespace MorrowRim_Telvanni
{
    class DeathActionWorker_Atronach : DeathActionWorker
    {
        public override void PawnDied(Corpse corpse)
        {
            CompRottable compRottable = corpse.TryGetComp<CompRottable>();
            Log.Message("Rotting");
            corpse.Destroy();
        }
    }
}
