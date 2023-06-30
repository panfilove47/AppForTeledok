using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApp.Models;

    public class Founder
    {
    
    public int Id { get; set; }
    [MinLength(12, ErrorMessage = "ИНН состоит из 12 цифр"), MaxLength(12, ErrorMessage = "ИНН состоит из 12 цифр"), RegularExpression(@"^[0-9]+$", ErrorMessage = "ИНН состоит из 12 цифр")]
    [Required(ErrorMessage = "Поле ИНН должно быть заполнено")]
    [Display(Name = "ИНН")]
    public string? INN { get; set; }
    [Required]
    [RegularExpression(@"^[А-ЯЁа-яё]+\s+[А-ЯЁа-яё]+\s+[А-ЯЁа-яё]+$", ErrorMessage = "Введите полное ФИО")]
    [Display(Name = "ФИО")]
    public string? FIO { get; set; }
    [DataType(DataType.Date)]
    [Display(Name = "Дата создания")]
    public DateTime CreatedAt { get; set; }
    [DataType(DataType.Date)]
    [Display(Name = "Дата последнего изменения")]
    public DateTime? LastUpdatedAt { get; set; }
    [Required]
    [Display(Name = "Id Клиента")]
    public int ClientId { get; set; }
    public Client? Client { get; set; }
}

