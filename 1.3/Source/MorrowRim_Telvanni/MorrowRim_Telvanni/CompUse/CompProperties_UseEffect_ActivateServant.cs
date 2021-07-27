using System;
using Verse;
using RimWorld;

namespace MorrowRim_Telvanni
{
	class CompProperties_UseEffect_ActivateServant : CompProperties_UseEffect
	{
		public CompProperties_UseEffect_ActivateServant()
		{
			this.compClass = typeof(CompUseEffect_ActivateServant);
		}
		public PawnKindDef ServantKind = null;
		public HediffDef hediffToApply = null;
		public bool applyAwakeningHediff = false;
		public bool bond = false;
		public bool useQuality = true;
		public bool checkResearch = true;
		public bool addDrafter = true;
	}
}