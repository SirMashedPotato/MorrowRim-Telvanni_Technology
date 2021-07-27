using System;
using System.Collections.Generic;
using Verse;
using Verse.AI;
using RimWorld;
using System.Linq;

namespace MorrowRim_Telvanni
{
    class JobDriver_ApplyGolemCore_Upgrade : JobDriver
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
            return this.pawn.Reserve(this.Target, this.job, 1, -1, null, errorOnFailed) && this.pawn.Reserve(this.Item, this.job, 1, -1, null, errorOnFailed);
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
            yield return Toils_General.Do(new Action(this.ApplyQualityUpgradeCore));
            yield break;
        }

        private void ApplyQualityUpgradeCore()
        {
            Pawn p = this.Target;
            //basic upgrade
            if (p.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.MorrowRim_GolemQuality) != null)
            {
                Hediff hediff = p.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.MorrowRim_GolemQuality);
                if (hediff.Severity == 0.6f)
                {
                    p.health.RemoveHediff(hediff);
                    p.health.AddHediff(HediffDefOf.MorrowRim_GolemQuality_Legendary).Severity = 0.01f;
                }
                else
                {
                    hediff.Severity += 0.1f;
                }
            }
            //legendary upgrade
            else
            {
                if (p.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.MorrowRim_GolemQuality_Legendary) != null
                    && p.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.MorrowRim_GolemQuality_Legendary).Severity != 1f)
                {
                    p.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.MorrowRim_GolemQuality_Legendary).Severity += 0.1f;
                }
            }
            Messages.Message("MorrowRim_GolemCoreApplied".Translate(this.Item, this.Target), p, MessageTypeDefOf.PositiveEvent, true);
            this.Item.SplitOff(1).Destroy(DestroyMode.Vanish);
            GolemUtility.IncrementRecord(this.pawn);
            this.Target.jobs.EndCurrentJob(JobCondition.InterruptForced, true, true);
        }

        private const int DurationTicks = 100;
    }
}
