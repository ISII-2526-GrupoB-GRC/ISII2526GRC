using AppForSEII2526.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AppForSEII2526.API.Data
{
    public static class DbSeeder
    {
        public static void Initialize(ApplicationDbContext context, IServiceProvider serviceProvider, ILogger logger)
        {
            // Definimos los roles, igual que en el ejemplo de referencia
            List<string> rolesNames = new List<string> { "Administrator", "Employee", "Customer" };

            // 1. Sembrar Roles
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            try
            {
                SeedRoles(roleManager, rolesNames);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred seeding the roles in the Database.");
            }

            // 2. Sembrar Usuarios (tus 3 usuarios como administradores)
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            try
            {
                SeedUsers(userManager, rolesNames);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred seeding the Users in the Database.");
            }

            // 3. Sembrar Datos del Negocio (Scales, Models, Devices, Repairs, etc.)
            try
            {
                SeedBusinessData(context, userManager);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred seeding the Business Data in the Database.");
            }
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                if (!roleManager.RoleExistsAsync(roleName).Result)
                {
                    IdentityRole role = new IdentityRole
                    {
                        Name = roleName,
                        NormalizedName = roleName.ToUpper()
                    };
                    IdentityResult roleResult = roleManager.CreateAsync(role).Result;
                }
            }
        }

        public static void SeedUsers(UserManager<ApplicationUser> userManager, List<string> roles)
        {
            // Lista de usuarios a crear según tu repositorio original
            // CAMBIO: UserName ahora coincide con el Email
            var usersToSeed = new List<(ApplicationUser User, string Password)>
            {
                (new ApplicationUser
                {
                    UserName = "jmromero@example.com", // Cambiado de "jmromero"
                    Name = "José María",
                    Surname = "Romero Tendero",
                    Email = "jmromero@example.com",
                    EmailConfirmed = true,
                    PhoneNumber = "600123456",
                    PhoneNumberConfirmed = true
                }, "Password123!"),

                (new ApplicationUser
                {
                    UserName = "rdiaz@example.com", // Cambiado de "rdiaz"
                    Name = "Rodrigo",
                    Surname = "Díaz Quintanar",
                    Email = "rdiaz@example.com",
                    EmailConfirmed = true,
                    PhoneNumber = "600234567",
                    PhoneNumberConfirmed = true
                }, "Password123!"),

                (new ApplicationUser
                {
                    UserName = "grosillo@example.com", // Cambiado de "grosillo"
                    Name = "Guillermo",
                    Surname = "Rosillo Serrano",
                    Email = "grosillo@example.com",
                    EmailConfirmed = true,
                    PhoneNumber = "600345678",
                    PhoneNumberConfirmed = true
                }, "Password123!")
            };

            foreach (var (userTemplate, password) in usersToSeed)
            {
                // Comprobamos si el usuario existe por nombre de usuario (ahora es el email)
                if (userManager.FindByNameAsync(userTemplate.UserName).Result == null)
                {
                    var result = userManager.CreateAsync(userTemplate, password);
                    result.Wait();

                    if (result.IsCompletedSuccessfully)
                    {
                        // Asignar rol de Administrador (índice 0 en la lista de roles)
                        userManager.AddToRoleAsync(userTemplate, roles[0]).Wait();
                    }
                }
            }
        }

        public static void SeedBusinessData(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            // --- 1. Scales ---
            if (!context.Scale.Any())
            {
                var scales = new List<Scale>
                {
                    new Scale { Name = "Lujo" },
                    new Scale { Name = "Media" },
                    new Scale { Name = "Básica" }
                };
                context.Scale.AddRange(scales);
                context.SaveChanges();
            }

            // --- 2. Models (Modelos) ---
            if (!context.Modelo.Any())
            {
                var models = new List<Model>
                {
                    new Model { NameModel = "iPhone 14 Pro" },
                    new Model { NameModel = "Samsung Galaxy S23" },
                    new Model { NameModel = "Google Pixel 8" },
                    new Model { NameModel = "Xiaomi 13 Pro" },
                    new Model { NameModel = "OnePlus 11" }
                };
                context.Modelo.AddRange(models);
                context.SaveChanges();
            }

            // --- 3. Devices (Dispositivos) ---
            if (!context.Device.Any())
            {
                // Recuperamos los modelos para asignarlos
                var models = context.Modelo.ToList();
                // Aseguramos el orden o buscamos por nombre para mantener consistencia con los datos originales
                var miPhone = models.First(m => m.NameModel == "iPhone 14 Pro");
                var mSamsung = models.First(m => m.NameModel == "Samsung Galaxy S23");
                var mPixel = models.First(m => m.NameModel == "Google Pixel 8");
                var mXiaomi = models.First(m => m.NameModel == "Xiaomi 13 Pro");
                var mOnePlus = models.First(m => m.NameModel == "OnePlus 11");

                var devices = new List<Device>
                {
                    new Device
                    {
                        Brand = "Apple", Color = "Negro", Name = "iPhone 14 Pro Max",
                        priceForPurchace = 1199.99, priceForRent = 50.00, Quality = Device.QualityType.High,
                        quantityForPurchase = 15, quantityForRent = 8, Year = 2023, Model = miPhone
                    },
                    new Device
                    {
                        Brand = "Samsung", Color = "Blanco", Name = "Galaxy S23 Ultra",
                        priceForPurchace = 1099.99, priceForRent = 45.00, Quality = Device.QualityType.High,
                        quantityForPurchase = 20, quantityForRent = 10, Year = 2023, Model = mSamsung
                    },
                    new Device
                    {
                        Brand = "Google", Color = "Azul", Name = "Pixel 8 Pro",
                        priceForPurchace = 899.99, priceForRent = 40.00, Quality = Device.QualityType.Medium,
                        quantityForPurchase = 12, quantityForRent = 6, Year = 2023, Model = mPixel
                    },
                    new Device
                    {
                        Brand = "Xiaomi", Color = "Verde", Name = "Xiaomi 13 Pro 5G",
                        priceForPurchace = 799.99, priceForRent = 35.00, Quality = Device.QualityType.Medium,
                        quantityForPurchase = 18, quantityForRent = 9, Year = 2023, Model = mXiaomi
                    },
                    new Device
                    {
                        Brand = "OnePlus", Color = "Gris", Name = "OnePlus 11 5G",
                        priceForPurchace = 699.99, priceForRent = 30.00, Quality = Device.QualityType.Medium,
                        quantityForPurchase = 10, quantityForRent = 5, Year = 2023, Model = mOnePlus
                    }
                };
                context.Device.AddRange(devices);
                context.SaveChanges();
            }

            // --- 4. Repairs (Reparaciones) ---
            if (!context.Repair.Any())
            {
                var scales = context.Scale.ToList();
                var sLujo = scales.First(s => s.Name == "Lujo");
                var sMedia = scales.First(s => s.Name == "Media");
                var sBasica = scales.First(s => s.Name == "Básica");

                var repairs = new List<Repair>
                {
                    new Repair { Name = "Cambio pantalla", Description = "Sustitución completa de pantalla OLED", Cost = 150.00m, Scale = sLujo },
                    new Repair { Name = "Cambio batería", Description = "Reemplazo de batería original", Cost = 80.00m, Scale = sMedia },
                    new Repair { Name = "Reparación puerto carga", Description = "Reparación del puerto de carga USB-C", Cost = 45.00m, Scale = sBasica },
                    new Repair { Name = "Cambio cámara", Description = "Sustitución de cámara trasera", Cost = 120.00m, Scale = sLujo },
                    new Repair { Name = "Limpieza por agua", Description = "Limpieza interna por daños líquidos", Cost = 60.00m, Scale = sMedia }
                };
                context.Repair.AddRange(repairs);
                context.SaveChanges();
            }

            // Recuperamos datos necesarios para operaciones transaccionales
            var users = context.ApplicationUser.ToList();

            // CAMBIO: Buscamos por el UserName actualizado (que ahora es el email)
            var userJm = users.FirstOrDefault(u => u.UserName == "jmromero@example.com");
            var userRd = users.FirstOrDefault(u => u.UserName == "rdiaz@example.com");
            var userGr = users.FirstOrDefault(u => u.UserName == "grosillo@example.com");

            // Si los usuarios no se han creado correctamente arriba por alguna razón, abortamos la carga de datos dependientes
            if (userJm == null || userRd == null || userGr == null) return;

            var devicesDb = context.Device.ToList();
            // Mapeo por nombre para facilitar
            var dIphone = devicesDb.First(d => d.Name == "iPhone 14 Pro Max");
            var dGalaxy = devicesDb.First(d => d.Name == "Galaxy S23 Ultra");
            var dPixel = devicesDb.First(d => d.Name == "Pixel 8 Pro");
            var dXiaomi = devicesDb.First(d => d.Name == "Xiaomi 13 Pro 5G");
            var dOnePlus = devicesDb.First(d => d.Name == "OnePlus 11 5G");

            // --- 5. Purchases y PurchaseItems ---
            if (!context.Purchase.Any())
            {
                var purchases = new List<Purchase>
                {
                    new Purchase { DeliveryAddress = "Calle Mayor 123, Madrid", PaymentMethod = PaymentMethodTypes.CreditCard, PurchaseDate = new DateTime(2024, 1, 15), TotalPrice = 1199.99, TotalQuanty = 1, ApplicationUser = userJm },
                    new Purchase { DeliveryAddress = "Avenida Libertad 45, Barcelona", PaymentMethod = PaymentMethodTypes.PayPal, PurchaseDate = new DateTime(2024, 2, 20), TotalPrice = 2199.98, TotalQuanty = 2, ApplicationUser = userRd },
                    new Purchase { DeliveryAddress = "Plaza España 67, Valencia", PaymentMethod = PaymentMethodTypes.Cash, PurchaseDate = new DateTime(2024, 3, 10), TotalPrice = 899.99, TotalQuanty = 1, ApplicationUser = userGr },
                    new Purchase { DeliveryAddress = "Calle Real 89, Sevilla", PaymentMethod = PaymentMethodTypes.CreditCard, PurchaseDate = new DateTime(2024, 4, 5), TotalPrice = 1599.98, TotalQuanty = 2, ApplicationUser = userJm },
                    new Purchase { DeliveryAddress = "Avenida Andalucía 234, Málaga", PaymentMethod = PaymentMethodTypes.PayPal, PurchaseDate = new DateTime(2024, 5, 12), TotalPrice = 699.99, TotalQuanty = 1, ApplicationUser = userRd }
                };
                context.Purchase.AddRange(purchases);
                context.SaveChanges(); // Guardamos para generar IDs

                var purchaseItems = new List<PurchaseItem>
                {
                    new PurchaseItem { Device = dIphone, Purchase = purchases[0], Description = "iPhone 14 Pro Max Negro", Price = 1199.99, Quantity = 1 },
                    new PurchaseItem { Device = dGalaxy, Purchase = purchases[1], Description = "Galaxy S23 Ultra Blanco", Price = 1099.99, Quantity = 2 },
                    new PurchaseItem { Device = dPixel, Purchase = purchases[2], Description = "Pixel 8 Pro Azul", Price = 899.99, Quantity = 1 },
                    new PurchaseItem { Device = dXiaomi, Purchase = purchases[3], Description = "Xiaomi 13 Pro Verde", Price = 799.99, Quantity = 2 },
                    new PurchaseItem { Device = dOnePlus, Purchase = purchases[4], Description = "OnePlus 11 Gris", Price = 699.99, Quantity = 1 }
                };
                context.PurchaseItem.AddRange(purchaseItems);
                context.SaveChanges();
            }

            // --- 6. Rentals y RentDevices ---
            if (!context.Rental.Any())
            {
                var rentals = new List<Rental>
                {
                    new Rental { PaymentMethod = PaymentMethodTypes.CreditCard, RentalDate = new DateTime(2024, 1, 10), RentalDateFrom = new DateTime(2024, 1, 15), RentalDateTo = new DateTime(2024, 1, 22), TotalPrice = 350.00, ApplicationUser = userJm, DeliveryAddress = "Calle Mayor 123, Madrid" },
                    new Rental { PaymentMethod = PaymentMethodTypes.PayPal, RentalDate = new DateTime(2024, 2, 5), RentalDateFrom = new DateTime(2024, 2, 10), RentalDateTo = new DateTime(2024, 2, 24), TotalPrice = 630.00, ApplicationUser = userRd, DeliveryAddress = "Avenida Libertad 45, Barcelona" },
                    new Rental { PaymentMethod = PaymentMethodTypes.Cash, RentalDate = new DateTime(2024, 3, 1), RentalDateFrom = new DateTime(2024, 3, 5), RentalDateTo = new DateTime(2024, 3, 12), TotalPrice = 280.00, ApplicationUser = userGr, DeliveryAddress = "Plaza España 67, Valencia" },
                    new Rental { PaymentMethod = PaymentMethodTypes.CreditCard, RentalDate = new DateTime(2024, 4, 12), RentalDateFrom = new DateTime(2024, 4, 15), RentalDateTo = new DateTime(2024, 4, 29), TotalPrice = 490.00, ApplicationUser = userJm, DeliveryAddress = "Calle Real 89, Sevilla" },
                    new Rental { PaymentMethod = PaymentMethodTypes.PayPal, RentalDate = new DateTime(2024, 5, 20), RentalDateFrom = new DateTime(2024, 5, 25), RentalDateTo = new DateTime(2024, 6, 1), TotalPrice = 210.00, ApplicationUser = userRd, DeliveryAddress = "Avenida Andalucía 234, Málaga" }
                };
                context.Rental.AddRange(rentals);
                context.SaveChanges();

                var rentDevices = new List<RentDevice>
                {
                    new RentDevice { Device = dIphone, Rental = rentals[0], Price = 50.00, Quantity = 1 },
                    new RentDevice { Device = dGalaxy, Rental = rentals[1], Price = 45.00, Quantity = 2 },
                    new RentDevice { Device = dPixel, Rental = rentals[2], Price = 40.00, Quantity = 1 },
                    new RentDevice { Device = dXiaomi, Rental = rentals[3], Price = 35.00, Quantity = 2 },
                    new RentDevice { Device = dOnePlus, Rental = rentals[4], Price = 30.00, Quantity = 1 }
                };
                context.RentDevice.AddRange(rentDevices);
                context.SaveChanges();
            }

            // --- 7. Receipts y ReceiptItems ---
            if (!context.Receipt.Any())
            {
                var repairsDb = context.Repair.ToList();
                var rPantalla = repairsDb.First(r => r.Name == "Cambio pantalla");
                var rBateria = repairsDb.First(r => r.Name == "Cambio batería");
                var rPuerto = repairsDb.First(r => r.Name == "Reparación puerto carga");
                var rCamara = repairsDb.First(r => r.Name == "Cambio cámara");
                var rLimpieza = repairsDb.First(r => r.Name == "Limpieza por agua");

                var receipts = new List<Receipt>
                {
                    new Receipt { PaymentMethod = PaymentMethodTypes.CreditCard, ReceiptDate = new DateTime(2024, 1, 18), TotalPrice = 150.00m, deliveryAddres = "Calle Mayor 123, Madrid", ApplicationUser = userJm },
                    new Receipt { PaymentMethod = PaymentMethodTypes.PayPal, ReceiptDate = new DateTime(2024, 2, 22), TotalPrice = 80.00m, deliveryAddres = "Avenida Libertad 45, Barcelona", ApplicationUser = userRd },
                    new Receipt { PaymentMethod = PaymentMethodTypes.Cash, ReceiptDate = new DateTime(2024, 3, 15), TotalPrice = 45.00m, deliveryAddres = "Plaza España 67, Valencia", ApplicationUser = userGr },
                    new Receipt { PaymentMethod = PaymentMethodTypes.CreditCard, ReceiptDate = new DateTime(2024, 4, 8), TotalPrice = 120.00m, deliveryAddres = "Calle Real 89, Sevilla", ApplicationUser = userJm },
                    new Receipt { PaymentMethod = PaymentMethodTypes.PayPal, ReceiptDate = new DateTime(2024, 5, 14), TotalPrice = 60.00m, deliveryAddres = "Avenida Andalucía 234, Málaga", ApplicationUser = userRd }
                };
                context.Receipt.AddRange(receipts);
                context.SaveChanges();

                var receiptItems = new List<ReceiptItem>
                {
                    new ReceiptItem { Repair = rPantalla, Receipt = receipts[0], Model = "iPhone 14 Pro" },
                    new ReceiptItem { Repair = rBateria, Receipt = receipts[1], Model = "Galaxy S23" },
                    new ReceiptItem { Repair = rPuerto, Receipt = receipts[2], Model = "Pixel 8" },
                    new ReceiptItem { Repair = rCamara, Receipt = receipts[3], Model = "Xiaomi 13 Pro" },
                    new ReceiptItem { Repair = rLimpieza, Receipt = receipts[4], Model = "OnePlus 11" }
                };
                context.ReceiptItem.AddRange(receiptItems);
                context.SaveChanges();
            }
        }
    }
}