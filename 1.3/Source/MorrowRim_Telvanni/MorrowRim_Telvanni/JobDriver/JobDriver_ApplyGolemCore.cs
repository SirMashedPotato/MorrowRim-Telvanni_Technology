using System;
using System.Collections.Generic;
using Verse;
using Verse.AI;
using RimWorld;
using System.Linq;

namespace MorrowRim_Telvanni
{
    class JobDriver_ApplyGolemCore : JobDriver
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
            //this.Target.pather.StopDead();
            PawnUtility.ForceWait(this.Target, 1500, null, true);
            return this.pawn.Reserve(this.Target, this.job, 1, 1, null, errorOnFailed) && this.pawn.Reserve(this.Item, this.job, 1, 1, null, errorOnFailed);
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            yield return Toils_Goto.GotoThing(TargetIndex.B, PathEndMode.Touch).FailOnDespawnedOrNull(TargetIndex.B).FailOnDespawnedOrNull(TargetIndex.A);
            yield return Toils_Haul.StartCarryThing(TargetIndex.B, false, false, false);
            yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.Touch).FailOnDespawnedOrNull(TargetIndex.A);
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
            var props = AddHediffProperties.Get(this.Item.def);
            if (props.hediffToAdd != null)
            {
                if (props.isExclusive)
                {
                    foreach (Hediff h in p.health.hediffSet.hediffs.Where(x => x.Part == GolemUtility.GetPartToApplyTo(this.Target, this.Item)))
                    {
                        Log.Message("Probably going to error here");
                        p.health.RemoveHediff(h);
                        break;
                    }
                }
                if (props.isLeveled)
                {
                    Hediff hediff = p.health.hediffSet.GetFirstHediffOfDef(props.hediffToAdd);
                    if (hediff != null)
                    {
                        if (hediff.Severity != props.maxSeverity)
                        {
                            hediff.Severity += 0.1f;
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        p.health.AddHediff(props.hediffToAdd, GolemUtility.GetPartToApplyTo(this.Target, this.Item)).Severity = props.initialSeverity;
                    }
                } 
                else
                {
                    p.health.AddHediff(props.hediffToAdd, GolemUtility.GetPartToApplyTo(this.Target, this.Item));
                }

                Messages.Message("MorrowRim_GolemCoreApplied".Translate(this.Item, this.Target), p, MessageTypeDefOf.PositiveEvent, true);
                this.Item.SplitOff(1).Destroy(DestroyMode.Vanish);
                GolemUtility.IncrementRecord(this.pawn);
                this.Target.jobs.EndCurrentJob(JobCondition.InterruptForced, true, true);
            }
        }

        private const int DurationTicks = 100;
    }
}
