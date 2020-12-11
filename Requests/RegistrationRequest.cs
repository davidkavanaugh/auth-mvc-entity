using System.ComponentModel.DataAnnotations;

public class RegistrationRequest
{
    [Required]
    [MinLength(2)]
    public string FirstName { get; set; }
    [Required]
    [MinLength(2)]
    public string LastName { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [DataType(DataType.Password)]
    [Required]
    [MinLength(8, ErrorMessage = "Password must be 8 characters or longer!")]
    public string Password { get; set; }
    [Compare("Password")]
    [DataType(DataType.Password)]
    public string Confirm { get; set; }
}