using System;
using Verse;
using RimWorld;

namespace MorrowRim_Telvanni
{
    class DeathActionWorker_ElementalSpider : DeathActionWorker
    {

        public override RulePackDef DeathRules
        {
            get
            {
                return RulePackDefOf.Transition_DiedExplosive;
            }
        }

        public override bool DangerousInMelee
        {
            get
            {
                return true;
            }
        }

        public override void PawnDied(Corpse corpse)
        {
            FilthMaker.TryMakeFilth(corpse.Position, corpse.Map, ThingDefOf.Filth_BloodInsect, Rand.RangeInclusive(1, 3));

            var spiderProps = SpiderProperties.Get(corpse.InnerPawn.def);

            GenExplosion.DoExplosion(corpse.Position, corpse.Map, spiderProps.radius, spiderProps.damageDef, corpse.InnerPawn, spiderProps.damageAmount, -1, spiderProps.soundDef,
                null, null, null, spiderProps.thingToSpawnDef, 1, spiderProps.number);
            AlbinoSpiderUtility.PlaySpiderSound(corpse);

            corpse.Destroy();
        }
    }
}
