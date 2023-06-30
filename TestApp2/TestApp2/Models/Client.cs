using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApp.Models;

    public class Client
    {
    public int Id { get; set; }
    [MinLength(10, ErrorMessage = "ИНН состоит из 10 цифр"), MaxLength(10, ErrorMessage = "ИНН состоит из 10 цифр"), RegularExpression(@"^[0-9]+$")]
    [Required(ErrorMessage = "Поле ИНН должно быть заполнено")]
    [Display(Name = "ИНН")]
    public string? INN { get; set; }
    [Required(ErrorMessage = "Заполните наименование организации")]
    [Display(Name = "Наименование")]
    public string? Name { get; set; }
    [Required]
    [RegularExpression(@"^(Юридическое лицо|Индивидуальный предприниматель)$", ErrorMessage = "Выберите 'Юридическое лицо' или 'Индивидуальный предприниматель'")]
    [Display(Name = "Тип")]
    public string? ClientType { get; set; }
    [DataType(DataType.Date)]
    [Display(Name = "Дата создания")]
    public DateTime CreatedAt { get; set; }
    [DataType(DataType.Date)]
    [Display(Name = "Дата последнего изменения")]
    public DateTime? LastUpdatedAt { get; set; }
    public ICollection<Founder>? Founders { get; set; }
}
