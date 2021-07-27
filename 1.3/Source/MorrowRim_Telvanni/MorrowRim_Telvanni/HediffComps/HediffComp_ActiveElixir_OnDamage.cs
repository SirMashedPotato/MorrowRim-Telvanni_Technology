using RimWorld;
using Verse;

namespace MorrowRim_Telvanni
{
    class HediffComp_ActiveElixir_OnDamage : HediffComp
    {
        public HediffCompProperties_ActiveElixir_OnDamage Props
        {
            get
            {
                return (HediffCompProperties_ActiveElixir_OnDamage)this.props;
            }
        }

        public override void Notify_PawnPostApplyDamage(DamageInfo dinfo, float totalDamageDealt)
        {
            if (Props.thingToSpawn != null)
            {
                Pawn pawn = parent.pawn;
                Gas gas = pawn.Position.GetGas(pawn.Map);
                if (gas == null)
                {
                    GenExplosion.DoExplosion(pawn.Position, pawn.Map, Props.Radius, DamageDefOf.Smoke, pawn, -1, -1, null, null, null, null, Props.thingToSpawn, 1);
                    parent.Severity -= 0.05f;
                }
            }
            base.Notify_PawnPostApplyDamage(dinfo, totalDamageDealt);
        }
    }
}
