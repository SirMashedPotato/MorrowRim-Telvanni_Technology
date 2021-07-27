using System;
using Verse;
using Verse.Sound;
using RimWorld;
using Verse.AI;

namespace MorrowRim_Telvanni
{
    class Projectile_Spider : Projectile_Explosive
    {

        private static readonly RecordDef spiderThrowTracker = DefDatabase<RecordDef>.GetNamed("MorrowRim_TelvanniSpidersThrown");

        protected override void Impact(Thing hitThing)
        {
            if(GetPawnKind(this.def, out PawnKindDef spiderKind))
            {
                Pawn newPawn = PawnGenerator.GeneratePawn(spiderKind, Faction.OfPlayer);
                PawnUtility.TrySpawnHatchedOrBornPawn(newPawn, this);
                AlbinoSpiderUtility.PlaySpiderSound(newPawn);
                
                IncrementRecord(this.launcher as Pawn);

                if (this.intendedTarget != null && this.intendedTarget.Pawn != null)
                {
                    Job job = new Job(JobDefOf.AttackMelee, this.intendedTarget);
                    newPawn.jobs.StartJob(job);
                }
            }
            this.landed = true;
            this.Destroy(DestroyMode.Vanish);
        }

        private bool GetPawnKind(ThingDef parent, out PawnKindDef toSpawn)
        {
            var props = CreateAtProperties.Get(parent);
            if (props != null && props.pawnKindToSpawn != null)
            {
                toSpawn = props.pawnKindToSpawn;
                return true;
            }
            toSpawn = null;
            return false;
        }

        private void IncrementRecord(Pawn pawn)
        {
            if (spiderThrowTracker != null)
            {
                pawn.records.Increment(spiderThrowTracker);
            }
        }
    }
}
