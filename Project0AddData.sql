Delete from Inventory
delete from CustOrder
delete from Orders
delete from Customer
delete from Product
delete from Location

INSERT INTO Location (BranchName, Street, City, State, Zipcode) VALUES 
('Flagship', '123 Main', 'Arlington', 'TX', '76010'),
('Branch1', '1 Branch St', 'Dallas', 'TX', '76110'),
('Branch2', '2 Branch St', 'Austin', 'TX', '76111');

INSERT INTO Product (Name, Price) VALUES
('Coconut Vinegar (Sukang Tuba)', 10),
('White Vinegar (Sukang Puti)', 15),
('Nipa Palm Vinegar (Sukang Paombong)', 15),
('Cane Vinegar (Sukang Maasim)', 20),
('Ilocano Cane Vinegar (Sukang Iloco)', 25),
('Soy Sauce (Toyo)', 20)


Select * from Product
Select * from Location

INSERT INTO Inventory (ProductID, Stock, LocationID) VALUES
(1000, 20, 1000),
(1001, 20, 1000),
(1002, 20, 1000),
(1003, 20, 1000),
(1004, 20, 1000),
(1005, 20, 1000),
(1000, 20, 1001),
(1001, 20, 1001),
(1002, 20, 1001),
(1003, 20, 1001),
(1004, 20, 1001),
(1005, 20, 1001),
(1000, 20, 1002),
(1001, 20, 1002),
(1002, 20, 1002),
(1003, 20, 1002),
(1004, 20, 1002),
(1005, 20, 1002)

SELECT * FROM Inventory


