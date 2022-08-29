namespace Gls.Cookbook.Domain.Models
{
    public class RecipeDirection
    {
        public int Id { get; set; }
        public int LineNumber { get; set; }
        public string Direction { get; set; }
        public string Note { get; set; }
    }
}