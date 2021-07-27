﻿using System;
using Verse;
using RimWorld;
using System.Collections.Generic;
using UnityEngine;

namespace MorrowRim_Telvanni
{
    class Gas_StormStave : Gas
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
                                DamageInfo damageInfo = new DamageInfo();
                                damageInfo.Def = DamageDefOf.EMP;
                                damageInfo.SetAmount(Rand.RangeInclusive(1, 10));
                                thing.TakeDamage(damageInfo);
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
