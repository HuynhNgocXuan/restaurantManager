USE [master]
GO
/****** Object:  Database [QuanLyQuanAn]    Script Date: 6/24/2024 1:43:08 PM ******/
CREATE DATABASE [QuanLyQuanAn]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QuanLyQuanAn', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER01\MSSQL\DATA\QuanLyQuanAn.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'QuanLyQuanAn_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER01\MSSQL\DATA\QuanLyQuanAn_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [QuanLyQuanAn] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QuanLyQuanAn].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QuanLyQuanAn] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QuanLyQuanAn] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QuanLyQuanAn] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QuanLyQuanAn] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QuanLyQuanAn] SET ARITHABORT OFF 
GO
ALTER DATABASE [QuanLyQuanAn] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [QuanLyQuanAn] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QuanLyQuanAn] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QuanLyQuanAn] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QuanLyQuanAn] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QuanLyQuanAn] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QuanLyQuanAn] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QuanLyQuanAn] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QuanLyQuanAn] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QuanLyQuanAn] SET  ENABLE_BROKER 
GO
ALTER DATABASE [QuanLyQuanAn] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QuanLyQuanAn] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QuanLyQuanAn] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QuanLyQuanAn] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QuanLyQuanAn] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QuanLyQuanAn] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QuanLyQuanAn] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QuanLyQuanAn] SET RECOVERY FULL 
GO
ALTER DATABASE [QuanLyQuanAn] SET  MULTI_USER 
GO
ALTER DATABASE [QuanLyQuanAn] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QuanLyQuanAn] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QuanLyQuanAn] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QuanLyQuanAn] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [QuanLyQuanAn] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [QuanLyQuanAn] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'QuanLyQuanAn', N'ON'
GO
ALTER DATABASE [QuanLyQuanAn] SET QUERY_STORE = ON
GO
ALTER DATABASE [QuanLyQuanAn] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [QuanLyQuanAn]
GO
/****** Object:  UserDefinedFunction [dbo].[fuConvertToUnsign1]    Script Date: 6/24/2024 1:43:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



create function [dbo].[fuConvertToUnsign1](@strInput NVARCHAR(4000)) 
RETURNS NVARCHAR(4000) 
AS BEGIN IF @strInput IS NULL 
RETURN @strInput IF @strInput = '' 
RETURN @strInput DECLARE @RT NVARCHAR(4000) DECLARE @SIGN_CHARS NCHAR(136) 
DECLARE @UNSIGN_CHARS NCHAR (136) SET @SIGN_CHARS = N'ăâđêôơưàảãạáằẳẵặắầẩẫậấèẻẽẹéềểễệế
ìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵý ĂÂĐÊÔƠƯÀẢÃẠÁẰẲẴẶẮẦẨẪẬẤÈẺẼẸÉỀỂỄỆẾÌỈĨỊÍ ÒỎÕỌÓỒỔỖỘỐỜỞỠỢỚÙỦŨỤÚỪỬỮỰỨỲỶỸỴÝ' 
+NCHAR(272)+ NCHAR(208) SET @UNSIGN_CHARS = N'aadeoouaaaaaaaaaaaaaaaeeeeeeeeee 
iiiiiooooooooooooooouuuuuuuuuuyyyyy AADEOOUAAAAAAAAAAAAAAAEEEEEEEEEEIIIII OOOOOOOOOOOOOOOUUUUUUUUUUYYYYYDD' 
DECLARE @COUNTER int DECLARE @COUNTER1 int SET @COUNTER = 1 
WHILE (@COUNTER <=LEN(@strInput)) BEGIN SET @COUNTER1 = 1 WHILE (@COUNTER1 <=LEN(@SIGN_CHARS)+1) 
BEGIN IF UNICODE(SUBSTRING(@SIGN_CHARS, @COUNTER1,1)) = UNICODE(SUBSTRING(@strInput,@COUNTER ,1) ) 
BEGIN IF @COUNTER=1 SET @strInput = SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + 
SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)-1) 
ELSE SET @strInput = SUBSTRING(@strInput, 1, @COUNTER-1) +SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + 
SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)- @COUNTER) 
BREAK END SET @COUNTER1 = @COUNTER1 +1 END SET @COUNTER = @COUNTER +1 END 
SET @strInput = replace(@strInput,' ','-') RETURN @strInput END

GO
/****** Object:  Table [dbo].[Account]    Script Date: 6/24/2024 1:43:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[userName] [nvarchar](100) NOT NULL,
	[userPassWord] [varchar](1000) NOT NULL,
	[displayName] [nvarchar](100) NOT NULL,
	[accountType] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[userName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bill]    Script Date: 6/24/2024 1:43:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bill](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[dateCheckIn] [date] NOT NULL,
	[dateCheckOut] [date] NULL,
	[payState] [int] NOT NULL,
	[idTable] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BillInfo]    Script Date: 6/24/2024 1:43:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillInfo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idBill] [int] NOT NULL,
	[idFood] [int] NOT NULL,
	[count] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EatTable]    Script Date: 6/24/2024 1:43:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EatTable](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[tableName] [nvarchar](100) NOT NULL,
	[tableStatus] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Food]    Script Date: 6/24/2024 1:43:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Food](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[foodName] [nvarchar](100) NOT NULL,
	[price] [float] NOT NULL,
	[idCategory] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FoodCategory]    Script Date: 6/24/2024 1:43:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FoodCategory](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[foodCategoryName] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Account] ([userName], [userPassWord], [displayName], [accountType]) VALUES (N'st1', N'123456', N'Tên nhân viên 1', 0)
INSERT [dbo].[Account] ([userName], [userPassWord], [displayName], [accountType]) VALUES (N'X2K4', N'ad123', N'Xuân Huỳnh', 1)
GO
SET IDENTITY_INSERT [dbo].[EatTable] ON 

INSERT [dbo].[EatTable] ([id], [tableName], [tableStatus]) VALUES (1, N'Bàn 1', N'Trống')
INSERT [dbo].[EatTable] ([id], [tableName], [tableStatus]) VALUES (2, N'Bàn 2', N'Trống')
INSERT [dbo].[EatTable] ([id], [tableName], [tableStatus]) VALUES (3, N'Bàn 3', N'Trống')
INSERT [dbo].[EatTable] ([id], [tableName], [tableStatus]) VALUES (4, N'Bàn 4', N'Trống')
INSERT [dbo].[EatTable] ([id], [tableName], [tableStatus]) VALUES (5, N'Bàn 5', N'Trống')
INSERT [dbo].[EatTable] ([id], [tableName], [tableStatus]) VALUES (6, N'Bàn 6', N'Trống')
INSERT [dbo].[EatTable] ([id], [tableName], [tableStatus]) VALUES (7, N'Bàn 7', N'Trống')
INSERT [dbo].[EatTable] ([id], [tableName], [tableStatus]) VALUES (8, N'Bàn 8', N'Trống')
INSERT [dbo].[EatTable] ([id], [tableName], [tableStatus]) VALUES (9, N'Bàn 9', N'Trống')
INSERT [dbo].[EatTable] ([id], [tableName], [tableStatus]) VALUES (10, N'Bàn 10', N'Trống')
INSERT [dbo].[EatTable] ([id], [tableName], [tableStatus]) VALUES (11, N'Bàn 11', N'Trống')
INSERT [dbo].[EatTable] ([id], [tableName], [tableStatus]) VALUES (12, N'Bàn 12', N'Trống')
INSERT [dbo].[EatTable] ([id], [tableName], [tableStatus]) VALUES (13, N'Bàn 13', N'Trống')
INSERT [dbo].[EatTable] ([id], [tableName], [tableStatus]) VALUES (14, N'Bàn 14', N'Trống')
INSERT [dbo].[EatTable] ([id], [tableName], [tableStatus]) VALUES (15, N'Bàn 15', N'Trống')
INSERT [dbo].[EatTable] ([id], [tableName], [tableStatus]) VALUES (16, N'Bàn 16', N'Trống')
INSERT [dbo].[EatTable] ([id], [tableName], [tableStatus]) VALUES (17, N'Bàn 17', N'Trống')
INSERT [dbo].[EatTable] ([id], [tableName], [tableStatus]) VALUES (18, N'Bàn 18', N'Trống')
INSERT [dbo].[EatTable] ([id], [tableName], [tableStatus]) VALUES (19, N'Bàn 19', N'Trống')
INSERT [dbo].[EatTable] ([id], [tableName], [tableStatus]) VALUES (20, N'Bàn 20', N'Trống')
INSERT [dbo].[EatTable] ([id], [tableName], [tableStatus]) VALUES (21, N'Bàn 21', N'Trống')
INSERT [dbo].[EatTable] ([id], [tableName], [tableStatus]) VALUES (22, N'Bàn 22', N'Trống')
INSERT [dbo].[EatTable] ([id], [tableName], [tableStatus]) VALUES (23, N'Bàn 23', N'Trống')
INSERT [dbo].[EatTable] ([id], [tableName], [tableStatus]) VALUES (24, N'Bàn 24', N'Trống')
INSERT [dbo].[EatTable] ([id], [tableName], [tableStatus]) VALUES (25, N'Bàn 25', N'Trống')
INSERT [dbo].[EatTable] ([id], [tableName], [tableStatus]) VALUES (26, N'Bàn 26', N'Trống')
INSERT [dbo].[EatTable] ([id], [tableName], [tableStatus]) VALUES (27, N'Bàn 27', N'Trống')
INSERT [dbo].[EatTable] ([id], [tableName], [tableStatus]) VALUES (28, N'Bàn 28', N'Trống')
INSERT [dbo].[EatTable] ([id], [tableName], [tableStatus]) VALUES (29, N'Bàn 29', N'Trống')
INSERT [dbo].[EatTable] ([id], [tableName], [tableStatus]) VALUES (30, N'Bàn 30', N'Trống')
INSERT [dbo].[EatTable] ([id], [tableName], [tableStatus]) VALUES (31, N'Bàn 31', N'Trống')
INSERT [dbo].[EatTable] ([id], [tableName], [tableStatus]) VALUES (32, N'Bàn 32', N'Trống')
INSERT [dbo].[EatTable] ([id], [tableName], [tableStatus]) VALUES (33, N'Bàn 33', N'Trống')
INSERT [dbo].[EatTable] ([id], [tableName], [tableStatus]) VALUES (34, N'Bàn 34', N'Trống')
INSERT [dbo].[EatTable] ([id], [tableName], [tableStatus]) VALUES (35, N'Bàn 35', N'Trống')
INSERT [dbo].[EatTable] ([id], [tableName], [tableStatus]) VALUES (36, N'Bàn 36', N'Trống')
INSERT [dbo].[EatTable] ([id], [tableName], [tableStatus]) VALUES (37, N'Bàn 37', N'Trống')
INSERT [dbo].[EatTable] ([id], [tableName], [tableStatus]) VALUES (38, N'Bàn 38', N'Trống')
INSERT [dbo].[EatTable] ([id], [tableName], [tableStatus]) VALUES (39, N'Bàn 39', N'Trống')
INSERT [dbo].[EatTable] ([id], [tableName], [tableStatus]) VALUES (40, N'Bàn 40', N'Trống')
SET IDENTITY_INSERT [dbo].[EatTable] OFF
GO
SET IDENTITY_INSERT [dbo].[Food] ON 

INSERT [dbo].[Food] ([id], [foodName], [price], [idCategory]) VALUES (1, N'Gà tần thuốc bắc ngải cứu', 350000, 1)
INSERT [dbo].[Food] ([id], [foodName], [price], [idCategory]) VALUES (2, N'Gỏi gà xé chua ngọt', 400000, 1)
INSERT [dbo].[Food] ([id], [foodName], [price], [idCategory]) VALUES (3, N'Gà chiên nước mắm', 200000, 1)
INSERT [dbo].[Food] ([id], [foodName], [price], [idCategory]) VALUES (4, N'Gà quay mật ong', 230000, 1)
INSERT [dbo].[Food] ([id], [foodName], [price], [idCategory]) VALUES (5, N'Cá lăng xào nấm', 180000, 2)
INSERT [dbo].[Food] ([id], [foodName], [price], [idCategory]) VALUES (6, N'Cá chép om dưa', 110000, 2)
INSERT [dbo].[Food] ([id], [foodName], [price], [idCategory]) VALUES (7, N'Cá quả nướng chuối', 200000, 2)
INSERT [dbo].[Food] ([id], [foodName], [price], [idCategory]) VALUES (8, N'Cá song hấp xì dầu', 500000, 2)
INSERT [dbo].[Food] ([id], [foodName], [price], [idCategory]) VALUES (9, N'Ốc hương(nướng, rang, muối, hấp bia', 150000, 3)
INSERT [dbo].[Food] ([id], [foodName], [price], [idCategory]) VALUES (10, N'Ngao nướng mỡ hành(hấp bia)', 200000, 3)
INSERT [dbo].[Food] ([id], [foodName], [price], [idCategory]) VALUES (11, N'Bề bề', 390000, 3)
INSERT [dbo].[Food] ([id], [foodName], [price], [idCategory]) VALUES (12, N'Cua biển', 350000, 3)
INSERT [dbo].[Food] ([id], [foodName], [price], [idCategory]) VALUES (13, N'Rượu gạo', 30000, 4)
INSERT [dbo].[Food] ([id], [foodName], [price], [idCategory]) VALUES (14, N'Bia Sài Gòn', 15000, 4)
INSERT [dbo].[Food] ([id], [foodName], [price], [idCategory]) VALUES (15, N'Nước ngọt(peps, coca', 10000, 4)
INSERT [dbo].[Food] ([id], [foodName], [price], [idCategory]) VALUES (16, N'Nước suối', 10000, 4)
SET IDENTITY_INSERT [dbo].[Food] OFF
GO
SET IDENTITY_INSERT [dbo].[FoodCategory] ON 

INSERT [dbo].[FoodCategory] ([id], [foodCategoryName]) VALUES (1, N'Các món gà')
INSERT [dbo].[FoodCategory] ([id], [foodCategoryName]) VALUES (2, N'Các món cá')
INSERT [dbo].[FoodCategory] ([id], [foodCategoryName]) VALUES (3, N'Hải sản các loại')
INSERT [dbo].[FoodCategory] ([id], [foodCategoryName]) VALUES (4, N'Đồ uống các loại')
SET IDENTITY_INSERT [dbo].[FoodCategory] OFF
GO
ALTER TABLE [dbo].[Account] ADD  DEFAULT ((123456)) FOR [userPassWord]
GO
ALTER TABLE [dbo].[Account] ADD  DEFAULT (N'Huỳnh Ngọc Xuân') FOR [displayName]
GO
ALTER TABLE [dbo].[Account] ADD  DEFAULT ((0)) FOR [accountType]
GO
ALTER TABLE [dbo].[Bill] ADD  DEFAULT (getdate()) FOR [dateCheckIn]
GO
ALTER TABLE [dbo].[Bill] ADD  DEFAULT ((0)) FOR [payState]
GO
ALTER TABLE [dbo].[BillInfo] ADD  DEFAULT ((0)) FOR [count]
GO
ALTER TABLE [dbo].[EatTable] ADD  DEFAULT (N'Chưa đặt tên') FOR [tableName]
GO
ALTER TABLE [dbo].[EatTable] ADD  DEFAULT (N'Trống') FOR [tableStatus]
GO
ALTER TABLE [dbo].[Food] ADD  DEFAULT (N'Chưa đặt tên') FOR [foodName]
GO
ALTER TABLE [dbo].[Food] ADD  DEFAULT ((0)) FOR [price]
GO
ALTER TABLE [dbo].[FoodCategory] ADD  DEFAULT (N'Chưa đặt tên') FOR [foodCategoryName]
GO
ALTER TABLE [dbo].[Bill]  WITH CHECK ADD  CONSTRAINT [FK_idTable] FOREIGN KEY([idTable])
REFERENCES [dbo].[EatTable] ([id])
GO
ALTER TABLE [dbo].[Bill] CHECK CONSTRAINT [FK_idTable]
GO
ALTER TABLE [dbo].[BillInfo]  WITH CHECK ADD  CONSTRAINT [FK_idBill] FOREIGN KEY([idBill])
REFERENCES [dbo].[Bill] ([id])
GO
ALTER TABLE [dbo].[BillInfo] CHECK CONSTRAINT [FK_idBill]
GO
ALTER TABLE [dbo].[BillInfo]  WITH CHECK ADD  CONSTRAINT [FK_idFood] FOREIGN KEY([idFood])
REFERENCES [dbo].[Food] ([id])
GO
ALTER TABLE [dbo].[BillInfo] CHECK CONSTRAINT [FK_idFood]
GO
ALTER TABLE [dbo].[Food]  WITH CHECK ADD  CONSTRAINT [FK_idFoodCategory] FOREIGN KEY([idCategory])
REFERENCES [dbo].[FoodCategory] ([id])
GO
ALTER TABLE [dbo].[Food] CHECK CONSTRAINT [FK_idFoodCategory]
GO
/****** Object:  StoredProcedure [dbo].[USP_GetAccountByUserName]    Script Date: 6/24/2024 1:43:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



create procedure [dbo].[USP_GetAccountByUserName]
@userName nvarchar(100)
as
begin
	select * from Account where userName = @userName
end
GO
/****** Object:  StoredProcedure [dbo].[USP_GetBillCountByDate]    Script Date: 6/24/2024 1:43:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



create procedure [dbo].[USP_GetBillCountByDate]
@checkIn date , @checkOut date
as
begin
	select COUNT(*) from Bill as b, EatTable as tb 
	where dateCheckIn >= @checkIn and dateCheckOut <= @checkOut and b.idTable = tb.id and payState = 1
end
GO
/****** Object:  StoredProcedure [dbo].[USP_GetListAccount]    Script Date: 6/24/2024 1:43:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[USP_GetListAccount]
as 
begin
	select userName as [Tên tài khoản], displayName as [Tên hiển thị], accountType as [Loại tài khoản] from Account
end
GO
/****** Object:  StoredProcedure [dbo].[USP_GetListFood]    Script Date: 6/24/2024 1:43:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[USP_GetListFood] 
as 
begin 
	select id as [STT], foodName as [Tên món ăn], price as [Giá] , idCategory as [Loại thức ăn] from Food
end
GO
/****** Object:  StoredProcedure [dbo].[USP_GetListTableForAdmin]    Script Date: 6/24/2024 1:43:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[USP_GetListTableForAdmin]
as 
begin
	select id as [STT], tableName as [Tên bàn], tableStatus as [Trạng thái] from EatTable
end
GO
/****** Object:  StoredProcedure [dbo].[USP_GetTablesList]    Script Date: 6/24/2024 1:43:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



create procedure [dbo].[USP_GetTablesList]
as 
	Select * from EatTable
GO
/****** Object:  StoredProcedure [dbo].[USP_InsertBillInfo]    Script Date: 6/24/2024 1:43:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[USP_InsertBillInfo] 
@idBill int, @idFood int, @count int
as
begin
	declare @idExistBillInfo int
	declare @countFood int = 1

	select @idExistBillInfo = id, @countFood = b.count  from BillInfo as b where idBill = @idBill and idFood = @idFood
	
	if (@idExistBillInfo > 0) 
		begin
			declare @newCount int = @countFood + @count
			if (@newCount > 0)
				update BillInfo set count = @newCount where idBill = @idBill and idFood = @idFood
			else
				delete BillInfo where idBill = @idBill and idFood = @idFood

		end
	else if (@count > 0)
		begin
			insert BillInfo
					(idBill, idFood, count)
			values	(@idBill, @idFood, @count)
		end
end
GO
/****** Object:  StoredProcedure [dbo].[USP_Login]    Script Date: 6/24/2024 1:43:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[USP_Login]
@userName nvarchar(100), @userPassWord nvarchar(100)
as 
begin 
	select * from Account 
	where userName = @userName and userPassWord = @userPassWord
end 
GO
/****** Object:  StoredProcedure [dbo].[USP_SwitchTable]    Script Date: 6/24/2024 1:43:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[USP_SwitchTable] 
@idOldTable int , @idNewTable int
as
begin
	declare @idOldBill int
	declare @idNewBill int

	declare @isEmpty int = 0 

	select @idOldBill = id from Bill where idTable = @idOldTable and payState = 0
	select @idNewBill = id from Bill where idTable = @idNewTable and payState = 0

	declare @countOldBillInfo int
	declare @idOldBillInfo int
	select @countOldBillInfo = COUNT(*) from BillInfo where idBill = @idOldBill

	if (@countOldBillInfo > 0)
	begin 
		if (@idNewBill is null)
		begin 
			execute USP_InsertBill @idTable = @idNewTable
			select @idNewBill = MAX(id) from Bill where idTable = @idNewTable and payState = 0
		end 

		update BillInfo set idBill = @idNewBill where idBill = @idOldBill
		delete Bill where id = @idOldBill
		update EatTable set tableStatus = N'Trống' where id = @idOldTable
	end
end
GO
/****** Object:  StoredProcedure [dbo].[USP_UpdateAccount]    Script Date: 6/24/2024 1:43:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



create procedure [dbo].[USP_UpdateAccount] 
@userName varchar(100), @displayName nvarchar(100), @password nvarchar(100), @newPassword nvarchar(100)
as 
begin
	declare @isRightPass int = 0
	select @isRightPass = COUNT(*) from Account where userName = @userName and userPassWord = @password
	
	if (@isRightPass = 1)
	begin
		declare @oldDisplayName nvarchar(100)
		select @oldDisplayName = displayName from Account where userName = @userName
		
		if (@newPassword is null or @newPassword = '')
			update Account set displayName = @displayName where userName = @userName

		else if (@displayName = @oldDisplayName)
			update Account set userPassWord = @newPassword where userName = @userName
		
		else 
			update Account set displayName = @displayName, userPassWord = @newPassword where userName = @userName
	end
end	
GO
USE [master]
GO
ALTER DATABASE [QuanLyQuanAn] SET  READ_WRITE 
GO
