using System;
using System.Collections.Generic;
using Verse;
using RimWorld;

namespace MorrowRim_Telvanni
{
	/* Based on  IngestionOutcomeDoer_GiveHediff */
	public class IngestionOutcomeDoer_Cleansing : IngestionOutcomeDoer
	{
		protected override void DoIngestionOutcomeSpecial(Pawn pawn, Thing ingested)
		{
			float q;
			ingested.TryGetQuality(out QualityCategory qc);
			q = (float)qc;
			float actual = (q++)/10;
			Hediff hediff = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.MorrowRim_PotionSickness);
			if (hediff != null)
			{
				if (qc == QualityCategory.Legendary || hediff.Severity <= actual)
				{
					pawn.health.RemoveHediff(hediff);
				}
				else
				{
					pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.MorrowRim_PotionSickness).Severity = hediff.Severity - actual;
				}
			}
		}
		/*
		public override IEnumerable<StatDrawEntry> SpecialDisplayStats(ThingDef parentDef)
		{
			if (parentDef.IsDrug && this.chance >= 1f)
			{
				for (int i = 0; i != hediffDefs.Count; i++)
				{
					foreach (StatDrawEntry statDrawEntry in this.hediffDefs[i].SpecialDisplayStats(StatRequest.ForEmpty()))
					{
						yield return statDrawEntry;
					}
					IEnumerator<StatDrawEntry> enumerator = null;
				}

			}
			yield break;
		}
		
		public List<HediffDef> hediffDefs;
		public float severity = -1;
		*/
	}
}
