using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.RepairDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Microsoft.Extensions.Logging;

namespace AppForSEII2526.UT.RepairsController_test
{
    public class GetRepairs_test : AppForSEII25264SqliteUT
    {
        public GetRepairs_test()
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

            //receipt.TotalPrice = receipt.ReceiptItems.Sum(ri => ri.Repair.Cost); -> A mí no me hace falta porque lo calcula el constructor
            //purchase.TotalPrice = purchase.PurshaseItems.Sum(pi => pi.Device.priceForPurchase); -> Igual para totalQuantity 

            //Ahora se añaden los datos a la base de datos de prueba
            _context.Add(user);
            _context.AddRange(scales);
            _context.AddRange(repairs);
            _context.Add(receipt);

            _context.SaveChanges();
        }

        public static IEnumerable<object[]> TestCasesFor_GetRepairs_OK()
        {
            var repairDTOs = new List<RepairDTO>()
            {
                new RepairDTO( 1,"Cambio de Pantalla", 50.0m, "Pantalla", "Lujo"),
                new RepairDTO( 2,"Cambio de Batería", 20.0m, "Batería", "Media"),
                new RepairDTO( 3,"Migración de datos", 15.0m, "Migración de datos", "Básica")
            };


            var repairDTOsTC1 = new List<RepairDTO>() { repairDTOs[0], repairDTOs[1], repairDTOs[2] };

            var repairDTOsTC2 = new List<RepairDTO>() { repairDTOs[1] };

            var repairDTOsTC3 = new List<RepairDTO>() { repairDTOs[2] };



            var allTests = new List<object[]>
            {

                new object[] {null, null, repairDTOsTC1 },
                new object[] {"Batería", null, repairDTOsTC2 },
                new object[] {null, "Básica", repairDTOsTC3 }
            };


            return allTests;
        }

        [Theory] //prueba parametrizada
        [MemberData(nameof(TestCasesFor_GetRepairs_OK))]//le decimos que es la que va ir a ir bien
        [Trait("Database", "WithoutFixture")]//siempre
        [Trait("LevelTesting", "Unit Testing")]//siempre
        public async Task GetRepairs_OK(string? nameFilter, string? scaleFilter, List<RepairDTO> expectedRepairs)
        {
            //Arrange:creamos el controlador con el contexto de prueba
            var controller = new RepairsController(_context, null); //sacado del proyeco de prueba

            //Act : llamamos al metodo que queremos probar con los filtros pasados como parámetros
            var result = await controller.GetRepairs(nameFilter, scaleFilter);

            //Assert: nos aseguramo de que el tipo de la respuesta es OK
            var okResult = Assert.IsType<OkObjectResult>(result);
            //y obtenemos la lista de las reparaciones devueltas - se usa el equal de la clase RepairDTO
            var returnRepairs = Assert.IsType<List<RepairDTO>>(okResult.Value);
            Assert.Equal(expectedRepairs, returnRepairs);
        }


    }
}