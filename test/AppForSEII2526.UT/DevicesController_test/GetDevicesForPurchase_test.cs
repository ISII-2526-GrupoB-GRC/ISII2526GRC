using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.DeviceDTOs;
using Microsoft.CodeAnalysis.Operations;
using RabbitMQ.Client;
namespace AppForSEII2526.UT.DevicesController_test
{
    public class GetDevicesForPurchase_test : AppForSEII25264SqliteUT
    {

        //se definen los datos de prueba que seran reutizados entre varias pruebas (ok, bad request)
        public GetDevicesForPurchase_test()
        {

            ApplicationUser user = new ApplicationUser
            {
                UserName = "testUser",
                Name = "Test",
                Surname = "User",



            };
            var models = new List<Model> {

                new Model("modelo1")
                , new Model("modelo2")
                , new Model("modelo3")


            };

            var devices = new List<Device>
            {
                new Device("marca1","azul","nombre1",12,12,2025,models[0]),
                new Device("marca2","azul","nombre2",12,12,2025,models[1]),
                new Device("marca3","negro","nombre3",12,12,2025,models[2])



            };
            var DeliveryAddress = "123 Test st";

            var purchase = new Purchase()
            {
                ApplicationUser = user,

                DeliveryAddress = DeliveryAddress,
                PaymentMethod = PaymentMethodTypes.CreditCard,
                PurchaseDate = DateTime.Now,
                TotalPrice = 0,
                TotalQuanty = 0,
                PurchaseItems = new List<PurchaseItem>()
                {
                    new PurchaseItem()
                    {

                        Device = devices[0]
                    },

                }

            };


            purchase.TotalPrice = purchase.PurchaseItems.Sum(pi => pi.Device.priceForPurchace * pi.Quantity);
            purchase.TotalQuanty = purchase.PurchaseItems.Sum(pi => pi.Quantity);


            _context.Add(user);
            _context.AddRange(models);
            _context.AddRange(devices);
            _context.Add(purchase);
            _context.SaveChanges();

        }



        public static IEnumerable<Object[]> TestCasesFor_GetDevicesForPurchase_OK()
        {



            var purchaseDTOs = new List<DeviceForPurchaseDTO>()
            {
                new DeviceForPurchaseDTO("nombre1","marca1","modelo1","azul",12),
                new DeviceForPurchaseDTO("nombre2","marca2","modelo2","azul",12),
                new DeviceForPurchaseDTO("nombre3","marca3","modelo3","negro",12)

            };

            var purchaseDTOsTC1 = new List<DeviceForPurchaseDTO>() { purchaseDTOs[0], purchaseDTOs[1], purchaseDTOs[2] };
            var purchaseDTOsTC2 = new List<DeviceForPurchaseDTO>() { purchaseDTOs[0] };
            var purchaseDTOsTC3 = new List<DeviceForPurchaseDTO>() { purchaseDTOs[2] };


            var alltest = new List<object[]>
            {
                new object[] { null,null,purchaseDTOsTC1},
                new object[] { "marca1",null,purchaseDTOsTC2},
                new object[] { null,"negro",purchaseDTOsTC3}
            };
            return alltest;



        }
        [Theory] //prueba parametrizada
        [MemberData(nameof(TestCasesFor_GetDevicesForPurchase_OK))] //le decimos que use los casos de prueba definidos en el metodo
        [Trait("Database", "WithoutFixture")]//siempre
        [Trait("LevelTestin", "Unit testing")] //siempre
        public async Task GetDevicesForPuchase_OK(string? brand, string? colour, List<DeviceForPurchaseDTO> expectedDevicesForPurchase)
        {
            var controller = new DevicesController(_context, null);

            var result = await controller.GetDevicesForPurchase(brand, colour);

            var okResult = Assert.IsType<OkObjectResult>(result);

            var returnDevicesForPurchase = Assert.IsAssignableFrom<IList<DeviceForPurchaseDTO>>(okResult.Value);
            Assert.Equal(expectedDevicesForPurchase, returnDevicesForPurchase);



        }


    }
}
