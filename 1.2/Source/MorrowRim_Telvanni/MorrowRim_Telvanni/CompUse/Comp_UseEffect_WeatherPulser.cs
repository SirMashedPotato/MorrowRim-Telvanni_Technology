using System;
using Verse;
using RimWorld;

namespace MorrowRim_Telvanni
{
	class Comp_UseEffect_WeatherPulser : CompUseEffect
	{
		public CompProperties_UseEffect_WeatherPulser Props
		{
			get
			{
				return (CompProperties_UseEffect_WeatherPulser)this.props;
			}
		}
		public override void DoEffect(Pawn usedBy)
		{
			base.DoEffect(usedBy);
			if (this.Props.weatherDef != null)
			{
				usedBy.Map.weatherManager.TransitionTo(Props.weatherDef);
                if (this.Props.addHediff != null)
                {
					usedBy.health.AddHediff(this.Props.addHediff);
                }
				//check if heart stone is saved
				HeartStoneUtility.SpawnHeartStone(usedBy);
			}
		}
	}
}
