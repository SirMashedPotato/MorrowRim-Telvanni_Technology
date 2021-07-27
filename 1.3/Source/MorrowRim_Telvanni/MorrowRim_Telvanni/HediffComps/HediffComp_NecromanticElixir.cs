using RimWorld;
using Verse;

namespace MorrowRim_Telvanni
{
    class HediffComp_NecromanticElixir : HediffComp
    {
        public HediffCompProperties_NecromanticElixir Props
        {
            get
            {
                return (HediffCompProperties_NecromanticElixir)this.props;
            }
        }

        public override void Notify_PawnDied()
        {        
            if (parent.pawn.Corpse.Map != null && parent.pawn.Corpse != null && parent.pawn.health.hediffSet.GetFirstHediffOfDef(Props.resDef) == null)
            {
                Pawn pawn = parent.pawn.Corpse.InnerPawn;
                ResurrectionUtility.Resurrect(parent.pawn.Corpse.InnerPawn);
                pawn.health.AddHediff(Props.resDef).Severity = Props.severity;
                Messages.Message("MorrowRim_NecromanticResurrection".Translate(pawn.Name, this.Def.label), pawn, MessageTypeDefOf.PositiveEvent);
                pawn.health.RemoveHediff(parent);
                return;
            }
            base.Notify_PawnDied();
        }
    }
}
