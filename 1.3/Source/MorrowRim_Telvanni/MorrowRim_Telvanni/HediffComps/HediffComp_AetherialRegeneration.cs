using Verse;
using RimWorld;
using System.Linq;

namespace MorrowRim_Telvanni
{
    class HediffComp_AetherialRegeneration : HediffComp
    {
        public int ticks = 0;

        public HediffCompProperties_AetherialRegeneration Props
        {
            get
            {
                return (HediffCompProperties_AetherialRegeneration)this.props;
            }
        }

		public override void CompPostTick(ref float severityAdjustment)
		{
			base.CompPostTick(ref severityAdjustment);
			Pawn pawn = base.Pawn;
			if (ticks >= Props.Ticks)
			{
				if (pawn.Dead)
				{
					ticks = 0;
					return;
				}
				else
				{
					float CurrentNumber = Props.BaseNumber;
                    if (Props.ModifiedByPumping)
                    {
						CurrentNumber *= pawn.health.capacities.GetLevel(PawnCapacityDefOf.BloodPumping);

					}
					for (int i = 0; i != Props.BaseNumber; i++)
					{
						Hediff hediff;
						if (!(from hd in pawn.health.hediffSet.hediffs
							  where hd.def.displayWound || hd.def.tendable
							  select hd).TryRandomElement(out hediff))
						{
							return;
						}
						//check if part is missing
						if (pawn.health.hediffSet.PartIsMissing(hediff.Part))
						{
							pawn.health.RestorePart(hediff.Part);
						}
						else
						{
							hediff.Severity -= Props.Severity;
						}
					}
				}
				ticks = 0;
			}
			else ticks++;
		}
	}
}
