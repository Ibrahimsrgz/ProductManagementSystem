namespace ProductManagementSystem.MauiClient.Dtos.Product
{
    public class GetProductItemsResponseDto
    {
        public List<ProductItemDto> Items { get; set; }
        public int TotalCount { get; set; }
    }
}
