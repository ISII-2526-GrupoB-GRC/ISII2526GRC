using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AppForSEII2526.UT.DevicesController_test
{
    using AppForSEII2526.API.DTOs.RentDTOs;

    public class GetDevices_ForRental_test : AppForSEII25264SqliteUT
    {
        public GetDevices_ForRental_test()
        {
            // Plan (pseudocódigo):
            // - Crear el contexto con CreateContext() y asegurar la BD.
            // - Si ya hay dispositivos, no resembrar.
            // - Sembrar datos mínimos:
            //   * 1 Modelo.
            //   * 3 Dispositivos del modelo (2 disponibles, 1 alquilado).
            //   * 1 Rental activo (hoy a hoy+3).
            //   * 1 RentDevice que vincule el Rental con el dispositivo alquilado.
            // - Guardar cambios.
            // - Uso de helpers SetAny/SetIfExists para asignar propiedades con nombres variables.

            using var ctx = CreateContext();
            ctx.Database.EnsureCreated();

            if (ctx.Device.Any())
                return;

            var today = DateTime.UtcNow.Date;

            // Modelo
            var model = Activator.CreateInstance(typeof(Model))!;
            SetAny(model, new[] { "Name", "Nombre" }, "Modelo Pruebas");
            SetAny(model, new[] { "Description", "Descripcion" }, "Modelo base para pruebas de alquiler");
            ctx.Modelo.Add((Model)model);
            ctx.SaveChanges();
            var modelId = GetPrimaryKeyValue<int>(ctx, model);

            // Dispositivo disponible 1
            var device1 = Activator.CreateInstance(typeof(Device))!;
            SetAny(device1, new[] { "ModelId", "ModeloId" }, modelId);
            SetAny(device1, new[] { "SerialNumber", "NumeroSerie" }, "SN-ALQ-001");
            SetAny(device1, new[] { "IsAvailable", "Disponible" }, true);
            SetAny(device1, new[] { "Status", "Estado" }, "Disponible");
            ctx.Device.Add((Device)device1);

            // Dispositivo disponible 2
            var device2 = Activator.CreateInstance(typeof(Device))!;
            SetAny(device2, new[] { "ModelId", "ModeloId" }, modelId);
            SetAny(device2, new[] { "SerialNumber", "NumeroSerie" }, "SN-ALQ-002");
            SetAny(device2, new[] { "IsAvailable", "Disponible" }, true);
            SetAny(device2, new[] { "Status", "Estado" }, "Disponible");
            ctx.Device.Add((Device)device2);

            // Dispositivo que estará alquilado
            var device3 = Activator.CreateInstance(typeof(Device))!;
            SetAny(device3, new[] { "ModelId", "ModeloId" }, modelId);
            SetAny(device3, new[] { "SerialNumber", "NumeroSerie" }, "SN-ALQ-003");
            SetAny(device3, new[] { "IsAvailable", "Disponible" }, false);
            SetAny(device3, new[] { "Status", "Estado" }, "Alquilado");
            ctx.Device.Add((Device)device3);

            ctx.SaveChanges();

            var device3Id = GetPrimaryKeyValue<int>(ctx, device3);

            // Rental activo (hoy -> hoy + 3 días)
            var rental = Activator.CreateInstance(typeof(Rental))!;
            SetAny(rental, new[] { "StartDate", "FechaInicio" }, today);
            SetAny(rental, new[] { "EndDate", "FechaFin" }, today.AddDays(3));
            SetAny(rental, new[] { "Status", "Estado" }, "Activo");
            SetAny(rental, new[] { "CustomerName", "ClienteNombre", "Customer", "Cliente" }, "Cliente Pruebas");
            ctx.Rental.Add((Rental)rental);
            ctx.SaveChanges();

            var rentalId = GetPrimaryKeyValue<int>(ctx, rental);

            // Vincular el Rental con el dispositivo 3 (alquilado)
            var rentDevice = Activator.CreateInstance(typeof(RentDevice))!;
            SetAny(rentDevice, new[] { "RentalId" }, rentalId);
            SetAny(rentDevice, new[] { "DeviceId" }, device3Id);
            ctx.RentDevice.Add((RentDevice)rentDevice);

            ctx.SaveChanges();
        }

        private static void SetIfExists(object entity, string propertyName, object? value)
        {
            var prop = entity.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (prop == null || !prop.CanWrite) return;

            try
            {
                if (value is not null)
                {
                    var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                    var converted = prop.PropertyType.IsInstanceOfType(value) ? value : Convert.ChangeType(value, targetType);
                    prop.SetValue(entity, converted);
                }
                else
                {
                    prop.SetValue(entity, null);
                }
            }
            catch
            {
                // Ignorar conversiones inválidas.
            }
        }

        private static void SetAny(object entity, IEnumerable<string> candidatePropertyNames, object? value)
        {
            foreach (var name in candidatePropertyNames)
            {
                var prop = entity.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (prop != null && prop.CanWrite)
                {
                    SetIfExists(entity, name, value);
                    return;
                }
            }
        }

        private static TKey GetPrimaryKeyValue<TKey>(DbContext ctx, object entity)
        {
            var entry = ctx.Entry(entity);
            var key = entry.Metadata.FindPrimaryKey();
            if (key == null || key.Properties.Count == 0)
                return default!;

            var pkProp = key.Properties[0].Name;
            var value = entry.Property(pkProp).CurrentValue;
            return value is null ? default! : (TKey)Convert.ChangeType(value, typeof(TKey));
        }
    }
}
