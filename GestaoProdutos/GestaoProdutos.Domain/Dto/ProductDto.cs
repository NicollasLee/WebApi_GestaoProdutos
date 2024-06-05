using System;

namespace GestaoProdutos.Domain.Dto
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierDescription { get; set; }
        public string SupplierCNPJ { get; set; }
    }

}
