using System;
using System.Collections.Generic;
using Verse;
using RimWorld;

namespace MorrowRim_Telvanni
{
    class IngestionOutcomeDoer_RemoveHediff : IngestionOutcomeDoer
    {
		protected override void DoIngestionOutcomeSpecial(Pawn pawn, Thing ingested)
		{
            if (Hediff != null && pawn.health.hediffSet.GetFirstHediffOfDef(Hediff) != null)
            {
                pawn.health.RemoveHediff(pawn.health.hediffSet.GetFirstHediffOfDef(Hediff));
            }

		}

		public HediffDef Hediff;
    }
}
