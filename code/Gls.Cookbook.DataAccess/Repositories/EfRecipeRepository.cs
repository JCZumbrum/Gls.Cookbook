using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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
                .Include(r => r.Sections).ThenInclude(s => s.IngredientSections).ThenInclude(s => s.Ingredients)
                .Include(r => r.Sections).ThenInclude(s => s.DirectionSections).ThenInclude(s => s.Directions);
        }

        public async Task<int> AddAsync(Recipe recipe)
        {
            RecipeEntity recipeEntity = recipe.MapToEntity();

            await dbContext.Recipes.AddAsync(recipeEntity);
            await dbContext.SaveChangesAsync();

            return recipeEntity.Id;
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

        public async Task<List<RecipeHeader>> GetHeadersAsync()
        {
            return await dbContext.Recipes.Select(r => new RecipeHeader() { Id = r.Id, Name = r.Name, Description = r.Description }).ToListAsync();
        }

        public async Task<List<Recipe>> GetAllAsync()
        {
            return await GetEntities().Select(e => e.MapToDomain()).ToListAsync();
        }

        public async Task<Recipe> GetByIdAsync(int recipeId)
        {
            RecipeEntity entity = await GetEntityByIdAsync(recipeId);
            return entity.MapToDomain();
        }

        public async Task<Recipe> GetByNameAsync(string name)
        {
            RecipeEntity entity = await GetEntities().FirstOrDefaultAsync(r => r.Name == name);
            return entity.MapToDomain();
        }

        public async Task UpdateAsync(Recipe recipe)
        {
            RecipeEntity recipeEntity = await GetEntityByIdAsync(recipe.Id);

            recipeEntity.Name = recipe.Name;
            recipeEntity.Description = recipe.Description;
            recipeEntity.Tags = JsonSerializer.Serialize(recipe.Tags);

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

        #region Recipe Section

        private void DeleteSection(RecipeSectionEntity section)
        {
            foreach (RecipeDirectionSectionEntity directionSection in section.DirectionSections)
                DeleteDirectionSection(directionSection);

            foreach (RecipeIngredientSectionEntity ingredientSection in section.IngredientSections)
                DeleteIngredientSection(ingredientSection);

            dbContext.RecipeSections.Remove(section);
        }

        private void UpdateSection(RecipeSectionEntity recipeSectionEntity, RecipeSection recipeSection)
        {
            recipeSectionEntity.Index = recipeSection.Index;
            recipeSectionEntity.Name = recipeSection.Name;

            #region Refresh Ingredient Sections

            Dictionary<int, RecipeIngredientSectionEntity> entityIngredientSectionDictionary = recipeSectionEntity.IngredientSections.ToDictionary(i => i.Id);
            ILookup<bool, RecipeIngredientSection> ingredientSectionLookupByState = recipeSection.IngredientSections.ToLookup(i => i.Id == 0);
            List<RecipeIngredientSectionEntity> exceptIngredientSectionList = recipeSectionEntity.IngredientSections.ExceptBy(ingredientSectionLookupByState[false].Select(n => n.Id), e => e.Id).ToList();

            // delete ingredient sections that no longer exist
            foreach (RecipeIngredientSectionEntity exceptIngredientSection in exceptIngredientSectionList)
            {
                recipeSectionEntity.IngredientSections.Remove(exceptIngredientSection);
                DeleteIngredientSection(exceptIngredientSection);
            }

            // update existing ingredient sections
            // ingredientSectionLookupByState[false] => ingredient sections that are not new
            foreach (RecipeIngredientSection modifyIngredientSection in ingredientSectionLookupByState[false])
            {
                RecipeIngredientSectionEntity recipeIngredientSectionEntity = entityIngredientSectionDictionary[modifyIngredientSection.Id];
                UpdateIngredientSection(recipeIngredientSectionEntity, modifyIngredientSection);
            }

            // add new ingredient sections
            foreach (RecipeIngredientSection newIngredientSection in ingredientSectionLookupByState[true])
                recipeSectionEntity.IngredientSections.Add(newIngredientSection.MapToEntity(recipeSectionEntity));

            #endregion

            #region Refresh Direction Sections

            Dictionary<int, RecipeDirectionSectionEntity> entityDirectionSectionDictionary = recipeSectionEntity.DirectionSections.ToDictionary(i => i.Id);
            ILookup<bool, RecipeDirectionSection> directionSectionLookupByState = recipeSection.DirectionSections.ToLookup(i => i.Id == 0);
            List<RecipeDirectionSectionEntity> exceptDirectionSectionList = recipeSectionEntity.DirectionSections.ExceptBy(directionSectionLookupByState[false].Select(n => n.Id), e => e.Id).ToList();

            // delete direction sections that no longer exist
            foreach (RecipeDirectionSectionEntity exceptDirectionSection in exceptDirectionSectionList)
            {
                recipeSectionEntity.DirectionSections.Remove(exceptDirectionSection);
                DeleteDirectionSection(exceptDirectionSection);
            }

            // update existing direction section
            // directionSectionLookupByState[false] => direction sections that are not new
            foreach (RecipeDirectionSection modifyDirectionSection in directionSectionLookupByState[false])
            {
                RecipeDirectionEntity instructionEntity = entityInstructionDictionary[modifyInstruction.Id];

                instructionEntity.Index = modifyInstruction.Index;
                instructionEntity.Direction = modifyInstruction.Direction;
                instructionEntity.Note = modifyInstruction.Note;
            }

            // add new instructions
            foreach (RecipeDirectionSection newDirectionSection in directionSectionLookupByState[true])
                recipeSectionEntity.DirectionSections.Add(newDirectionSection.MapToEntity(recipeSectionEntity));

            #endregion
        }

        #endregion

        #region Ingredient Sections

        private void DeleteIngredientSection(RecipeIngredientSectionEntity ingredientSection)
        {
            foreach (RecipeIngredientEntity ingredient in ingredientSection.Ingredients)
                dbContext.RecipeIngredients.Remove(ingredient);

            dbContext.RecipeIngredientSections.Remove(ingredientSection);
        }

        private void UpdateIngredientSection(RecipeIngredientSectionEntity recipeIngredientSectionEntity, RecipeIngredientSection recipeIngredientSection)
        {
            recipeIngredientSectionEntity.Index = recipeIngredientSection.Index;
            recipeIngredientSectionEntity.Name = recipeIngredientSection.Name;

            Dictionary<int, RecipeIngredientEntity> entityIngredientDictionary = recipeIngredientSectionEntity.Ingredients.ToDictionary(i => i.Id);
            ILookup<bool, RecipeIngredient> ingredientLookupByState = recipeIngredientSection.Ingredients.ToLookup(i => i.Id == 0);
            List<RecipeIngredientEntity> exceptIngredientList = recipeIngredientSectionEntity.Ingredients.ExceptBy(ingredientLookupByState[false].Select(n => n.Id), e => e.Id).ToList();

            // delete ingredients that no longer exist
            foreach (RecipeIngredientEntity exceptIngredient in exceptIngredientList)
            {
                recipeIngredientSectionEntity.Ingredients.Remove(exceptIngredient);
                dbContext.RecipeIngredients.Remove(exceptIngredient);
            }

            // update existing ingredients
            // ingredientLookupByState[false] => ingredients that are not new
            foreach (RecipeIngredient modifyIngredient in ingredientLookupByState[false])
            {
                RecipeIngredientEntity ingredientEntity = entityIngredientDictionary[modifyIngredient.Id];

                ingredientEntity.Index = modifyIngredient.Index;
                ingredientEntity.MinimumQuantityText = modifyIngredient.MinimumQuantityText;
                ingredientEntity.MaximumQuantityText = modifyIngredient.MaximumQuantityText;
                ingredientEntity.MeasurementId = modifyIngredient.MeasurementId;
                ingredientEntity.IngredientId = modifyIngredient.IngredientId;
                ingredientEntity.Note = modifyIngredient.Note;
            }

            // add new ingredients
            foreach (RecipeIngredient newIngredient in ingredientLookupByState[true])
                recipeIngredientSectionEntity.Ingredients.Add(newIngredient.MapToEntity(recipeIngredientSectionEntity));
        }

        #endregion

        #region Direction Sections

        private void DeleteDirectionSection(RecipeDirectionSectionEntity directionSection)
        {
            foreach (RecipeDirectionEntity direction in directionSection.Directions)
                dbContext.RecipeDirections.Remove(direction);

            dbContext.RecipeDirectionSections.Remove(directionSection);
        }

        private void UpdateDirectionSection()
        {
            throw new NotImplementedException();
        }

        #endregion

        public Task<List<(string Tag, int Count)>> GetAllTagsAsync()
        {
            List<(string Tag, int Count)> tags =
                dbContext.Recipes
                .Select(r => r.Tags)
                .AsEnumerable()
                .SelectMany(t => JsonSerializer.Deserialize<List<string>>(t, default(JsonSerializerOptions)))
                .GroupBy(t => t)
                .Select(g => new Tuple<string, int>(g.Key, g.Count()).ToValueTuple())
                .ToList();

            return Task.FromResult(tags);
        }

        public async Task<bool> ExistsByMeasurementId(int measurementId)
        {
            return await dbContext.Recipes
                .Include(r => r.Sections).ThenInclude(s => s.IngredientSections).ThenInclude(s => s.Ingredients)
                .AnyAsync(r => r.Sections.Any(s => s.IngredientSections.Any(s => s.Ingredients.Any(i => i.MeasurementId == measurementId))));
        }

        public async Task<bool> ExistsByIngredientId(int ingredientId)
        {
            return await dbContext.Recipes
                .Include(r => r.Sections).ThenInclude(s => s.IngredientSections).ThenInclude(s => s.Ingredients)
                .AnyAsync(r => r.Sections.Any(s => s.IngredientSections.Any(s => s.Ingredients.Any(i => i.IngredientId == ingredientId))));
        }
    }
}
