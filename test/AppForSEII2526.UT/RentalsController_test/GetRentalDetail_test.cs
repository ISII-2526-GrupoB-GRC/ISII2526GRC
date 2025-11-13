using AppForSEII2526.API.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AppForSEII2526.API.Models.Device;
using AppForSEII2526.API.DTOs.RentDTOs;

namespace AppForSEII2526.UT.RentalsController_test
{
    public class GetRentalDetail_test : AppForSEII25264SqliteUT
    {
        private DateTime today = new DateTime(2025, 5, 4);
        public GetRentalDetail_test()
        {
            var models = new List<Model>()
            {
                new Model("Portátil"),
                new Model("Ordenador de Sobremesa"),
                new Model("Tablet"),
            };

            var devices = new List<Device>()
            {
                new Device("Dell", models[0],"Gris", "Dell XPS 13", 1200.99, 45d, QualityType.Medium, 8, 12, 2023),
                new Device("HP", models[1], "Plata", "HP Sobre x360", 1350.00, 50d, QualityType.High, 10, 15, 2023),
                new Device("Samsung", models[2], "Blanco", "Samsung Galaxy tab 8", 521.99, 30d, QualityType.High, 5, 8, 2022)
            };

            // ApplicationUser user1 = new ApplicationUser("Ana", "González", "Calle Serrano 45, Madrid");
            ApplicationUser user1 = new ApplicationUser()
            {
                UserName = "Ana",
                Name = "Ana",
                Surname = "González",
            };

            var deliveryAddress = "Calle Serrano 45, Madrid";

            var rental = new Rental()
            {
                ApplicationUser = user1,
                DeliveryAddress = deliveryAddress,
                RentalDate = today,
                PaymentMethod = PaymentMethodTypes.CreditCard,
                RentalDateFrom = today.AddDays(2),
                RentalDateTo = today.AddDays(5),
                RentDevices = new List<RentDevice>()
            };

            rental.RentDevices.Add(new RentDevice(1, devices[0], rental));

            double numDays = (rental.RentalDateTo - rental.RentalDateFrom).Days;

            rental.TotalPrice = rental.RentDevices.Sum(rd => rd.Price * rd.Quantity * numDays);

            _context.Add(user1);
            _context.AddRange(models);
            _context.AddRange(devices);
            _context.Add(rental);
            _context.SaveChanges();
        }

        [Fact]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "UnitTesting")]
        public async Task GetRentalDetail_NotFound_test()
        {
            // Arrange
            var mock = new Mock<ILogger<RentalsController>>();
            ILogger<RentalsController> logger = mock.Object;
            var controller = new RentalsController(_context, logger);
            // Act
            var result = await controller.GetRentalDetail(1000000); //1.000.000 es un id que no existe
            // Assert
            // Comprobamos que la respuesta es NotFound
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "UnitTesting")]
        public async Task GetRental_Found_test()
        {
            // Arrange
            var mock = new Mock<ILogger<RentalsController>>();
            ILogger<RentalsController> logger = mock.Object;
            var controller = new RentalsController(_context, logger);

            var expectedRental = new RentalDetailDTO(
                "Ana",
                "González",
                "Calle Serrano 45, Madrid",
                today,
                135d,         // Precio del dispositivo por día(45) * número de días (3) * cantidad (1)
                today.AddDays(2),
                today.AddDays(5),
                new List<RentalItemDTO>()
                );

            expectedRental.RentedDevices.Add(new RentalItemDTO
            (
                "Dell",     // Marca
                "Portátil", // Modelo
                45d,        // Precio
                1           // Cantidad
            ));

            // Act
            // Llamamos al System Under Test (SUT)
            var result = await controller.GetRentalDetail(1); // El 1 es el primer alquiler

            // Assert
            // Comprobamos que la respuesta es OK yse obtiene el rental
            var okResult = Assert.IsType<OkObjectResult>(result);

            //Comprobamos que el rental obtenido es el esperado (Que son el mismo -Equals-)
            var rentalDTOActual = Assert.IsType<RentalDetailDTO>(okResult.Value);
            Assert.Equal(expectedRental.RentedDevices, rentalDTOActual.RentedDevices);
        }
    }
}