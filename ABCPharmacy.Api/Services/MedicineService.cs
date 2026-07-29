using ABCPharmacy.Api.DTOs;
using ABCPharmacy.Api.Models;
using ABCPharmacy.Api.Repositories;

namespace ABCPharmacy.Api.Services
{
    public class MedicineService : IMedicineService
    {
        private readonly IMedicineRepository _repository;

        private readonly ILogger<MedicineService> _logger;


        public MedicineService(
            IMedicineRepository repository,
            ILogger<MedicineService> logger)
        {
            _repository = repository;
            _logger = logger;
        }



        public async Task<List<MedicineDto>> GetAllAsync()
        {
            var medicines = await _repository.GetAllAsync();


            return medicines.Select(MapToDto).ToList();
        }



        public async Task<MedicineDto?> GetByIdAsync(int id)
        {
            var medicine =
                await _repository.GetByIdAsync(id);


            if (medicine == null)
                return null;


            return MapToDto(medicine);
        }



        public async Task<List<MedicineDto>> SearchAsync(
            string name)
        {
            var medicines =
                await _repository.SearchAsync(name);


            return medicines
                .Select(MapToDto)
                .ToList();
        }



        public async Task AddAsync(
            CreateMedicineDto medicineDto)
        {

            ValidateMedicine(medicineDto);



            var medicine = new Medicine
            {
                FullName = medicineDto.FullName,

                Notes = medicineDto.Notes,

                ExpiryDate = medicineDto.ExpiryDate,

                Quantity = medicineDto.Quantity,

                Price = medicineDto.Price,

                Brand = medicineDto.Brand
            };


            await _repository.AddAsync(medicine);


            _logger.LogInformation(
                "Medicine {Name} added successfully",
                medicine.FullName);
        }




        private void ValidateMedicine(
            CreateMedicineDto medicine)
        {

            if (string.IsNullOrWhiteSpace(
                medicine.FullName))
            {
                throw new Exception(
                    "Medicine name is required");
            }


            if (string.IsNullOrWhiteSpace(
                medicine.Brand))
            {
                throw new Exception(
                    "Brand is required");
            }


            if (medicine.Price <= 0)
            {
                throw new Exception(
                    "Price must be greater than zero");
            }


            if (medicine.Quantity < 0)
            {
                throw new Exception(
                    "Quantity cannot be negative");
            }


            if (medicine.ExpiryDate <= DateTime.Today)
            {
                throw new Exception(
                    "Expiry date must be future date");
            }
        }




        private MedicineDto MapToDto(
            Medicine medicine)
        {
            return new MedicineDto
            {
                Id = medicine.Id,

                FullName = medicine.FullName,

                ExpiryDate = medicine.ExpiryDate,

                Quantity = medicine.Quantity,

                Price = medicine.Price,

                Brand = medicine.Brand,
                Notes=medicine.Notes

            };
        }
    }
}