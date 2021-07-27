using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


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
        public static void CheckDepletedHeartStone(Pawn worker, List<Thing> things)
        {
            foreach(Thing thing in things)
            {
                if(thing.def.costList != null && (thing.def.defName != "MorrowRim_GolemAetherialHeart" && thing.def.defName != "MorrowRim_ImplantedHeartStone"))
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

    /* Patches for Golems */
    //patch to treat golems as non-flesh
    //improves compatabilty with other mods, but may also cause some issues
    [HarmonyPatch(typeof(RaceProperties))]
    public static class RaceProperties_Is_Flesh_Patch
    {
        [HarmonyPostfix]
        [HarmonyPatch("IsFlesh", MethodType.Getter)]
        public static void GolemPatch(ref bool __result, ref RaceProperties __instance)
        {
            if (__result && __instance.FleshType != null && __instance.FleshType.defName == "MorrowRim_FleshGolem")
            {
                __result = false;
                return;
            }
        }
    }

    //patch to add configurable hostility response to golems
    /*
    [HarmonyPatch(typeof(Pawn_PlayerSettings))]
    public static class Pawn_PlayerSettings_UsesConfigurableHostilityResponse_Patch
    {
        [HarmonyPostfix]
        [HarmonyPatch("UsesConfigurableHostilityResponse", MethodType.Getter)]
        public static void GolemPatch(ref bool __result, ref Pawn ___pawn)
        {
            if (!__result && ___pawn.RaceProps.FleshType != null && ___pawn.RaceProps.FleshType.defName == "MorrowRim_FleshGolem")
            {
                __result = true;
                return;
            }
        }
    }
    */
    //Harmony patch to remove operations button from health tab for Golems
    //Need to remove as operations just don't work on them
    //And get error if tab is blank when opened
    //Really simple patch, modifies bool before original code runs if pawn is a golem
    /*
    [HarmonyPatch(typeof(HealthCardUtility))]
    [HarmonyPatch("DrawHealthSummary")]
    public static class HealthCardUtility_DrawHealthSummary_Patch
    {
        [HarmonyPrefix]
        public static void GolemPatch(Pawn pawn, ref bool allowOperations)
        {
            if (GolemUtility.IsGolem(pawn))
            {
                allowOperations = false;
            }
        }
    }
    */
    //add gizmo for golem
    /*
    [HarmonyPatch(typeof(Pawn))]
    [HarmonyPatch("GetGizmos")]
    public static class Pawn_GetGizmos_Patch
    {
        public static MethodInfo methodInfo = typeof(Pawn_DraftController).GetMethod("GetGizmos", BindingFlags.Instance | BindingFlags.NonPublic);
        public static void Postfix(Pawn __instance, ref IEnumerable<Gizmo> __result)
        {
            if (__result == null) return;

            List<Gizmo> list = __result.ToList();
            if (__instance.Faction != null && __instance.Faction.IsPlayer && GolemUtility.IsGolem(__instance))
            {
                list.Add(GolemUtility.commandDestroy);
                list.Add(GolemUtility.commandRepair);
                if (__instance.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.MorrowRim_GolemCore_WorkDraft) != null)
                {
                    if (__instance.drafter == null)
                    {
                        __instance.drafter = new Pawn_DraftController(__instance);
                    }
                    IEnumerable<Gizmo> collection = (IEnumerable<Gizmo>)methodInfo.Invoke(__instance.drafter, new object[0]);
                    list.AddRange(collection);
                }
            }
            __result = list;
        }
    }
    */
    //Patches that enables drafting for golems
    /*
    [HarmonyPatch(typeof(FloatMenuMakerMap), "CanTakeOrder")]
    public static class GolemsObeyOrders
    {
        public static void Postfix(Pawn pawn, ref bool __result)
        {
            if (pawn.drafter != null)
            {
                __result = true;
            }
        }
    }
    */
}
