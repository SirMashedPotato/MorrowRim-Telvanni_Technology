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

        public BodyPartDef partToAddTo = null;
        public bool isExclusive = false;
        public bool isLeveled = false;
        public float initialSeverity = 0.1f;
        public float maxSeverity = 0.5f;
        public ThingDef thingDef = null;

        public static AddHediffProperties Get(Def def)
        {
            return def.GetModExtension<AddHediffProperties>();
        }
    }
}
