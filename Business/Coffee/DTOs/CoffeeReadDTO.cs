using System.Globalization;

namespace Business.Coffee.DTOs
{
    public class CoffeeReadDTO
    {
        public string? Message { get; set; }

		public string? Prepared => DateTime.UtcNow.ToString("yyyy-MM-ddTHH\\:mm\\:ss+ffff", CultureInfo.InvariantCulture);

    }
}
