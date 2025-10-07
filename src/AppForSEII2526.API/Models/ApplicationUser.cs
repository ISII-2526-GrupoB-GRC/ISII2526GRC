using Microsoft.AspNetCore.Identity;

namespace AppForSEII2526.API.Models;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser {

    public ApplicationUser() { }


    public ApplicationUser(string id, string name, string surname, string userName)
    {
        this.Id = id;
        this.Name = name;
        this.Surname = surname;
        this.UserName = userName;
        this.Email = userName;
    }

    [Display(Name = "Name")]
    public string Name
    {
        get;
        set;
    }

    [Display(Name = "Surname")]
    public string Surname
    {
        get;
        set;
    }

    [Display(Name = "Country")]
    public string Country {
        get;
        set;
    }



}