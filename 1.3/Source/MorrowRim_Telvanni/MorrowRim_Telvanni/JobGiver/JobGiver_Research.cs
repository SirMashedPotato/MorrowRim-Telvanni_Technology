using System;
using System.Collections.Generic;
using Verse;
using Verse.AI;
using RimWorld;

namespace MorrowRim_Telvanni
{
	class JobGiver_Research : ThinkNode_JobGiver
	{
		protected override Job TryGiveJob(Pawn pawn)
		{
			Predicate<Thing> predicate = (Thing t) => t.def.category == ThingCategory.Building && HasJobOnThing(pawn, t);
			Thing thing = GenClosest.ClosestThingReachable(pawn.Position, pawn.Map, ThingRequest.ForGroup(ThingRequestGroup.ResearchBench), PathEndMode.OnCell, TraverseParms.For(pawn, Danger.Some, TraverseMode.ByPawn, false), 100f, predicate);
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

		//copied from WorkGiver_Research

		public bool HasJobOnThing(Pawn pawn, Thing t, bool forced = false)
		{
			ResearchProjectDef currentProj = Find.ResearchManager.currentProj;
			if (currentProj == null)
			{
				return false;
			}
			Building_ResearchBench building_ResearchBench = t as Building_ResearchBench;
			return building_ResearchBench != null && currentProj.CanBeResearchedAt(building_ResearchBench, false) && pawn.CanReserve(t, 1, -1, null, forced) && ForbidUtility.InAllowedArea(t.Position, pawn);
		}



		public Job JobOnThing(Pawn pawn, Thing t, bool forced = false)
		{
			return JobMaker.MakeJob(JobDefOf.MorrowRim_ServantResearch, t);
		}
	}
}
