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
		public static HediffDef MorrowRim_ArmorCorrosion;
		public static HediffDef MorrowRim_StaveParalysis;
		public static HediffDef MorrowRim_PotionSickness;
		public static HediffDef MorrowRim_MindControl;

		//Active elixir
		public static HediffDef MorrowRim_ElixirActiveAsh_High;
		public static HediffDef MorrowRim_ElixirActiveFire_High;
		public static HediffDef MorrowRim_ElixirActiveFrost_High;
		public static HediffDef MorrowRim_ElixirActiveGrave_High;
		public static HediffDef MorrowRim_ElixirActiveParalysis_High;
		public static HediffDef MorrowRim_ElixirActiveSpectral_High;
		public static HediffDef MorrowRim_ElixirActiveStorm_High;
		public static HediffDef MorrowRim_ElixirActiveToxin_High;
		public static HediffDef MorrowRim_ElixirActiveVampiric_High;
		public static HediffDef MorrowRim_ElixirActiveProtection_High;

		//Golem hediffs
		public static HediffDef MorrowRim_GolemQuality;
		public static HediffDef MorrowRim_GolemQuality_Legendary;
		public static HediffDef MorrowRim_GolemShifterCore;
		public static HediffDef MorrowRim_GolemCore_WorkDraft;
	}
}
