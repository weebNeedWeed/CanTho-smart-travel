using System.ComponentModel.DataAnnotations;

namespace API.Areas.Admin.ViewModels.Auth;

public record LoginViewModel
{
    [DataType(DataType.Text)]
    [Required]
    public string username { get; set; }
    [DataType(DataType.Password)]
    [Required]
    public string password { get; set; }
}
