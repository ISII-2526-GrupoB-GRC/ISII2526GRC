using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.DevicesController_test
{
    using AppForSEII2526.API.DTOs.RentDTOs;
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
                new Device(models[0], "Dell", 2023, "Gris", 45),
                new Device(models[0], "HP", 2023, "Plata", 50),
                new Device(models[0], "Apple", 2023, "Gris Espacial", 48),
                new Device(models[1], "Lenovo", 2023, "Negro", 22),
                //Con error en precio, debería ser 10, pero es 0:
                new Device(models[2], "Samsung", 2023, "Negro", 0)
            };

            ApplicationUser user1 = new ApplicationUser("Ana", "González", "Calle Serrano 45, Madrid");

            var rental = new Rental("Ana", "Gonzalez", "Calle Serrano 45, Madrid", DateTime.Now, PaymentMethodTypes.CreditCard, DateTime.Today.AddDays(2), DateTime.Today.AddDays(5), new List<RentDevice>(), user1);

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
                new DeviceForRentalDTO("MacBook Air M2", "Portátil", "Apple", 2023, "Gris Espacial", 48),
                new DeviceForRentalDTO("Lenovo ThinkCentre M90", "Ordenador de Sobremesa", "Lenovo", 2023, "Negro", 22),
            };

            var deviceDTOsTC1 = new List<DeviceForRentalDTO>() { deviceDTOs[1], deviceDTOs[2] }
                    //El método GetDevicesForRental devuelve los dispositivos ordenados por modelo
                    .OrderBy(d => d.Model).ToList();

            var deviceDTOsTC2 = new List<DeviceForRentalDTO>() { deviceDTOs[1] };
            var deviceDTOsTC3 = new List<DeviceForRentalDTO>() { deviceDTOs[2] };

            var deviceDTOsTC4 = new List<DeviceForRentalDTO>() { deviceDTOs[0], deviceDTOs[1], deviceDTOs[2] }
                //the GetMoviesForPurchase method returns the movies ordered by title
                .OrderBy(d => d.Model).ToList();

            var allTests = new List<object[]>
            {
                //filters to apply
                // Nombre(dispositivo), modelo, marca, año, color, precio(alquiler)
                new object[] { null, null, null, null, null, deviceDTOsTC1,  },
                new object[] { null, null, null, null, null, deviceDTOsTC2, },
                new object[] { null, null, "Drama", null, null, deviceDTOsTC3, },
                new object[] { null, null, null, DateTime.Today.AddDays(6), DateTime.Today.AddDays(8), deviceDTOsTC4 },
            };









        }
    }    
}
