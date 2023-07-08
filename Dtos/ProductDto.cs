namespace BikeStoresApi.Dtos
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string ModelYear { get; set; }
        public decimal ListPrice { get; set; }
        public long CategoryId { get; set; }
    }
    public class GetProductDto : ProductDto
    {
        public long Id { get; set; }
        public string CategoryName { get; set; }
    }
    public class UpdateProductDto : ProductDto
    {
        public long Id { get; set; }
    }
    public class AddProductDto : ProductDto { }
}
