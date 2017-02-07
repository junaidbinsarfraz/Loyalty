USE Loyalty
GO

INSERT INTO [Users] 
([Users].Name, [Users].Username, [Users].Password, [Users].MobileNumber, [Users].Role)
VALUES
('Admin', 'admin', 'a', '343243443', 'Admin')
GO

INSERT INTO [Users] 
([Users].Name, [Users].Username, [Users].Password, [Users].MobileNumber, [Users].Role)
VALUES
('Sale Person 1', 'sp1', 'sp', '343243443', 'Sale Person')
GO

INSERT INTO [Users] 
([Users].Name, [Users].Username, [Users].Password, [Users].MobileNumber, [Users].Role)
VALUES
('Sale Person 2', 'sp2', 'sp', '343243443', 'Sale Person')
GO

INSERT INTO [Products] 
([Products].Name, [Products].Code, [Products].Description, [Products].SellingPrice, [Products].Quantity, [Products].TotalSold, [Products].CostPrice)
VALUES
('Product 1', 'P1', 'This is Product 1', 25.0, 20, 0, 20.0)
GO

INSERT INTO [Products] 
([Products].Name, [Products].Code, [Products].Description, [Products].SellingPrice, [Products].Quantity, [Products].TotalSold, [Products].CostPrice)
VALUES
('Product 2', 'P2', 'This is Product 2', 50.5, 10, 0, 40.0)
GO

INSERT INTO [Products] 
([Products].Name, [Products].Code, [Products].Description, [Products].SellingPrice, [Products].Quantity, [Products].TotalSold, [Products].CostPrice)
VALUES
('Product 3', 'P3', 'This is Product 3', 145.0, 40, 0, 120.0)
GO
