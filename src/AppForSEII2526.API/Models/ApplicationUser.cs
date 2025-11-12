using Microsoft.AspNetCore.Identity;

namespace AppForSEII2526.API.Models;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{

    public ApplicationUser() { }

    public ApplicationUser(int id, string name, string surname, string address)
    {
        this.Id = id;

        this.Name = name;
        this.Surname = surname;
        //esto aqui no va


    }


    [Display(Name = "Name")]
    [StringLength(80, ErrorMessage = "Máximo número de caracteres alcanzado (80)", MinimumLength = 1)]
    public string Name
    {
        get;
        set;
    }

    [Display(Name = "Surname")]
    [StringLength(80, ErrorMessage = "Máximo número de caracteres alcanzado (80)", MinimumLength = 1)]
    public string Surname
    {
        get;
        set;
    }


    public int Id { get; set; }

    public IList<Rental> rentals { get; set; }
    public IList<Purchase> purchases { get; set; }
    public IList<Receipt> receipts { get; set; }





}