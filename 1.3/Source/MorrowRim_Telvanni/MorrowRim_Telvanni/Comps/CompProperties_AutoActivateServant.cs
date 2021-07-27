using System;
using Verse;
using RimWorld;

namespace MorrowRim_Telvanni
{
    class CompProperties_AutoActivateServant : CompProperties
    {
        public CompProperties_AutoActivateServant()
        {
            this.compClass = typeof(Comp_AutoActivateServant);
        }

        public PawnKindDef ServantKind = null;
    }
}
