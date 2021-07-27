using System;
using System.Collections.Generic;
using Verse;
using Verse.AI;
using RimWorld;
using System.Linq;

namespace MorrowRim_Telvanni
{
    class JobGiver_SmoothFloor : JobGiver_ConstructAffectFloor
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

        //copied from WorkGiver_ConstructSmoothFloor

        protected override DesignationDef DesDef
        {
            get
            {
                return RimWorld.DesignationDefOf.SmoothFloor;
            }
        }

        public Job JobOnCell(Pawn pawn, IntVec3 c, bool forced = false)
        {
            return JobMaker.MakeJob(RimWorld.JobDefOf.SmoothFloor, c);
        }
    }
}
