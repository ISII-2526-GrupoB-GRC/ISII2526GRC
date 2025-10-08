using Microsoft.AspNetCore.Identity;

namespace AppForSEII2526.API.Models;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser {

    [Display(Name = "Name")]
    [StringLength(50, ErrorMessage = "Máximo número de caracteres alcanzado (50)", MinimumLength = 1)]
    public string Name
    {
        get;
        set;
    }

    [Display(Name = "Surname")]
    [StringLength(80, ErrorMessage = "Máximo número de caracteres alcanzado (50)", MinimumLength = 1)]
    public string Surname
    {
        get;
        set;
    }

    [Display(Name = "Delivery Address")]
    [StringLength(80, ErrorMessage = "Máximo número de caracteres alcanzado (50)", MinimumLength = 1)]
    public string DeliveryAddress
    {
        get;
        set;
    }

    public IList<Rental> rentals { get; set; }
    public IList<Purchase> purchases { get; set; }
    public IList<Receipt> receipts { get; set; }


}