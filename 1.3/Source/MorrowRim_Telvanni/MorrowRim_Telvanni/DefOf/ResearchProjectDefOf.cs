using System;
using Verse;
using RimWorld;

namespace MorrowRim_Telvanni
{
	[DefOf]
	public static class ResearchProjectDefOf
	{
		static ResearchProjectDefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(ResearchProjectDefOf));
		}
		public static ResearchProjectDef MorrowRim_HeartStoneStudiesI;
		public static ResearchProjectDef MorrowRim_HeartStoneStudiesII;
		public static ResearchProjectDef MorrowRim_GolemAdvanced;
	}
}