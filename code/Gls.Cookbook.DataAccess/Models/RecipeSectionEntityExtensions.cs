using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gls.Cookbook.Domain.Models;

namespace Gls.Cookbook.DataAccess.Models
{
    public static class RecipeSectionEntityExtensions
    {
        public static RecipeSectionEntity MapToEntity(this RecipeSection recipeSection, RecipeEntity recipeEntity)
        {
            RecipeSectionEntity entity = new RecipeSectionEntity()
            {
                Id = recipeSection.Id,
                Recipe = recipeEntity,
                Order = recipeSection.Order,
                Name = recipeSection.Name
            };

            entity.Ingredients = new List<RecipeIngredientEntity>(
                recipeSection.Ingredients.Select(
                    i => new RecipeIngredientEntity()
                    {
                        Id = i.Id,
                        RecipeSection = entity,
                        Ingredient = new IngredientEntity() { Id = i.Ingredient.Id, Name = i.Ingredient.Name, Description = i.Ingredient.Description },
                        Measurement = new MeasurementEntity() { Id = i.Measurement.Id, MeasurementType = i.Measurement.MeasurementType, MeasurementSystem = i.Measurement.MeasurementSystem, Name = i.Measurement.Name, Abbreviation = i.Measurement.Abbreviation },
                        Quantity = i.Quantity,
                        Note = i.Note
                    }));

            entity.Instructions = new List<RecipeInstructionEntity>(
                recipeSection.Instructions.Select(
                    i => new RecipeInstructionEntity()
                    {
                        Id = i.Id,
                        RecipeSection = entity,
                        LineNumber = i.LineNumber,
                        Instruction = i.Instruction,
                        Note = i.Note
                    }));

            return entity;
        }

        public static RecipeSection MapToRecipeSection(this RecipeSectionEntity entity)
        {
            RecipeSection recipeSection = new RecipeSection()
            {
                Id = entity.Id,
                Order = entity.Order,
                Name = entity.Name
            };

            recipeSection.Ingredients = new List<RecipeIngredient>(
                entity.Ingredients.Select(
                    i => new RecipeIngredient()
                    {
                        Id = i.Id,
                        Ingredient = new Ingredient() { Id = i.Ingredient.Id, Name = i.Ingredient.Name, Description = i.Ingredient.Description },
                        Measurement = new Measurement() { Id = i.Measurement.Id, MeasurementType = i.Measurement.MeasurementType, MeasurementSystem = i.Measurement.MeasurementSystem, Name = i.Measurement.Name, Abbreviation = i.Measurement.Abbreviation },
                        Quantity = i.Quantity,
                        Note = i.Note
                    }));

            recipeSection.Instructions = new List<RecipeInstruction>(
                entity.Instructions.Select(
                    i => new RecipeInstruction()
                    {
                        Id = i.Id,
                        LineNumber = i.LineNumber,
                        Instruction = i.Instruction,
                        Note = i.Note
                    }));

            return recipeSection;
        }
    }
}
