using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.PurchaseDTOs;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.PurchasesController_test
{

    public class PostPurchases_test : AppForSEII25264SqliteUT
    {


        public PostPurchases_test()
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

        public static IEnumerable<Object[]> TestCasesFor_CreatePurchase()
        {
            var purchaseNoItems = new PurchaseForCreateDTO(new List<PurchaseItemDTO>(), "Test", "User", "123 Test st", PaymentMethodTypes.CreditCard);

            var purchaseItems = new List<PurchaseItemDTO>
            {
                new PurchaseItemDTO(   "marca1",
                     "modelo1",
                     "azul",
                    100.0,
                     2,
                     "Test description")


            };
            //var purchaseNoPaymentMethod = new PurchaseForCreateDTO(purchaseItems, "Test", "User", "123 Test st", null); // no se como evaluar el null debido a que es un enum
            var purchaseItemsExamenModeloXiaomi = new List<PurchaseItemDTO>
            {
                new PurchaseItemDTO(   "marca1",
                     "Xiaomi",
                     "azul",
                    100.0,
                     2,
                     "Test description")


            };


            var purchaseItemsExamenModeloHuawei = new List<PurchaseItemDTO>
            {
                new PurchaseItemDTO(   "marca1",
                     "Huawei",
                     "azul",
                    100.0,
                     2,
                     "Test description")


            };


            var purchaseItemsExamenMarcaXiaomi = new List<PurchaseItemDTO>
            {
                new PurchaseItemDTO(   "Xiaomi",
                     "modelo1",
                     "azul",
                    100.0,
                     2,
                     "Test description")


            };


            var purchaseItemsExamenMarcaHuawei = new List<PurchaseItemDTO>
            {
                new PurchaseItemDTO(   "Huawei",
                     "modelo1",
                     "azul",
                    100.0,
                     2,
                     "Test description")


            };



            var purchaseNoUserName = new PurchaseForCreateDTO(purchaseItems, "", "User", "123 Test st", PaymentMethodTypes.CreditCard);
            var purchaseNoDeliveryAddress = new PurchaseForCreateDTO(purchaseItems, "Test", "User", "", PaymentMethodTypes.CreditCard);
            var purchaseBadNameModelXiaomi = new PurchaseForCreateDTO(purchaseItemsExamenModeloXiaomi, "Test", "User", "123 Test st", PaymentMethodTypes.CreditCard);
            var purchaseBadNameMarcaXiaomi = new PurchaseForCreateDTO(purchaseItemsExamenMarcaXiaomi, "Test", "User", "123 Test st", PaymentMethodTypes.CreditCard);
            var purchaseBadNameModelHuawei = new PurchaseForCreateDTO(purchaseItemsExamenModeloHuawei, "Test", "User", "123 Test st", PaymentMethodTypes.CreditCard);

            var purchaseBadNameMarcaHuawei = new PurchaseForCreateDTO(purchaseItemsExamenMarcaHuawei, "Test", "User", "123 Test st", PaymentMethodTypes.CreditCard);



            var allTest = new List<Object[]>
            {
                new object[] {purchaseNoItems, "ERROR! You must buy at least one item" },
                new object[] {purchaseNoUserName, "Error!. UserName is not registered" },
                new object[]{purchaseNoDeliveryAddress, "ERROR! You must put a delivery addres" },
                new object[]{ purchaseBadNameModelXiaomi, "ERROR: las tecnologias de estas marcas ya no estan disponibles, siguiendo recomendaciones de las autoridades competentes en materia de seguridad" },
                new object[]{ purchaseBadNameMarcaXiaomi, "ERROR: las tecnologias de estas marcas ya no estan disponibles, siguiendo recomendaciones de las autoridades competentes en materia de seguridad" },
                new object[]{ purchaseBadNameModelHuawei, "ERROR: las tecnologias de estas marcas ya no estan disponibles, siguiendo recomendaciones de las autoridades competentes en materia de seguridad" },

                new object[]{ purchaseBadNameMarcaHuawei, "ERROR: las tecnologias de estas marcas ya no estan disponibles, siguiendo recomendaciones de las autoridades competentes en materia de seguridad" }



            };
            return allTest;


        }

        [Theory]
        [MemberData(nameof(TestCasesFor_CreatePurchase))]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task CreatePurchase_Error_test(PurchaseForCreateDTO purchaseDTO, string errorExected)
        {

            var mock = new Mock<ILogger<PurchaseController>>();
            ILogger<PurchaseController> logger = mock.Object;

            var controller = new PurchaseController(_context, logger);

            var result = await controller.CreatePurchase(purchaseDTO);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var validationProblem = Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);

            var errorActual = validationProblem.Errors.First().Value[0];

            Assert.StartsWith(errorExected, errorActual);





        }


        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task CreatePurchase_OK_Test()
        {
            var mock = new Mock<ILogger<PurchaseController>>();
            ILogger<PurchaseController> logger = mock.Object;
            var controller = new PurchaseController(_context, logger);

            var purchaseItems = new List<PurchaseItemDTO>
            {
                new PurchaseItemDTO(   "marca1",
                     "modelo1",
                     "azul",
                    12,
                     1,
                     "Test description")

            };
            var purchaseDTO = new PurchaseForCreateDTO(purchaseItems, "testUser", "Test User", "123 Test st", PaymentMethodTypes.CreditCard);

            var result = await controller.CreatePurchase(purchaseDTO);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var purchaseDetailDTOActual = Assert.IsType<PurchaseDetailDTO>(createdAtActionResult.Value);

            Assert.Equal("Test", purchaseDetailDTOActual.name);


            Assert.Equal("User", purchaseDetailDTOActual.surname);
            Assert.Equal(purchaseDTO.deliveryAddress, purchaseDetailDTOActual.deliveryAddress);
            Assert.Equal(purchaseDTO.purchaseItems, purchaseDetailDTOActual.purchaseItems);
            Assert.Equal(
                purchaseDTO.purchaseItems.Sum(i => i.price * i.quantity),
                purchaseDetailDTOActual.totalPrice);
            Assert.Equal(
                purchaseDTO.purchaseItems.Sum(i => i.quantity),
                purchaseDetailDTOActual.totalQuantity);
        }



    }
}
