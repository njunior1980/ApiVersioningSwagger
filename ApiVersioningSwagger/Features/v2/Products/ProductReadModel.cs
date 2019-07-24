namespace ApiVersioningSwagger.Features.v2.Products
{
    public class ProductReadModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Departament { get; set; }
        public string Price { get; set; }
        public string Sku { get; set; } // new property
    }

    public class ProductReadModelMobile
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Sku { get; set; }
    }
}
