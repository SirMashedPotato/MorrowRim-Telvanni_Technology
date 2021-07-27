using System;
using Verse;
using RimWorld;

namespace MorrowRim_Telvanni
{
    class HediffCompProperties_AetherialRegeneration : HediffCompProperties
    {
        public HediffCompProperties_AetherialRegeneration()
        {
            this.compClass = typeof(HediffComp_AetherialRegeneration);
        }
        public int Ticks = 100;
        public float BaseNumber = 3f;
        public float Tend = 0.2f;
        public float Severity = 0.5f;
        public bool ModifiedByPumping = false;
    }
}
