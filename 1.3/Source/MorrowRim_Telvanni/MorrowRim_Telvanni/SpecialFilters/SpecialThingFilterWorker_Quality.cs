using System;
using Verse;
using RimWorld;

namespace MorrowRim_Telvanni
{
    class SpecialThingFilterWorker_Quality : SpecialThingFilterWorker
	{
		public override bool Matches(Thing t)
		{
			return this.CanEverMatch(t.def) && GetQC(t, out QualityCategory qc) 
				&& GetAllowedQC(t.def, out QualityCategory aqc) && qc == aqc;
		}

		public override bool CanEverMatch(ThingDef def)
		{
			return def.HasComp(typeof(CompQuality));
		}

		public override bool AlwaysMatches(ThingDef def)
		{
			return def.HasComp(typeof(CompQuality));
		}

		private bool GetQC(Thing t, out QualityCategory qc)
		{
			return t.TryGetQuality(out qc);
		}

		private bool GetAllowedQC(ThingDef def, out QualityCategory allowedqc)
        {
			var props = SpecialFilterProperties.Get(def);
			if (props != null && props.UseQualityCategory)
			{
				allowedqc = props.qualityAllowed;
				return true;
			}
			allowedqc = QualityCategory.Normal;
			return false;
		}
	}
}
