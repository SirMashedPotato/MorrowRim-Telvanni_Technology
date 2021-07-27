using System;
using Verse;
using RimWorld;

namespace MorrowRim_Telvanni
{
    class Comp_AutoActivateServant : ThingComp
    {
		public CompProperties_AutoActivateServant Props
		{
			get
			{
				return (CompProperties_AutoActivateServant)this.props;
			}
		}

        public override void CompTick()
        {
            if (Props.ServantKind != null && parent.Spawned)
            {
                SpawnServant();
                DestroySelf();
            }
            base.CompTick();
        }

        private void SpawnServant()
        {
            Pawn servant = PawnGenerator.GeneratePawn(this.Props.ServantKind, Faction.OfPlayerSilentFail);
            PawnUtility.TrySpawnHatchedOrBornPawn(servant, parent);
        }

        private void DestroySelf()
        {
            parent.Destroy();
        }
    }
}
