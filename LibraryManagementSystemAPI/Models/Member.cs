using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystemAPI.Models;

public class Member
{
    public int Id { get; set; }
    public string Name { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    public string Address { get; set; }
    public string Postcode { get; set; }
    [Phone]
    public string Phone { get; set; }
}