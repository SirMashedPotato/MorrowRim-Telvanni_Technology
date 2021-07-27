﻿using System;
using Verse;
using RimWorld;
using System.Collections.Generic;
using UnityEngine;

namespace MorrowRim_Telvanni
{
    class Gas_AcidicStave : Gas
    {
        private int tickerInterval = 0;
        private int tickerMax = 120;

        /* get thingDefs that aren't included in this mod*/
        private static readonly HediffDef acidDef = DefDatabase<HediffDef>.GetNamed("VEF_AcidBuildup");
        private static readonly HediffDef hediffDef = DefDatabase<HediffDef>.GetNamed("MorrowRim_ElixirActiveAcidic_High");

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
                                if (!p.RaceProps.IsFlesh || p.health.hediffSet.GetFirstHediffOfDef(hediffDef) != null)
                                {
                                    return;
                                }
                                float num = 0.028758334f;
                                num *= p.GetStatValue(StatDefOf.ToxicSensitivity, true);
                                if (num != 0f && !this.Destroyed)
                                {
                                    float num2 = Mathf.Lerp(0.85f, 1.15f, Rand.ValueSeeded(p.thingIDNumber ^ 74374237));
                                    num *= num2;
                                    HealthUtility.AdjustSeverity(p, acidDef, num);
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
