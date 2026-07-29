using ABCPharmacy.Api.DTOs;

namespace ABCPharmacy.Api.Services
{
    public interface ISaleService
    {
        Task<List<SaleDto>> GetAllAsync();

        Task CreateSaleAsync(
            CreateSaleDto saleDto);
    }
}