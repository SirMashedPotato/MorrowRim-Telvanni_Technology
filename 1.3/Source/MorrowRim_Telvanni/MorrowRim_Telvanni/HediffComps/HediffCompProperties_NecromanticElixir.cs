using Verse;

namespace MorrowRim_Telvanni
{
    class HediffCompProperties_NecromanticElixir : HediffCompProperties
    {

        public HediffCompProperties_NecromanticElixir()
        {
            this.compClass = typeof(HediffComp_NecromanticElixir);
        }

        public HediffDef resDef = null;
        public float severity = 0f;
    }
}

