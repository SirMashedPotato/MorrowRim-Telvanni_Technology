using System;
using Verse;
using RimWorld;

namespace MorrowRim_Telvanni
{
	class CompProperties_ReplaceOnMature : CompProperties
	{
		public CompProperties_ReplaceOnMature()
		{
			this.compClass = typeof(Comp_ReplaceOnMature);
		}
		public ThingDef MatureInto = null;
	}
}