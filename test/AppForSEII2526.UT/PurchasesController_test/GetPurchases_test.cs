using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.PurchaseDTOs;
using AppForSEII2526.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace AppForSEII2526.UT.PurchasesController_test
{
    public class GetPurchases_test : AppForSEII25264SqliteUT
    {
        public GetPurchases_test()
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = "testUser",
                Name = "Test",
                Surname = "User",



            };

            var models = new List<Model>
            {
                new Model("modelo1"),
                new Model("modelo2"),
                new Model("modelo3")

            };

            var devices = new List<Device>
            {


                new Device("marca1","azul","nombre1",12,12,2025,models[0]),
                new Device("marca2","azul","nombre2",12,12,2025,models[1]),
                new Device("marca3","azul","nombre3",12,12,2025,models[2])
            };

            var DeliveryAddress = "123 Test st";
            var purchase = new Purchase()
            {
                Id = 1,
                ApplicationUser = user,

                DeliveryAddress = DeliveryAddress,
                PaymentMethod = PaymentMethodTypes.CreditCard,
                PurchaseDate = DateTime.Today,
                TotalPrice = 0,
                TotalQuanty = 0,
                PurchaseItems = new List<PurchaseItem>()
                {
                    new PurchaseItem(1,devices[0])


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



        [Fact]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetPurchase_NotFound_Test()
        {
            var mock = new Mock<ILogger<PurchaseController>>();
            ILogger<PurchaseController> logger = mock.Object;


            var controller = new PurchaseController(_context, logger);


            var result = await controller.Get_Purchase_Detail_DTO(0);

            Assert.IsType<NotFoundResult>(result);
        }


        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task GetPurchase_Found_Test()
        {
            var mock = new Mock<ILogger<PurchaseController>>();
            ILogger<PurchaseController> logger = mock.Object;
            var controller = new PurchaseController(_context, logger);


            var expectedPurchase = new PurchaseDetailDTO(1, "Test", "User", "123 Test st", DateTime.Today, 12, 1,

                new List<PurchaseItemDTO>());
            expectedPurchase.purchaseItems.Add(new PurchaseItemDTO("marca1", "modelo1", "azul", 12, 1, ""));

            var result = await controller.Get_Purchase_Detail_DTO(1);


            var okResult = Assert.IsType<OkObjectResult>(result);
            var purchaseDTOActual = Assert.IsType<PurchaseDetailDTO>(okResult.Value);
            var eq = expectedPurchase.Equals(purchaseDTOActual);

            //we check that the expected and actual are the same

            Assert.Equal(expectedPurchase, purchaseDTOActual);



        }


    }
}
