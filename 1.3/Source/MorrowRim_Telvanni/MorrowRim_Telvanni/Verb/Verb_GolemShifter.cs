using System;
using Verse;
using Verse.AI;
using RimWorld;

namespace MorrowRim_Telvanni
{
    /*
    class Verb_GolemShifter : Verb_CastBase
    {
        protected override bool TryCastShot()
        {
            Pawn user = this.caster as Pawn;
            Pawn target = this.currentTarget.Pawn;

            MoteMaker.MakeStaticMote(target.Position, target.Map, RimWorld.ThingDefOf.Mote_PsycastAreaEffect, 5f);
            target.pather.StopDead();

            target.Position = user.Position;
            target.pather.ResetToCurrentPosition();
            target.jobs.EndCurrentJob(JobCondition.InterruptForced, true, true);

            MoteMaker.MakeStaticMote(target.Position, target.Map, RimWorld.ThingDefOf.Mote_PsycastAreaEffect, 5f);

            UseCharge(base.ReloadableCompSource);
            return true;
        }

        private static void UseCharge(CompReloadable comp)
        {
            if (comp == null || !comp.CanBeUsed)
            {
                return;
            }
            comp.UsedOnce();
        }
        */
        /* Targeting */
        /*
        public override bool IsUsableOn(Thing target)
        {
            return GolemUtility.IsGolem(target) && GolemUtility.CanShiftGolem(target);
        }

        public override TargetingParameters targetParams => GetTargetingParameters();

        protected TargetingParameters GetTargetingParameters()
        {
            return new TargetingParameters
            {
                canTargetPawns = true,
                validator = ((TargetInfo x) => TargetValidator(x.Thing))
            };
        }

        public bool TargetValidator(Thing t)
        {
            Pawn pawn = t as Pawn;
            if (pawn != null)
            {
                if (GolemUtility.IsGolem(pawn) && GolemUtility.CanShiftGolem(t))
                {
                    return true;
                }
            }
            return false;
        }
    }
    */
}
