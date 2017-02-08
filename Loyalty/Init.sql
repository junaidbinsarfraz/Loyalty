USE Loyalty
GO

INSERT INTO [Users] 
([Users].Name, [Users].Username, [Users].Password, [Users].MobileNumber, [Users].Role, [Users].Status)
VALUES
('Admin', 'admin', 'a', '343243443', 'Admin', 1)
GO

INSERT INTO [Users] 
([Users].Name, [Users].Username, [Users].Password, [Users].MobileNumber, [Users].Role, [Users].Status)
VALUES
('Sale Person 1', 'sp1', 'sp', '343243443', 'Sale Person', 1)
GO

INSERT INTO [Users] 
([Users].Name, [Users].Username, [Users].Password, [Users].MobileNumber, [Users].Role, [Users].Status)
VALUES
('Sale Person 2', 'sp2', 'sp', '343243443', 'Sale Person', 1)
GO

INSERT INTO [Products] 
([Products].Name, [Products].Code, [Products].Description, [Products].SellingPrice, [Products].Quantity, [Products].TotalSold, [Products].CostPrice, [Products].Status)
VALUES
('Product 1', 'P1', 'This is Product 1', 25.0, 20, 0, 20.0, 1)
GO

INSERT INTO [Products] 
([Products].Name, [Products].Code, [Products].Description, [Products].SellingPrice, [Products].Quantity, [Products].TotalSold, [Products].CostPrice, [Products].Status)
VALUES
('Product 2', 'P2', 'This is Product 2', 50.5, 10, 0, 40.0, 1)
GO

INSERT INTO [Products] 
([Products].Name, [Products].Code, [Products].Description, [Products].SellingPrice, [Products].Quantity, [Products].TotalSold, [Products].CostPrice, [Products].Status)
VALUES
('Product 3', 'P3', 'This is Product 3', 145.0, 40, 0, 120.0, 1)
GO
