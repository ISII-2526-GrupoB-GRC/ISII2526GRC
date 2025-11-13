using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.ReceiptDTOs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.ReceiptsController_test
{
    public class PostReceipts_test : AppForSEII25264SqliteUT
    {
        public PostReceipts_test()
        {
            //Se definen los datos de prueba que seran reutilizados entre varias pruebas (ok, badrequest)
            ApplicationUser user = new ApplicationUser()
            {
                UserName = "testuser",
                Name = "Test",
                Surname = "User",
                
            };
            var DeliveryAddress = "123 Test St";
            var scales = new List<Scale>() //Model
            {
                new Scale() { Name = "Lujo" },
                new Scale() { Name = "Media" },
                new Scale() { Name = "Básica" }
            };
            var repairs = new List<Repair>() //Devices
            {
                new Repair() { Name = "Pantalla", Cost = 50.0m, Scale = scales[0], Description = "Cambio de Pantalla" },
                new Repair() { Name = "Batería", Cost = 20.0m, Scale = scales[1], Description = "Cambio de Batería" },
                new Repair() { Name = "Migración de datos", Cost = 15.0m, Scale = scales[2], Description = "Migración de datos" }
            };
            var receipt = new Receipt() //Purchase
            {
                ApplicationUser = user,
                ReceiptDate = DateTime.SpecifyKind(DateTime.Today, DateTimeKind.Unspecified),
                PaymentMethod = PaymentMethodTypes.PayPal,
                deliveryAddres = DeliveryAddress,
                ReceiptItems = new List<ReceiptItem>()
                {
                    new ReceiptItem() //PurchaseItem
                    {
                        Model = "iPhone X",
                        Repair = repairs[0]
                    }
                }
            };
            receipt.TotalPrice = receipt.ReceiptItems.Sum(ri => ri.Repair.Cost);
            _context.Add(user);
            _context.AddRange(scales);
            _context.AddRange(repairs);
            _context.Add(receipt);
            _context.SaveChanges();
        }

        public static IEnumerable<object[]> TestCasesFor_CreatePurchase()
        {
            //        public ReceiptForCreateDTO(string username, string usersurname, string userdeliveryaddress, PaymentMethodTypes paymentMethod, IList<ReceiptItemDTO> receiptItems)

            var receiptNoItem = new ReceiptForCreateDTO("testuser", "Test User", "C/ test", PaymentMethodTypes.CreditCard, new List<ReceiptItemDTO>());

            var rentalItems = new List<ReceiptItemDTO>
            {
                new ReceiptItemDTO("Pantalla", "Lujo", 50.0m, "iPhone X")
            };

            var receiptInvalidUser = new ReceiptForCreateDTO("nonexistentuser", "Test User", "C/ test", PaymentMethodTypes.CreditCard, rentalItems);
            var RepairNotFound = new ReceiptForCreateDTO("testuser", "Test User", "C/ test", PaymentMethodTypes.CreditCard,
                new List<ReceiptItemDTO>
                {
                    new ReceiptItemDTO("Reparación Inexistente", "Lujo", 100.0m, "iPhone X")
                });


            var allTest = new List<object[]>
            {
                new object[] { receiptNoItem, "At least one receipt item is required." , },
                new object[] { receiptInvalidUser, "The specified user does not exist." , },
                new object[] { RepairNotFound, "The repair 'Reparación Inexistente' does not exist.", },
            };
            return allTest;
        }

        [Theory]
        [MemberData(nameof(TestCasesFor_CreatePurchase))]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]

        public async Task CreateReceipt_Error_Test(ReceiptForCreateDTO receiptDTO, string errorExpected)
        {
            //Arrange
            var mock = new Mock<ILogger<ReceiptsController>>();
            ILogger<ReceiptsController> logger = mock.Object;

            var controller = new ReceiptsController(_context, logger);

            //Act
            var result = await controller.CreateReceipt(receiptDTO);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var validationProblem = Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);

            var erroractual = validationProblem.Errors.First().Value[0];

            Assert.StartsWith(errorExpected, erroractual);
        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task CreateReceipt_OK_Test()
        {
            //Arrange
            var mock = new Mock<ILogger<ReceiptsController>>();
            ILogger<ReceiptsController> logger = mock.Object;
            var controller = new ReceiptsController(_context, logger);

            var rentalItems = new List<ReceiptItemDTO>
            {
                new ReceiptItemDTO("Pantalla", "Lujo", 50.0m, "iPhone X"),
                new ReceiptItemDTO("Batería", "Media", 20.0m, "Samsung S10")
            };
            var receiptDTO = new ReceiptForCreateDTO("testuser", "Test User", "C/ test", PaymentMethodTypes.CreditCard, rentalItems);
            //Act
            var result = await controller.CreateReceipt(receiptDTO);
            //Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var receiptDetail = Assert.IsType<ReceiptDetailDTO>(createdAtActionResult.Value);
            Assert.Equal("Test", receiptDetail.username);
            Assert.Equal(2, receiptDetail.receiptitems.Count);
            Assert.Equal(70.0m, receiptDetail.totalPrice);
        }
    }
}