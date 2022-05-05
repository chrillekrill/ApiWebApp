using System.ComponentModel.DataAnnotations;

namespace ApiWebApp.DTO
{
    public class UpdateDTO
    {
        [MaxLength(100)]
        public string Header { get; set; }
        [MaxLength(200)]
        public string Body { get; set; }
        public decimal Price { get; set; }
    }
}
