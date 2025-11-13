using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.RentDTOs;
using AppForSEII2526.API.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static AppForSEII2526.API.Models.Device;

namespace AppForSEII2526.UT.RentalsController_test
{
    public class CreateRental_test : AppForSEII25264SqliteUT
    {
        public CreateRental_test()
        {
            var models = new List<Model>()
            {
                new Model("Portátil"),
                new Model("Ordenador de Sobremesa"),
            };

            var devices = new List<Device>()
            {
                new Device("Dell", models[0],"Gris", "Dell XPS 13", 1200.99, 45d, QualityType.Medium, 8, 12, 2023),
                new Device("HP", models[0], "Plata", "HP Spectre x360", 1350.00, 50d, QualityType.High, 10, 15, 2023),
                new Device("Lenovo", models[1], "Negro", "Lenovo ThinkCentre M90", 899.50, 22d, QualityType.Medium, 12, 10, 2023),
            };

            ApplicationUser user1 = new ApplicationUser
            {
                UserName = "Pedro",
                Name = "Pedro",
                Surname = "Martín",
            };

            var deliveryAddress = "Calle Guerrero 3, Madrid";

            var rental = new Rental(deliveryAddress, DateTime.Today, PaymentMethodTypes.CreditCard, DateTime.Today.AddDays(2), DateTime.Today.AddDays(5), new List<RentDevice>(), user1);

            rental.RentDevices.Add(new RentDevice(1, devices[0], rental));

            _context.Add(user1);
            _context.AddRange(models);
            _context.AddRange(devices);
            _context.Add(rental);
            _context.SaveChanges();
        }

        public static IEnumerable<object[]> TestCasesFor_CreateRental()
        {
            var today = DateTime.Today; // Variable para almacenar la fecha actual

            var rentalItems1 = new List<RentalItemDTO>() { new RentalItemDTO("Dell", "Portátil", 45d, 2), new RentalItemDTO("Lenovo", "Ordenador de Sobremesa", 22, 3) }; //Lista de dispositivos para alquilar válida
            var rentalItems2 = new List<RentalItemDTO>() { new RentalItemDTO("HP", "Portátil", 50d, 100) }; // Lista de dispositivo con cantidad que supera la disponible

            var rentalNoName = new RentalForCreateDTO(null, "Martín", "Calle Guerrero 3, Madrid", PaymentMethodTypes.PayPal, today.AddDays(1), today.AddDays(5), rentalItems1); // 1 - Nombre nulo
            var RentalApplicationUser = new RentalForCreateDTO("ignacio.schwarzenegger@example.com", "Martín", "Calle Guerrero 3, Madrid", PaymentMethodTypes.PayPal, today.AddDays(1), today.AddDays(5), rentalItems1); // 2 - UserName no registrado
            var rentalNoSurname = new RentalForCreateDTO("Pedro", null, "Calle Guerrero 3, Madrid", PaymentMethodTypes.PayPal, today.AddDays(1), today.AddDays(5), rentalItems1); // 3 - Apellido nulo
            var rentalNoAddress = new RentalForCreateDTO("Pedro", "Martín", null, PaymentMethodTypes.PayPal, today.AddDays(1), today.AddDays(5), rentalItems1); // 4 - Dirección nula
            var rentalFechaAntesHoy = new RentalForCreateDTO("Pedro", "Martín", "Calle Guerrero 3, Madrid", PaymentMethodTypes.PayPal, today.AddDays(-1), today.AddDays(5), rentalItems1); // 5 - RentalDateFrom < today (debe fallar)

            var from = today.AddDays(5); // Empezar dentro de 5 dias
            var to = today.AddDays(2);   // Acabar Pasado mañana
            var rentalToAntesDeFrom = new RentalForCreateDTO("Pedro", "Martín", "Calle Guerrero 3, Madrid", PaymentMethodTypes.PayPal, from, to, rentalItems1); // 6 - RentalDateTo < RentalDateFrom

            var rentalNoItem = new RentalForCreateDTO("Pedro", "Martín", "Calle Guerrero 3, Madrid", PaymentMethodTypes.CreditCard, today.AddDays(1), today.AddDays(5), new List<RentalItemDTO>()); // 7 Lista vacía - Item nulo
            var rentalDeviceNoAvailable = new RentalForCreateDTO("Pedro", "Martín", "Calle Guerrero 3, Madrid", PaymentMethodTypes.PayPal, today.AddDays(1), today.AddDays(5), new List<RentalItemDTO>(rentalItems2)); // 8 - rentalItems2 tiene un dispositivo con cantidad 100 que supera la disponible 15

            var rentalDeviceNoBrand = new RentalForCreateDTO("Pedro", "Martín", "Calle Guerrero 3, Madrid", PaymentMethodTypes.PayPal, today.AddDays(1), today.AddDays(5), new List<RentalItemDTO>() { new RentalItemDTO(null, "Portátil", 50d, 2) }); // 9 - Marca nula
            var rentalDeviceNoModel = new RentalForCreateDTO("Pedro", "Martín", "Calle Guerrero 3, Madrid", PaymentMethodTypes.PayPal, today.AddDays(1), today.AddDays(5), new List<RentalItemDTO>() { new RentalItemDTO("HP", null, 50d, 2) }); // 10 - Modelo nulo

            var allTests = new List<object[]>
            {
                new object[] { rentalNoName, "El campo Nombre es obligatorio", }, // 1
                new object[] { RentalApplicationUser, "Error! UserName no registrado", }, // 2
                new object[] { rentalNoSurname, "El campo Apellido es obligatorio", }, // 3
                new object[] { rentalNoAddress, "El campo Dirección es obligatorio", }, // 4
                new object[] { rentalFechaAntesHoy, "Error! La fecha de alquiler debe comenzar más tarde de hoy", }, // 5
                new object[] { rentalToAntesDeFrom, "Error! El alquiler debe acabar más tarde de la fecha de comienzo", }, // 6
                new object[] { rentalNoItem, "Error! Se debe introducir al menos un dispositivo para alquilar", }, // 7
                new object[] { rentalDeviceNoAvailable, "Error! Campos Modelo: 'Portátil' Marca: 'HP' vacíos o se supera cantidad disponible", }, // 8
                new object[] { rentalDeviceNoBrand, "Error! Campos Modelo: 'Portátil' Marca: '' vacíos o se supera cantidad disponible", }, // 9
                new object[] { rentalDeviceNoModel, "Error! Campos Modelo: '' Marca: 'HP' vacíos o se supera cantidad disponible", }, // 10
            };

            return allTests;
        }

