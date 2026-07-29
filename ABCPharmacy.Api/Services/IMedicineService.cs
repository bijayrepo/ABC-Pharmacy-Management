using ABCPharmacy.Api.DTOs;

namespace ABCPharmacy.Api.Services
{
    public interface IMedicineService
    {
        Task<List<MedicineDto>> GetAllAsync();

        Task<MedicineDto?> GetByIdAsync(int id);

        Task<List<MedicineDto>> SearchAsync(string name);

        Task AddAsync(CreateMedicineDto medicineDto);
    }
}