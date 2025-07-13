USE [master]
GO
/****** Object:  Database [aspnet-CrfDesign.Server.WebAPI-D7DD320B-A2C5-40A7-8965-595F58679CED]    Script Date: 7/12/2025 2:05:07 PM ******/
CREATE DATABASE CrfDesign
go
USE CrfDesign
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 7/12/2025 2:05:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 7/12/2025 2:05:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 7/12/2025 2:05:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 7/12/2025 2:05:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 7/12/2025 2:05:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 7/12/2025 2:05:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 7/12/2025 2:05:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[DoctorNumber] [int] NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[Phone] [nvarchar](max) NULL,
	[QuickLookId] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 7/12/2025 2:05:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CrfOptionCategories]    Script Date: 7/12/2025 2:05:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CrfOptionCategories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
	[ModifiedDateTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_CrfOptionCategories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CrfOptions]    Script Date: 7/12/2025 2:05:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CrfOptions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
	[ModifiedDateTime] [datetime2](7) NOT NULL,
	[CrfOptionCategoryId] [int] NULL,
	[CrfPageComponentId] [int] NULL,
 CONSTRAINT [PK_CrfOptions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CrfPageComponents]    Script Date: 7/12/2025 2:05:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CrfPageComponents](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CRFPageId] [int] NOT NULL,
	[QuestionText] [nvarchar](max) NULL,
	[RenderType] [nvarchar](max) NULL,
	[QuestionTypeId] [int] NULL,
	[IsRequired] [bit] NOT NULL,
	[ValidationPattern] [nvarchar](max) NULL,
	[CategoryId] [int] NULL,
	[CategoryName] [nvarchar](100) NULL,
	[Name] [nvarchar](50) NULL,
	[IsDeleted] [bit] NULL,
	[ModifiedDateTime] [datetime2](7) NULL,
 CONSTRAINT [PK_CrfPageComponents] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CrfPages]    Script Date: 7/12/2025 2:05:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CrfPages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudyId] [int] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[IsLockedForChanges] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[ModifiedDateTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_CrfPages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuestionTypes]    Script Date: 7/12/2025 2:05:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
	[ModifiedDateTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_QuestionTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'00000000000000_CreateIdentitySchema', N'5.0.17')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250417195454_InitTables17Apr2025', N'5.0.17')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250520104021_Update20May2025', N'5.0.17')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250701061327_UserDetails', N'5.0.17')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'2ad02ffc-994e-4385-905d-df7d8db21dbf', N'DataMonitor', N'DATAMONITOR', N'2760aabf-d80a-43fe-8870-ccdf211edc03')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'97c17158-dc85-404d-84bc-0336ba17ae04', N'Investigator', N'INVESTIGATOR', N'4750b24a-35c0-4e65-9b88-3285523ae89f')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'aa9fa444-6946-48e2-8a8f-1de85c3d81e2', N'Admin', N'ADMIN', N'ae219eee-eaa6-4e64-bdd0-5917062c1a21')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'b076016d-7881-468c-9d6c-39e59df1218c', N'InternationalReviewBoard', N'INTERNATIONALREVIEWBOARD', N'57af0438-9712-422c-afea-23a89e74925d')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'cf3597ae-937b-41c3-a858-3899095b4ec8', N'ClinicalTrialLeader', N'CLINICALTRIALLEADER', N'460dd139-3802-4250-9e4a-e83a981d0da4')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'f8e44894-9136-454f-adc4-6b32fe87b52f', N'SiteManager', N'SITEMANAGER', N'c9231e1f-1d96-4448-9814-761cc8757b95')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'88ace8ee-37a9-4e10-8a62-f10a1483ea80', N'2ad02ffc-994e-4385-905d-df7d8db21dbf')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'88ace8ee-37a9-4e10-8a62-f10a1483ea80', N'97c17158-dc85-404d-84bc-0336ba17ae04')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'88ace8ee-37a9-4e10-8a62-f10a1483ea80', N'aa9fa444-6946-48e2-8a8f-1de85c3d81e2')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'88ace8ee-37a9-4e10-8a62-f10a1483ea80', N'b076016d-7881-468c-9d6c-39e59df1218c')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'88ace8ee-37a9-4e10-8a62-f10a1483ea80', N'cf3597ae-937b-41c3-a858-3899095b4ec8')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'88ace8ee-37a9-4e10-8a62-f10a1483ea80', N'f8e44894-9136-454f-adc4-6b32fe87b52f')
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [DoctorNumber], [FirstName], [LastName], [Phone], [QuickLookId]) 
VALUES (N'88ace8ee-37a9-4e10-8a62-f10a1483ea80', N'rm13rotem@gmail.com', N'RM13ROTEM@GMAIL.COM',
N'rm13rotem@gmail.com', N'RM13ROTEM@GMAIL.COM', 1,
N'AQAAAAEAACcQAAAAEJmRww270KtItTk6AICN+2nCJRIT9xBH3EktgKu47WgS+cOU9dr+M4PWuTe7BPUInA==',
N'I44IZEMQB7OWEW3TCWSFPFWZDN44CNCP', N'b53512c3-3b90-4976-9a04-df0badb797ed', N'+972528829604', 
0, 0, NULL, 1, 0, 0, N'Rotem', N'Meron', NULL, N'user102000')
GO
SET IDENTITY_INSERT [dbo].[CrfOptionCategories] ON 
GO
INSERT [dbo].[CrfOptionCategories] ([Id], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (1, N'yesno', 0, CAST(N'2025-05-18T18:13:00.0000000' AS DateTime2))
GO
INSERT [dbo].[CrfOptionCategories] ([Id], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (2, N'yesNoExplain', 0, CAST(N'2025-05-21T12:26:00.0000000' AS DateTime2))
GO
INSERT [dbo].[CrfOptionCategories] ([Id], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (3, N'gender', 0, CAST(N'2025-05-25T14:56:00.0000000' AS DateTime2))
GO
INSERT [dbo].[CrfOptionCategories] ([Id], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (4, N'race', 0, CAST(N'2025-05-25T15:03:00.0000000' AS DateTime2))
GO
INSERT [dbo].[CrfOptionCategories] ([Id], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (5, N'ethnicity_hispanic', 0, CAST(N'2025-05-25T15:05:00.0000000' AS DateTime2))
GO
INSERT [dbo].[CrfOptionCategories] ([Id], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (6, N'Severity', 0, CAST(N'2025-05-27T17:41:00.0000000' AS DateTime2))
GO
INSERT [dbo].[CrfOptionCategories] ([Id], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (7, N'Related', 0, CAST(N'2025-05-27T17:41:00.0000000' AS DateTime2))
GO
INSERT [dbo].[CrfOptionCategories] ([Id], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (8, N'ActionTaken', 0, CAST(N'2025-05-27T17:42:00.0000000' AS DateTime2))
GO
INSERT [dbo].[CrfOptionCategories] ([Id], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (9, N'Outcome', 0, CAST(N'2025-05-27T17:42:00.0000000' AS DateTime2))
GO
INSERT [dbo].[CrfOptionCategories] ([Id], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (10, N'withdrawalReason', 0, CAST(N'2025-05-28T10:28:00.0000000' AS DateTime2))
GO
INSERT [dbo].[CrfOptionCategories] ([Id], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (11, N'InclusionExclusion', 0, CAST(N'2025-06-03T15:17:00.0000000' AS DateTime2))
GO
INSERT [dbo].[CrfOptionCategories] ([Id], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (12, N'HeightUnit', 0, CAST(N'2025-06-03T20:25:00.0000000' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[CrfOptionCategories] OFF
GO
SET IDENTITY_INSERT [dbo].[CrfOptions] ON 
GO
INSERT [dbo].[CrfOptions] ([Id], [Name], [IsDeleted], [ModifiedDateTime], [CrfOptionCategoryId], [CrfPageComponentId]) VALUES (2, N'Yes', 0, CAST(N'2025-05-05T00:00:00.0000000' AS DateTime2), 1, NULL)
GO
INSERT [dbo].[CrfOptions] ([Id], [Name], [IsDeleted], [ModifiedDateTime], [CrfOptionCategoryId], [CrfPageComponentId]) VALUES (4, N'No', 0, CAST(N'2025-05-21T08:28:31.5210000' AS DateTime2), 1, NULL)
GO
INSERT [dbo].[CrfOptions] ([Id], [Name], [IsDeleted], [ModifiedDateTime], [CrfOptionCategoryId], [CrfPageComponentId]) VALUES (5, N'Yes', 0, CAST(N'2025-05-23T09:22:51.4020000' AS DateTime2), 2, NULL)
GO
INSERT [dbo].[CrfOptions] ([Id], [Name], [IsDeleted], [ModifiedDateTime], [CrfOptionCategoryId], [CrfPageComponentId]) VALUES (6, N'No (explain, if No)', 0, CAST(N'2025-05-23T09:23:12.5720000' AS DateTime2), 2, NULL)
GO
INSERT [dbo].[CrfOptions] ([Id], [Name], [IsDeleted], [ModifiedDateTime], [CrfOptionCategoryId], [CrfPageComponentId]) VALUES (7, N'Male', 0, CAST(N'2025-05-25T15:05:02.2650000' AS DateTime2), 3, NULL)
GO
INSERT [dbo].[CrfOptions] ([Id], [Name], [IsDeleted], [ModifiedDateTime], [CrfOptionCategoryId], [CrfPageComponentId]) VALUES (8, N'Hispanic or Latino', 0, CAST(N'2025-05-25T15:06:05.7910000' AS DateTime2), 5, NULL)
GO
INSERT [dbo].[CrfOptions] ([Id], [Name], [IsDeleted], [ModifiedDateTime], [CrfOptionCategoryId], [CrfPageComponentId]) VALUES (9, N'Female', 0, CAST(N'2025-05-25T15:05:18.2550000' AS DateTime2), 3, NULL)
GO
INSERT [dbo].[CrfOptions] ([Id], [Name], [IsDeleted], [ModifiedDateTime], [CrfOptionCategoryId], [CrfPageComponentId]) VALUES (10, N'Not Hispanic or Latino', 0, CAST(N'2025-05-25T15:06:53.0490000' AS DateTime2), 5, NULL)
GO
INSERT [dbo].[CrfOptions] ([Id], [Name], [IsDeleted], [ModifiedDateTime], [CrfOptionCategoryId], [CrfPageComponentId]) VALUES (11, N'White', 0, CAST(N'2025-05-25T15:07:43.6900000' AS DateTime2), 4, NULL)
GO
INSERT [dbo].[CrfOptions] ([Id], [Name], [IsDeleted], [ModifiedDateTime], [CrfOptionCategoryId], [CrfPageComponentId]) VALUES (12, N'Black or African American', 0, CAST(N'2025-05-25T15:08:10.1170000' AS DateTime2), 4, NULL)
GO
INSERT [dbo].[CrfOptions] ([Id], [Name], [IsDeleted], [ModifiedDateTime], [CrfOptionCategoryId], [CrfPageComponentId]) VALUES (13, N'Asian', 0, CAST(N'2025-05-25T15:08:35.5390000' AS DateTime2), 4, NULL)
GO
INSERT [dbo].[CrfOptions] ([Id], [Name], [IsDeleted], [ModifiedDateTime], [CrfOptionCategoryId], [CrfPageComponentId]) VALUES (14, N'Native Hawaiian or Other Pacific Islander', 0, CAST(N'2025-05-25T15:08:57.0830000' AS DateTime2), 4, NULL)
GO
INSERT [dbo].[CrfOptions] ([Id], [Name], [IsDeleted], [ModifiedDateTime], [CrfOptionCategoryId], [CrfPageComponentId]) VALUES (15, N'American Indian/Alaskan Native', 0, CAST(N'2025-05-25T15:36:21.0000000' AS DateTime2), 4, NULL)
GO
INSERT [dbo].[CrfOptions] ([Id], [Name], [IsDeleted], [ModifiedDateTime], [CrfOptionCategoryId], [CrfPageComponentId]) VALUES (16, N'Other', 0, CAST(N'2025-05-25T15:36:36.5040000' AS DateTime2), 4, NULL)
GO
INSERT [dbo].[CrfOptions] ([Id], [Name], [IsDeleted], [ModifiedDateTime], [CrfOptionCategoryId], [CrfPageComponentId]) VALUES (17, N'Mild', 0, CAST(N'2025-05-27T17:42:29.6280000' AS DateTime2), 6, NULL)
GO
INSERT [dbo].[CrfOptions] ([Id], [Name], [IsDeleted], [ModifiedDateTime], [CrfOptionCategoryId], [CrfPageComponentId]) VALUES (18, N'Moderate', 0, CAST(N'2025-05-27T17:42:58.3230000' AS DateTime2), 6, NULL)
GO
INSERT [dbo].[CrfOptions] ([Id], [Name], [IsDeleted], [ModifiedDateTime], [CrfOptionCategoryId], [CrfPageComponentId]) VALUES (19, N'Severe', 0, CAST(N'2025-05-27T17:43:04.5200000' AS DateTime2), 6, NULL)
GO
INSERT [dbo].[CrfOptions] ([Id], [Name], [IsDeleted], [ModifiedDateTime], [CrfOptionCategoryId], [CrfPageComponentId]) VALUES (20, N'Not Related', 0, CAST(N'2025-05-27T17:44:02.0500000' AS DateTime2), 7, NULL)
GO
INSERT [dbo].[CrfOptions] ([Id], [Name], [IsDeleted], [ModifiedDateTime], [CrfOptionCategoryId], [CrfPageComponentId]) VALUES (21, N'Unlikely', 0, CAST(N'2025-05-27T17:44:44.8020000' AS DateTime2), 7, NULL)
GO
INSERT [dbo].[CrfOptions] ([Id], [Name], [IsDeleted], [ModifiedDateTime], [CrfOptionCategoryId], [CrfPageComponentId]) VALUES (22, N'Possibly Related', 0, CAST(N'2025-05-27T17:44:48.7050000' AS DateTime2), 7, NULL)
GO
INSERT [dbo].[CrfOptions] ([Id], [Name], [IsDeleted], [ModifiedDateTime], [CrfOptionCategoryId], [CrfPageComponentId]) VALUES (23, N'Probably Related', 0, CAST(N'2025-05-27T17:44:58.7050000' AS DateTime2), 7, NULL)
GO
INSERT [dbo].[CrfOptions] ([Id], [Name], [IsDeleted], [ModifiedDateTime], [CrfOptionCategoryId], [CrfPageComponentId]) VALUES (24, N'Definitely Related', 0, CAST(N'2025-05-27T17:45:02.7340000' AS DateTime2), 7, NULL)
GO
INSERT [dbo].[CrfOptions] ([Id], [Name], [IsDeleted], [ModifiedDateTime], [CrfOptionCategoryId], [CrfPageComponentId]) VALUES (25, N'None', 0, CAST(N'2025-05-27T17:46:29.2170000' AS DateTime2), 8, NULL)
GO
INSERT [dbo].[CrfOptions] ([Id], [Name], [IsDeleted], [ModifiedDateTime], [CrfOptionCategoryId], [CrfPageComponentId]) VALUES (26, N'Recovered', 0, CAST(N'2025-05-27T17:46:59.3480000' AS DateTime2), 9, NULL)
GO
INSERT [dbo].[CrfOptions] ([Id], [Name], [IsDeleted], [ModifiedDateTime], [CrfOptionCategoryId], [CrfPageComponentId]) VALUES (27, N'Other Drug Therapy, Specify as Con. Med.', 0, CAST(N'2025-05-27T17:47:53.9110000' AS DateTime2), 8, NULL)
GO
INSERT [dbo].[CrfOptions] ([Id], [Name], [IsDeleted], [ModifiedDateTime], [CrfOptionCategoryId], [CrfPageComponentId]) VALUES (28, N'Other Treatment or Procedure, Specify', 0, CAST(N'2025-05-27T17:47:57.5660000' AS DateTime2), 8, NULL)
GO
INSERT [dbo].[CrfOptions] ([Id], [Name], [IsDeleted], [ModifiedDateTime], [CrfOptionCategoryId], [CrfPageComponentId]) VALUES (29, N'Discontinuation of Drug Therapy', 0, CAST(N'2025-05-27T17:48:46.5010000' AS DateTime2), 8, NULL)
GO
INSERT [dbo].[CrfOptions] ([Id], [Name], [IsDeleted], [ModifiedDateTime], [CrfOptionCategoryId], [CrfPageComponentId]) VALUES (30, N'Discontinuation from Study', 0, CAST(N'2025-05-27T17:50:50.0270000' AS DateTime2), 8, NULL)
GO
INSERT [dbo].[CrfOptions] ([Id], [Name], [IsDeleted], [ModifiedDateTime], [CrfOptionCategoryId], [CrfPageComponentId]) VALUES (31, N'Continuing without Treatment', 0, CAST(N'2025-05-27T17:52:17.2370000' AS DateTime2), 9, NULL)
GO
INSERT [dbo].[CrfOptions] ([Id], [Name], [IsDeleted], [ModifiedDateTime], [CrfOptionCategoryId], [CrfPageComponentId]) VALUES (32, N'Continuing with Treatment', 0, CAST(N'2025-05-27T17:52:29.8270000' AS DateTime2), 9, NULL)
GO
INSERT [dbo].[CrfOptions] ([Id], [Name], [IsDeleted], [ModifiedDateTime], [CrfOptionCategoryId], [CrfPageComponentId]) VALUES (33, N'Death', 0, CAST(N'2025-05-27T17:52:34.1180000' AS DateTime2), 9, NULL)
GO
INSERT [dbo].[CrfOptions] ([Id], [Name], [IsDeleted], [ModifiedDateTime], [CrfOptionCategoryId], [CrfPageComponentId]) VALUES (34, N'NA. Completed Study', 0, CAST(N'2025-05-28T10:31:33.7590000' AS DateTime2), 10, NULL)
GO
INSERT [dbo].[CrfOptions] ([Id], [Name], [IsDeleted], [ModifiedDateTime], [CrfOptionCategoryId], [CrfPageComponentId]) VALUES (35, N'Adverse Event, specify:', 0, CAST(N'2025-05-28T10:34:43.5520000' AS DateTime2), 10, NULL)
GO
INSERT [dbo].[CrfOptions] ([Id], [Name], [IsDeleted], [ModifiedDateTime], [CrfOptionCategoryId], [CrfPageComponentId]) VALUES (36, N'Terminated by Sponsor', 0, CAST(N'2025-05-28T11:50:58.1030000' AS DateTime2), 10, NULL)
GO
INSERT [dbo].[CrfOptions] ([Id], [Name], [IsDeleted], [ModifiedDateTime], [CrfOptionCategoryId], [CrfPageComponentId]) VALUES (37, N' Consent Withdrawn', 0, CAST(N'2025-05-28T11:56:24.5050000' AS DateTime2), 10, NULL)
GO
INSERT [dbo].[CrfOptions] ([Id], [Name], [IsDeleted], [ModifiedDateTime], [CrfOptionCategoryId], [CrfPageComponentId]) VALUES (38, N' Lost to Follow–up', 0, CAST(N'2025-05-28T12:00:07.6790000' AS DateTime2), 10, NULL)
GO
INSERT [dbo].[CrfOptions] ([Id], [Name], [IsDeleted], [ModifiedDateTime], [CrfOptionCategoryId], [CrfPageComponentId]) VALUES (39, N'Other, specify: ', 0, CAST(N'2025-05-28T12:02:27.5980000' AS DateTime2), 10, NULL)
GO
INSERT [dbo].[CrfOptions] ([Id], [Name], [IsDeleted], [ModifiedDateTime], [CrfOptionCategoryId], [CrfPageComponentId]) VALUES (40, N'Inclusion', 0, CAST(N'2025-06-03T15:18:00.0000000' AS DateTime2), 11, NULL)
GO
INSERT [dbo].[CrfOptions] ([Id], [Name], [IsDeleted], [ModifiedDateTime], [CrfOptionCategoryId], [CrfPageComponentId]) VALUES (41, N'Exclusion', 0, CAST(N'2025-06-03T15:19:13.6030000' AS DateTime2), 11, NULL)
GO
INSERT [dbo].[CrfOptions] ([Id], [Name], [IsDeleted], [ModifiedDateTime], [CrfOptionCategoryId], [CrfPageComponentId]) VALUES (42, N'cm', 0, CAST(N'2025-06-04T12:53:55.0670000' AS DateTime2), 12, NULL)
GO
INSERT [dbo].[CrfOptions] ([Id], [Name], [IsDeleted], [ModifiedDateTime], [CrfOptionCategoryId], [CrfPageComponentId]) VALUES (43, N'inch', 0, CAST(N'2025-06-04T12:54:30.4510000' AS DateTime2), 12, NULL)
GO
SET IDENTITY_INSERT [dbo].[CrfOptions] OFF
GO
SET IDENTITY_INSERT [dbo].[CrfPageComponents] ON 
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (1, 4, N'Femal. Age 25 and 65 years old', N'yesno', 3, 1, NULL, 1, NULL, N'INC[0]', 0, CAST(N'2025-06-06T06:04:49.2528913' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (2, 4, N'The subject smokes > 9 cigarettes per day (average daily consumption during past month). ', N'SingleChoise', 3, 1, NULL, 1, NULL, N'INC[1]', 0, CAST(N'2025-06-06T06:04:57.6154538' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (3, 4, N'The subject has smoked daily > 1 year', N'yesno', 3, 1, NULL, 1, NULL, N'INC[2]', 0, CAST(N'2025-06-06T06:05:03.7748234' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (4, 4, N'The subject is motivated to quit smoking with the help of an alternative', N'yesno', 3, 1, NULL, 1, NULL, N'INC[3]', 0, CAST(N'2025-06-06T06:05:09.4405303' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (5, 6, N'Date of Information Session', N'Date', 4, 1, NULL, 1, NULL, N'VisitDate', 0, CAST(N'2025-06-06T06:13:33.2975274' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (6, 6, N'Did the subject attend the Information Session?', N'yesNoExplain', 3, 1, NULL, 2, NULL, N'IsAttended', 0, CAST(N'2025-06-06T06:13:35.8106087' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (7, 6, N'Comments', NULL, 1, 1, NULL, 1, NULL, N'Comments', 0, CAST(N'2025-06-06T06:13:39.3655367' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (8, 4, N'The subject is in good general health as evident by Medical History, physical examination, routine blood chemistry (Hb, total WBC, creatinine, electrolytes), and an ECG ', N'yesno', 3, 1, NULL, 1, NULL, N'INC[4]', 0, CAST(N'2025-06-06T06:05:15.8017867' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (9, 12, N'Femal. Age 25 and 65 years old', N'yesno', 3, 1, NULL, 1, N'yesno', N'y', 0, CAST(N'2025-05-21T13:55:35.0867552' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (10, 12, N'The subject smokes > 9 cigarettes per day (average daily consumption during past month). ', N'SingleChoise', 3, 1, NULL, NULL, NULL, N'9', 0, CAST(N'2025-05-21T13:55:59.2211987' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (11, 12, N'Femal. Age 25 and 65 years old', N'yesno', 3, 1, NULL, 1, N'yesno', N'y', 0, CAST(N'2025-05-21T13:56:03.3353174' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (12, 12, N'Femal. Age 25 and 65 years old', N'yesno', 3, 1, NULL, 1, N'yesno', N'y', 0, CAST(N'2025-05-21T13:56:03.6046648' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (13, 12, N'Femal. Age 25 and 65 years old', N'yesno', 3, 1, NULL, 1, N'yesno', N'y', 0, CAST(N'2025-05-21T13:56:03.6258883' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (14, 1, N'Date dd/mm/yyyy', NULL, 4, 0, NULL, 0, NULL, N'DateOfVisit', 0, CAST(N'2025-06-06T06:05:51.3912570' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (15, 1, N'Date of Birth dd/mm/yyyy', NULL, 4, 0, NULL, 0, NULL, N'Date_of_Birth', 0, CAST(N'2025-06-06T06:05:56.2430369' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (16, 1, N'Gender', NULL, 3, 0, NULL, 3, NULL, N'Gender', 0, CAST(N'2025-06-06T06:05:59.8859999' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (17, 1, N'Ethnicity', NULL, 3, 0, NULL, 5, NULL, N'Ethnicity', 0, CAST(N'2025-06-06T06:06:06.1079501' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (18, 1, N'Race', NULL, 3, 0, NULL, 4, NULL, N'Race', 0, CAST(N'2025-06-06T06:06:14.6547471' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (19, 1, N'Other Race (Text)', NULL, 1, 0, NULL, 0, NULL, N'Race_Other', 0, CAST(N'2025-06-06T06:06:19.5655783' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (20, 13, NULL, N'yesno', 1, 0, NULL, 1, NULL, N'something', 0, CAST(N'2025-06-06T06:15:54.1910160' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (21, 14, N'Was Subject randomized?', N'yesno', 3, 0, NULL, 1, NULL, N'IsRandomized', 0, CAST(N'2025-06-06T06:16:08.0826033' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (22, 15, N'Onset Date & Time', N'yesno', 4, 0, NULL, 1, NULL, N'OnsetDateTime', 0, CAST(N'2025-06-06T06:16:27.1718134' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (23, 15, N'Resolved Date & Time', N'yesno', 4, 0, NULL, 1, NULL, N'ResolvedDateTime', 0, CAST(N'2025-06-06T06:16:27.7340552' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (24, 15, N'Onset Date & Time', N'yesno', 3, 0, NULL, 1, NULL, N'IsSerious', 0, CAST(N'2025-06-06T06:16:30.0499954' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (25, 15, N'Onset Date & Time', N'severity', 3, 0, NULL, 6, NULL, N'Severity', 0, CAST(N'2025-06-06T06:16:29.7817986' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (26, 15, N'Relationship to Product', N'related', 3, 0, NULL, 7, NULL, N'Related', 0, CAST(N'2025-06-06T06:16:30.3151369' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (27, 15, N'Action Taken', N'yesno', 3, 0, NULL, 8, NULL, N'ActionTaken', 0, CAST(N'2025-06-06T06:16:32.5016747' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (28, 15, N'Outcome to Date', N'yesno', 3, 0, NULL, 9, NULL, N'Outcome', 0, CAST(N'2025-06-06T06:16:34.1563170' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (29, 15, N'Description', N'yesno', 1, 1, NULL, 0, NULL, N'AdverseEventDescription', 0, CAST(N'2025-06-06T06:16:37.6156760' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (30, 15, N'Adverse Event Exact identification by Medical Dictionary', N'yesno', 1, 0, NULL, 0, NULL, N'AdverseEventIdByMed', 0, CAST(N'2025-06-06T06:16:38.6797563' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (31, 15, N'Other Drug Taken - Specify', N'yesno', 1, 0, NULL, 0, NULL, N'Action_OtherDrug_Specify', 0, CAST(N'2025-06-06T06:16:40.2270250' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (32, 16, N'Date the subject completed OR withdrew from the study', N'yesno', 4, 0, NULL, 0, NULL, N'CompletionDate', 0, CAST(N'2025-06-06T06:17:13.2466144' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (33, 16, N'Reason for Withdrawal (check one):', N'yesno', 3, 0, NULL, 10, NULL, N'ReasonWithdrawn', 0, CAST(N'2025-06-06T06:17:13.8287226' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (34, 16, N'Adverse Event, specifiy:', N'yesno', 1, 0, NULL, 0, NULL, N'AdverseEventSpecify', 0, CAST(N'2025-06-06T06:17:20.9423981' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (35, 16, N'Other, specifiy:', NULL, 1, 0, NULL, 0, NULL, N'OtherSpecify', 0, CAST(N'2025-06-06T06:18:12.0508037' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (36, 16, N'Investigator Comment', NULL, 1, 0, NULL, 0, NULL, N'InvestigatorComment', 0, CAST(N'2025-06-06T06:18:28.6254825' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (37, 4, N'The subject practices, by self report, good oral hygiene (including brushing teeth) at least twice per day and having regular dental check ups', N'yesno', 3, 1, NULL, 1, NULL, N'INC[5]', 0, CAST(N'2025-06-06T06:05:23.9465156' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (38, 4, N'The subject agrees to comply with the requirements of the protocol and complete study measures', N'yesno', 3, 1, NULL, 1, NULL, N'INC[7]', 0, CAST(N'2025-06-06T06:05:36.2551045' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (39, 5, N'The subject is a current user of ST (defined as daily usage during more than 1 week within past 6 months) or is unable to refrain from NRT or any other non-protocol treatment during the study. Use of pipes, cigars, cigarillos, snuff, and chewing tobacco is also prohibited during the study. ', NULL, 3, 1, NULL, 1, NULL, N'EXC[0]', 0, CAST(N'2025-06-06T06:11:25.6709488' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (40, 5, N'The subject is a female who is pregnant or lactating. ', NULL, 3, 1, NULL, 1, NULL, N'EXC[1]', 0, CAST(N'2025-06-06T06:12:11.7185098' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (41, 5, N'The subject has oral conditions that could potentially be made worse by use of study product, for instance, exposed dental cervices in the upper sulcus.', NULL, 3, 1, NULL, 1, NULL, N'EXC[2]', 0, CAST(N'2025-06-06T06:12:18.6825246' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (42, 5, N'The subject has used any type of pharmaceutical (including some psychotropics, e.g., wellbutrin) or other products for smoking cessation within the past 3 months. ', NULL, 3, 1, NULL, 1, NULL, N'EXC[3]', 0, CAST(N'2025-06-06T06:12:25.7770465' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (43, 5, N'The subject has a history of clinically significant renal, hepatic, neurological, or chronic pulmonary disease that in the judgment of the investigator precludes participation. ', NULL, 3, 1, NULL, 1, NULL, N'EXC[4]', 0, CAST(N'2025-06-06T06:12:40.4942571' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (44, 5, N'The subject has a history of cardiovascular disease, including myocardial infarction within the last 3 months, significant cardiac arrhythmias, or poorly controlled hypertension (defined as a diastolic pressure of more than 90 mm Hg or a systolic pressure of more than 140 mm Hg) that in the judgment of the investigator precludes participation.', NULL, 3, 1, NULL, 1, NULL, N'EXC[5]', 0, CAST(N'2025-06-06T06:12:48.3777884' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (45, 5, N'The subject has a history of alcohol or substance abuse or dependence other than cigarette smoking within the past year ', NULL, 3, 1, NULL, 1, NULL, N'EXC[6]', 0, CAST(N'2025-06-06T06:12:34.6523384' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (46, 5, N'Use of any illicit drug or smoked marijuana in the last 3 months. ', NULL, 3, 1, NULL, 1, NULL, N'EXC[7]', 0, CAST(N'2025-06-06T06:12:59.4387724' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (47, 5, N'The subject is unwilling to be randomized into active or placebo conditions, or be available for follow-up assessments. ', NULL, 3, 1, NULL, 1, NULL, N'EXC[8]', 0, CAST(N'2025-06-06T06:13:00.0198148' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (48, 5, N'The subject resides in a household where another member is currently participating in the study. ', NULL, 3, 1, NULL, 1, NULL, N'EXC[9]', 0, CAST(N'2025-06-06T06:13:15.6131131' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (49, 7, N'Date of Consent dd/mm/yyyy', NULL, 4, 1, NULL, 0, NULL, N'ConsentDate', 0, CAST(N'2025-06-06T06:14:03.7863867' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (50, 7, N'Did the subject meet all of the inclusion/exclusion criteria?', NULL, 3, 1, NULL, 1, NULL, N'IsIncExcMet', 0, CAST(N'2025-06-06T06:14:04.6203427' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (51, 7, N'If the subject did not meet all of the Inclusion/Exclusion criteria, provide criterion number and explanation below.', NULL, 1, 0, NULL, 0, NULL, N'IfNot', 0, CAST(N'2025-06-06T06:14:05.9450414' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (52, 7, N'Category (Inc/Exc)?', NULL, 3, 0, NULL, 11, NULL, N'Category', 0, CAST(N'2025-06-06T06:14:16.1749225' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (53, 7, N'Inclusion/ Exclusion No. ', NULL, 6, 0, NULL, 0, NULL, N'IncExcQuestionNumber', 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (54, 7, N'Explanation', NULL, 1, 0, NULL, 0, NULL, N'Explanation', 0, CAST(N'2025-06-06T06:14:36.4764939' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (55, 7, N'Exception Granted?', NULL, 3, 0, NULL, 1, NULL, N'IsExceptionGranted', 0, CAST(N'2025-06-06T06:14:24.5323799' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (56, 7, N'If Yes, Date of Granted Exception', NULL, 4, 0, NULL, 0, NULL, N'ExceptionDate', 0, CAST(N'2025-06-06T06:14:24.0691990' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (57, 8, N'Were Body Measurements Collected? ', NULL, 3, 0, NULL, 1, NULL, N'IsCollected', 0, CAST(N'2025-06-06T06:14:55.8708119' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (58, 8, N'Date', NULL, 4, 0, NULL, 0, NULL, N'VisitDate', 0, CAST(N'2025-06-06T06:14:57.8107634' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (59, 8, N'Height', NULL, 6, 0, NULL, 0, NULL, N'Height', 0, CAST(N'2025-06-06T06:14:59.5665255' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (60, 8, N'Unit of Height', NULL, 3, 0, NULL, 1, NULL, N'HeightUnit', 0, CAST(N'2025-06-06T06:15:01.8895993' AS DateTime2))
GO
INSERT [dbo].[CrfPageComponents] ([Id], [CRFPageId], [QuestionText], [RenderType], [QuestionTypeId], [IsRequired], [ValidationPattern], [CategoryId], [CategoryName], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (61, 2, N'ToDo - add components to this CRF', NULL, 1, 0, NULL, 1, NULL, N'PE[0]', 0, CAST(N'2025-06-06T06:07:48.0963975' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[CrfPageComponents] OFF
GO
SET IDENTITY_INSERT [dbo].[CrfPages] ON 
GO
INSERT [dbo].[CrfPages] ([Id], [StudyId], [Name], [Description], [CreatedAt], [IsLockedForChanges], [IsDeleted], [ModifiedDateTime]) VALUES (1, 1, N'Demographics', N'Demographics CRF page', CAST(N'2025-04-17T23:50:00.0000000' AS DateTime2), 0, 0, CAST(N'2025-04-17T23:50:00.0000000' AS DateTime2))
GO
INSERT [dbo].[CrfPages] ([Id], [StudyId], [Name], [Description], [CreatedAt], [IsLockedForChanges], [IsDeleted], [ModifiedDateTime]) VALUES (2, 1, N'Physical Examination', N'Physical Examination', CAST(N'2025-04-17T23:52:00.0000000' AS DateTime2), 0, 0, CAST(N'2025-04-17T23:52:00.0000000' AS DateTime2))
GO
INSERT [dbo].[CrfPages] ([Id], [StudyId], [Name], [Description], [CreatedAt], [IsLockedForChanges], [IsDeleted], [ModifiedDateTime]) VALUES (3, 1, N'Vital Signs', N'Measure the vital signs of the patient', CAST(N'2025-05-18T13:07:00.0000000' AS DateTime2), 0, 0, CAST(N'2025-05-18T13:07:00.0000000' AS DateTime2))
GO
INSERT [dbo].[CrfPages] ([Id], [StudyId], [Name], [Description], [CreatedAt], [IsLockedForChanges], [IsDeleted], [ModifiedDateTime]) VALUES (4, 1, N'Inclusion Criteria', N'Who is eligible to apply to the study', CAST(N'2025-05-18T13:25:00.0000000' AS DateTime2), 0, 0, CAST(N'2025-05-19T13:25:00.0000000' AS DateTime2))
GO
INSERT [dbo].[CrfPages] ([Id], [StudyId], [Name], [Description], [CreatedAt], [IsLockedForChanges], [IsDeleted], [ModifiedDateTime]) VALUES (5, 1, N'Exclusion Criteria', N'who is not eligible for study', CAST(N'2025-05-19T06:37:00.0000000' AS DateTime2), 0, 0, CAST(N'2025-05-19T06:46:00.0000000' AS DateTime2))
GO
INSERT [dbo].[CrfPages] ([Id], [StudyId], [Name], [Description], [CreatedAt], [IsLockedForChanges], [IsDeleted], [ModifiedDateTime]) VALUES (6, 1, N'Information Session', N'Informed consent explained', CAST(N'2025-05-19T06:51:00.0000000' AS DateTime2), 1, 0, CAST(N'2025-06-09T13:03:25.8449808' AS DateTime2))
GO
INSERT [dbo].[CrfPages] ([Id], [StudyId], [Name], [Description], [CreatedAt], [IsLockedForChanges], [IsDeleted], [ModifiedDateTime]) VALUES (7, 1, N'Subject Elegibility', N'Who is eligible to apply to the study', CAST(N'2025-05-18T13:25:00.0000000' AS DateTime2), 0, 0, CAST(N'2025-05-19T13:25:00.0000000' AS DateTime2))
GO
INSERT [dbo].[CrfPages] ([Id], [StudyId], [Name], [Description], [CreatedAt], [IsLockedForChanges], [IsDeleted], [ModifiedDateTime]) VALUES (8, 1, N'Body Measurements', N'Measuring things such as Height, Weight, ...', CAST(N'2025-05-18T13:25:00.0000000' AS DateTime2), 0, 0, CAST(N'2025-05-27T13:25:00.0000000' AS DateTime2))
GO
INSERT [dbo].[CrfPages] ([Id], [StudyId], [Name], [Description], [CreatedAt], [IsLockedForChanges], [IsDeleted], [ModifiedDateTime]) VALUES (9, 1, N'Vital Signs', N'Blood Pressure, Pulse, Heart Rate, ...', CAST(N'2025-05-18T13:25:00.0000000' AS DateTime2), 0, 0, CAST(N'2025-05-27T13:25:00.0000000' AS DateTime2))
GO
INSERT [dbo].[CrfPages] ([Id], [StudyId], [Name], [Description], [CreatedAt], [IsLockedForChanges], [IsDeleted], [ModifiedDateTime]) VALUES (10, 1, N'Laboratory Evaluations', N'Lab results', CAST(N'2025-05-18T13:25:00.0000000' AS DateTime2), 0, 0, CAST(N'2025-05-19T13:25:00.0000000' AS DateTime2))
GO
INSERT [dbo].[CrfPages] ([Id], [StudyId], [Name], [Description], [CreatedAt], [IsLockedForChanges], [IsDeleted], [ModifiedDateTime]) VALUES (11, 1, N'Urine Pregnancy Test', N'Quick Result', CAST(N'2025-05-18T13:25:00.0000000' AS DateTime2), 0, 0, CAST(N'2025-05-27T13:25:00.0000000' AS DateTime2))
GO
INSERT [dbo].[CrfPages] ([Id], [StudyId], [Name], [Description], [CreatedAt], [IsLockedForChanges], [IsDeleted], [ModifiedDateTime]) VALUES (12, 1, N'Medical History', N'Med History of Patient', CAST(N'2025-05-18T13:25:00.0000000' AS DateTime2), 0, 0, CAST(N'2025-05-19T13:25:00.0000000' AS DateTime2))
GO
INSERT [dbo].[CrfPages] ([Id], [StudyId], [Name], [Description], [CreatedAt], [IsLockedForChanges], [IsDeleted], [ModifiedDateTime]) VALUES (13, 1, N'ECG Test', N'12-LEAD ELECTROCARDIOGRAM REPORT', CAST(N'2025-05-27T13:51:00.0000000' AS DateTime2), 0, 0, CAST(N'2025-05-27T13:52:00.0000000' AS DateTime2))
GO
INSERT [dbo].[CrfPages] ([Id], [StudyId], [Name], [Description], [CreatedAt], [IsLockedForChanges], [IsDeleted], [ModifiedDateTime]) VALUES (14, 1, N'Randomization', N'Randomization of Patients', CAST(N'2025-05-27T13:51:00.0000000' AS DateTime2), 0, 0, CAST(N'2025-05-27T13:52:00.0000000' AS DateTime2))
GO
INSERT [dbo].[CrfPages] ([Id], [StudyId], [Name], [Description], [CreatedAt], [IsLockedForChanges], [IsDeleted], [ModifiedDateTime]) VALUES (15, 1, N'Adverse Events', N'Adverse Events of Patients', CAST(N'2025-05-27T13:51:00.0000000' AS DateTime2), 0, 0, CAST(N'2025-05-27T13:53:00.0000000' AS DateTime2))
GO
INSERT [dbo].[CrfPages] ([Id], [StudyId], [Name], [Description], [CreatedAt], [IsLockedForChanges], [IsDeleted], [ModifiedDateTime]) VALUES (16, 1, N'Study Completion', N'Termination of Study', CAST(N'2025-05-27T13:51:00.0000000' AS DateTime2), 0, 0, CAST(N'2025-05-27T13:53:00.0000000' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[CrfPages] OFF
GO
SET IDENTITY_INSERT [dbo].[QuestionTypes] ON 
GO
INSERT [dbo].[QuestionTypes] ([Id], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (1, N'Text', 0, CAST(N'2025-05-18T12:52:00.0000000' AS DateTime2))
GO
INSERT [dbo].[QuestionTypes] ([Id], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (2, N'MultipleChoice', 0, CAST(N'2025-05-18T12:53:00.0000000' AS DateTime2))
GO
INSERT [dbo].[QuestionTypes] ([Id], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (3, N'SingleChoice', 0, CAST(N'2025-05-18T12:53:00.0000000' AS DateTime2))
GO
INSERT [dbo].[QuestionTypes] ([Id], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (4, N'Date', 0, CAST(N'2025-05-18T12:53:00.0000000' AS DateTime2))
GO
INSERT [dbo].[QuestionTypes] ([Id], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (5, N'Checkbox', 0, CAST(N'2025-05-18T12:54:00.0000000' AS DateTime2))
GO
INSERT [dbo].[QuestionTypes] ([Id], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (6, N'Numeric', 0, CAST(N'2025-05-18T13:04:00.0000000' AS DateTime2))
GO
INSERT [dbo].[QuestionTypes] ([Id], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (7, N'Boolean', 0, CAST(N'2025-05-18T13:05:00.0000000' AS DateTime2))
GO
INSERT [dbo].[QuestionTypes] ([Id], [Name], [IsDeleted], [ModifiedDateTime]) VALUES (8, N'OpenSingleChoise', 0, CAST(N'2025-05-18T13:05:00.0000000' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[QuestionTypes] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 7/12/2025 2:05:08 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 7/12/2025 2:05:08 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 7/12/2025 2:05:08 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 7/12/2025 2:05:08 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 7/12/2025 2:05:08 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 7/12/2025 2:05:08 PM ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 7/12/2025 2:05:08 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CrfPageComponents_CRFPageId]    Script Date: 7/12/2025 2:05:08 PM ******/
CREATE NONCLUSTERED INDEX [IX_CrfPageComponents_CRFPageId] ON [dbo].[CrfPageComponents]
(
	[CRFPageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CrfPageComponents_QuestionTypeId]    Script Date: 7/12/2025 2:05:08 PM ******/
CREATE NONCLUSTERED INDEX [IX_CrfPageComponents_QuestionTypeId] ON [dbo].[CrfPageComponents]
(
	[QuestionTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetUsers] ADD  DEFAULT ((0)) FOR [DoctorNumber]
GO
ALTER TABLE [dbo].[CrfPageComponents] ADD  CONSTRAINT [DF_CrfPageComponents_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[CrfPageComponents] ADD  CONSTRAINT [DF_CrfPageComponents_ModifiedDateTime]  DEFAULT (getdate()) FOR [ModifiedDateTime]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[CrfOptions]  WITH CHECK ADD  CONSTRAINT [FK_CrfOptions_CrfCategory] FOREIGN KEY([CrfOptionCategoryId])
REFERENCES [dbo].[CrfOptionCategories] ([Id])
GO
ALTER TABLE [dbo].[CrfOptions] CHECK CONSTRAINT [FK_CrfOptions_CrfCategory]
GO
ALTER TABLE [dbo].[CrfPageComponents]  WITH CHECK ADD  CONSTRAINT [FK_CrfPageComponents_CrfPages_CRFPageId] FOREIGN KEY([CRFPageId])
REFERENCES [dbo].[CrfPages] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CrfPageComponents] CHECK CONSTRAINT [FK_CrfPageComponents_CrfPages_CRFPageId]
GO
ALTER TABLE [dbo].[CrfPageComponents]  WITH CHECK ADD  CONSTRAINT [FK_CrfPageComponents_QuestionTypes_QuestionTypeId] FOREIGN KEY([QuestionTypeId])
REFERENCES [dbo].[QuestionTypes] ([Id])
GO
ALTER TABLE [dbo].[CrfPageComponents] CHECK CONSTRAINT [FK_CrfPageComponents_QuestionTypes_QuestionTypeId]
GO
