-- =========================================
-- TẠO DATABASE
-- =========================================
IF DB_ID('QL_BanNuoc') IS NULL
BEGIN
    CREATE DATABASE QL_BanNuoc;
END
GO

USE QL_BanNuoc;
GO

-- =========================================
-- BẢNG CATEGORY
-- =========================================
CREATE TABLE Category
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL UNIQUE
);
GO


-- =========================================
-- BẢNG DRINK
-- =========================================
CREATE TABLE Drink
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(150) NOT NULL,
    Price DECIMAL(10,2) NOT NULL,
    CategoryId INT NOT NULL,
    ImageUrl NVARCHAR(255) NULL,
    Description NVARCHAR(500) NULL,
    IsAvailable BIT NOT NULL DEFAULT 1,

    CONSTRAINT FK_Drink_Category
        FOREIGN KEY (CategoryId) REFERENCES Category(Id),

    CONSTRAINT CK_Drink_Price
        CHECK (Price >= 0)
);
GO


-- =========================================
-- BẢNG ORDER
-- =========================================
CREATE TABLE [Order]
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CustomerName NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20) NOT NULL,
    OrderType NVARCHAR(20) NOT NULL,
    TableNumber INT NULL,
    Note NVARCHAR(500) NULL,
    TotalPrice DECIMAL(10,2) NOT NULL DEFAULT 0,
    Status NVARCHAR(20) NOT NULL DEFAULT N'Pending',
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT CK_Order_OrderType
        CHECK (OrderType IN ('DineIn', 'TakeAway')),

    CONSTRAINT CK_Order_Status
        CHECK (Status IN ('Pending', 'Confirmed', 'Preparing', 'Completed', 'Cancelled')),

    CONSTRAINT CK_Order_TotalPrice
        CHECK (TotalPrice >= 0)
);
GO


-- =========================================
-- BẢNG ORDERITEM
-- =========================================
CREATE TABLE OrderItem
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT NOT NULL,
    DrinkId INT NOT NULL,
    Quantity INT NOT NULL,
    Price DECIMAL(10,2) NOT NULL,

    CONSTRAINT FK_OrderItem_Order
        FOREIGN KEY (OrderId) REFERENCES [Order](Id)
        ON DELETE CASCADE,

    CONSTRAINT FK_OrderItem_Drink
        FOREIGN KEY (DrinkId) REFERENCES Drink(Id),

    CONSTRAINT CK_OrderItem_Quantity
        CHECK (Quantity > 0),

    CONSTRAINT CK_OrderItem_Price
        CHECK (Price >= 0)
);
GO


-- =========================================
-- BẢNG ADMIN
-- =========================================
CREATE TABLE Admin
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    [Password] NVARCHAR(255) NOT NULL,
    FullName NVARCHAR(100) NULL
);
GO