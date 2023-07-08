namespace BikeStoresApi.Dtos
{
    public class CategoryDto
    {
        public string Name { get; set; } = "Food";
    }
    public class GetCategoryDto : CategoryDto
    {
        public long Id { get; set; }
    }
    public class UpdateCategoryDto : CategoryDto
    {
        public long Id { get; set; }
    }
    public class AddCategoryDto : CategoryDto { }
}
