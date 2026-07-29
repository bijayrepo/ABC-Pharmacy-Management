namespace ABCPharmacy.Api.DTOs
{
    public class SaleDto
    {
        public int Id { get; set; }

        public string MedicineName { get; set; } = string.Empty;

        public int QuantitySold { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime SaleDate { get; set; }
    }
}
