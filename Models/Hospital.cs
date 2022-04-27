using System.ComponentModel.DataAnnotations;

namespace WebLab5.Models;

public class Hospital
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Address is required")]
    public string Address { get; set; }

    [Required(ErrorMessage = "Phone number is required")]
    public string Phone { get; set; }
}