using System;
using Verse;
using Verse.AI;
using RimWorld;

namespace MorrowRim_Telvanni
{
    class JobGiver_SelfRepair : ThinkNode_JobGiver
	{
		protected override Job TryGiveJob(Pawn pawn)
		{
			if (!GolemUtility.CheckGolemForSelfRepair(pawn) || !pawn.health.capacities.CapableOf(PawnCapacityDefOf.Manipulation) || pawn.InAggroMentalState)
			{
				return null;
			}
			Predicate<Thing> predicate = (Thing t) => t.def.category == ThingCategory.Item && HasJobOnThing(pawn, t);
			Thing thing = GenClosest.ClosestThingReachable(pawn.Position, pawn.Map, ThingRequest.ForDef(ThingDefOf.MorrowRim_GolemRepairCrystal), PathEndMode.OnCell, TraverseParms.For(pawn, Danger.Some, TraverseMode.ByPawn, false), 100f, predicate);
			Job result;
			if (thing == null)
			{
				result = null;
			}
			else
			{
				result = JobOnThing(pawn, thing);
			}
			return result;
		}

		public bool HasJobOnThing(Pawn pawn, Thing t, bool forced = false)
		{
			return t.def == ThingDefOf.MorrowRim_GolemRepairCrystal && pawn.CanReserve(t, 1, 1, null, forced) && ForbidUtility.InAllowedArea(t.Position, pawn);
		}

		public Job JobOnThing(Pawn pawn, Thing t, bool forced = false)
		{
			Job job = JobMaker.MakeJob(JobDefOf.MorrowRim_ServantSelfRepair, pawn, t);
			job.endAfterTendedOnce = true;
			return job;
		}
	}
}
