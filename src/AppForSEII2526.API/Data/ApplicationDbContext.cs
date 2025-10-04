using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options) {

    public DbSet<Modelo> Modelo { get; set; }
    public DbSet<Purchase> Purchase { get; set; }
    public DbSet<PurchaseItem> PurchaseItem { get; set; }
    public DbSet<Device> Device { get; set; }
}
