namespace Products.API.Models
{
    public class CategoryModel
    {
        public CategoryModel()
        {

        }

        public CategoryModel(int categoryId, string name)
        {
            CategoryId = categoryId;
            Name = name;
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }
    }
}