using System;
using System.Collections.Generic;
using Verse;
using Verse.AI;
using RimWorld;
using System.Linq;

namespace MorrowRim_Telvanni
{
    public abstract class JobGiver_ConstructAffectFloor : ThinkNode_JobGiver
    {
		public virtual ThingRequest PotentialWorkThingRequest
		{
			get
			{
				return ThingRequest.ForGroup(ThingRequestGroup.Undefined);
			}
		}
		//copied from WorkGiver_ConstructAffectFloor

		protected abstract DesignationDef DesDef { get; }

		public IEnumerable<IntVec3> PotentialWorkCellsGlobal(Pawn pawn)
		{
			foreach (Designation designation in pawn.Map.designationManager.SpawnedDesignationsOfDef(this.DesDef).Where(x => HasJobOnCell(pawn, x.target.Cell)))
			{
				yield return designation.target.Cell;
			}
			IEnumerator<Designation> enumerator = null;
			yield break;
		}

		public bool ShouldSkip(Pawn pawn, bool forced = false)
		{
			return !pawn.Map.designationManager.AnySpawnedDesignationOfDef(this.DesDef);
		}

		public bool HasJobOnCell(Pawn pawn, IntVec3 c, bool forced = false)
		{
			return !c.IsForbidden(pawn) && pawn.Map.designationManager.DesignationAt(c, this.DesDef) != null && pawn.CanReserve(c, 1, -1, ReservationLayerDefOf.Floor, forced);
		}
	}
}
