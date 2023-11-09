using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GuiShopping.CartAPI.Model
{
    [Table("product")]
    public class Product
    {
        [Key , DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        public long Id { get; set; }
        [Column("name")]
        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Column("price")]
        [Required]
        [Range(1, 1000)]
        public decimal Price { get; set; }
        [Column("description")]
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        [Column("category_name")]
        [Required]
        [StringLength(50)]
        public string CategoryName { get; set;}
        [Column("image_url")]
        [Required]
        [StringLength(300)]
        public string Image_URL { get; set; }
    }
}
