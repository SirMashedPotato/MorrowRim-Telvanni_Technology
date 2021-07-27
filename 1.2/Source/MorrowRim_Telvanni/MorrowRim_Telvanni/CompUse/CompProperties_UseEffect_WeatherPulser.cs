using System;
using Verse;
using RimWorld;

namespace MorrowRim_Telvanni
{
	class CompProperties_UseEffect_WeatherPulser : CompProperties_UseEffect
	{
		public CompProperties_UseEffect_WeatherPulser()
		{
			this.compClass = typeof(Comp_UseEffect_WeatherPulser);
		}
		public WeatherDef weatherDef;

		public HediffDef addHediff = null;
	}
}