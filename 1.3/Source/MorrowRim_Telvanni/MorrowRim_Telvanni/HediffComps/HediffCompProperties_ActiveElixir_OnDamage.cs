using Verse;

namespace MorrowRim_Telvanni
{
    class HediffCompProperties_ActiveElixir_OnDamage : HediffCompProperties
    {

        public HediffCompProperties_ActiveElixir_OnDamage()
        {
            this.compClass = typeof(HediffComp_ActiveElixir_OnDamage);
        }

        public ThingDef thingToSpawn = null;
        public float Radius = 3;
    }
}

