using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;
using System;
using System.Collections.Generic;

namespace MorrowRim_Telvanni
{
    //Setting the Harmony instance
    [StaticConstructorOnStartup]
    public class Main
    {
        static Main()
        {
            var harmony = new Harmony("com.MorrowRim_Telvanni");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    //Patch that randomly spawns depleted heart stones after using a heart stone in crafting
    [HarmonyPatch(typeof(QuestManager))]
    [HarmonyPatch("Notify_ThingsProduced")]
    public static class QuestManager_NotifyThingsProduced_Patch
    {
        

        [HarmonyPostfix]
        [HarmonyPriority(Priority.Last)]
        public static void checkDepletedHeartStone(Pawn worker, List<Thing> things)
        {
            foreach(Thing thing in things)
            {
                if(thing.def.costList != null)
                {
                    foreach (ThingDefCountClass x in thing.def.costList)
                    {
                        if (x.thingDef == ThingDefOf.MorrowRim_HeartStone)
                        {
                            HeartStoneUtility.SpawnHeartStone(worker);
                        }
                    }
                }
            }
        }
    }
}
