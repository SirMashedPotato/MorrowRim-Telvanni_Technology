using Verse;
using RimWorld;
using System.Collections.Generic;

namespace MorrowRim_Telvanni
{
    class AddHediffProperties : DefModExtension
    {
        public HediffDef hediffToAdd = null;
        public float severity = 1.0f;
        public bool showRadius = false;
        public float radius = 3f;

        public static AddHediffProperties Get(Def def)
        {
            return def.GetModExtension<AddHediffProperties>();
        }
    }
}
