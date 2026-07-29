using ABCPharmacy.Api.Models;
namespace ABCPharmacy.Api.Repositories
{
    public interface ISaleRepository
    {
        Task<List<Sale>> GetAllAsync();

        Task AddAsync(Sale sale);
    }
}