        [Theory]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        [MemberData(nameof(TestCasesFor_CreateRental))]
        // Test to create a rental with invalid data
        public async Task CreateRental_Error_test(RentalForCreateDTO rentalForCreate, string errorExpected)
        {
            // Arrange
            var mock = new Mock<ILogger<RentalsController>>();
            ILogger<RentalsController> logger = mock.Object;
            var controller = new RentalsController(_context, logger);
            // Act
            var result = await controller.CreateRental(rentalForCreate);
            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var problemDetails = Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);
            var errorActual = problemDetails.Errors.First().Value[0];

            Assert.StartsWith(errorExpected, errorActual);
        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task CreateRental_Success_test()
        {
            // Arrange
            var mock = new Mock<ILogger<RentalsController>>();
            ILogger<RentalsController> logger = mock.Object;

            var controller = new RentalsController(_context, logger);

            //we use always relative dates to avoid errors if the test is run some years later

            DateTime today = DateTime.Today;
            DateTime from = today.AddDays(2);
            DateTime to = today.AddDays(5);

            RentalForCreateDTO rentalDTO = new RentalForCreateDTO("Pedro", "Martín", "Calle Guerrero 3, Madrid", PaymentMethodTypes.PayPal, from, to, new List<RentalItemDTO>() { new RentalItemDTO("Dell", "Portátil", 45d, 2) });

            //Calculo del total price:
            var numDays = (to - from).Days;
            double totalPrice = 45d * 2 * numDays;

            //the id is 2 because there is another rental in the database
            RentalDetailDTO expectedrentalDetailDTO = new RentalDetailDTO("Pedro", "Martín", "Calle Guerrero 3, Madrid", today, totalPrice, from, to, new List<RentalItemDTO>());
            expectedrentalDetailDTO.RentedDevices.Add(new RentalItemDTO("Dell", "Portátil", 45d, 2));

            // Act
            var result = await controller.CreateRental(rentalDTO);

            //Assert
            //we check that the response type is BadRequest and obtain the error returned
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var actualRentalDetailDTO = Assert.IsType<RentalDetailDTO>(createdResult.Value);

            Assert.Equal(expectedrentalDetailDTO, actualRentalDetailDTO);
        }
    }
}