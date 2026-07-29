using System.Text.Json;
using ABCPharmacy.Api.Models;

namespace ABCPharmacy.Api.Repositories
{

    public class JsonMedicineRepository : IMedicineRepository
    {
        private readonly string _filePath;

        private readonly JsonSerializerOptions _options;


        public JsonMedicineRepository()
        {
            _filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Data",
                "medicines.json"
            );


            _options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };
        }



        public async Task<List<Medicine>> GetAllAsync()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Medicine>();
            }


            var json = await File.ReadAllTextAsync(_filePath);


            if (string.IsNullOrEmpty(json))
            {
                return new List<Medicine>();
            }


            return JsonSerializer.Deserialize<List<Medicine>>
                (json, _options)
                ?? new List<Medicine>();
        }



        public async Task<Medicine?> GetByIdAsync(int id)
        {
            var medicines = await GetAllAsync();


            return medicines.FirstOrDefault(x => x.Id == id);
        }



        public async Task<List<Medicine>> SearchAsync(string name)
        {
            var medicines = await GetAllAsync();


            return medicines
                .Where(x =>
                    x.FullName.Contains(
                        name,
                        StringComparison.OrdinalIgnoreCase))
                .ToList();
        }



        public async Task AddAsync(Medicine medicine)
        {
            var medicines = await GetAllAsync();


            medicine.Id = medicines.Count == 0
                ? 1
                : medicines.Max(x => x.Id) + 1;


            medicines.Add(medicine);


            await SaveAsync(medicines);
        }



        public async Task UpdateAsync(Medicine medicine)
        {
            var medicines = await GetAllAsync();


            var existing =
                medicines.FirstOrDefault(x => x.Id == medicine.Id);


            if (existing == null)
                return;


            existing.FullName = medicine.FullName;
            existing.Notes = medicine.Notes;
            existing.ExpiryDate = medicine.ExpiryDate;
            existing.Quantity = medicine.Quantity;
            existing.Price = medicine.Price;
            existing.Brand = medicine.Brand;


            await SaveAsync(medicines);
        }



        private async Task SaveAsync(List<Medicine> medicines)
        {
            var json =
                JsonSerializer.Serialize(
                    medicines,
                    _options);


            await File.WriteAllTextAsync(
                _filePath,
                json);
        }
    }
}