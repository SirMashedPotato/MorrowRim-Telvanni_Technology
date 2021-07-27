using Verse;
using RimWorld;
using System.Collections.Generic;

namespace MorrowRim_Telvanni
{
    class CreateAtProperties : DefModExtension
    {
        public ThingDef thingToSpawn = null;
        public PawnKindDef pawnKindToSpawn = null;
        public float range = 15.9f;
        public int numberToSpawn = 1;

        public static CreateAtProperties Get(Def def)
        {
            return def.GetModExtension<CreateAtProperties>();
        }
    }
}
