using RimWorld;
using System.Reflection;
using Verse;
using System;
using System.Collections.Generic;
using Verse.Sound;
using System.Linq;
using UnityEngine;

namespace MorrowRim_Telvanni
{
    public static class GolemUtility
    {

        //checks if thing is a golem
        public static bool IsGolem(Thing t)
        {
            if (t is Pawn)
            {
                Pawn p = t as Pawn;
                return p.def.race.FleshType != null && p.def.race.FleshType.defName == "MorrowRim_FleshGolem";
            }
            return false;
        }

        //checks for quality level
        public static bool CheckQualityLevel(Thing t)
        {
            if (t is Pawn)
            {
                Pawn p = t as Pawn;
                return (p.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.MorrowRim_GolemQuality_Legendary) == null  
                    && p.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.MorrowRim_GolemQuality) != null
                    && p.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.MorrowRim_GolemQuality).Severity < 0.7)
                    || p.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.MorrowRim_GolemQuality_Legendary).Severity < HediffDefOf.MorrowRim_GolemQuality_Legendary.maxSeverity;
            }
            return false;
        }

        //just checks if research is complete
        public static bool CheckResearch()
        {
            return ResearchProjectDefOf.MorrowRim_GolemAdvanced.IsFinished;
        }

        //checks if golem already has a core
        public static bool CheckGolemForCore(Thing t, Thing item)
        {
            if (t is Pawn)
            {
                Pawn p = t as Pawn;
                var props = AddHediffProperties.Get(item.def);
                if(props.thingDef != null && props.thingDef != p.def)
                {
                    return false;
                }
                if (props.hediffToAdd != null)
                {
                    //check for levelled
                    Hediff hediff = p.health.hediffSet.GetFirstHediffOfDef(props.hediffToAdd);
                    if (props.isLeveled && hediff != null && hediff.Severity != props.maxSeverity)
                    {
                        return true;
                    }

                    return hediff == null;
                }
            }
            return false;
        }

        //get part to apply to
        public static BodyPartRecord GetPartToApplyTo(Thing t, Thing item)
        {
            if (t is Pawn)
            {
                Pawn p = t as Pawn;
                var props = AddHediffProperties.Get(item.def);
                if (props.partToAddTo != null)
                {
                    return p.RaceProps.body.GetPartsWithDef(props.partToAddTo).FirstOrFallback();
                }
            }
            return null;
        }

        //check for shifter target
        public static bool CanShiftGolem(Thing t)
        {
            if (t is Pawn)
            {
                Pawn p = t as Pawn;
                return p.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.MorrowRim_GolemShifterCore) != null;
            }
            return false;
        }

        //checks golem for repairing
        public static bool CheckGolemForRepair(Thing t)
        {
            if (t is Pawn)
            {
                Pawn p = t as Pawn;
                Hediff hediff;
                if ((from hd in p.health.hediffSet.hediffs
                      where (hd.def.displayWound || hd.def.tendable) && !p.health.hediffSet.PartIsMissing(hd.Part)
                      select hd).TryRandomElement(out hediff))
                {
                    return true;
                }
            }
            return false;
        }

        //checks if golem has enough wounds to self repair
        //ensures repair crystals are not wasted
        public static bool CheckGolemForSelfRepair(Thing t)
        {
            int num = 0;
            if (t is Pawn)
            {
                Pawn p = t as Pawn;
                List<Hediff> hediffs = new List<Hediff> { };
                foreach (Hediff h in p.health.hediffSet.GetHediffs<Hediff>().Where(x => (x.def.displayWound || x.def.tendable) && !p.health.hediffSet.PartIsMissing(x.Part)))
                {
                    if (num++ >= 5)
                    {
                        break;
                    }
                }
            }
            return num >= 5;
        }

        //simple code that increases record
        public static void IncrementRecord(Pawn pawn)
        {
             pawn.records.Increment(RecordDefOf.MorrowRim_TelvanniGolemCores);
        }

        //Get values for work

        public static float GetWork_Construct(Pawn actor)
        {
            return actor.health.capacities.GetLevel(PawnCapacityDefOf.Consciousness) * actor.health.capacities.GetLevel(PawnCapacityDefOf.Manipulation);
        }

        public static float GetWork_Intellectual(Pawn actor)
        {
            return actor.health.capacities.GetLevel(PawnCapacityDefOf.Consciousness) * actor.health.capacities.GetLevel(PawnCapacityDefOf.Sight);
        }

        public static float GetWork_Animals(Pawn actor)
        {
            return actor.health.capacities.GetLevel(PawnCapacityDefOf.Manipulation) * actor.health.capacities.GetLevel(PawnCapacityDefOf.Sight);
        }

        //command buttons
        /*
        public static Command_Target commandRepair = new Command_Target()
        {
            defaultLabel = "MorrowRim_DesignatorRepairGolem".Translate(),
            defaultDesc = "MorrowRim_DesignatorRepairGolemDesc".Translate(),
            icon = ContentFinder<Texture2D>.Get("UI/Designators/MorrowRim_RepairGolem", true),
            targetingParams = new TargetingParameters()
            {
                validator = delegate (TargetInfo target) {
                    if (target == null) return false;
                    if (target.Thing == null) return false;
                    if (!GolemUtility.IsGolem(target.Thing)) return false;
                    if (!GolemUtility.CheckGolemForRepair(target.Thing)) return false;
                    return true;
                }
            },
            action = (Thing t) => {
                t.Map.designationManager.AddDesignation(new Designation() { target = t, def = DesignationDefOf.MorrowRim_RepairGolem });
            },
        };

        public static Command_Target commandDestroy = new Command_Target()
        {
            defaultLabel = "MorrowRim_DesignatorDestroyGolem".Translate(),
            defaultDesc = "MorrowRim_DesignatorDestroyGolemDesc".Translate(),
            icon = ContentFinder<Texture2D>.Get("UI/Designators/MorrowRim_DestroyGolem", true),
            targetingParams = new TargetingParameters()
            {
                validator = delegate (TargetInfo target) {
                    if (target == null) return false;
                    if (target.Thing == null) return false;
                    if (!GolemUtility.IsGolem(target.Thing)) return false;
                    return true;
                }
            },
            action = (Thing t) => {
                t.Map.designationManager.AddDesignation(new Designation() { target = t, def = DesignationDefOf.MorrowRim_DestroyGolem });
            },
        };
        */
    }
}
