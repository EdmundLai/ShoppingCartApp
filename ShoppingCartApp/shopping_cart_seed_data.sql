USE [ShoppingAppContext-1]
GO

INSERT INTO [dbo].[UserRoles]
           ([UserRoleName])
     VALUES
           ('Customer'),
	       ('Admin')
GO

INSERT INTO [dbo].[Users]
           ([UserRoleId]
           ,[Username]
           ,[Password])
     VALUES
           (1
           , 'Bob'
           , 'customer'),
		   (2
           , 'Tom'
           , 'admin')
GO

INSERT INTO [dbo].[Categories]
           ([CategoryName])
     VALUES
           ('Food'),
		   ('Games'),
		   ('Electronics'),
		   ('Books')
GO

INSERT INTO [dbo].[Products]
           ([CategoryId]
           ,[ProductName]
           ,[ProductCost])
     VALUES
           (1
           , 'Swiss Cheese',
           5),
		   (1
           , 'Steak',
           10),
		   (2
           , 'Monopoly',
           20),
		   (2
           , 'Chess Set',
           25),
		   (3
           , 'Flat Screen TV',
           500),
		   (3
           , 'High Fidelity Headphones',
           200),
		   (4
           , 'Tale of Two Cities',
           15),
		   (3
           , 'Moby Dick',
           20)

GO