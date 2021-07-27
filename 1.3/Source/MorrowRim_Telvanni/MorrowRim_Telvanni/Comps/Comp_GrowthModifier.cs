using RimWorld;
using Verse;

namespace MorrowRim_Telvanni
{
    class Comp_GrowthModifier : ThingComp
    {
        public CompProperties_GrowthModifier Props
        {
            get
            {
                return (CompProperties_GrowthModifier)this.props;
            }
        }

        public override void CompTickLong()
        {
            base.CompTickLong();
            Plant plant = parent as Plant;

            if (Props.weather != null && Props.weather.Contains(plant.Map.weatherManager.curWeather) && plant.Growth >= Props.minGrowth)
            {
                if (!Props.allowIndoors && !plant.Position.Roofed(plant.Map))
                {
                    return;
                }
                if(Props.checkLight && plant.GrowthRateFactor_Light < 0.001f)
                {
                    return;
                }
                plant.Growth += Props.amount;
            }
        }
    }
}