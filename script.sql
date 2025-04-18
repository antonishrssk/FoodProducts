USE [master]
GO
/****** Object:  Database [FoodProducts]    Script Date: 13.04.2025 18:31:20 ******/
CREATE DATABASE [FoodProducts]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FoodProducts', FILENAME = N'C:\Users\Professional\FoodProducts.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'FoodProducts_log', FILENAME = N'C:\Users\Professional\FoodProducts_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [FoodProducts] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FoodProducts].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FoodProducts] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FoodProducts] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FoodProducts] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FoodProducts] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FoodProducts] SET ARITHABORT OFF 
GO
ALTER DATABASE [FoodProducts] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FoodProducts] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FoodProducts] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FoodProducts] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FoodProducts] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FoodProducts] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FoodProducts] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FoodProducts] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FoodProducts] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FoodProducts] SET  DISABLE_BROKER 
GO
ALTER DATABASE [FoodProducts] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FoodProducts] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FoodProducts] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FoodProducts] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FoodProducts] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FoodProducts] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FoodProducts] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FoodProducts] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [FoodProducts] SET  MULTI_USER 
GO
ALTER DATABASE [FoodProducts] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FoodProducts] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FoodProducts] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FoodProducts] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [FoodProducts] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [FoodProducts] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [FoodProducts] SET QUERY_STORE = OFF
GO
USE [FoodProducts]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 13.04.2025 18:31:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Address] [nvarchar](200) NOT NULL,
	[DirectorFio] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 13.04.2025 18:31:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[GroupId] [tinyint] NOT NULL,
	[PackagingTypeId] [tinyint] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductGroup]    Script Date: 13.04.2025 18:31:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductGroup](
	[Id] [tinyint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_ProductGroup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductOutput]    Script Date: 13.04.2025 18:31:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductOutput](
	[CompanyId] [smallint] NOT NULL,
	[ProductId] [int] NOT NULL,
	[ProductAmount] [int] NOT NULL,
 CONSTRAINT [PK_ProductOutput] PRIMARY KEY CLUSTERED 
(
	[CompanyId] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductPackagingType]    Script Date: 13.04.2025 18:31:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductPackagingType](
	[Id] [tinyint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_ProductPackagingType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 13.04.2025 18:31:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [tinyint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 13.04.2025 18:31:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[Patronymic] [nvarchar](80) NULL,
	[Email] [nvarchar](256) NOT NULL,
	[Password] [nvarchar](256) NOT NULL,
	[RoleId] [tinyint] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Company] ON 

INSERT [dbo].[Company] ([Id], [Name], [Address], [DirectorFio]) VALUES (1, N'ООО "Мясной мир"', N'г. Москва, ул. Амурская, д. 1а', N'Иванов Алексей Сергеевич')
INSERT [dbo].[Company] ([Id], [Name], [Address], [DirectorFio]) VALUES (2, N'ООО "Хлебный дом"', N'г. Москва, ул. Петрозаводская, д. 7', N'Смирнова Екатерина Валерьевна')
INSERT [dbo].[Company] ([Id], [Name], [Address], [DirectorFio]) VALUES (3, N'ОАО "Молоко с поля"', N'г. Москва, ул. Ленина, д. 37а', N'Петрова Мария Николаевна')
INSERT [dbo].[Company] ([Id], [Name], [Address], [DirectorFio]) VALUES (5, N'ООО "Овощной уголок"', N'г. Москва, ул. Солнечная, д. 16', N'Федоров Дмитрий Алексеевич')
INSERT [dbo].[Company] ([Id], [Name], [Address], [DirectorFio]) VALUES (6, N'ОАО "Сладкие мгновения"', N'г. Москва, ул. Южная, д. 13', N'Кузнецова Анна Викторовна')
SET IDENTITY_INSERT [dbo].[Company] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([Id], [Name], [GroupId], [PackagingTypeId]) VALUES (1, N'Куриное филе', 1, 2)
INSERT [dbo].[Product] ([Id], [Name], [GroupId], [PackagingTypeId]) VALUES (2, N'Хлеб ржаной', 2, 1)
INSERT [dbo].[Product] ([Id], [Name], [GroupId], [PackagingTypeId]) VALUES (3, N'Йогурт греческий', 3, 2)
INSERT [dbo].[Product] ([Id], [Name], [GroupId], [PackagingTypeId]) VALUES (4, N'Помидоры черри', 4, 2)
INSERT [dbo].[Product] ([Id], [Name], [GroupId], [PackagingTypeId]) VALUES (5, N'Шоколадные конфеты', 5, 4)
INSERT [dbo].[Product] ([Id], [Name], [GroupId], [PackagingTypeId]) VALUES (6, N'Говяжий стейк', 1, 2)
INSERT [dbo].[Product] ([Id], [Name], [GroupId], [PackagingTypeId]) VALUES (7, N'Булочка с корицей', 2, 1)
INSERT [dbo].[Product] ([Id], [Name], [GroupId], [PackagingTypeId]) VALUES (8, N'Творог', 3, 2)
INSERT [dbo].[Product] ([Id], [Name], [GroupId], [PackagingTypeId]) VALUES (9, N'Картофель', 4, 2)
INSERT [dbo].[Product] ([Id], [Name], [GroupId], [PackagingTypeId]) VALUES (10, N'Торт "Наполеон"', 5, 1)
INSERT [dbo].[Product] ([Id], [Name], [GroupId], [PackagingTypeId]) VALUES (11, N'Ветчина', 1, 5)
INSERT [dbo].[Product] ([Id], [Name], [GroupId], [PackagingTypeId]) VALUES (12, N'Кефир', 1, 2)
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductGroup] ON 

INSERT [dbo].[ProductGroup] ([Id], [Name]) VALUES (1, N'Мясо и мясопродукты')
INSERT [dbo].[ProductGroup] ([Id], [Name]) VALUES (2, N'Хлеб и хлебобулочные изделия')
INSERT [dbo].[ProductGroup] ([Id], [Name]) VALUES (3, N'Молоко и молочные продукты')
INSERT [dbo].[ProductGroup] ([Id], [Name]) VALUES (4, N'Овощи и фрукты')
INSERT [dbo].[ProductGroup] ([Id], [Name]) VALUES (5, N'Кондитерские изделия')
SET IDENTITY_INSERT [dbo].[ProductGroup] OFF
GO
INSERT [dbo].[ProductOutput] ([CompanyId], [ProductId], [ProductAmount]) VALUES (1, 1, 880)
INSERT [dbo].[ProductOutput] ([CompanyId], [ProductId], [ProductAmount]) VALUES (1, 11, 2510)
INSERT [dbo].[ProductOutput] ([CompanyId], [ProductId], [ProductAmount]) VALUES (2, 2, 570)
INSERT [dbo].[ProductOutput] ([CompanyId], [ProductId], [ProductAmount]) VALUES (5, 4, 2650)
INSERT [dbo].[ProductOutput] ([CompanyId], [ProductId], [ProductAmount]) VALUES (5, 9, 5555)
INSERT [dbo].[ProductOutput] ([CompanyId], [ProductId], [ProductAmount]) VALUES (6, 5, 2025)
INSERT [dbo].[ProductOutput] ([CompanyId], [ProductId], [ProductAmount]) VALUES (6, 10, 430)
GO
SET IDENTITY_INSERT [dbo].[ProductPackagingType] ON 

INSERT [dbo].[ProductPackagingType] ([Id], [Name]) VALUES (1, N'Картонная')
INSERT [dbo].[ProductPackagingType] ([Id], [Name]) VALUES (2, N'Пластиковая')
INSERT [dbo].[ProductPackagingType] ([Id], [Name]) VALUES (3, N'Стеклянная')
INSERT [dbo].[ProductPackagingType] ([Id], [Name]) VALUES (4, N'Металлическая')
INSERT [dbo].[ProductPackagingType] ([Id], [Name]) VALUES (5, N'Дой-пак')
SET IDENTITY_INSERT [dbo].[ProductPackagingType] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([Id], [Name]) VALUES (1, N'Администратор')
INSERT [dbo].[Role] ([Id], [Name]) VALUES (2, N'Менеджер')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [LastName], [FirstName], [Patronymic], [Email], [Password], [RoleId]) VALUES (1, N'Иванов', N'Петр', N'Дмитриевич', N'ivanov.p@mail.ru', N'1234', 1)
INSERT [dbo].[User] ([Id], [LastName], [FirstName], [Patronymic], [Email], [Password], [RoleId]) VALUES (2, N'Корявый', N'Эдуард', NULL, N'eduardk@gmail.com', N'edkor', 2)
INSERT [dbo].[User] ([Id], [LastName], [FirstName], [Patronymic], [Email], [Password], [RoleId]) VALUES (3, N'Киселев', N'Илья', N'Андреевич', N'ilyailyailyailya@yandex.ru', N'nissan123', 2)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_ProductGroup] FOREIGN KEY([GroupId])
REFERENCES [dbo].[ProductGroup] ([Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_ProductGroup]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_ProductPackagingType] FOREIGN KEY([PackagingTypeId])
REFERENCES [dbo].[ProductPackagingType] ([Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_ProductPackagingType]
GO
ALTER TABLE [dbo].[ProductOutput]  WITH CHECK ADD  CONSTRAINT [FK_ProductOutput_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([Id])
GO
ALTER TABLE [dbo].[ProductOutput] CHECK CONSTRAINT [FK_ProductOutput_Company]
GO
ALTER TABLE [dbo].[ProductOutput]  WITH CHECK ADD  CONSTRAINT [FK_ProductOutput_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[ProductOutput] CHECK CONSTRAINT [FK_ProductOutput_Product]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
USE [master]
GO
ALTER DATABASE [FoodProducts] SET  READ_WRITE 
GO
