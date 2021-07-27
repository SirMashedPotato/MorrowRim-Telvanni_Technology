using Verse;
using RimWorld;
using System.Collections.Generic;

namespace MorrowRim_Telvanni
{
    class SpecialFilterProperties : DefModExtension
    {
        public bool UseQualityCategory = true;
        public QualityCategory qualityAllowed = QualityCategory.Normal;

        public static SpecialFilterProperties Get(Def def)
        {
            return def.GetModExtension<SpecialFilterProperties>();
        }
    }
}
