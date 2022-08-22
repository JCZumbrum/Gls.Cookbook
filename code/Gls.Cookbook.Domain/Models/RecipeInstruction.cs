namespace Gls.Cookbook.Domain.Models
{
    public class RecipeInstruction
    {
        public int Id { get; set; }
        public int LineNumber { get; set; }
        public string Instruction { get; set; }
        public string Note { get; set; }
    }
}