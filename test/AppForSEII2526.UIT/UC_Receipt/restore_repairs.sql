-- Script para restaurar las reparaciones desde el backup

-- Restaurar las reparaciones
SET IDENTITY_INSERT Repair ON;
INSERT INTO Repair (Id, Name, Cost, Description, ScaleId)
SELECT Id, Name, Cost, Description, ScaleId FROM #Backup_Repair;
SET IDENTITY_INSERT Repair OFF;

-- Restaurar los recibos
SET IDENTITY_INSERT Receipt ON;
INSERT INTO Receipt (Id, ApplicationUserId, ReceiptDate, PaymentMethod, deliveryAddres, TotalPrice)
SELECT Id, ApplicationUserId, ReceiptDate, PaymentMethod, deliveryAddres, TotalPrice FROM #Backup_Receipt;
SET IDENTITY_INSERT Receipt OFF;

-- Restaurar los items de recibo
INSERT INTO ReceiptItem (RepairId, ReceiptId, Model)
SELECT RepairId, ReceiptId, Model FROM #Backup_ReceiptItem;

-- Limpiar las tablas temporales
DROP TABLE #Backup_ReceiptItem;
DROP TABLE #Backup_Receipt;
DROP TABLE #Backup_Repair;

-- Verificar que se han restaurado las reparaciones
SELECT COUNT(*) as TotalRepairs FROM Repair;
SELECT * FROM Repair;