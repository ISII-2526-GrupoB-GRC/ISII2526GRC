-- Script de datos de prueba para dispositivos informáticos

USE [aspnet-AppForSEII2526.Web-660902f3-55b0-42b4-a4c6-7893d47fb56a]
GO

-- Insertar Roles
INSERT INTO AspNetRoles (Id, Name, NormalizedName, ConcurrencyStamp) VALUES
('1', 'Admin', 'ADMIN', NEWID()),
('2', 'Customer', 'CUSTOMER', NEWID()),
('3', 'Employee', 'EMPLOYEE', NEWID());

-- Insertar Usuarios (contraseñas hasheadas para "Password123!")
INSERT INTO AspNetUsers (Id, Name, Surname, DeliveryAddress, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount) VALUES
('user1', 'Ana', 'González', 'Calle Serrano 45, Madrid', 'ana.gonzalez@example.com', 'ANA.GONZALEZ@EXAMPLE.COM', 'ana.gonzalez@example.com', 'ANA.GONZALEZ@EXAMPLE.COM', 1, 'AQAAAAIAAYagAAAAEJ5K8F7Z9X2Q3P4R5S6T7U8V9W0X1Y2Z3A4B5C6D7E8F9G0H1I2J3K4L5M6N7O8P9Q0R==', NEWID(), NEWID(), '600123456', 1, 0, 1, 0),
('user2', 'Pedro', 'Martín', 'Rambla Catalunya 88, Barcelona', 'pedro.martin@example.com', 'PEDRO.MARTIN@EXAMPLE.COM', 'pedro.martin@example.com', 'PEDRO.MARTIN@EXAMPLE.COM', 1, 'AQAAAAIAAYagAAAAEJ5K8F7Z9X2Q3P4R5S6T7U8V9W0X1Y2Z3A4B5C6D7E8F9G0H1I2J3K4L5M6N7O8P9Q0R==', NEWID(), NEWID(), '600234567', 1, 0, 1, 0),
('user3', 'Laura', 'Fernández', 'Avenida Diagonal 120, Valencia', 'laura.fernandez@example.com', 'LAURA.FERNANDEZ@EXAMPLE.COM', 'laura.fernandez@example.com', 'LAURA.FERNANDEZ@EXAMPLE.COM', 1, 'AQAAAAIAAYagAAAAEJ5K8F7Z9X2Q3P4R5S6T7U8V9W0X1Y2Z3A4B5C6D7E8F9G0H1I2J3K4L5M6N7O8P9Q0R==', NEWID(), NEWID(), '600345678', 0, 0, 1, 0),
('admin1', 'Admin', 'Sistema', 'Oficina Central, Madrid', 'admin@example.com', 'ADMIN@EXAMPLE.COM', 'admin@example.com', 'ADMIN@EXAMPLE.COM', 1, 'AQAAAAIAAYagAAAAEJ5K8F7Z9X2Q3P4R5S6T7U8V9W0X1Y2Z3A4B5C6D7E8F9G0H1I2J3K4L5M6N7O8P9Q0R==', NEWID(), NEWID(), '600456789', 1, 0, 1, 0);

-- Asignar Roles a Usuarios
INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES
('user1', '2'), -- Ana es Customer
('user2', '2'), -- Pedro es Customer
('user3', '2'), -- Laura es Customer
('admin1', '1'); -- Admin es Admin

-- Insertar Modelos (Categorías de dispositivos informáticos)
INSERT INTO Modelo (NameModel) VALUES
('Portátil'),
('Ordenador de Sobremesa'),
('Tablet'),
('Monitor'),
('Impresora'),
('Router'),
('Servidor'),
('Cámara Web');

-- Insertar Escalas (Tamaños/Capacidades)
INSERT INTO Scale (Name) VALUES
('Pequeño (hasta 13")'),
('Mediano (14"-16")'),
('Grande (17"+)'),
('Empresarial'),
('Profesional');

