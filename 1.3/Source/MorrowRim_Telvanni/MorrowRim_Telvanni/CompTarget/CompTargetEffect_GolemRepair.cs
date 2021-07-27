using System;
using Verse;
using Verse.AI;
using RimWorld;

namespace MorrowRim_Telvanni
{
    class CompTargetEffect_GolemRepair : CompTargetEffect
	{
		public override void DoEffectOn(Pawn user, Thing target)
		{
			if (GolemUtility.IsGolem(target) && GolemUtility.CheckQualityLevel(target))
			{
				if (!user.IsColonistPlayerControlled)
				{
					return;
				}
				if (!user.CanReserveAndReach(target, PathEndMode.Touch, Danger.Deadly, 1, -1, null, false))
				{
					return;
				}
				Job job = JobMaker.MakeJob(JobDefOf.MorrowRim_RepairGolem, target, this.parent);
				job.count = 1;
				user.jobs.TryTakeOrderedJob(job, JobTag.Misc);
			}
		}
	}
}
