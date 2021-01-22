using System;
using System.ComponentModel.DataAnnotations;

namespace CRUD_web_api.Models
{
  public class Product
  {
    [Key]
    public int Id { get; set; }

    [MinLength(3, ErrorMessage = "Esse campo deve ter mais de 3 caracteres")]
    [MaxLength(60, ErrorMessage = "Esse campo deve ter mais de 60 caracteres")]
    public string Title { get; set; }

    [MaxLength(1024, ErrorMessage = "Esse campo não pode ter mais de 1024 caracteres")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Esse campo é obrigatório")]
    public decimal Price { get; set; }

    public decimal Quantity { get; set; }

    public string Image { get; set; }

    public DateTime CreateDate { get; set; }
    
    public DateTime LastUpdateDate { get; set; }

    [Required(ErrorMessage = "Esse campo é obrigatório")]
    [Range(1, int.MaxValue, ErrorMessage = "Categoria inválida")]
    public int CategoryId { get; set; }

    public Category Category { get; set; }
  }
}