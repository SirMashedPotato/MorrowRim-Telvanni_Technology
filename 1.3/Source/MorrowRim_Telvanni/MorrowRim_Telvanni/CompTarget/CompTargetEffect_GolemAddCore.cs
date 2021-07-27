using System;
using Verse;
using Verse.AI;
using RimWorld;

namespace MorrowRim_Telvanni
{
    class CompTargetEffect_GolemAddCore : CompTargetEffect
    {
		public override void DoEffectOn(Pawn user, Thing target)
		{
			if (GolemUtility.IsGolem(target) && GolemUtility.CheckGolemForCore(target, this.parent))
			{
				if (!user.IsColonistPlayerControlled)
				{
					return;
				}
				if (!user.CanReserveAndReach(target, PathEndMode.Touch, Danger.Deadly, 1, 1, null, false))
				{
					return;
				}
				Job job = JobMaker.MakeJob(JobDefOf.MorrowRim_ApplyGolemCore, target, this.parent);
				job.count = 1;
				user.jobs.TryTakeOrderedJob(job, JobTag.Misc);
			}
		}
	}
}
