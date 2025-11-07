using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.DevicesController_test
{
    using AppForSEII2526.API.Controllers;
    using AppForSEII2526.API.DTOs.DeviceDTOs;
    using AppForSEII2526.API.Models;

    public class GetDevices_ForRental_test : AppForSEII25264SqliteUT
    {
        public GetDevices_ForRental_test()
        {
            var models = new List<Model>()
            {
                new Model("Portátil"),
                new Model("Ordenador de Sobremesa"),
                new Model("Tablet"),
                new Model("Monitor")
            };

            var devices = new List<Device>()
            {
                new Device("Dell", models[0],"Gris", 0, "Dell XS 13", 1200.99, 50, Device.QualityType.Medium, 8, 12, 2023),
                new Device("HP", models[0], "Plata", 1, "HP Spectre x360", 1350.00, 50, Device.QualityType.High, 10, 15, 2023),
                new Device("Apple", models[0], "Gris Espacial", 2, "MacBook Air M2", 1499.99, 45, Device.QualityType.High, 5, 8, 2023),
                new Device("Lenovo", models[1], "Negro", 3, "Lenovo ThinkCentre M90", 899.50, 22, Device.QualityType.Medium, 12, 10, 2023),
                new Device("Samsung", models[2], "Blanco", 4, "Samsung Galaxy Tab S9", 750.00, 35, Device.QualityType.High, 15, 20, 2023)
            };

            ApplicationUser user1 = new ApplicationUser("Ana", "González", "Calle Serrano 45, Madrid");

            var rental = new Rental("Ana", "Gonzalez", "Calle Serrano 45, Madrid", DateTime.Now, PaymentMethodTypes.CreditCard,
                DateTime.Today.AddDays(2), DateTime.Today.AddDays(5), new List<RentDevice>(), user1);

            rental.RentDevices.Add(new RentDevice(devices[0], rental));

            _context.Add(user1);
            _context.AddRange(models);
            _context.AddRange(devices);
            _context.Add(rental);
            _context.SaveChanges();
        }

        public static IEnumerable<object[]> GetDevices_ForRental_TestData()
        {
            var deviceDTOs = new List<DeviceForRentalDTO>()
            {
                new DeviceForRentalDTO("Dell XPS 13", "Portátil", "Dell", 2023, "Gris", 45),
                new DeviceForRentalDTO("HP Spectre x360", "Portátil", "HP", 2023, "Plata", 50),
                new DeviceForRentalDTO("MacBook Air M2", "Portátil", "Apple", 2023, "Gris Espacial", 45),
                new DeviceForRentalDTO("Lenovo ThinkCentre M90", "Ordenador de Sobremesa", "Lenovo", 2023, "Negro", 22),
            };

            var deviceDTOsTC1 = new List<DeviceForRentalDTO>() { deviceDTOs[0], deviceDTOs[1], deviceDTOs[2], deviceDTOs[3] }
                .OrderBy(d => d.Model).ToList(); //El método GetDevicesForRental devuelve los dispositivos ordenados por modelo

            var deviceDTOsTC2 = new List<DeviceForRentalDTO>() { deviceDTOs[1] };

            var deviceDTOsTC3 = new List<DeviceForRentalDTO>() { deviceDTOs[0], deviceDTOs[2] };

            var allTests = new List<object[]>
            {
                // filters to apply
                // modelo, precio de alquiler
                new object[] { null, null, deviceDTOsTC1 }, // Devuelve todo
                new object[] { "HP", null, deviceDTOsTC2 }, // Devuelve sólo el dispositivo HP
                new object[] { null, 45,  deviceDTOsTC3 }   // Devuelve sólo dispositivos con precio 45
            };
                
            return allTests;

        }

        [Theory]
        [MemberData(nameof(GetDevices_ForRental_TestData))]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]

        public async Task GetDevices_ForRental_testData(string? filterNameModel, double? filterPrice,
            IList<DeviceForRentalDTO> expectedDevices)
        {
            // Arrange
            var controller = new DevicesController(_context, null);

            // Act
            var result = await controller.GetDevicesForRental(filterNameModel, filterPrice);

            //Assert
            //we check that the response type is OK 
            var okResult = Assert.IsType<OkObjectResult>(result);
            //and obtain the list of movies
            var deviceDTOsActual = Assert.IsType<List<DeviceForRentalDTO>>(okResult.Value);
            Assert.Equal(expectedDevices, deviceDTOsActual);

        }

        [Fact] // PRUEBA UNITARIA 1
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task GetMoviesForRental_showAll_test() //Debe devolver si no se pasan parámetros todos los dispositivos disponibles para alquilar
        {
            // Arrange
            var mock = new Mock<ILogger<DevicesController>>();
            ILogger<DevicesController> logger = mock.Object;
            var controller = new DevicesController(_context, logger);
            // Act
            var result = await controller.GetDevicesForRental(null, null);
            //Assert
            //we check that the response type is OK 
            var okResult = Assert.IsType<OkObjectResult>(result);
            //and obtain the list of devices
            var deviceDTOsActual = Assert.IsType<List<DeviceForRentalDTO>>(okResult.Value);
            Assert.Equal(4, deviceDTOsActual.Count); //Hay 4 dispositivos disponibles para alquiler
        
        }
    }    
}
