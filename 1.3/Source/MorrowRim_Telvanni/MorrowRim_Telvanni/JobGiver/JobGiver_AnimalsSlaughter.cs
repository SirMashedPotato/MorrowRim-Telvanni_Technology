using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using Verse.AI;
using RimWorld;

namespace MorrowRim_Telvanni
{
    class JobGiver_AnimalsSlaughter : ThinkNode_JobGiver
    {
		protected override Job TryGiveJob(Pawn pawn)
		{
			Predicate<Thing> predicate = (Thing t) => t.def.category == ThingCategory.Pawn && HasJobOnThing(pawn, t);
			Thing thing = GenClosest.ClosestThingReachable(pawn.Position, pawn.Map, ThingRequest.ForGroup(ThingRequestGroup.Pawn), PathEndMode.OnCell, TraverseParms.For(pawn, Danger.Some, TraverseMode.ByPawn, false), 100f, predicate);
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

		//

		public bool HasJobOnThing(Pawn pawn, Thing t, bool forced = false)
		{
			Pawn pawn2 = t as Pawn;
			if (pawn2 == null || !pawn2.RaceProps.Animal)
			{
				return false;
			}
			if (pawn.Map.designationManager.DesignationOn(t, RimWorld.DesignationDefOf.Slaughter) == null)
			{
				return false;
			}
			if (pawn.Faction != t.Faction)
			{
				return false;
			}
			if (pawn2.InAggroMentalState)
			{
				return false;
			}
			if (!pawn.CanReserve(t, 1, -1, null, forced))
			{
				return false;
			}
			return true;
		}

		public Job JobOnThing(Pawn pawn, Thing t, bool forced = false)
		{
			return JobMaker.MakeJob(RimWorld.JobDefOf.Slaughter, t);
		}
	}
}
