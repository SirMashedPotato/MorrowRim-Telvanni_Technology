using System;
using Verse;
using RimWorld;

namespace MorrowRim_Telvanni
{
	[DefOf]
	public static class HediffDefOf
	{
		static HediffDefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(HediffDefOf));
		}
		public static HediffDef DarkDescent_ServantAwakening;
		public static HediffDef MorrowRim_ArmorCorrosion;
		public static HediffDef MorrowRim_StaveParalysis;
		public static HediffDef MorrowRim_PotionSickness;
		public static HediffDef MorrowRim_MindControl;
	}
}
