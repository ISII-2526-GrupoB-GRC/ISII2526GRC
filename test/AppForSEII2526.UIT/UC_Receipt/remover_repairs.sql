-- Script para simular que no hay reparaciones disponibles
-- Guarda una copia temporal de los datos antes de eliminarlos

-- Crear tablas temporales para backup (SQL Server usa CREATE TABLE sin IF NOT EXISTS)
IF OBJECT_ID('tempdb..#Backup_ReceiptItem', 'U') IS NOT NULL DROP TABLE #Backup_ReceiptItem;
IF OBJECT_ID('tempdb..#Backup_Receipt', 'U') IS NOT NULL DROP TABLE #Backup_Receipt;
IF OBJECT_ID('tempdb..#Backup_Repair', 'U') IS NOT NULL DROP TABLE #Backup_Repair;

CREATE TABLE #Backup_ReceiptItem (
    RepairId INT,
    ReceiptId INT,
    Model NVARCHAR(15)
);

CREATE TABLE #Backup_Receipt (
    Id INT,
    ApplicationUserId NVARCHAR(450),
    ReceiptDate DATETIME2,
    PaymentMethod INT,
    deliveryAddres NVARCHAR(MAX),
    TotalPrice DECIMAL(10,2)
);

CREATE TABLE #Backup_Repair (
    Id INT,
    Name NVARCHAR(50),
    Cost DECIMAL(10,2),
    Description NVARCHAR(80),
    ScaleId INT
);

-- Guardar los datos existentes
INSERT INTO #Backup_ReceiptItem SELECT RepairId, ReceiptId, Model FROM ReceiptItem;
INSERT INTO #Backup_Receipt SELECT Id, ApplicationUserId, ReceiptDate, PaymentMethod, deliveryAddres, TotalPrice FROM Receipt;
INSERT INTO #Backup_Repair SELECT Id, Name, Cost, Description, ScaleId FROM Repair;

-- Eliminar las reparaciones (en orden por restricciones FK)
DELETE FROM ReceiptItem;
DELETE FROM Receipt;
DELETE FROM Repair;

-- Verificar que no hay reparaciones
SELECT COUNT(*) as TotalRepairs FROM Repair;