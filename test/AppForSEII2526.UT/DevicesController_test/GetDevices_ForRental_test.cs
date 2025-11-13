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
    using System.Security.Cryptography.Xml;
    using static AppForSEII2526.API.Models.Device;

    public class GetDevices_ForRental_test : AppForSEII25264SqliteUT
    {
        public GetDevices_ForRental_test()
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
                new Device("HP", models[0], "Plata", "HP Spectre x360", 1350.00, 50d, QualityType.High, 10, 15, 2023),
                new Device("Samsung", models[2], "Blanco", "Samsung Galaxy tab 8", 521.99, 30d, QualityType.High, 5, 8, 2022),
                new Device("Lenovo", models[1], "Negro", "Lenovo ThinkCentre M90", 899.50, 22d, QualityType.Medium, 12, 10, 2023),
            };

            ApplicationUser user1 = new ApplicationUser()
            {
                UserName = "Ana",
                Name = "Ana",
                Surname = "González",
            };

            var rental = new Rental()
            {
                ApplicationUser = user1,
                DeliveryAddress = "Calle Serrano 45, Madrid",
                RentalDate = DateTime.Now,
                PaymentMethod = PaymentMethodTypes.CreditCard,
                RentalDateFrom = DateTime.Today.AddDays(2),
                RentalDateTo = DateTime.Today.AddDays(5),
                RentDevices = new List<RentDevice>()
            };

            rental.RentDevices.Add(new RentDevice(1, devices[0], rental));

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
                new DeviceForRentalDTO("Dell XPS 13", "Portátil", "Dell", 2023, "Gris", 45d),
                new DeviceForRentalDTO("HP Spectre x360", "Portátil", "HP", 2023, "Plata", 50d),
                new DeviceForRentalDTO("Samsung Galaxy tab 8", "Tablet", "Samsung", 2022, "Blanco", 30d),
                new DeviceForRentalDTO("Lenovo ThinkCentre M90", "Ordenador de Sobremesa", "Lenovo", 2023, "Negro", 22d),
            };

            var deviceDTOsTC1 = new List<DeviceForRentalDTO>() { deviceDTOs[0], deviceDTOs[1], deviceDTOs[2], deviceDTOs[3] }; // Se devuelven todos los dispositivos

            var deviceDTOsTC2 = new List<DeviceForRentalDTO>() { deviceDTOs[0], deviceDTOs[1] }; // Se devuelve sólo los modelo: Portátil

            var deviceDTOsTC3 = new List<DeviceForRentalDTO>() { deviceDTOs[0] }; // Se devuelven los dispositivos con precio 45.00

            var deviceDTOsTC4 = new List<DeviceForRentalDTO>() { deviceDTOs[1] }; // se devuelven modelo: Portátil y precio 50

            var allTests = new List<object[]>
            {
                // filters to apply
                // modelo, precio de alquiler
                new object[] { null, null, deviceDTOsTC1, }, // Devuelve todo
                new object[] { "Portátil", null, deviceDTOsTC2, }, // Devuelve sólo los dispositivos portátiles
                new object[] { null, 45d,  deviceDTOsTC3, },   // Devuelve sólo dispositivos con precio 50.00
                new object[] { "Portátil", 50d, deviceDTOsTC4, } // Devuelve los portátiles que valgan 50.00
            };

            return allTests;

        }

        [Theory]
        [Trait("LevelTesting", "Unit Testing")]
        [MemberData(nameof(GetDevices_ForRental_TestData))]

        // Paso todos los tests a la task:
        public async Task GetDevices_ForRental_testData(string? filterNameModel, double? filterPrice,
            List<DeviceForRentalDTO> expectedDevices)
        {
            // Arrange
            var mock = new Mock<ILogger<DevicesController>>();
            ILogger<DevicesController> logger = mock.Object;
            var controller = new DevicesController(_context, logger);
            // Act
            var result = await controller.GetDevicesForRental(filterNameModel, filterPrice);
            //Assert we check that the response type is OK 
            var okResult = Assert.IsType<OkObjectResult>(result);
            //and obtain the list of movies
            var deviceDTOsActual = Assert.IsType<List<DeviceForRentalDTO>>(okResult.Value);
            Assert.Equal(expectedDevices, deviceDTOsActual);
        }
    }
}