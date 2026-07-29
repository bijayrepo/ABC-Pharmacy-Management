using System.Text.Json;
using ABCPharmacy.Api.Models;
namespace ABCPharmacy.Api.Repositories
{
    public class JsonSaleRepository : ISaleRepository
    {
        private readonly string _filePath;


        private readonly JsonSerializerOptions _options;



        public JsonSaleRepository()
        {
            _filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Data",
                "sales.json"
            );


            _options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        }



        public async Task<List<Sale>> GetAllAsync()
        {
            if (!File.Exists(_filePath))
                return new List<Sale>();


            var json =
                await File.ReadAllTextAsync(_filePath);


            return JsonSerializer.Deserialize<List<Sale>>
                (json, _options)
                ?? new List<Sale>();
        }




        public async Task AddAsync(Sale sale)
        {
            var sales = await GetAllAsync();


            sale.Id = sales.Count == 0
                ? 1
                : sales.Max(x => x.Id) + 1;


            sales.Add(sale);


            var json =
                JsonSerializer.Serialize(
                    sales,
                    _options);


            await File.WriteAllTextAsync(
                _filePath,
                json);
        }
    }
}