using System;
using System.Collections.Generic;
using Verse;
using Verse.AI;
using RimWorld;
using System.Linq;

namespace MorrowRim_Telvanni
{
    class JobGiver_SmoothWall : ThinkNode_JobGiver
    {
        protected override Job TryGiveJob(Pawn pawn)
        {
            Job result;
            if (!ShouldSkip(pawn) && PotentialWorkCellsGlobal(pawn).Any())
            {
                result = JobOnCell(pawn, PotentialWorkCellsGlobal(pawn).RandomElement());
            }
            else
            {
                result = null;
            }

            return result;
        }

        //copied from WorkGiver_ConstructSmoothWall

        public bool ShouldSkip(Pawn pawn, bool forced = false)
        {
            return !pawn.Map.designationManager.AnySpawnedDesignationOfDef(RimWorld.DesignationDefOf.SmoothWall);
        }

        public IEnumerable<IntVec3> PotentialWorkCellsGlobal(Pawn pawn)
        {
            foreach (Designation designation in pawn.Map.designationManager.SpawnedDesignationsOfDef(RimWorld.DesignationDefOf.SmoothWall).Where(x => HasJobOnCell(pawn, x.target.Cell)))
            {
                yield return designation.target.Cell;
            }
            IEnumerator<Designation> enumerator = null;
            yield break;
        }

        public bool HasJobOnCell(Pawn pawn, IntVec3 c, bool forced = false)
        {
            if (c.IsForbidden(pawn) || pawn.Map.designationManager.DesignationAt(c, RimWorld.DesignationDefOf.SmoothWall) == null)
            {
                return false;
            }
            Building edifice = c.GetEdifice(pawn.Map);
            if (edifice == null || !edifice.def.IsSmoothable)
            {
                Log.ErrorOnce("Failed to find valid edifice when trying to smooth a wall", 58988176, false);
                pawn.Map.designationManager.TryRemoveDesignation(c, RimWorld.DesignationDefOf.SmoothWall);
                return false;
            }
            return pawn.CanReserve(edifice, 1, -1, null, forced) && pawn.CanReserve(c, 1, -1, null, forced);
        }

        public Job JobOnCell(Pawn pawn, IntVec3 c, bool forced = false)
        {
            return JobMaker.MakeJob(RimWorld.JobDefOf.SmoothWall, c.GetEdifice(pawn.Map));
        }
    }
}