-- Insertar Dispositivos
INSERT INTO Device (Brand, Color, Name, priceForPurchace, priceForRent, Quality, quantityForPurchase, quantityForRent, Year, ModelId) VALUES
-- Portátiles
('Dell', 'Gris', 'Dell XPS 13', 1299.99, 45.00, 5, 15, 8, 2023, 1),
('HP', 'Plata', 'HP Spectre x360', 1499.99, 50.00, 5, 10, 5, 2023, 1),
('Lenovo', 'Negro', 'Lenovo ThinkPad X1', 1799.99, 55.00, 5, 12, 6, 2024, 1),
('Apple', 'Gris Espacial', 'MacBook Air M2', 1199.99, 48.00, 5, 8, 4, 2023, 1),
('ASUS', 'Azul', 'ASUS ZenBook 14', 999.99, 40.00, 4, 20, 10, 2023, 1),

-- Ordenadores de Sobremesa
('Dell', 'Negro', 'Dell OptiPlex 7090', 899.99, 35.00, 4, 10, 5, 2023, 2),
('HP', 'Plata', 'HP EliteDesk 800', 1099.99, 38.00, 5, 8, 4, 2024, 2),
('Lenovo', 'Negro', 'Lenovo ThinkCentre M90', 799.99, 32.00, 4, 15, 7, 2023, 2),

-- Tablets
('Apple', 'Gris Espacial', 'iPad Pro 11"', 899.99, 35.00, 5, 20, 12, 2023, 3),
('Samsung', 'Negro', 'Galaxy Tab S8', 749.99, 30.00, 4, 18, 10, 2023, 3),
('Microsoft', 'Plata', 'Surface Pro 9', 1099.99, 42.00, 5, 12, 6, 2024, 3),

-- Monitores
('Dell', 'Negro', 'Dell UltraSharp 27"', 449.99, 18.00, 5, 25, 15, 2023, 4),
('LG', 'Plata', 'LG UltraWide 34"', 599.99, 22.00, 5, 15, 8, 2023, 4),
('BenQ', 'Negro', 'BenQ PD2700U 4K', 499.99, 20.00, 4, 20, 10, 2023, 4),

-- Impresoras
('HP', 'Blanco', 'HP LaserJet Pro M404', 279.99, 12.00, 4, 30, 15, 2023, 5),
('Canon', 'Negro', 'Canon PIXMA TR8620', 199.99, 10.00, 4, 25, 12, 2023, 5),
('Epson', 'Blanco', 'Epson EcoTank L3250', 249.99, 11.00, 4, 22, 11, 2023, 5),

-- Routers
('TP-Link', 'Blanco', 'TP-Link Archer AX73', 149.99, 8.00, 4, 40, 20, 2023, 6),
('ASUS', 'Negro', 'ASUS RT-AX86U', 249.99, 12.00, 5, 30, 15, 2024, 6),
('Netgear', 'Negro', 'Netgear Nighthawk AX12', 399.99, 15.00, 5, 20, 10, 2023, 6),

-- Servidores
('Dell', 'Negro', 'Dell PowerEdge R740', 3499.99, 120.00, 5, 5, 3, 2023, 7),
('HP', 'Plata', 'HP ProLiant DL380 Gen10', 2999.99, 110.00, 5, 6, 3, 2023, 7),

-- Cámaras Web
('Logitech', 'Negro', 'Logitech Brio 4K', 199.99, 8.00, 5, 50, 25, 2023, 8),
('Razer', 'Negro', 'Razer Kiyo Pro', 149.99, 7.00, 4, 40, 20, 2023, 8);

-- Insertar Reparaciones
INSERT INTO Repair (Description, Cost, Name, ScaleId) VALUES
('Sustitución de disco duro por SSD', 120.00, 'Upgrade SSD', 1),
('Cambio de pantalla LCD', 180.00, 'Reparación Pantalla', 2),
('Limpieza interna y cambio pasta térmica', 45.00, 'Mantenimiento', 1),
('Reparación de teclado', 75.00, 'Cambio Teclado', 2),
('Cambio de batería', 85.00, 'Cambio Batería', 1),
('Ampliación de memoria RAM', 95.00, 'Upgrade RAM', 3),
('Reparación de conectores USB', 60.00, 'Reparación Puertos', 1),
('Formateo e instalación de OS', 50.00, 'Instalación Sistema', 1),
('Reparación de fuente de alimentación', 110.00, 'Cambio Fuente', 4),
('Diagnóstico avanzado', 35.00, 'Diagnóstico', 1);

