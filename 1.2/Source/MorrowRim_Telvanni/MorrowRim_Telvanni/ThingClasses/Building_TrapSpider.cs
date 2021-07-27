using System;
using Verse;
using RimWorld;
using System.Collections.Generic;
using Verse.AI;

namespace MorrowRim_Telvanni
{
    class Building_TrapSpider : Building_Trap
    {
        protected override void SpringSub(Pawn p)
        {
            var props = CreateAtProperties.Get(this.def);
            if (props != null && props.pawnKindToSpawn != null)
            {
                List<Pawn> spawns = new List<Pawn>();
                for (int i = 0; i != props.numberToSpawn; i++)
                {
                    Pawn newPawn = PawnGenerator.GeneratePawn(props.pawnKindToSpawn, Faction.OfPlayer);
                    PawnUtility.TrySpawnHatchedOrBornPawn(newPawn, this);
                    AlbinoSpiderUtility.PlaySpiderSound(newPawn);
                    spawns.Add(newPawn);
                }
                foreach(Pawn sp in spawns)
                {
                    Job job = new Job(JobDefOf.AttackMelee, p);
                    sp.jobs.StartJob(job);
                }
            }
            this.Destroy();
        }
    }
}
