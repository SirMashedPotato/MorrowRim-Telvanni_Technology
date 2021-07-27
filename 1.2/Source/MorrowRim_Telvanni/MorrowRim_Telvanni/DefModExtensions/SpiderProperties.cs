using Verse;
using RimWorld;
using System.Collections.Generic;

namespace MorrowRim_Telvanni
{
    class SpiderProperties : DefModExtension
    {
        public float radius = 1.9f;
        public int damageAmount = 0;
        public int number = 1;
        public SoundDef soundDef = null;
        public DamageDef damageDef = null;
        public ThingDef thingToSpawnDef = null;

        public static SpiderProperties Get(Def def)
        {
            return def.GetModExtension<SpiderProperties>();
        }
    }
}
