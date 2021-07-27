using System;
using Verse;
using Verse.AI;
using RimWorld;

namespace MorrowRim_Telvanni
{
    class Verb_AddHeddifAtronach : Verb_CastBase
    {

        protected override bool TryCastShot()
        {
            ThingDef item = this.EquipmentSource.def;
            if (!CheckTarget(this.currentTarget.Pawn))
            {
                Messages.Message("MorrowRim_TargetNotAtronach".Translate(), this.CurrentTarget.Pawn, MessageTypeDefOf.RejectInput);
                return false;
            }
            Pawn user = this.caster as Pawn;
            var props = AddHediffProperties.Get(item);
            if (props.hediffToAdd != null)
            {
                Find.BattleLog.Add(new BattleLogEntry_ItemUsed(user, this.CurrentTarget.Pawn, item, RulePackDefOf.Event_ItemUsed));
                this.currentTarget.Pawn.health.AddHediff(props.hediffToAdd).Severity = props.severity;
                UseCharge(base.ReloadableCompSource);
                return true;
            }
            return false;
        }

        private bool CheckTarget(Pawn target)
        {
            var props = AtronachProperties.Get(target.def);
            return props != null && props.isAtronach;
        }

        private static void UseCharge(CompReloadable comp)
        {
            if (comp == null || !comp.CanBeUsed)
            {
                return;
            }
            comp.UsedOnce();
        }

        /*
        public override float HighlightFieldRadiusAroundTarget(out bool needLOSToCenter)
        {
            var props = AddHediffProperties.Get(item);
            if ()
            {
                needLOSToCenter = false;
                return;
            }
        }
        */
    }
}
