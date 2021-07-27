using System;
using Verse;
using RimWorld;

namespace MorrowRim_Telvanni
{
    class SpecialThingFilterWorker_QualityNone : SpecialThingFilterWorker
	{
		public override bool Matches(Thing t)
		{
			return this.CanEverMatch(t.def) && !GetQC(t , out QualityCategory _);
		}

		public override bool CanEverMatch(ThingDef def)
		{
			return !def.HasComp(typeof(CompQuality));
		}

		public override bool AlwaysMatches(ThingDef def)
		{
			return !def.HasComp(typeof(CompQuality));
		}

		private bool GetQC(Thing t, out QualityCategory qc)
        {
			return t.TryGetQuality(out qc);
		}
	}
}
