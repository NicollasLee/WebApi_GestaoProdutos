namespace GestaoProdutos.Domain.Entidades
{
    public class ProductQueryParams
    {
        public string Description { get; set; }
        public string Status { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
