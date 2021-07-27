using System;
using Verse;
using RimWorld;
using System.Collections.Generic;
using UnityEngine;

namespace MorrowRim_Telvanni
{
    class Gas_FireStave : Gas
    {
        private int tickerInterval = 0;
        private int tickerMax = 120;
        public override void Tick()
        {
            base.Tick();
            try
            {
                if (tickerInterval >= tickerMax)
                {
                    HashSet<Thing> hashSet = new HashSet<Thing>(this.Position.GetThingList(this.Map));
                    if (hashSet != null)
                    {
                        foreach (Thing thing in hashSet)
                        {
                            //check if is pawn
                            if (thing != null && thing is Pawn)
                            {
                                Pawn p = thing as Pawn;
                                if (!p.RaceProps.IsFlesh || p.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.MorrowRim_ElixirActiveFire_High) != null)
                                {
                                    return;
                                }
                                float num = 0.028758334f;
                                //get number, need to b e a little fancy because temperature
                                //only modify if number is above zero
                                if (p.ComfortableTemperatureRange().max > 0)
                                {
                                    num /= p.ComfortableTemperatureRange().max / 10;
                                }
                                if (num != 0f && !this.Destroyed)
                                {
                                    float num2 = Mathf.Lerp(0.85f, 1.15f, Rand.ValueSeeded(p.thingIDNumber ^ 74374237));
                                    num *= num2;
                                    HealthUtility.AdjustSeverity(p, RimWorld.HediffDefOf.Heatstroke, num * 1);
                                }
                            }
                        }
                    }
                    tickerInterval = 0;
                }
                tickerInterval++;
            }
            catch (NullReferenceException e)
            {

            }
        }
    }
}
