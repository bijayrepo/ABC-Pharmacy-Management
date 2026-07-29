using ABCPharmacy.Api.DTOs;
using ABCPharmacy.Api.Models;
using ABCPharmacy.Api.Repositories;

namespace ABCPharmacy.Api.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;

        private readonly IMedicineRepository _medicineRepository;
        public SaleService(
            ISaleRepository saleRepository,
            IMedicineRepository medicineRepository)
        {
            _saleRepository = saleRepository;

            _medicineRepository = medicineRepository;
        }
        public async Task<List<SaleDto>> GetAllAsync()
        {
            var sales =
                await _saleRepository.GetAllAsync();


            return sales.Select(x => new SaleDto
            {
                Id = x.Id,

                MedicineName = x.MedicineName,

                QuantitySold = x.QuantitySold,

                TotalPrice = x.TotalPrice,

                SaleDate = x.SaleDate

            }).ToList();
        }
        public async Task CreateSaleAsync(
            CreateSaleDto saleDto)
        {

            var medicine =
                await _medicineRepository
                .GetByIdAsync(
                    saleDto.MedicineId);



            if (medicine == null)
                throw new Exception(
                    "Medicine not found");



            if (medicine.Quantity < saleDto.QuantitySold)
                throw new Exception(
                    "Insufficient stock");



            medicine.Quantity -=
                saleDto.QuantitySold;



            await _medicineRepository
                .UpdateAsync(medicine);



            var sale = new Sale
            {
                MedicineId = medicine.Id,

                MedicineName = medicine.FullName,

                QuantitySold = saleDto.QuantitySold,

                TotalPrice =
                    medicine.Price *
                    saleDto.QuantitySold,

                SaleDate = DateTime.Now
            };


            await _saleRepository.AddAsync(sale);
        }
    }
}