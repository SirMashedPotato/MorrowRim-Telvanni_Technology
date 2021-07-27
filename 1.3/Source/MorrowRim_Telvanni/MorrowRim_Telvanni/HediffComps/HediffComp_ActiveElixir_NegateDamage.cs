using RimWorld;
using Verse;
using Verse.Sound;

namespace MorrowRim_Telvanni
{
    class HediffComp_ActiveElixir_NegateDamage : HediffComp
    {
        public HediffCompProperties_ActiveElixir_NegateDamage Props
        {
            get
            {
                return (HediffCompProperties_ActiveElixir_NegateDamage)this.props;
            }
        }

        public override void Notify_PawnPostApplyDamage(DamageInfo dinfo, float totalDamageDealt)
        {
            parent.Severity -= 0.05f;
            SoundDef sound = RimWorld.SoundDefOf.MetalHitImportant;
            sound.PlayOneShot(new TargetInfo(parent.pawn.Position, parent.pawn.Map, false));
            //toxicity
            if (Pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.MorrowRim_PotionSickness) != null)
            {
                Pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.MorrowRim_PotionSickness).Severity += 0.025f;
            } 
            else
            {
                Pawn.health.AddHediff(HediffDefOf.MorrowRim_PotionSickness).Severity = 0.025f;
            }
        }
    }
}
