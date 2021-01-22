using System.ComponentModel.DataAnnotations;

namespace CRUD_web_api.Models
{
  public class Category
  {
    [Key]
    public int Id { get; set; }

    [MinLength(3, ErrorMessage = "Esse campo deve ter mais de 3 caracteres")]
    [MaxLength(60, ErrorMessage = "Esse campo deve ter mais de 60 caracteres")]
    public string Title { get; set; }
  }
}