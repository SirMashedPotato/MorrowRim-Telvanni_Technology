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
		public bool applyAwakeningHediff = true;
		public HediffDef drainToActivator = null;
		public float drainToActivatorSeverity = 0.10f;
		public bool bond = true;
	}
}