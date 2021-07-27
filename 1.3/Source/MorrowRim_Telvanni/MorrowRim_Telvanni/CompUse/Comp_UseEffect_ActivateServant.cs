using System;
using Verse;
using RimWorld;

namespace MorrowRim_Telvanni
{
	class CompUseEffect_ActivateServant : CompUseEffect
	{
		public CompProperties_UseEffect_ActivateServant Props
		{
			get
			{
				return (CompProperties_UseEffect_ActivateServant)this.props;
			}
		}
		public override void DoEffect(Pawn usedBy)
		{
			base.DoEffect(usedBy);
			if (this.Props.ServantKind != null)
			{
				Pawn servant = PawnGenerator.GeneratePawn(this.Props.ServantKind, usedBy.Faction);
				PawnUtility.TrySpawnHatchedOrBornPawn(servant, usedBy);

				//optionals
				if (this.Props.bond) servant.relations.AddDirectRelation(PawnRelationDefOf.Bond, usedBy);
				if (this.Props.hediffToApply != null)
				{
					servant.health.AddHediff(this.Props.hediffToApply);
				}
				/*
				if (this.Props.applyAwakeningHediff)
				{
					servant.health.AddHediff(HediffDefOf.DarkDescent_ServantAwakening).Severity = 0.9f;
				}
				*/
				if (this.Props.useQuality)
				{
					parent.TryGetQuality(out QualityCategory qc);
					float q = (float)qc;
					q /= 10f;
					if (this.Props.checkResearch && GolemUtility.CheckResearch())
					{
						q += 0.1f;
					}
					if (qc == QualityCategory.Legendary || q == 0.7f)
					{
						servant.health.AddHediff(HediffDefOf.MorrowRim_GolemQuality_Legendary).Severity = 0.01f;
						return;
					}
					servant.health.AddHediff(HediffDefOf.MorrowRim_GolemQuality).Severity = q + 0.1f;
				}
                if (Props.addDrafter && servant.drafter == null)
                {
					servant.drafter = new Pawn_DraftController(servant);
				}
			}
		}
	}
}
