using System.ComponentModel.DataAnnotations;

namespace ApiWebApp.Data
{
    public class Advert
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Header { get; set; }
        [MaxLength(200)]
        public string Body { get; set; }
        [Range(18, 2)]
        public decimal Price { get; set; }
    }
}
