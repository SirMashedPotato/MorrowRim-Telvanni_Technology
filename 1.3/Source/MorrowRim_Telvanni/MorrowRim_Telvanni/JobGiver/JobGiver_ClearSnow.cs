using System;
using System.Collections.Generic;
using Verse;
using Verse.AI;
using RimWorld;
using System.Linq;

namespace MorrowRim_Telvanni
{
    class JobGiver_ClearSnow : ThinkNode_JobGiver
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

        //copied from WorkGiver_ClearSnow

        public bool ShouldSkip(Pawn pawn, bool forced = false)
        {
            return pawn.Map.areaManager.SnowClear.TrueCount == 0;
        }

        public IEnumerable<IntVec3> PotentialWorkCellsGlobal(Pawn pawn)
        {
            return pawn.Map.areaManager.SnowClear.ActiveCells.Where(x => HasJobOnCell(pawn, x));
        }

        public bool HasJobOnCell(Pawn pawn, IntVec3 c, bool forced = false)
        {
            return pawn.Map.snowGrid.GetDepth(c) >= 0.2f && !c.IsForbidden(pawn) && pawn.CanReserve(c, 1, -1, null, forced);
        }

        public Job JobOnCell(Pawn pawn, IntVec3 c, bool forced = false)
        {
            return JobMaker.MakeJob(RimWorld.JobDefOf.ClearSnow, c);
        }
    }
}
