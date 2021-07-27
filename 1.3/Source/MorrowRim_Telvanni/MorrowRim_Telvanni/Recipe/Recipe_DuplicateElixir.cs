using System;
using System.Collections.Generic;
using Verse;
using RimWorld;

namespace MorrowRim_Telvanni
{
    class Recipe_DuplicateElixir : RecipeWorker
    {
        public override void ConsumeIngredient(Thing ingredient, RecipeDef recipe, Map map)
        {
            if (ingredient.def.thingCategories.Contains(ThingCategoryDefOf.MorrowRim_Elixir))
            {
                //set up like this to ensure it actually produces 20 elixir
                //having a single base.Consume at the end results in 15 of the produced elixir being deleted as well
                //could always increase this.stackCOunt to 35, but that may have unforseen consequences
                Thing thing = ThingMaker.MakeThing(ingredient.def);
                thing.stackCount = 2 * ingredient.stackCount;
                if (ingredient.TryGetQuality(out QualityCategory qc))
                {
                    base.ConsumeIngredient(ingredient, recipe, map);
                    thing.TryGetComp<CompQuality>().SetQuality(qc, ArtGenerationContext.Colony);
                    GenPlace.TryPlaceThing(thing, ingredient.Position, map, ThingPlaceMode.Near, null, null, default(Rot4));
                }
                //Incase elixir doesn't have a quality comp
                else
                {
                    base.ConsumeIngredient(ingredient, recipe, map);
                    GenPlace.TryPlaceThing(thing, ingredient.Position, map, ThingPlaceMode.Near, null, null, default(Rot4));
                }
            } 
            else
            {
                base.ConsumeIngredient(ingredient, recipe, map);
            }
        }
    }
}
