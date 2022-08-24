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

        public async Task AddAsync(Recipe recipe)
        {
            RecipeEntity recipeEntity = recipe.MapToEntity();
            await dbContext.Recipes.AddAsync(recipeEntity);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int recipeId)
        {
            RecipeEntity entity = await dbContext.Recipes.FirstOrDefaultAsync(r => r.Id == recipeId);
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
            RecipeEntity entity = await dbContext.Recipes.Include(r => r.Notes).Include(r => r.Sections).FirstOrDefaultAsync(r => r.Id == recipeId);
            return entity.MapToRecipe();
        }

        public async Task UpdateAsync(Recipe recipe)
        {
            RecipeEntity recipeEntity = await dbContext.Recipes.Include(r => r.Notes).Include(r => r.Sections).FirstOrDefaultAsync(r => r.Id == recipe.Id);

            recipeEntity.Name = recipe.Name;
            recipeEntity.Description = recipe.Description;

            Dictionary<int, RecipeSectionEntity> entitySectionDictionary = recipeEntity.Sections.ToDictionary(rs => rs.Id);

            ILookup<bool, RecipeSection> recipeSectionLookupByState = recipe.Sections.ToLookup(rs => rs.Id == 0);

            // recipeSectionLookupByState[false] => sections that are not new
            IEnumerable<RecipeSectionEntity> exceptSectionList = recipeEntity.Sections.ExceptBy(recipeSectionLookupByState[false].Select(rs => rs.Id), e => e.Id);

            // delete sections that no longer exist
            foreach (RecipeSectionEntity exceptSection in exceptSectionList)
                DeleteSection(exceptSection);

            // modify existing sections
            foreach (RecipeSection modifySection in recipeSectionLookupByState[false])
            {
                RecipeSectionEntity recipeSectionEntity = entitySectionDictionary[modifySection.Id];
                UpdateSection(recipeSectionEntity, modifySection);
            }

            // add new sections
            foreach (RecipeSection newSection in recipeSectionLookupByState[true])
            {
                RecipeSectionEntity recipeSectionEntity = new RecipeSectionEntity()
                {
                    Recipe = recipeEntity,
                    Name = newSection.Name,
                    Order = newSection.Order
                };

                recipeSectionEntity.Ingredients = newSection.Ingredients.Select(
                    i => new RecipeIngredientEntity()
                    {
                        RecipeSection = recipeSectionEntity,
                        Ingredient = i.Ingredient,
                        Measurement = i.Measurement,
                        Quantity = i.Quantity,
                        Note = i.Note
                    }).ToList();
                recipeSectionEntity.Instructions = newSection.Instructions.Select(i => new RecipeInstructionEntity() { }).ToList();
            }

            dbContext.Update(recipeEntity);
            await dbContext.SaveChangesAsync();
        }

        private void DeleteSection(RecipeSectionEntity section)
        {
            foreach (RecipeInstructionEntity instruction in section.Instructions)
                dbContext.RecipeInstructions.Remove(instruction);

            foreach (RecipeIngredientEntity ingredient in section.Ingredients)
                dbContext.RecipeIngredients.Remove(ingredient);

            dbContext.RecipeSections.Remove(section);
        }

        private void UpdateSection(RecipeSectionEntity recipeSectionEntity, RecipeSection recipeSection)
        {
            recipeSectionEntity.Name = recipeSection.Name;


        }
    }
}
