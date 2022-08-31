using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gls.Cookbook.DataAccess.Models;
using Gls.Cookbook.Domain.Models;
using Gls.Cookbook.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gls.Cookbook.DataAccess.Repositories
{
    public class EfRecipeRepository : IRecipeRepository
    {
        private CookbookDbContext dbContext;

        public EfRecipeRepository(CookbookDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private async Task<RecipeEntity> GetEntityByIdAsync(int recipeId)
        {
            return await GetEntities().FirstOrDefaultAsync(r => r.Id == recipeId);
        }

        private IQueryable<RecipeEntity> GetEntities()
        {
            return dbContext.Recipes
                .Include(r => r.Notes)
                .Include(r => r.Sections).ThenInclude(s => s.Ingredients)
                .Include(r => r.Sections).ThenInclude(s => s.Directions);
        }

        public async Task AddAsync(Recipe recipe)
        {
            RecipeEntity recipeEntity = recipe.MapToEntity();

            await dbContext.Recipes.AddAsync(recipeEntity);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int recipeId)
        {
            RecipeEntity entity = await GetEntityByIdAsync(recipeId);
            if (entity == null)
                return;

            foreach (RecipeNoteEntity note in entity.Notes)
                dbContext.RecipeNotes.Remove(note);

            foreach (RecipeSectionEntity section in entity.Sections)
            {
                DeleteSection(section);
            }

            dbContext.Recipes.Remove(entity);

            await dbContext.SaveChangesAsync();
        }

        public async Task<List<RecipeHeader>> GetHeaders()
        {
            return await dbContext.Recipes.Select(r => new RecipeHeader() { Id = r.Id, Name = r.Name, Description = r.Description }).ToListAsync();
        }

        public async Task<Recipe> GetByIdAsync(int recipeId)
        {
            RecipeEntity entity = await GetEntityByIdAsync(recipeId);
            return entity.MapToRecipe();
        }

        public async Task<Recipe> GetByNameAsync(string name)
        {
            RecipeEntity entity = await GetEntities().FirstOrDefaultAsync(r => r.Name == name);
            return entity.MapToRecipe();
        }

        public async Task UpdateAsync(Recipe recipe)
        {
            RecipeEntity recipeEntity = await GetEntityByIdAsync(recipe.Id);

            recipeEntity.Name = recipe.Name;
            recipeEntity.Description = recipe.Description;

            #region Refresh Recipe Sections

            Dictionary<int, RecipeSectionEntity> entitySectionDictionary = recipeEntity.Sections.ToDictionary(rs => rs.Id);
            ILookup<bool, RecipeSection> recipeSectionLookupByState = recipe.Sections.ToLookup(rs => rs.Id == 0);
            List<RecipeSectionEntity> exceptSectionList = recipeEntity.Sections.ExceptBy(recipeSectionLookupByState[false].Select(rs => rs.Id), e => e.Id).ToList();

            // delete sections that no longer exist
            foreach (RecipeSectionEntity exceptSection in exceptSectionList)
            {
                recipeEntity.Sections.Remove(exceptSection);
                DeleteSection(exceptSection);
            }

            // update existing sections
            // recipeSectionLookupByState[false] => sections that are not new
            foreach (RecipeSection modifySection in recipeSectionLookupByState[false])
            {
                RecipeSectionEntity recipeSectionEntity = entitySectionDictionary[modifySection.Id];
                UpdateSection(recipeSectionEntity, modifySection);
            }

            // add new sections
            foreach (RecipeSection newSection in recipeSectionLookupByState[true])
                recipeEntity.Sections.Add(newSection.MapToEntity(recipeEntity));

            #endregion

            #region Refresh Recipe Notes

            Dictionary<int, RecipeNoteEntity> entityNoteDictionary = recipeEntity.Notes.ToDictionary(n => n.Id);
            ILookup<bool, RecipeNote> recipeNoteLookupByState = recipe.Notes.ToLookup(n => n.Id == 0);
            List<RecipeNoteEntity> exceptNoteList = recipeEntity.Notes.ExceptBy(recipeNoteLookupByState[false].Select(n => n.Id), e => e.Id).ToList();

            // delete notes that no longer exist
            foreach (RecipeNoteEntity exceptNote in exceptNoteList)
            {
                recipeEntity.Notes.Remove(exceptNote);
                dbContext.RecipeNotes.Remove(exceptNote);
            }

            // update existing notes
            // recipeNoteLookupByState[false] => notes that are not new
            foreach (RecipeNote modifyNote in recipeNoteLookupByState[false])
            {
                RecipeNoteEntity recipeNoteEntity = entityNoteDictionary[modifyNote.Id];
                recipeNoteEntity.Index = modifyNote.Index;
                recipeNoteEntity.Note = modifyNote.Note;
            }

            // add new notes
            foreach (RecipeNote newNote in recipeNoteLookupByState[true])
                recipeEntity.Notes.Add(new RecipeNoteEntity() { Recipe = recipeEntity, Index = newNote.Index, Note = newNote.Note });

            #endregion

            dbContext.Recipes.Update(recipeEntity);
            await dbContext.SaveChangesAsync();
        }

        private void DeleteSection(RecipeSectionEntity section)
        {
            foreach (RecipeDirectionEntity instruction in section.Directions)
                dbContext.RecipeInstructions.Remove(instruction);

            foreach (RecipeIngredientEntity ingredient in section.Ingredients)
                dbContext.RecipeIngredients.Remove(ingredient);

            dbContext.RecipeSections.Remove(section);
        }

        private void UpdateSection(RecipeSectionEntity recipeSectionEntity, RecipeSection recipeSection)
        {
            recipeSectionEntity.Name = recipeSection.Name;

            #region Refresh Ingredients

            Dictionary<int, RecipeIngredientEntity> entityIngredientDictionary = recipeSectionEntity.Ingredients.ToDictionary(i => i.Id);
            ILookup<bool, RecipeIngredient> ingredientLookupByState = recipeSection.Ingredients.ToLookup(i => i.Id == 0);
            List<RecipeIngredientEntity> exceptIngredientList = recipeSectionEntity.Ingredients.ExceptBy(ingredientLookupByState[false].Select(n => n.Id), e => e.Id).ToList();

            // delete ingredients that no longer exist
            foreach (RecipeIngredientEntity exceptIngredient in exceptIngredientList)
            {
                recipeSectionEntity.Ingredients.Remove(exceptIngredient);
                dbContext.RecipeIngredients.Remove(exceptIngredient);
            }

            // update existing ingredients
            // ingredientLookupByState[false] => ingredients that are not new
            foreach (RecipeIngredient modifyIngredient in ingredientLookupByState[false])
            {
                RecipeIngredientEntity ingredientEntity = entityIngredientDictionary[modifyIngredient.Id];

                ingredientEntity.Index = modifyIngredient.Index;
                ingredientEntity.IngredientId = modifyIngredient.IngredientId;
                ingredientEntity.QuantityText = modifyIngredient.QuantityText;
                ingredientEntity.Quantity = modifyIngredient.Quantity;
                ingredientEntity.MeasurementId = modifyIngredient.MeasurementId;
                ingredientEntity.Note = modifyIngredient.Note;
            }

            // add new ingredients
            foreach (RecipeIngredient newIngredient in ingredientLookupByState[true])
                recipeSectionEntity.Ingredients.Add(newIngredient.MapToEntity(recipeSectionEntity));

            #endregion

            #region Refresh Instructions

            Dictionary<int, RecipeDirectionEntity> entityInstructionDictionary = recipeSectionEntity.Directions.ToDictionary(i => i.Id);
            ILookup<bool, RecipeDirection> instructionLookupByState = recipeSection.Directions.ToLookup(i => i.Id == 0);
            List<RecipeDirectionEntity> exceptInstructionList = recipeSectionEntity.Directions.ExceptBy(instructionLookupByState[false].Select(n => n.Id), e => e.Id).ToList();

            // delete instructions that no longer exist
            foreach (RecipeDirectionEntity exceptInstruction in exceptInstructionList)
            {
                recipeSectionEntity.Directions.Remove(exceptInstruction);
                dbContext.RecipeInstructions.Remove(exceptInstruction);
            }

            // update existing instructions
            // instructionLookupByState[false] => ingredients that are not new
            foreach (RecipeDirection modifyInstruction in instructionLookupByState[false])
            {
                RecipeDirectionEntity instructionEntity = entityInstructionDictionary[modifyInstruction.Id];

                instructionEntity.Index = modifyInstruction.Index;
                instructionEntity.Direction = modifyInstruction.Direction;
                instructionEntity.Note = modifyInstruction.Note;
            }

            // add new instructions
            foreach (RecipeDirection newInstruction in instructionLookupByState[true])
                recipeSectionEntity.Directions.Add(newInstruction.MapToEntity(recipeSectionEntity));

            #endregion
        }
    }
}
