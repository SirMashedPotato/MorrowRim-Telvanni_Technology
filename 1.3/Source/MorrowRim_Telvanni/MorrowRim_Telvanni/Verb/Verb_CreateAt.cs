using System;
using Verse;
using Verse.AI;
using RimWorld;

namespace MorrowRim_Telvanni
{
    class Verb_CreateAt : Verb_CastBase
    {
        protected override bool TryCastShot()
        {
            if (!CheckPosition()) return false;
            Pawn user = this.caster as Pawn;
            ThingDef item = this.EquipmentSource.def;
            if (GetThing(item, out ThingDef itemToSpawn))
            {
                Find.BattleLog.Add(new BattleLogEntry_ItemUsed(user, GenSpawn.Spawn(itemToSpawn, this.currentTarget.Cell, this.caster.Map), item, RulePackDefOf.Event_ItemUsed));
                this.currentTarget.Cell.GetThingList(this.caster.Map).Find(x => x.def == itemToSpawn).SetFaction(Faction.OfPlayerSilentFail);
                UseCharge(base.ReloadableCompSource);
                return true;
            }
            return false;
        }

        private bool CheckPosition()
        {
            IntVec3 cell = this.currentTarget.Cell;
            return cell.IsValid && cell.InBounds(this.caster.Map) && cell.Standable(this.caster.Map);
        }

        private bool GetThing(ThingDef item, out ThingDef toSpawn)
        {
            var props = CreateAtProperties.Get(item);
            if (props != null && props.thingToSpawn != null)
            {
                toSpawn = props.thingToSpawn;
                return true;
            }
            toSpawn = null;
            return false;
        }

        private float GetRange(ThingDef item)
        {
            return CreateAtProperties.Get(item).range;
        }

        private static void UseCharge(CompReloadable comp)
        {
            if (comp == null || !comp.CanBeUsed)
            {
                return;
            }
            comp.UsedOnce();
        }

        public override float HighlightFieldRadiusAroundTarget(out bool needLOSToCenter)
        {
            needLOSToCenter = true;
            return GetRange(this.EquipmentSource.def);
        }
    }
}
