using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.ReceiptDTOs;
using AppForSEII2526.API.Models;

namespace AppForSEII2526.UT.ReceiptsController_test
{
    public class GetReceipts_test : AppForSEII25264SqliteUT
    {
        public GetReceipts_test()
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

        [Fact]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetReceipt_NotFound_test()
        {
            // Arrange
            var mock = new Mock<ILogger<ReceiptsController>>();
            ILogger<ReceiptsController> logger = mock.Object;

            var controller = new ReceiptsController(_context, logger);

            //Act
            var result = await controller.GetReceipt(0); //ID que no existe

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetReceipt_Ok_test()
        {
            // Arrange
            var mock = new Mock<ILogger<ReceiptsController>>();
            ILogger<ReceiptsController> logger = mock.Object;
            var controller = new ReceiptsController(_context, logger);

            var expectedReceipt = new ReceiptDetailDTO(1,
                "Test",
                "User",
                "123 Test St",
                DateTime.SpecifyKind(DateTime.Today, DateTimeKind.Unspecified),
                50.0m,
                new List<ReceiptItemDTO>()
                {
                    new ReceiptItemDTO(
                        "Pantalla",
                        "Lujo",
                        50.0m,
                        "iPhone X"
                    )
                }
            );

            //Act
            var result = await controller.GetReceipt(1); //ID que existe

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var receiptDTO = Assert.IsType<ReceiptDetailDTO>(okResult.Value);
            var eq = expectedReceipt.Equals(receiptDTO);

            Assert.Equal(expectedReceipt, receiptDTO);
        }
    }
}