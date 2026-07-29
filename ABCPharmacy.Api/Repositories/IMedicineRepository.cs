using ABCPharmacy.Api.Models;

namespace ABCPharmacy.Api.Repositories
{
    public interface IMedicineRepository
    {
        Task<List<Medicine>> GetAllAsync();

        Task<Medicine?> GetByIdAsync(int id);

        Task<List<Medicine>> SearchAsync(string name);

        Task AddAsync(Medicine medicine);

        Task UpdateAsync(Medicine medicine);
    }
}