-- Insertar Compras
INSERT INTO Purchase (CustomerUserName, CustomerUserSurname, DeliveryAddress, PaymentMethod, PurchaseDate, TotalPrice, TotalQuanty, ApplicationUserId) VALUES
('Ana', 'González', 'Calle Serrano 45, Madrid', 1, '2024-10-15', 2499.98, 2, 'user1'),
('Pedro', 'Martín', 'Rambla Catalunya 88, Barcelona', 2, '2024-10-20', 1199.99, 1, 'user2'),
('Laura', 'Fernández', 'Avenida Diagonal 120, Valencia', 1, '2024-10-25', 1449.98, 2, 'user3'),
('Ana', 'González', 'Calle Serrano 45, Madrid', 2, '2024-11-01', 899.99, 1, 'user1');

-- Insertar Items de Compra
INSERT INTO PurchaseItem (DeviceId, PurchaseID, Description, Price, Quantity) VALUES
-- Compra 1 (Ana)
(1, 1, 'Dell XPS 13 - Portátil profesional', 1299.99, 1),
(4, 1, 'MacBook Air M2 - Portátil Apple', 1199.99, 1),
-- Compra 2 (Pedro)
(4, 2, 'MacBook Air M2 - Portátil Apple', 1199.99, 1),
-- Compra 3 (Laura)
(9, 3, 'iPad Pro 11" - Tablet profesional', 899.99, 1),
(15, 3, 'HP LaserJet Pro M404 - Impresora', 279.99, 2),
-- Compra 4 (Ana)
(11, 4, 'Surface Pro 9 - Tablet convertible', 899.99, 1);

-- Insertar Alquileres
INSERT INTO Rental (PaymentMethod, RentalDate, RentalDateFrom, RentalDateTo, TotalPrice, ApplicationUserId, Name, Surname, DeliveryAddress) VALUES
(1, '2024-11-01', '2024-11-05', '2024-11-12', 385.00, 'user1', 'Ana', 'González', 'Calle Serrano 45, Madrid'),
(2, '2024-11-02', '2024-11-08', '2024-11-15', 336.00, 'user2', 'Pedro', 'Martín', 'Rambla Catalunya 88, Barcelona'),
(1, '2024-11-03', '2024-11-10', '2024-11-17', 294.00, 'user3', 'Laura', 'Fernández', 'Avenida Diagonal 120, Valencia');

-- Insertar Dispositivos de Alquiler (CORREGIDO)
INSERT INTO RentDevice (DeviceId, RentalId, Price, Quantity) VALUES
-- Alquiler 1 (Ana: Dell XPS 13 y Lenovo ThinkPad por 7 días)
(1, 1, 45.00, 1),
(3, 1, 55.00, 1),
-- Alquiler 2 (Pedro: HP Spectre por 7 días)
(2, 2, 48.00, 1),
-- Alquiler 3 (Laura: Tablet y Monitor por 7 días)
(9, 3, 35.00, 1),
(12, 3, 18.00, 1);

-- Insertar Recibos (Facturas de reparación)
INSERT INTO Receipt (PaymentMethod, ReceiptDate, TotalPrice, ApplicationUserId) VALUES
(1, '2024-10-18', 165.00, 'user1'),
(2, '2024-10-22', 180.00, 'user2'),
(1, '2024-10-28', 205.00, 'user3'),
(2, '2024-11-02', 95.00, 'user1');

-- Insertar Items de Recibo
INSERT INTO ReceiptItem (RepairId, ReceiptId, Model) VALUES
-- Recibo 1 (Ana)
(1, 1, 'Dell XPS 13'),
(3, 1, 'Dell XPS 13'),
-- Recibo 2 (Pedro)
(2, 2, 'MacBook Air M2'),
-- Recibo 3 (Laura)
(1, 3, 'HP Spectre x360'),
(5, 3, 'HP Spectre x360'),
-- Recibo 4 (Ana)
(6, 4, 'Lenovo ThinkPad X1');

GO