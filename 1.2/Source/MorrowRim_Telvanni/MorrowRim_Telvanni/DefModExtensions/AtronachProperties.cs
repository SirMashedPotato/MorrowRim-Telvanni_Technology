using Verse;
using RimWorld;
using System.Collections.Generic;

namespace MorrowRim_Telvanni
{
    class AtronachProperties : DefModExtension
    {
        public bool isAtronach = false;

        public static AtronachProperties Get(Def def)
        {
            return def.GetModExtension<AtronachProperties>();
        }
    }
}
