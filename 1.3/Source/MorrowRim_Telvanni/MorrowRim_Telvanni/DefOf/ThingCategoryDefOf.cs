using System;
using Verse;
using RimWorld;

namespace MorrowRim_Telvanni
{
	[DefOf]
	public static class ThingCategoryDefOf
	{
		static ThingCategoryDefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(ThingCategoryDefOf));
		}
		public static ThingCategoryDef MorrowRim_Elixir;
	}
}