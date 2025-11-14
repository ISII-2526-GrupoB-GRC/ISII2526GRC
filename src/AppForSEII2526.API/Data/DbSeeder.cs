using Microsoft.AspNetCore.Identity;

namespace AppForSEII2526.API.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            // Verificar si ya existen datos
            if (context.Scale.Any())
            {
                return; // La base de datos ya ha sido poblada
            }

            // 1. Poblar Scales (3 escalas)
            var scales = new List<Scale>
            {
                new Scale { Name = "Lujo" },
                new Scale { Name = "Media" },
                new Scale { Name = "Básica" }
            };
            await context.Scale.AddRangeAsync(scales);
            await context.SaveChangesAsync();

            // 2. Poblar Usuarios (3 usuarios)
            var users = new List<(ApplicationUser User, string Password)>
            {
                (new ApplicationUser
                {
                    UserName = "jmromero",
                    Name = "José María",
                    Surname = "Romero Tendero",
                    Email = "jmromero@example.com",
                    EmailConfirmed = true,
                    PhoneNumber = "600123456",
                    PhoneNumberConfirmed = true
                }, "Password123!"),

                (new ApplicationUser
                {
                    UserName = "rdiaz",
                    Name = "Rodrigo",
                    Surname = "Díaz Quintanar",
                    Email = "rdiaz@example.com",
                    EmailConfirmed = true,
                    PhoneNumber = "600234567",
                    PhoneNumberConfirmed = true
                }, "Password123!"),

                (new ApplicationUser
                {
                    UserName = "grosillo",
                    Name = "Guillermo",
                    Surname = "Rosillo Serrano",
                    Email = "grosillo@example.com",
                    EmailConfirmed = true,
                    PhoneNumber = "600345678",
                    PhoneNumberConfirmed = true
                }, "Password123!")
            };

            foreach (var (user, password) in users)
            {
                await userManager.CreateAsync(user, password);
            }
            await context.SaveChangesAsync();

            // 3. Poblar Modelos (5 modelos)
            var models = new List<Model>
            {
                new Model { NameModel = "iPhone 14 Pro" },
                new Model { NameModel = "Samsung Galaxy S23" },
                new Model { NameModel = "Google Pixel 8" },
                new Model { NameModel = "Xiaomi 13 Pro" },
                new Model { NameModel = "OnePlus 11" }
            };
            await context.Modelo.AddRangeAsync(models);
            await context.SaveChangesAsync();

            // 4. Poblar Dispositivos (5 dispositivos)
            var devices = new List<Device>
            {
                new Device
                {
                    Brand = "Apple",
                    Color = "Negro",
                    Name = "iPhone 14 Pro Max",
                    priceForPurchace = 1199.99,
                    priceForRent = 50.00,
                    Quality = Device.QualityType.High,
                    quantityForPurchase = 15,
                    quantityForRent = 8,
                    Year = 2023,
                    Model = models[0]
                },
                new Device
                {
                    Brand = "Samsung",
                    Color = "Blanco",
                    Name = "Galaxy S23 Ultra",
                    priceForPurchace = 1099.99,
                    priceForRent = 45.00,
                    Quality = Device.QualityType.High,
                    quantityForPurchase = 20,
                    quantityForRent = 10,
                    Year = 2023,
                    Model = models[1]
                },
                new Device
                {
                    Brand = "Google",
                    Color = "Azul",
                    Name = "Pixel 8 Pro",
                    priceForPurchace = 899.99,
                    priceForRent = 40.00,
                    Quality = Device.QualityType.Medium,
                    quantityForPurchase = 12,
                    quantityForRent = 6,
                    Year = 2023,
                    Model = models[2]
                },
                new Device
                {
                    Brand = "Xiaomi",
                    Color = "Verde",
                    Name = "Xiaomi 13 Pro 5G",
                    priceForPurchace = 799.99,
                    priceForRent = 35.00,
                    Quality = Device.QualityType.Medium,
                    quantityForPurchase = 18,
                    quantityForRent = 9,
                    Year = 2023,
                    Model = models[3]
                },
                new Device
                {
                    Brand = "OnePlus",
                    Color = "Gris",
                    Name = "OnePlus 11 5G",
                    priceForPurchace = 699.99,
                    priceForRent = 30.00,
                    Quality = Device.QualityType.Medium,
                    quantityForPurchase = 10,
                    quantityForRent = 5,
                    Year = 2023,
                    Model = models[4]
                }
            };
            await context.Device.AddRangeAsync(devices);
            await context.SaveChangesAsync();

            // 5. Poblar Reparaciones (5 reparaciones)
            var repairs = new List<Repair>
            {
                new Repair
                {
                    Name = "Cambio pantalla",
                    Description = "Sustitución completa de pantalla OLED",
                    Cost = 150.00m,
                    Scale = scales[0]
                },
                new Repair
                {
                    Name = "Cambio batería",
                    Description = "Reemplazo de batería original",
                    Cost = 80.00m,
                    Scale = scales[1]
                },
                new Repair
                {
                    Name = "Reparación puerto carga",
                    Description = "Reparación del puerto de carga USB-C",
                    Cost = 45.00m,
                    Scale = scales[2]
                },
                new Repair
                {
                    Name = "Cambio cámara",
                    Description = "Sustitución de cámara trasera",
                    Cost = 120.00m,
                    Scale = scales[0]
                },
                new Repair
                {
                    Name = "Limpieza por agua",
                    Description = "Limpieza interna por daños líquidos",
                    Cost = 60.00m,
                    Scale = scales[1]
                }
            };
            await context.Repair.AddRangeAsync(repairs);
            await context.SaveChangesAsync();

            // 6. Poblar Compras (5 compras)
            var usersList = await context.ApplicationUser.ToListAsync();
            var purchases = new List<Purchase>
            {
                new Purchase
                {
                    DeliveryAddress = "Calle Mayor 123, Madrid",
                    PaymentMethod = PaymentMethodTypes.CreditCard,
                    PurchaseDate = DateTime.SpecifyKind(new DateTime(2024, 1, 15), DateTimeKind.Unspecified),
                    TotalPrice = 1199.99,
                    TotalQuanty = 1,
                    ApplicationUser = usersList[0]
                },
                new Purchase
                {
                    DeliveryAddress = "Avenida Libertad 45, Barcelona",
                    PaymentMethod = PaymentMethodTypes.PayPal,
                    PurchaseDate = DateTime.SpecifyKind(new DateTime(2024, 2, 20), DateTimeKind.Unspecified),
                    TotalPrice = 2199.98,
                    TotalQuanty = 2,
                    ApplicationUser = usersList[1]
                },
                new Purchase
                {
                    DeliveryAddress = "Plaza España 67, Valencia",
                    PaymentMethod = PaymentMethodTypes.Cash,
                    PurchaseDate = DateTime.SpecifyKind(new DateTime(2024, 3, 10), DateTimeKind.Unspecified),
                    TotalPrice = 899.99,
                    TotalQuanty = 1,
                    ApplicationUser = usersList[2]
                },
                new Purchase
                {
                    DeliveryAddress = "Calle Real 89, Sevilla",
                    PaymentMethod = PaymentMethodTypes.CreditCard,
                    PurchaseDate = DateTime.SpecifyKind(new DateTime(2024, 4, 5), DateTimeKind.Unspecified),
                    TotalPrice = 1599.98,
                    TotalQuanty = 2,
                    ApplicationUser = usersList[0]
                },
                new Purchase
                {
                    DeliveryAddress = "Avenida Andalucía 234, Málaga",
                    PaymentMethod = PaymentMethodTypes.PayPal,
                    PurchaseDate = DateTime.SpecifyKind(new DateTime(2024, 5, 12), DateTimeKind.Unspecified),
                    TotalPrice = 699.99,
                    TotalQuanty = 1,
                    ApplicationUser = usersList[1]
                }
            };
            await context.Purchase.AddRangeAsync(purchases);
            await context.SaveChangesAsync();

            // 7. Poblar PurchaseItems (5 items)
            var purchaseItems = new List<PurchaseItem>
            {
                new PurchaseItem
                {
                    Device = devices[0],
                    Purchase = purchases[0],
                    Description = "iPhone 14 Pro Max Negro",
                    Price = 1199.99,
                    Quantity = 1
                },
                new PurchaseItem
                {
                    Device = devices[1],
                    Purchase = purchases[1],
                    Description = "Galaxy S23 Ultra Blanco",
                    Price = 1099.99,
                    Quantity = 2
                },
                new PurchaseItem
                {
                    Device = devices[2],
                    Purchase = purchases[2],
                    Description = "Pixel 8 Pro Azul",
                    Price = 899.99,
                    Quantity = 1
                },
                new PurchaseItem
                {
                    Device = devices[3],
                    Purchase = purchases[3],
                    Description = "Xiaomi 13 Pro Verde",
                    Price = 799.99,
                    Quantity = 2
                },
                new PurchaseItem
                {
                    Device = devices[4],
                    Purchase = purchases[4],
                    Description = "OnePlus 11 Gris",
                    Price = 699.99,
                    Quantity = 1
                }
            };
            await context.PurchaseItem.AddRangeAsync(purchaseItems);
            await context.SaveChangesAsync();

            // 8. Poblar Alquileres (5 alquileres)
            var rentals = new List<Rental>
            {
                new Rental
                {
                    PaymentMethod = PaymentMethodTypes.CreditCard,
                    RentalDate = DateTime.SpecifyKind(new DateTime(2024, 1, 10), DateTimeKind.Unspecified),
                    RentalDateFrom = DateTime.SpecifyKind(new DateTime(2024, 1, 15), DateTimeKind.Unspecified),
                    RentalDateTo = DateTime.SpecifyKind(new DateTime(2024, 1, 22), DateTimeKind.Unspecified),
                    TotalPrice = 350.00,
                    ApplicationUser = usersList[0],
                    DeliveryAddress = "Calle Mayor 123, Madrid"
                },
                new Rental
                {
                    PaymentMethod = PaymentMethodTypes.PayPal,
                    RentalDate = DateTime.SpecifyKind(new DateTime(2024, 2, 5), DateTimeKind.Unspecified),
                    RentalDateFrom = DateTime.SpecifyKind(new DateTime(2024, 2, 10), DateTimeKind.Unspecified),
                    RentalDateTo = DateTime.SpecifyKind(new DateTime(2024, 2, 24), DateTimeKind.Unspecified),
                    TotalPrice = 630.00,
                    ApplicationUser = usersList[1],
                    DeliveryAddress = "Avenida Libertad 45, Barcelona"
                },
                new Rental
                {
                    PaymentMethod = PaymentMethodTypes.Cash,
                    RentalDate = DateTime.SpecifyKind(new DateTime(2024, 3, 1), DateTimeKind.Unspecified),
                    RentalDateFrom = DateTime.SpecifyKind(new DateTime(2024, 3, 5), DateTimeKind.Unspecified),
                    RentalDateTo = DateTime.SpecifyKind(new DateTime(2024, 3, 12), DateTimeKind.Unspecified),
                    TotalPrice = 280.00,
                    ApplicationUser = usersList[2],
                    DeliveryAddress = "Plaza España 67, Valencia"
                },
                new Rental
                {
                    PaymentMethod = PaymentMethodTypes.CreditCard,
                    RentalDate = DateTime.SpecifyKind(new DateTime(2024, 4, 12), DateTimeKind.Unspecified),
                    RentalDateFrom = DateTime.SpecifyKind(new DateTime(2024, 4, 15), DateTimeKind.Unspecified),
                    RentalDateTo = DateTime.SpecifyKind(new DateTime(2024, 4, 29), DateTimeKind.Unspecified),
                    TotalPrice = 490.00,
                    ApplicationUser = usersList[0],
                    DeliveryAddress = "Calle Real 89, Sevilla"
                },
                new Rental
                {
                    PaymentMethod = PaymentMethodTypes.PayPal,
                    RentalDate = DateTime.SpecifyKind(new DateTime(2024, 5, 20), DateTimeKind.Unspecified),
                    RentalDateFrom = DateTime.SpecifyKind(new DateTime(2024, 5, 25), DateTimeKind.Unspecified),
                    RentalDateTo = DateTime.SpecifyKind(new DateTime(2024, 6, 1), DateTimeKind.Unspecified),
                    TotalPrice = 210.00,
                    ApplicationUser = usersList[1],
                    DeliveryAddress = "Avenida Andalucía 234, Málaga"
                }
            };
            await context.Rental.AddRangeAsync(rentals);
            await context.SaveChangesAsync();

            // 9. Poblar RentDevices (5 dispositivos alquilados)
            var rentDevices = new List<RentDevice>
            {
                new RentDevice
                {
                    Device = devices[0],
                    Rental = rentals[0],
                    Price = 50.00,
                    Quantity = 1
                },
                new RentDevice
                {
                    Device = devices[1],
                    Rental = rentals[1],
                    Price = 45.00,
                    Quantity = 2
                },
                new RentDevice
                {
                    Device = devices[2],
                    Rental = rentals[2],
                    Price = 40.00,
                    Quantity = 1
                },
                new RentDevice
                {
                    Device = devices[3],
                    Rental = rentals[3],
                    Price = 35.00,
                    Quantity = 2
                },
                new RentDevice
                {
                    Device = devices[4],
                    Rental = rentals[4],
                    Price = 30.00,
                    Quantity = 1
                }
            };
            await context.RentDevice.AddRangeAsync(rentDevices);
            await context.SaveChangesAsync();

            // 10. Poblar Recibos (5 recibos)
            var receipts = new List<Receipt>
            {
                new Receipt
                {
                    PaymentMethod = PaymentMethodTypes.CreditCard,
                    ReceiptDate = DateTime.SpecifyKind(new DateTime(2024, 1, 18), DateTimeKind.Unspecified),
                    TotalPrice = 150.00m,
                    deliveryAddres = "Calle Mayor 123, Madrid",
                    ApplicationUser = usersList[0]
                },
                new Receipt
                {
                    PaymentMethod = PaymentMethodTypes.PayPal,
                    ReceiptDate = DateTime.SpecifyKind(new DateTime(2024, 2, 22), DateTimeKind.Unspecified),
                    TotalPrice = 80.00m,
                    deliveryAddres = "Avenida Libertad 45, Barcelona",
                    ApplicationUser = usersList[1]
                },
                new Receipt
                {
                    PaymentMethod = PaymentMethodTypes.Cash,
                    ReceiptDate = DateTime.SpecifyKind(new DateTime(2024, 3, 15), DateTimeKind.Unspecified),
                    TotalPrice = 45.00m,
                    deliveryAddres = "Plaza España 67, Valencia",
                    ApplicationUser = usersList[2]
                },
                new Receipt
                {
                    PaymentMethod = PaymentMethodTypes.CreditCard,
                    ReceiptDate = DateTime.SpecifyKind(new DateTime(2024, 4, 8), DateTimeKind.Unspecified),
                    TotalPrice = 120.00m,
                    deliveryAddres = "Calle Real 89, Sevilla",
                    ApplicationUser = usersList[0]
                },
                new Receipt
                {
                    PaymentMethod = PaymentMethodTypes.PayPal,
                    ReceiptDate = DateTime.SpecifyKind(new DateTime(2024, 5, 14), DateTimeKind.Unspecified),
                    TotalPrice = 60.00m,
                    deliveryAddres = "Avenida Andalucía 234, Málaga",
                    ApplicationUser = usersList[1]
                }
            };
            await context.Receipt.AddRangeAsync(receipts);
            await context.SaveChangesAsync();

            // 11. Poblar ReceiptItems (5 items de recibo)
            var receiptItems = new List<ReceiptItem>
            {
                new ReceiptItem
                {
                    Repair = repairs[0],
                    Receipt = receipts[0],
                    Model = "iPhone 14 Pro"
                },
                new ReceiptItem
                {
                    Repair = repairs[1],
                    Receipt = receipts[1],
                    Model = "Galaxy S23"
                },
                new ReceiptItem
                {
                    Repair = repairs[2],
                    Receipt = receipts[2],
                    Model = "Pixel 8"
                },
                new ReceiptItem
                {
                    Repair = repairs[3],
                    Receipt = receipts[3],
                    Model = "Xiaomi 13 Pro"
                },
                new ReceiptItem
                {
                    Repair = repairs[4],
                    Receipt = receipts[4],
                    Model = "OnePlus 11"
                }
            };
            await context.ReceiptItem.AddRangeAsync(receiptItems);
            await context.SaveChangesAsync();
        }
    }
}