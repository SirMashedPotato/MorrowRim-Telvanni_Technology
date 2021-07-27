using System;
using System.Collections.Generic;
using Verse;
using Verse.AI;
using RimWorld;
using System.Linq;

namespace MorrowRim_Telvanni
{
    class JobDriver_RepairSelf : JobDriver
    {
        private Pawn Target
        {
            get
            {
                return (Pawn)this.job.GetTarget(TargetIndex.A).Thing;
            }
        }

        private Thing Item
        {
            get
            {
                return this.job.GetTarget(TargetIndex.B).Thing;
            }
        }

        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return this.pawn.Reserve(this.Item, this.job, 1, 1, null, errorOnFailed);
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            yield return Toils_Goto.GotoThing(TargetIndex.B, PathEndMode.Touch).FailOnDespawnedOrNull(TargetIndex.B).FailOnDespawnedOrNull(TargetIndex.A);
            yield return Toils_Haul.StartCarryThing(TargetIndex.B, false, false, false);
            Toil toil = Toils_General.Wait(DurationTicks, TargetIndex.None);
            toil.WithProgressBarToilDelay(TargetIndex.A, false, -0.5f);
            toil.FailOnDespawnedOrNull(TargetIndex.A);
            toil.FailOnCannotTouch(TargetIndex.A, PathEndMode.Touch);
            yield return toil;
            yield return Toils_General.Do(new Action(this.ApplyCore));
            yield break;
        }

        private void ApplyCore()
        {
            Pawn p = this.Target;
            int tended = 0;
            for (int i = 0; i != this.MaxNumberTend; i++)
            {
                Hediff hediff;
                if (!(from hd in p.health.hediffSet.hediffs
                      where (hd.def.displayWound || hd.def.tendable) && !p.health.hediffSet.PartIsMissing(hd.Part)
                      select hd).TryRandomElement(out hediff))
                {
                    break;
                }
                p.health.RemoveHediff(hediff);
                tended++;
            }

            if (tended != 0)
            {
                Messages.Message("MorrowRim_GolemRepaired".Translate(this.Target, tended), p, MessageTypeDefOf.PositiveEvent, true);
                this.Item.SplitOff(1).Destroy(DestroyMode.Vanish);
            }
            else
            {
                Messages.Message("MorrowRim_GolemNotRepaired".Translate(this.Target), p, MessageTypeDefOf.NeutralEvent, true);
            }
            this.Target.jobs.EndCurrentJob(JobCondition.InterruptForced, true, true);
        }

        private const int DurationTicks = 100;

        public int MaxNumberTend = 5;
    }
}