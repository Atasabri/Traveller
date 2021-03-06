
CREATE proc [dbo].[City_Rate]
@Rate float ,
@City_ID int,
@User_ID int,
@ReturnValue int out
as
begin
if (select count(*) from City_Rates where User_ID=@User_ID and City_ID=@City_ID)>=1
select @ReturnValue=0
else
insert into City_Rates values (@Rate,@City_ID,@User_ID)
end
GO
/****** Object:  StoredProcedure [dbo].[EditFCM]    Script Date: 11/19/2019 10:29:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[EditFCM]
@user_id int,
@FCM nvarchar(max)
as
begin
  update Users set FCM=@FCM where ID=@user_id
end
GO
/****** Object:  StoredProcedure [dbo].[Like_Place]    Script Date: 11/19/2019 10:29:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Like_Place]
@User_ID int,
@Place_ID int,
@ReturnValue int out
as
begin
if (select count(*) from Place_Likes where User_ID=@User_ID and Place_ID=@Place_ID)>=1
select @ReturnValue=0
else
insert into Place_Likes values (@Place_ID,@User_ID)
end
GO
/****** Object:  StoredProcedure [dbo].[User_Rate]    Script Date: 11/19/2019 10:29:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[User_Rate]
@Rate float ,
@FUser_ID int,
@TUser_ID int,
@ReturnValue int out
as
begin
if (select count(*) from User_Rates where FUser_ID=@FUser_ID and TUser_ID=@TUser_ID)>=1
select @ReturnValue=0
else
insert into User_Rates values (@Rate,@FUser_ID,@TUser_ID)
end
GO
/****** Object:  Table [dbo].[AboutUsPhotos]    Script Date: 11/19/2019 10:29:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AboutUsPhotos](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Description_en] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AboutUsVideos]    Script Date: 11/19/2019 10:29:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AboutUsVideos](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Description_en] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Admins]    Script Date: 11/19/2019 10:29:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admins](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[IsManager] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Calls_Companies]    Script Date: 11/19/2019 10:29:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Calls_Companies](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Name_en] [nvarchar](50) NOT NULL,
	[Code] [nvarchar](max) NOT NULL,
	[Help_Number] [nvarchar](max) NOT NULL,
	[Link] [nvarchar](max) NOT NULL,
	[Country_ID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Cars]    Script Date: 11/19/2019 10:29:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cars](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Name_en] [nvarchar](100) NOT NULL,
	[Link] [nvarchar](max) NOT NULL,
	[City_ID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Cars_Companies]    Script Date: 11/19/2019 10:29:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cars_Companies](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Name_en] [nvarchar](50) NOT NULL,
	[Link] [nvarchar](max) NOT NULL,
	[Country_ID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Cities]    Script Date: 11/19/2019 10:29:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cities](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Name_en] [nvarchar](100) NOT NULL,
	[Log] [float] NOT NULL,
	[Lat] [float] NOT NULL,
	[Country_ID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[City_Photos]    Script Date: 11/19/2019 10:29:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[City_Photos](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[City_ID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[City_Rates]    Script Date: 11/19/2019 10:29:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[City_Rates](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Rate] [float] NOT NULL,
	[City_ID] [int] NOT NULL,
	[User_ID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[City_Reports]    Script Date: 11/19/2019 10:29:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[City_Reports](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[City_ID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Codes]    Script Date: 11/19/2019 10:29:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Codes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Contacts]    Script Date: 11/19/2019 10:29:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contacts](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Message] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Countries]    Script Date: 11/19/2019 10:29:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Name_en] [nvarchar](100) NOT NULL,
	[Currency] [nvarchar](10) NOT NULL,
	[CurrencyName] [nvarchar](max) NULL,
	[Log] [float] NOT NULL,
	[Lat] [float] NOT NULL,
	[Police_Number] [nvarchar](11) NOT NULL,
	[Ambulance_Number] [nvarchar](11) NOT NULL,
	[Fire_Number] [nvarchar](11) NOT NULL,
	[History] [nvarchar](max) NULL,
	[History_en] [nvarchar](max) NULL,
	[Roles] [nvarchar](max) NULL,
	[Roles_en] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Country_Photo_Comments]    Script Date: 11/19/2019 10:29:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country_Photo_Comments](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Comment] [nvarchar](max) NOT NULL,
	[Country_Photo_ID] [int] NULL,
	[User_ID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Country_Photos]    Script Date: 11/19/2019 10:29:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country_Photos](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Country_ID] [int] NULL,
	[Description_en] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Country_Video_Comments]    Script Date: 11/19/2019 10:29:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country_Video_Comments](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Comment] [nvarchar](max) NOT NULL,
	[Country_Video_ID] [int] NULL,
	[User_ID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Country_Videos]    Script Date: 11/19/2019 10:29:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country_Videos](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Country_ID] [int] NULL,
	[Description_en] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Favorites]    Script Date: 11/19/2019 10:29:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Favorites](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[User_ID] [int] NOT NULL,
	[Country_ID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Flys]    Script Date: 11/19/2019 10:29:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Flys](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Name_en] [nvarchar](100) NOT NULL,
	[Link] [nvarchar](max) NOT NULL,
	[City_ID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Hotels]    Script Date: 11/19/2019 10:29:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hotels](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Name_en] [nvarchar](100) NOT NULL,
	[Link] [nvarchar](max) NOT NULL,
	[City_ID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Offers]    Script Date: 11/19/2019 10:29:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Offers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Price] [float] NOT NULL,
	[FromDate] [date] NOT NULL,
	[ToDate] [date] NOT NULL,
	[Days_Number] [int] NOT NULL,
	[Visitors_Number] [int] NOT NULL,
	[Currency] [nvarchar](5) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Description_en] [nvarchar](max) NOT NULL,
	[Country_ID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Place_Comments]    Script Date: 11/19/2019 10:29:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Place_Comments](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Comment] [nvarchar](max) NOT NULL,
	[Place_ID] [int] NOT NULL,
	[User_ID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Place_Likes]    Script Date: 11/19/2019 10:29:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Place_Likes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Place_ID] [int] NOT NULL,
	[User_ID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Places]    Script Date: 11/19/2019 10:29:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Places](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Name_en] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Description_en] [nvarchar](max) NOT NULL,
	[Log] [float] NOT NULL,
	[Lat] [float] NOT NULL,
	[City_ID] [int] NOT NULL,
	[NameInCountry] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Places_Photos]    Script Date: 11/19/2019 10:29:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Places_Photos](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Place_ID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Places_Videos]    Script Date: 11/19/2019 10:29:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Places_Videos](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Place_ID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Report_Comments]    Script Date: 11/19/2019 10:29:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Report_Comments](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Comment] [nvarchar](max) NOT NULL,
	[Report_ID] [int] NOT NULL,
	[User_ID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Reservations]    Script Date: 11/19/2019 10:29:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reservations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Type] [nvarchar](10) NOT NULL,
	[Days] [int] NOT NULL,
	[Date] [date] NOT NULL,
	[Time] [time](7) NOT NULL,
	[Log] [float] NOT NULL,
	[Lat] [float] NOT NULL,
	[User_ID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Subscribers]    Script Date: 11/19/2019 10:29:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subscribers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tasks]    Script Date: 11/19/2019 10:29:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tasks](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Task] [nvarchar](max) NOT NULL,
	[FromManager] [int] NOT NULL,
	[ToAdmin] [int] NOT NULL,
	[Finished] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User_City]    Script Date: 11/19/2019 10:29:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_City](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[User_ID] [int] NOT NULL,
	[City_ID] [int] NOT NULL,
	[Hotel] [nvarchar](max) NOT NULL,
	[Place] [nvarchar](max) NOT NULL,
	[Resturant] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User_Photos]    Script Date: 11/19/2019 10:29:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Photos](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Lat] [float] NOT NULL,
	[Log] [float] NOT NULL,
	[Location] [nvarchar](100) NOT NULL,
	[date] [date] NOT NULL,
	[User_ID] [int] NOT NULL,
	[Country_ID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User_Rates]    Script Date: 11/19/2019 10:29:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Rates](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Rate] [float] NOT NULL,
	[FUser_ID] [int] NOT NULL,
	[TUser_ID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User_Videos]    Script Date: 11/19/2019 10:29:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Videos](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Lat] [float] NOT NULL,
	[Log] [float] NOT NULL,
	[Location] [nvarchar](100) NOT NULL,
	[User_ID] [int] NOT NULL,
	[date] [date] NOT NULL,
	[Country_ID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 11/19/2019 10:29:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Token] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](20) NULL,
	[Phone] [nvarchar](20) NOT NULL,
	[Is_Traveller] [bit] NOT NULL,
	[Facebook_ID] [nvarchar](max) NULL,
	[Twitter_ID] [nvarchar](max) NULL,
	[Google_ID] [nvarchar](max) NULL,
	[Country_ID] [int] NOT NULL,
	[FaceBook_Link] [nvarchar](100) NULL,
	[Twitter_Link] [nvarchar](100) NULL,
	[Google_Link] [nvarchar](100) NULL,
	[Snap_Link] [nvarchar](100) NULL,
	[Insta_Link] [nvarchar](100) NULL,
	[FCM] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[AboutUsPhotos] ON 

GO
INSERT [dbo].[AboutUsPhotos] ([ID], [Description], [Description_en]) VALUES (1, N'هذه الصورة من اجل التجربة', N'This image is for experiment')
GO
SET IDENTITY_INSERT [dbo].[AboutUsPhotos] OFF
GO
SET IDENTITY_INSERT [dbo].[Admins] ON 

GO
INSERT [dbo].[Admins] ([ID], [UserName], [Password], [IsManager]) VALUES (1, N'Ata Sabri', N'01142229025', 1)
GO
INSERT [dbo].[Admins] ([ID], [UserName], [Password], [IsManager]) VALUES (2, N'Ahmed', N'123456', 0)
GO
INSERT [dbo].[Admins] ([ID], [UserName], [Password], [IsManager]) VALUES (4, N'Faisal', N'asdFGH', 1)
GO
SET IDENTITY_INSERT [dbo].[Admins] OFF
GO
SET IDENTITY_INSERT [dbo].[Calls_Companies] ON 

GO
INSERT [dbo].[Calls_Companies] ([ID], [Name], [Name_en], [Code], [Help_Number], [Link], [Country_ID]) VALUES (1, N'اتصالات', N'Etisalat', N'011', N'011', N'www.etisalat.com', 1)
GO
INSERT [dbo].[Calls_Companies] ([ID], [Name], [Name_en], [Code], [Help_Number], [Link], [Country_ID]) VALUES (3, N'فودافون', N'Vodafone', N'010', N'012', N'www.Vodefone.com', 1)
GO
INSERT [dbo].[Calls_Companies] ([ID], [Name], [Name_en], [Code], [Help_Number], [Link], [Country_ID]) VALUES (5, N'اورانج', N'Orange', N'012', N'012', N'www.orange.com', 1)
GO
INSERT [dbo].[Calls_Companies] ([ID], [Name], [Name_en], [Code], [Help_Number], [Link], [Country_ID]) VALUES (6, N'وي', N'Wei', N'015', N'015', N'https://www.we.com/', 3)
GO
SET IDENTITY_INSERT [dbo].[Calls_Companies] OFF
GO
SET IDENTITY_INSERT [dbo].[Cars] ON 

GO
INSERT [dbo].[Cars] ([ID], [Name], [Name_en], [Link], [City_ID]) VALUES (1, N'حجز السيارات رقم 1', N'Car 1', N'http://www.rooyadev.com', 1)
GO
INSERT [dbo].[Cars] ([ID], [Name], [Name_en], [Link], [City_ID]) VALUES (2, N'حجز السيارات رقم 2', N'Car 2', N'http://www.rooyadev.com', 1)
GO
INSERT [dbo].[Cars] ([ID], [Name], [Name_en], [Link], [City_ID]) VALUES (3, N'حجز السيارات رقم 3', N'Car 3', N'http://www.rooyadev.com', 2)
GO
INSERT [dbo].[Cars] ([ID], [Name], [Name_en], [Link], [City_ID]) VALUES (4, N'حجز السيارات رقم 4', N'Car 4', N'http://www.rooyadev.com', 2)
GO
INSERT [dbo].[Cars] ([ID], [Name], [Name_en], [Link], [City_ID]) VALUES (5, N'حجز السيارات رقم 5', N'Car5', N'http://www.rooyadev.com', 3)
GO
SET IDENTITY_INSERT [dbo].[Cars] OFF
GO
SET IDENTITY_INSERT [dbo].[Cars_Companies] ON 

GO
INSERT [dbo].[Cars_Companies] ([ID], [Name], [Name_en], [Link], [Country_ID]) VALUES (1, N'اوبر', N'Uber', N'http://www.opper.com/', 1)
GO
INSERT [dbo].[Cars_Companies] ([ID], [Name], [Name_en], [Link], [Country_ID]) VALUES (2, N'كريم', N'Generous', N'http://www.Careem.com', 1)
GO
INSERT [dbo].[Cars_Companies] ([ID], [Name], [Name_en], [Link], [Country_ID]) VALUES (3, N'we', N'we', N'https://www.we.com/', 3)
GO
SET IDENTITY_INSERT [dbo].[Cars_Companies] OFF
GO
SET IDENTITY_INSERT [dbo].[Cities] ON 

GO
INSERT [dbo].[Cities] ([ID], [Name], [Name_en], [Log], [Lat], [Country_ID]) VALUES (1, N'القاهرة', N'Cairo', 31.1884235, 30.0596185, 1)
GO
INSERT [dbo].[Cities] ([ID], [Name], [Name_en], [Log], [Lat], [Country_ID]) VALUES (2, N'الاسكندرية', N'Alexandria', 29.8147983, 31.2243285, 1)
GO
INSERT [dbo].[Cities] ([ID], [Name], [Name_en], [Log], [Lat], [Country_ID]) VALUES (3, N'السادس من اكتوبر', N'6 October', 30.7424806, 29.9421804, 1)
GO
SET IDENTITY_INSERT [dbo].[Cities] OFF
GO
SET IDENTITY_INSERT [dbo].[City_Photos] ON 

GO
INSERT [dbo].[City_Photos] ([ID], [City_ID]) VALUES (1, 1)
GO
INSERT [dbo].[City_Photos] ([ID], [City_ID]) VALUES (2, 2)
GO
INSERT [dbo].[City_Photos] ([ID], [City_ID]) VALUES (3, 1)
GO
INSERT [dbo].[City_Photos] ([ID], [City_ID]) VALUES (4, 3)
GO
INSERT [dbo].[City_Photos] ([ID], [City_ID]) VALUES (5, 2)
GO
INSERT [dbo].[City_Photos] ([ID], [City_ID]) VALUES (6, 1)
GO
INSERT [dbo].[City_Photos] ([ID], [City_ID]) VALUES (7, 2)
GO
INSERT [dbo].[City_Photos] ([ID], [City_ID]) VALUES (1013, 1)
GO
SET IDENTITY_INSERT [dbo].[City_Photos] OFF
GO
SET IDENTITY_INSERT [dbo].[City_Rates] ON 

GO
INSERT [dbo].[City_Rates] ([ID], [Rate], [City_ID], [User_ID]) VALUES (1, 2.8, 1, 6)
GO
INSERT [dbo].[City_Rates] ([ID], [Rate], [City_ID], [User_ID]) VALUES (2, 4.5, 1, 5)
GO
SET IDENTITY_INSERT [dbo].[City_Rates] OFF
GO
SET IDENTITY_INSERT [dbo].[City_Reports] ON 

GO
INSERT [dbo].[City_Reports] ([ID], [City_ID]) VALUES (1, 1)
GO
INSERT [dbo].[City_Reports] ([ID], [City_ID]) VALUES (2, 1)
GO
INSERT [dbo].[City_Reports] ([ID], [City_ID]) VALUES (3, 2)
GO
INSERT [dbo].[City_Reports] ([ID], [City_ID]) VALUES (4, 3)
GO
INSERT [dbo].[City_Reports] ([ID], [City_ID]) VALUES (5, 1)
GO
INSERT [dbo].[City_Reports] ([ID], [City_ID]) VALUES (6, 2)
GO
SET IDENTITY_INSERT [dbo].[City_Reports] OFF
GO
SET IDENTITY_INSERT [dbo].[Codes] ON 

GO
INSERT [dbo].[Codes] ([ID]) VALUES (1)
GO
INSERT [dbo].[Codes] ([ID]) VALUES (9)
GO
INSERT [dbo].[Codes] ([ID]) VALUES (10)
GO
INSERT [dbo].[Codes] ([ID]) VALUES (11)
GO
SET IDENTITY_INSERT [dbo].[Codes] OFF
GO
SET IDENTITY_INSERT [dbo].[Contacts] ON 

GO
INSERT [dbo].[Contacts] ([ID], [Name], [Email], [Message]) VALUES (1, N'Ata Sabri', N'ataeldon@gmail.com', N' 
               This Is For Test        ')
GO
SET IDENTITY_INSERT [dbo].[Contacts] OFF
GO
SET IDENTITY_INSERT [dbo].[Countries] ON 

GO
INSERT [dbo].[Countries] ([ID], [Name], [Name_en], [Currency], [CurrencyName], [Log], [Lat], [Police_Number], [Ambulance_Number], [Fire_Number], [History], [History_en], [Roles], [Roles_en]) VALUES (1, N'مصر', N'Egypt', N'EGP', NULL, 26.3798881, 26.8447066, N'122', N'133', N'144', N'حضارة نقادة 1 (3900 - 3650 قبل الميلاد) وقد وجدت آثار هذه الحضارة في أكثر من موقع ابتداء من مصر الوسطى وحتى الشلال الأول، وهي ترتبط حضاريا بحضارة العَمرة (جنوب شرق العرابة المدفونة بمركز البلينا بمحافظة سوهاج الحالية). فقد تميزت بوجود صلات تجارية مع الواحة الخارجة غربا والبحر الأحمر شرقا ووصلت إلى الجندل الأول في الجنوب. كانت نقادة جبانة لاحدى المدن المصرية الهامة وهي مدينة  والتي كانت مركزا لعبادة الإله ست. عثر في جبانة نقادة على بعض الدبابيس وأدوات أخرى صغيرة مصنوعة من النحاس، أما عن مساكنهم فقد كانت بسيطة تشيد من اغصان الأشجار التي تكسى بالطين. وأما مقابرهم فقد كانت عبارة عن حفرة بيضاوية قليلة العمق، وكان المتوفي يدفن في وضع القرفصاء ويلف أحيانا بجلد ماعز. وشهدت حضارة نقاده 1 تحسن صناعة الأدوات الحجرية وتطور تقنيات حرق الفخار، يتميز ففخار نقادة الأولى باحمراره كما كانت عليه نقوش في اشكال هندسية. ومع تطور نقادة إلى حضارة نقادة 2 نحو 3500 قبل الميلاد تقدم تصنيع الأواني الحجرية وأتقنت صناعة الفخار كما بدأ المصري القديم يزين القوارير الفخارية برسم الإنسان والحيوان والنباتات.', N'The civilization of Naqada 1 (3900-3650 BC). The traces of this civilization were found in more than one location from Central Egypt to the first waterfall. It is associated with the civilization of Umrah (southeast of Al-Arrabeh buried in Al-Balina Center in the current governorate of Sohag). It was characterized by commercial links with the oasis of Kharga west and the Red Sea to the east and reached the first gondola in the south. Naqada was the cemetery of one of the important Egyptian cities, a city that was the center of the worship of God. In the Naqada cemetery, some pins and other small brass tools were found, and their dwellings were simply constructed from the branches of the mud-covered trees. Their tombs were a shallow oval pit, and the deceased was buried in a squatting position and sometimes wrapped in goatskin. The civilization of Naqada 1 witnessed an improvement in the manufacture of stone tools and the development of pottery burning techniques. The first pottery of Naqada is characterized by its redness and engravings in geometric forms. With the development of Naqada to the civilization of Naqada 2 around 3500 BC, the manufacture of stone vessels and mastered the pottery industry as the ancient Egyptian began decorating pottery bottles with human, animal and plant.', N'قوانين مصر', N'Laws of Egypt')
GO
INSERT [dbo].[Countries] ([ID], [Name], [Name_en], [Currency], [CurrencyName], [Log], [Lat], [Police_Number], [Ambulance_Number], [Fire_Number], [History], [History_en], [Roles], [Roles_en]) VALUES (3, N'الولايات المتحدة الامريكية', N'United States of America', N'USD', NULL, -113.7553377, 36.2412889, N'122', N'133', N'144', N'العملة عالميا تكون مغطاة بقيمتها ذهباً لأعطائها الموثوقية، والدولار الأمريكي يجب أن يكون مغطى بقيمته ذهباً، إلا أنه في الواقع لا أحد يستطيع أن يثبت ذلك فقد امتنعت الولايات المتحدة الأمريكية منذ فترة ليست بالقصيرة عن استبدال الدولار الأمريكي بما يعادل قيمته ذهباً مع أن سعر الذهب يباع عالمياً بالدولار، وسعر الذهب (أونصة الذهب) تحدد حسب سعر البورصة وتقيم بالدولار.

فالولايات المتحدة الأمريكية رفعت الغطاء الذهبي عن الدولار عام 1973 م، وذلك عندما طالب رئيس جمهورية فرنسا شارل ديغول استبدال ما هو متوفر لدي البنك المركزي الفرنسي من دولارات أمريكية بما يعادلها ذهباً.', N'Currency in the world is covered with gold value to give it credibility, and the US dollar must be covered in gold value, but in fact no one can prove that the United States has not for a short time to replace the dollar equivalent to the value of gold, although the price of gold sold Globally, the price of gold (ounce of gold) is determined by the exchange rate and is valued in dollars.\r\x3cbr\x3e\r\x3cbr\x3eThe United States of America raised the gold cover for the dollar in 1973 when President Charles de Gaulle demanded the replacement of what the French central bank has with its equivalent of gold.', N'انهار الدولار في نهاية عام 2007 على بداية عام 2008 ووصل إلى أدنى مستوياته في التاريخ وذلك بسبب الأزمة المالية وأزمة الرهن العقاري حيث سجل اليورو مستوى قياسي أمام الدولار في شهر مارس وصل إلى 1.60 وسجل الجنيه الأسترليني أكثر من 2 دولار وهبط الدولار دون الفرنك السويسري لأول مرة في التاريخ ووصل الدولار إلى أدنى مستوياته في 13 عاما أمام الين الياباني دون 97 ين وهبط أيضا أمام غالبية العملات العالمية.', N'The dollar collapsed at the end of 2007 and reached its lowest level in history due to the financial crisis and the mortgage crisis. The euro hit a record high against the dollar in March, reaching 1.60 and sterling pounding more than $ 2. The dollar fell below the Swiss franc for the first time In history, the dollar hit a 13-year low against the yen, below 97 yen, and also fell against most of the world\x26#39;s currencies.')
GO
SET IDENTITY_INSERT [dbo].[Countries] OFF
GO
SET IDENTITY_INSERT [dbo].[Country_Photos] ON 

GO
INSERT [dbo].[Country_Photos] ([ID], [Description], [Country_ID], [Description_en]) VALUES (1, N'هذه الصورة من اجل التجربة', 1, N'This image is for experiment')
GO
INSERT [dbo].[Country_Photos] ([ID], [Description], [Country_ID], [Description_en]) VALUES (2, N'تجربة', 3, N'Experiment')
GO
SET IDENTITY_INSERT [dbo].[Country_Photos] OFF
GO
SET IDENTITY_INSERT [dbo].[Country_Videos] ON 

GO
INSERT [dbo].[Country_Videos] ([ID], [Description], [Country_ID], [Description_en]) VALUES (1, N'هذا الفيديو من اجل التجربة', 1, N'This video is for trial')
GO
SET IDENTITY_INSERT [dbo].[Country_Videos] OFF
GO
SET IDENTITY_INSERT [dbo].[Favorites] ON 

GO
INSERT [dbo].[Favorites] ([ID], [User_ID], [Country_ID]) VALUES (2, 5, 1)
GO
INSERT [dbo].[Favorites] ([ID], [User_ID], [Country_ID]) VALUES (3, 6, 1)
GO
SET IDENTITY_INSERT [dbo].[Favorites] OFF
GO
SET IDENTITY_INSERT [dbo].[Flys] ON 

GO
INSERT [dbo].[Flys] ([ID], [Name], [Name_en], [Link], [City_ID]) VALUES (1, N'شركة الطيران رقم 1', N'Fly 1', N'http://www.rooyadev.com', 1)
GO
INSERT [dbo].[Flys] ([ID], [Name], [Name_en], [Link], [City_ID]) VALUES (2, N'شركة الطيران رقم 2', N'Fly 2', N'http://www.rooyadev.com', 1)
GO
INSERT [dbo].[Flys] ([ID], [Name], [Name_en], [Link], [City_ID]) VALUES (3, N'شركة الطيران رقم 3', N'Fly 3', N'http://www.rooyadev.com', 2)
GO
INSERT [dbo].[Flys] ([ID], [Name], [Name_en], [Link], [City_ID]) VALUES (4, N'شركة الطيران رقم 4', N'Fly 4', N'http://www.rooyadev.com', 2)
GO
INSERT [dbo].[Flys] ([ID], [Name], [Name_en], [Link], [City_ID]) VALUES (5, N'شركة الطيران رقم 5', N'Airline No. 5', N'http://www.rooyadev.com', 3)
GO
SET IDENTITY_INSERT [dbo].[Flys] OFF
GO
SET IDENTITY_INSERT [dbo].[Hotels] ON 

GO
INSERT [dbo].[Hotels] ([ID], [Name], [Name_en], [Link], [City_ID]) VALUES (1, N'الفندق رقم 1', N'Hotel number 1', N'http://www.rooyadev.com', 1)
GO
INSERT [dbo].[Hotels] ([ID], [Name], [Name_en], [Link], [City_ID]) VALUES (2, N'الفندق رقم 2', N'Hotel 2', N'http://www.rooyadev.com', 1)
GO
INSERT [dbo].[Hotels] ([ID], [Name], [Name_en], [Link], [City_ID]) VALUES (3, N'الفندق رقم 3', N'Hotel 3', N'http://www.rooyadev.com', 2)
GO
INSERT [dbo].[Hotels] ([ID], [Name], [Name_en], [Link], [City_ID]) VALUES (4, N'الفندق رقم 4', N'Hotel 4', N'http://www.rooyadev.com', 2)
GO
SET IDENTITY_INSERT [dbo].[Hotels] OFF
GO
SET IDENTITY_INSERT [dbo].[Offers] ON 

GO
INSERT [dbo].[Offers] ([ID], [Price], [FromDate], [ToDate], [Days_Number], [Visitors_Number], [Currency], [Description], [Description_en], [Country_ID]) VALUES (1, 12, CAST(0x0F3F0B00 AS Date), CAST(0x7C400B00 AS Date), 5, 2, N'EGP', N'العرض الاول', N'Offer 1 for test', 1)
GO
INSERT [dbo].[Offers] ([ID], [Price], [FromDate], [ToDate], [Days_Number], [Visitors_Number], [Currency], [Description], [Description_en], [Country_ID]) VALUES (2, 40, CAST(0xB73D0B00 AS Date), CAST(0xD63D0B00 AS Date), 3, 4, N'SP', N'العرض الثاني', N'Offer 2 For Test', 1)
GO
INSERT [dbo].[Offers] ([ID], [Price], [FromDate], [ToDate], [Days_Number], [Visitors_Number], [Currency], [Description], [Description_en], [Country_ID]) VALUES (3, 50, CAST(0x133E0B00 AS Date), CAST(0x913E0B00 AS Date), 6, 7, N'EGP', N'العرض الثالث', N'The third presentation', 1)
GO
INSERT [dbo].[Offers] ([ID], [Price], [FromDate], [ToDate], [Days_Number], [Visitors_Number], [Currency], [Description], [Description_en], [Country_ID]) VALUES (5, 12.98, CAST(0xBD3E0B00 AS Date), CAST(0xC53E0B00 AS Date), 9, 6, N'EGP', N'ghghgh', N'ghghgh', 1)
GO
SET IDENTITY_INSERT [dbo].[Offers] OFF
GO
SET IDENTITY_INSERT [dbo].[Place_Likes] ON 

GO
INSERT [dbo].[Place_Likes] ([ID], [Place_ID], [User_ID]) VALUES (1, 1, 6)
GO
INSERT [dbo].[Place_Likes] ([ID], [Place_ID], [User_ID]) VALUES (3, 1, 5)
GO
SET IDENTITY_INSERT [dbo].[Place_Likes] OFF
GO
SET IDENTITY_INSERT [dbo].[Places] ON 

GO
INSERT [dbo].[Places] ([ID], [Name], [Name_en], [Description], [Description_en], [Log], [Lat], [City_ID], [NameInCountry]) VALUES (1, N'المكان السياحي الاول', N'First tourist place', N'
تاريخ اليمن الإسلامي هو التاريخ الذي يتناول الفترة من دخول الإسلام إلى اليمن في القرن السابع الميلادي وحتى نهاية دول الخلافة الإسلامية، حيث كانت اليمن قبل الإسلام خاضعة للاحتلال الفارسي، بعد طردهم للأحباش من اليمن، وشهدت اليمن خلال تلك الفترة صراعات ونزاعات قبلية، واعتنقت القبائل اليمانية الإسلام في القرن السابع الميلادي، بعد أن أرسل النبي محمد علي بن أبي طالب إلى صنعاء، فأسلمت قبيلة همدان كلها في يوم واحد. وكان اليمن مستقراً في العصر النبوي، وشارك اليمنيون في حروب الرّدّة، ولعبت القبائل اليمانية دوراً مفصلياً خلال الفتوحات الإسلامية أيام الخلفاء الراشدين، وفي معركة القادسية وفتوحات مصر واستوطنت همدان في الجيزة، وكان السمح بن مالك الخولاني أحد القادة الذين حاولوا ضم الغال، وقتل في تولوز وأعقبه قائد يمني وهو عبد الرحمن الغافقي وقتل في معركة بلاط الشهداء، وغزا حميد بن معيوف الهمداني جزيرة كريت اليونانية واشترك اليمنيون في فتوحات النوبة، وكان الحميريون بقيادة صالح بن منصور الحميري من نشر الإسلام بين الأمازيغ في منطقة الريف المغربي وأقاموا إمارة نكور أو إمارة بني صالح. وفي العصر الأموي، ولي ربيع بن زياد الحارثي المذحجي خراسان وغزا يزيد بن شجرة الرهاوي المذحجي وعبد الله بن قيس التراغمي الكندي البحر وغزا معاوية بن خديج التُجيَّبي الكندي صقلية وكان أول عربي يغزوها وغزا بن حديج التُجيبي أفريقية (تونس) ثلاث مرات وغزا النوبة. وتولى إمارة مصر وبرقة. نالت أرض اليمن وشخوصها نصيباً كبيراً من القصص التي وردت في القرآن الكريم ومنها: أصحاب الجنة، وأصحاب الأخدود، وإرم ذات العماد، والنبي سليمان وملكة سبأ، والسيل العرم، وذو القرنين، والفيل وأبرهه ومحاولة هدم الكعبة وغيرها. تعد المساجد والأضرحة من أبرز معالم الحقبة الإسلامية في اليمن، ومنها الجامع الكبير بصنعاء، وجامع الجند بتعز وجامع وضريح الشيخ أحمد بن علوان بتعز، ومسجد أبان بعدن.', N'\r\x3cbr\x3eThe history of Islamic Yemen is the history of the period from the entry of Islam to Yemen in the seventh century AD until the end of the Islamic Caliphate, where Yemen before Islam was subject to the occupation of Persia, after the expulsion of Ahbash from Yemen, and saw Yemen during that period conflicts and tribal conflicts, and embraced the Yemeni tribes Islam in the seventh century AD, after the Prophet Muhammad Ali bin Abi Talib to Sanaa, and gave the entire Hamadan tribe in one day. The Yemenis participated in the wars of apostasy, and the Yemeni tribes played a detailed role during the Islamic conquests during the days of the Caliphs, in the Battle of Qadisiyah and the conquests of Egypt and the settlement of Hamdan in Giza. Samah bin Malik al-Khulani was one of the leaders who tried to annex Gaul, He was followed by a Yemeni leader, Abdul Rahman al-Ghafqi, who was killed in the Battle of the Martyrs\x26#39; Court. Hamid bin Mayouf al-Hamdani invaded the Greek island of Crete and the Yemenis participated in the Nubian conquests. The al-Humeirites, led by Saleh bin Mansour al-Humeiri, spread Islam among the Amazighs in the Moroccan countryside, Nkur or built in favor of the Emirate. In the Umayyad era, the wali of Rabee bin Ziyad al-Harithi, the Crusader Khorasan and the conqueror of Yazid ibn al-Rahawi al-Muhaji and Abdullah bin Qais al-Targhami of the Canadian Sea, conquered Mu\x26#39;awiyah ibn Khadij al-Taijibi of Sicily and was the first Arab to conquer it. And took the Principality of Egypt and Cyrenaica. The land of Yemen and its people received a large share of the stories mentioned in the Holy Quran, including the owners of Paradise, the owners of the groove, the pillar of the pillar, the prophet Solomon, the queen of Sheba, the Nile, the horns, the elephants and the Euphrates. Mosques and shrines are among the most prominent features of the Islamic era in Yemen, including the Great Mosque in Sana\x26#39;a, the Mosque of the Soldiers of Betzaz, the Mosque and the Shrine of Sheikh Ahmad Bin Alwan in Taiz and the Mosque of Aban in Aden.', 31.1303068, 29.9773008, 1, NULL)
GO
INSERT [dbo].[Places] ([ID], [Name], [Name_en], [Description], [Description_en], [Log], [Lat], [City_ID], [NameInCountry]) VALUES (2, N'المكان السياحي 2', N'Place 2', N'sdsd', N'this iS fOR tEST', 31.1353787, 29.9752733, 1, NULL)
GO
INSERT [dbo].[Places] ([ID], [Name], [Name_en], [Description], [Description_en], [Log], [Lat], [City_ID], [NameInCountry]) VALUES (3, N'المكان السياحي 3', N'Place 3', N'sdsd', N'this iS fOR tEST', 29.8690432, 31.2045091, 2, NULL)
GO
INSERT [dbo].[Places] ([ID], [Name], [Name_en], [Description], [Description_en], [Log], [Lat], [City_ID], [NameInCountry]) VALUES (4, N'المكان السياحي 4', N'Place 4', N'sdsd', N'this iS fOR tEST', 29.909087, 31.2089577, 2, NULL)
GO
INSERT [dbo].[Places] ([ID], [Name], [Name_en], [Description], [Description_en], [Log], [Lat], [City_ID], [NameInCountry]) VALUES (6, N'المكان السياحي 5', N'Place 5', N'NULLSSS', N'this iS fOR tEST', 31.1303068, 29.9773008, 3, NULL)
GO
INSERT [dbo].[Places] ([ID], [Name], [Name_en], [Description], [Description_en], [Log], [Lat], [City_ID], [NameInCountry]) VALUES (8, N'مكان سياحى رقم واحد', N'Tourist place number one', N'هذا من اجل التجربة', N'This is for experimentation', 31.767633, 30.987989, 1, NULL)
GO
INSERT [dbo].[Places] ([ID], [Name], [Name_en], [Description], [Description_en], [Log], [Lat], [City_ID], [NameInCountry]) VALUES (16, N'منطقة سياحية', N'tourist spot', N'هذا المكان السياحي جيد جدا لك ولعائلتك', N'This tourist place is very good for you and your family', 31.767633, 30.987989, 1, NULL)
GO
INSERT [dbo].[Places] ([ID], [Name], [Name_en], [Description], [Description_en], [Log], [Lat], [City_ID], [NameInCountry]) VALUES (18, N'منطقة سياحية', N'tourist spot', N'هذا المكان السياحي جيد جدا لك ولعائلتك', N'This tourist place is very good for you and your family', 31.767633, 30.987989, 1, NULL)
GO
INSERT [dbo].[Places] ([ID], [Name], [Name_en], [Description], [Description_en], [Log], [Lat], [City_ID], [NameInCountry]) VALUES (29, N'Ata Sabri', N'Ata Sabri', N'hghghg', N'hghghg', -113.7553377, 36.2412889, 1, N'Egypt')
GO
INSERT [dbo].[Places] ([ID], [Name], [Name_en], [Description], [Description_en], [Log], [Lat], [City_ID], [NameInCountry]) VALUES (30, N'Ata Sabri', N'Ata Sabri', N'jhjjjjjjjjjjj', N'jhjjjjjjjjjjj', -113.7553377, -113.7553377, 1, N'Egypt')
GO
SET IDENTITY_INSERT [dbo].[Places] OFF
GO
SET IDENTITY_INSERT [dbo].[Places_Photos] ON 

GO
INSERT [dbo].[Places_Photos] ([ID], [Place_ID]) VALUES (1, 1)
GO
INSERT [dbo].[Places_Photos] ([ID], [Place_ID]) VALUES (2, 1)
GO
INSERT [dbo].[Places_Photos] ([ID], [Place_ID]) VALUES (3, 2)
GO
INSERT [dbo].[Places_Photos] ([ID], [Place_ID]) VALUES (4, 2)
GO
INSERT [dbo].[Places_Photos] ([ID], [Place_ID]) VALUES (5, 2)
GO
INSERT [dbo].[Places_Photos] ([ID], [Place_ID]) VALUES (6, 3)
GO
INSERT [dbo].[Places_Photos] ([ID], [Place_ID]) VALUES (7, 4)
GO
INSERT [dbo].[Places_Photos] ([ID], [Place_ID]) VALUES (9, 4)
GO
INSERT [dbo].[Places_Photos] ([ID], [Place_ID]) VALUES (10, 1)
GO
INSERT [dbo].[Places_Photos] ([ID], [Place_ID]) VALUES (16, 8)
GO
INSERT [dbo].[Places_Photos] ([ID], [Place_ID]) VALUES (24, 16)
GO
INSERT [dbo].[Places_Photos] ([ID], [Place_ID]) VALUES (35, 29)
GO
INSERT [dbo].[Places_Photos] ([ID], [Place_ID]) VALUES (36, 30)
GO
SET IDENTITY_INSERT [dbo].[Places_Photos] OFF
GO
SET IDENTITY_INSERT [dbo].[Places_Videos] ON 

GO
INSERT [dbo].[Places_Videos] ([ID], [Place_ID]) VALUES (2, 2)
GO
INSERT [dbo].[Places_Videos] ([ID], [Place_ID]) VALUES (3, 2)
GO
INSERT [dbo].[Places_Videos] ([ID], [Place_ID]) VALUES (6, 3)
GO
INSERT [dbo].[Places_Videos] ([ID], [Place_ID]) VALUES (7, 3)
GO
INSERT [dbo].[Places_Videos] ([ID], [Place_ID]) VALUES (11, 1)
GO
INSERT [dbo].[Places_Videos] ([ID], [Place_ID]) VALUES (12, 1)
GO
INSERT [dbo].[Places_Videos] ([ID], [Place_ID]) VALUES (15, 8)
GO
INSERT [dbo].[Places_Videos] ([ID], [Place_ID]) VALUES (23, 16)
GO
SET IDENTITY_INSERT [dbo].[Places_Videos] OFF
GO
SET IDENTITY_INSERT [dbo].[Reservations] ON 

GO
INSERT [dbo].[Reservations] ([ID], [Name], [Type], [Days], [Date], [Time], [Log], [Lat], [User_ID]) VALUES (1, N'Hotel 1', N'Hotel', 4, CAST(0x7C400B00 AS Date), CAST(0x07009CD5FA650000 AS Time), 31.1303068, 29.9773008, 5)
GO
SET IDENTITY_INSERT [dbo].[Reservations] OFF
GO
SET IDENTITY_INSERT [dbo].[Subscribers] ON 

GO
INSERT [dbo].[Subscribers] ([ID], [Email]) VALUES (1, N'ataeldon@gmail.com')
GO
SET IDENTITY_INSERT [dbo].[Subscribers] OFF
GO
SET IDENTITY_INSERT [dbo].[Tasks] ON 

GO
INSERT [dbo].[Tasks] ([ID], [Task], [FromManager], [ToAdmin], [Finished]) VALUES (4, N'This Is Task 1', 1, 2, 0)
GO
INSERT [dbo].[Tasks] ([ID], [Task], [FromManager], [ToAdmin], [Finished]) VALUES (5, N'This Is Task 2', 1, 2, 1)
GO
SET IDENTITY_INSERT [dbo].[Tasks] OFF
GO
SET IDENTITY_INSERT [dbo].[User_City] ON 

GO
INSERT [dbo].[User_City] ([ID], [User_ID], [City_ID], [Hotel], [Place], [Resturant]) VALUES (1, 5, 1, N'test', N'Place 1', N'Resturant 1')
GO
INSERT [dbo].[User_City] ([ID], [User_ID], [City_ID], [Hotel], [Place], [Resturant]) VALUES (2, 5, 2, N'Hotel 2', N'Place 2', N'Resturant 2')
GO
INSERT [dbo].[User_City] ([ID], [User_ID], [City_ID], [Hotel], [Place], [Resturant]) VALUES (3, 6, 1, N'Hotel 3', N'Place 3', N'Resturant 3')
GO
INSERT [dbo].[User_City] ([ID], [User_ID], [City_ID], [Hotel], [Place], [Resturant]) VALUES (4, 7, 3, N'Hotel 4', N'Place 4', N'Resturant 4')
GO
INSERT [dbo].[User_City] ([ID], [User_ID], [City_ID], [Hotel], [Place], [Resturant]) VALUES (5, 7, 1, N'Hotel 5', N'Place 5', N'Resturant 5')
GO
SET IDENTITY_INSERT [dbo].[User_City] OFF
GO
SET IDENTITY_INSERT [dbo].[User_Photos] ON 

GO
INSERT [dbo].[User_Photos] ([ID], [Description], [Lat], [Log], [Location], [date], [User_ID], [Country_ID]) VALUES (6, N'This Is dsdsdsds', 21.33455, 31.2323, N'المنوفية', CAST(0x26350B00 AS Date), 5, 1)
GO
INSERT [dbo].[User_Photos] ([ID], [Description], [Lat], [Log], [Location], [date], [User_ID], [Country_ID]) VALUES (7, N'Thsdjkksjdksj', 31.32342, 32.3232, N'الزمالك', CAST(0x26350B00 AS Date), 6, 1)
GO
INSERT [dbo].[User_Photos] ([ID], [Description], [Lat], [Log], [Location], [date], [User_ID], [Country_ID]) VALUES (8, N'hghghgas', 31.32324, 31.8767656, N'القاهرة', CAST(0x26350B00 AS Date), 5, 1)
GO
INSERT [dbo].[User_Photos] ([ID], [Description], [Lat], [Log], [Location], [date], [User_ID], [Country_ID]) VALUES (9, N'This Is For Test', 32.3232, 32.7555, N'اسيوط', CAST(0x26350B00 AS Date), 5, 1)
GO
INSERT [dbo].[User_Photos] ([ID], [Description], [Lat], [Log], [Location], [date], [User_ID], [Country_ID]) VALUES (10, N'This Is dsdkjkjkjkjkkj', 31.8666, 22.8779765, N'المنوفية', CAST(0x26350B00 AS Date), 5, 1)
GO
SET IDENTITY_INSERT [dbo].[User_Photos] OFF
GO
SET IDENTITY_INSERT [dbo].[User_Rates] ON 

GO
INSERT [dbo].[User_Rates] ([ID], [Rate], [FUser_ID], [TUser_ID]) VALUES (1, 2.8, 6, 5)
GO
INSERT [dbo].[User_Rates] ([ID], [Rate], [FUser_ID], [TUser_ID]) VALUES (6, 4, 5, 6)
GO
INSERT [dbo].[User_Rates] ([ID], [Rate], [FUser_ID], [TUser_ID]) VALUES (8, 4, 5, 7)
GO
INSERT [dbo].[User_Rates] ([ID], [Rate], [FUser_ID], [TUser_ID]) VALUES (9, 4, 5, 6)
GO
SET IDENTITY_INSERT [dbo].[User_Rates] OFF
GO
SET IDENTITY_INSERT [dbo].[User_Videos] ON 

GO
INSERT [dbo].[User_Videos] ([ID], [Description], [Lat], [Log], [Location], [User_ID], [date], [Country_ID]) VALUES (1, N'Video1 For User 5', 22.334, 32.78766, N'المنوفية', 5, CAST(0x26350B00 AS Date), 1)
GO
INSERT [dbo].[User_Videos] ([ID], [Description], [Lat], [Log], [Location], [User_ID], [date], [Country_ID]) VALUES (3, N'Video2 For User 5', 33.9877, 32.876545, N'اسيوط', 5, CAST(0x26350B00 AS Date), 1)
GO
INSERT [dbo].[User_Videos] ([ID], [Description], [Lat], [Log], [Location], [User_ID], [date], [Country_ID]) VALUES (5, N'Video2 For User 7', 31.2332, 22.87676, N'تلا', 7, CAST(0x26350B00 AS Date), 1)
GO
INSERT [dbo].[User_Videos] ([ID], [Description], [Lat], [Log], [Location], [User_ID], [date], [Country_ID]) VALUES (6, N'This Ijsdjh', 31.978897, 33.867656, N'القاهرة', 5, CAST(0x26350B00 AS Date), 1)
GO
INSERT [dbo].[User_Videos] ([ID], [Description], [Lat], [Log], [Location], [User_ID], [date], [Country_ID]) VALUES (7, N'This Ijsdjh', 31.78654, 31.87656, N'القاهرة', 5, CAST(0x26350B00 AS Date), 1)
GO
SET IDENTITY_INSERT [dbo].[User_Videos] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

GO
INSERT [dbo].[Users] ([ID], [Name], [Token], [Email], [Password], [Phone], [Is_Traveller], [Facebook_ID], [Twitter_ID], [Google_ID], [Country_ID], [FaceBook_Link], [Twitter_Link], [Google_Link], [Snap_Link], [Insta_Link], [FCM]) VALUES (5, N'Ata_Sabri', N'Jl14VgpsIIaj/lG8cr5+4CCew0Uas2tqLoMlnRThsng=', N'ataeldon@gmail.com', N'0114222asd', N'01142229028', 1, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, N'eolSKRDY5CE:APA91bFu106EM1M63_uGSh7Jeo_OwnoFs-1NbrZXHli1pjC-MwJeOCf5HSthZ8oZ5uyI8q_YctgmYQuI110gNpkS-kCqOkz6l897pwU_yM8YoBT0ENooep9XF92mlE-EbyvTIf1SJLEHeBPeWuiLfJ3q5RNwlYCGlA')
GO
INSERT [dbo].[Users] ([ID], [Name], [Token], [Email], [Password], [Phone], [Is_Traveller], [Facebook_ID], [Twitter_ID], [Google_ID], [Country_ID], [FaceBook_Link], [Twitter_Link], [Google_Link], [Snap_Link], [Insta_Link], [FCM]) VALUES (6, N'Ata_Sabri.', N'Jl14VgpsIIaj/lG8cr5+4LWZqve9AVVAKsrs+O3OT9k=', N'ataeldo@gmail.com', N'0114222asd', N'01142229085', 0, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, N'ff2Iy9GF3YM:APA91bHpXktI8HkfL4aNlUr3rUW5ipFvunAn6g55-eithsb3U9_phfNvM8A6mEz7AztH5sVTvHay4RChnrcoMSiPkbhlEel73Py7ouNV2X9BE7_BiLpn7ywmvwTUZMB3iA9Fd6Fx89JUuhaP_Nw43eyAUQFsTfKevw')
GO
INSERT [dbo].[Users] ([ID], [Name], [Token], [Email], [Password], [Phone], [Is_Traveller], [Facebook_ID], [Twitter_ID], [Google_ID], [Country_ID], [FaceBook_Link], [Twitter_Link], [Google_Link], [Snap_Link], [Insta_Link], [FCM]) VALUES (7, N'Ata_SabriAta', N'Jl14VgpsIIaj/lG8cr5+4AlZIyrIK2e07a0DfdV14k8=', N'ataeldn@gmail.com', NULL, N'01142229825', 1, N'12345678', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, N'yt')
GO
INSERT [dbo].[Users] ([ID], [Name], [Token], [Email], [Password], [Phone], [Is_Traveller], [Facebook_ID], [Twitter_ID], [Google_ID], [Country_ID], [FaceBook_Link], [Twitter_Link], [Google_Link], [Snap_Link], [Insta_Link], [FCM]) VALUES (8, N'Ata_SabriAhmed', N'Jl14VgpsIIaj/lG8cr5+4EabudS89UmX1BbjB8TDJ1s=', N'ataelon@gmail.com', N'011422290asd', N'01149229825', 0, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, N'yt')
GO
INSERT [dbo].[Users] ([ID], [Name], [Token], [Email], [Password], [Phone], [Is_Traveller], [Facebook_ID], [Twitter_ID], [Google_ID], [Country_ID], [FaceBook_Link], [Twitter_Link], [Google_Link], [Snap_Link], [Insta_Link], [FCM]) VALUES (9, N'Ata_Sabri Ata', N'Jl14VgpsIIaj/lG8cr5+4NmrJc8GHSwTaUbqsJzyAw4=', N'ataedon@gmail.com', NULL, N'01142228025', 1, NULL, N'2584145701', NULL, 1, NULL, NULL, NULL, NULL, NULL, N'tyr')
GO
INSERT [dbo].[Users] ([ID], [Name], [Token], [Email], [Password], [Phone], [Is_Traveller], [Facebook_ID], [Twitter_ID], [Google_ID], [Country_ID], [FaceBook_Link], [Twitter_Link], [Google_Link], [Snap_Link], [Insta_Link], [FCM]) VALUES (10, N'Ata_Sabri Ata ahmed', N'Jl14VgpsIIaj/lG8cr5+4MXa+Gtu07nm5/zYYKYFwdwU/Q8tGsEC5EHpNbHTAA8p', N'Programming@mediagatestudios.com', NULL, N'01142289025', 0, NULL, NULL, N'1223344455', 1, N'www.facebook.com/atasabri', NULL, NULL, NULL, NULL, N'tr')
GO
INSERT [dbo].[Users] ([ID], [Name], [Token], [Email], [Password], [Phone], [Is_Traveller], [Facebook_ID], [Twitter_ID], [Google_ID], [Country_ID], [FaceBook_Link], [Twitter_Link], [Google_Link], [Snap_Link], [Insta_Link], [FCM]) VALUES (12, N'ata sabri ata ahmed', N'SkwhWSbjJD2KKGVGBTTlpeRWYhGwDPBXhzaTeo2FUI8k8BKT0EWcgnOvUbIftvbs', N'ataldon@gmail.com', N'01142229025', N'01142829025', 1, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, N'tr')
GO
INSERT [dbo].[Users] ([ID], [Name], [Token], [Email], [Password], [Phone], [Is_Traveller], [Facebook_ID], [Twitter_ID], [Google_ID], [Country_ID], [FaceBook_Link], [Twitter_Link], [Google_Link], [Snap_Link], [Insta_Link], [FCM]) VALUES (13, N'Ata_Sa', N'tBqS/U8f969MK8g7kepQGQ==', N'atadon@gmail.com', N'123456', N'01142229025', 0, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, N'tr')
GO
INSERT [dbo].[Users] ([ID], [Name], [Token], [Email], [Password], [Phone], [Is_Traveller], [Facebook_ID], [Twitter_ID], [Google_ID], [Country_ID], [FaceBook_Link], [Twitter_Link], [Google_Link], [Snap_Link], [Insta_Link], [FCM]) VALUES (14, N'Ata_Sab', N'dYrC64Mr+BwEgS6t8YlPTw==', N'atadonaa@gmail.com', N'123456', N'01142629021', 0, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, N'tr')
GO
INSERT [dbo].[Users] ([ID], [Name], [Token], [Email], [Password], [Phone], [Is_Traveller], [Facebook_ID], [Twitter_ID], [Google_ID], [Country_ID], [FaceBook_Link], [Twitter_Link], [Google_Link], [Snap_Link], [Insta_Link], [FCM]) VALUES (15, N'Ata_Sabr', N'Jl14VgpsIIaj/lG8cr5+4FGA8IUaNjqjN6QBAYaZe+M=', N'atadonab@gmail.com', N'123456', N'01142829021', 0, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, N'tr')
GO
INSERT [dbo].[Users] ([ID], [Name], [Token], [Email], [Password], [Phone], [Is_Traveller], [Facebook_ID], [Twitter_ID], [Google_ID], [Country_ID], [FaceBook_Link], [Twitter_Link], [Google_Link], [Snap_Link], [Insta_Link], [FCM]) VALUES (16, N'Ata_Sabrikkk', N'Jl14VgpsIIaj/lG8cr5+4KhdcrGPprkNOtdoVip+8v0=', N'atadonan@gmail.com', N'123456', N'01142827021', 0, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, N'tr')
GO
INSERT [dbo].[Users] ([ID], [Name], [Token], [Email], [Password], [Phone], [Is_Traveller], [Facebook_ID], [Twitter_ID], [Google_ID], [Country_ID], [FaceBook_Link], [Twitter_Link], [Google_Link], [Snap_Link], [Insta_Link], [FCM]) VALUES (17, N'Ata_Sabrikk', N'Jl14VgpsIIaj/lG8cr5+4FCAwkS88eHlaPXKrieBKOc=', N'atadoan@gmail.com', N'123456', N'01148827021', 0, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, N'tr')
GO
INSERT [dbo].[Users] ([ID], [Name], [Token], [Email], [Password], [Phone], [Is_Traveller], [Facebook_ID], [Twitter_ID], [Google_ID], [Country_ID], [FaceBook_Link], [Twitter_Link], [Google_Link], [Snap_Link], [Insta_Link], [FCM]) VALUES (21, N'Ata_Saaabfghyukj', N'CL7IoF/VjQdaqkYuK7gtDlrgL3rbG4k4kq29HFBU9Enhy53hvIWSTsxkPa35NQE6', N'atadiikjkjljn@gmail.com', NULL, N'01142929824', 0, N'1709392862431655', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, N'tr')
GO
INSERT [dbo].[Users] ([ID], [Name], [Token], [Email], [Password], [Phone], [Is_Traveller], [Facebook_ID], [Twitter_ID], [Google_ID], [Country_ID], [FaceBook_Link], [Twitter_Link], [Google_Link], [Snap_Link], [Insta_Link], [FCM]) VALUES (22, N'Ata_Saaa', N'CL7IoF/VjQdaqkYuK7gtDnohUDa5xNbkgHhu/jLPtsw=', N'atadiin@gmail.com', N'123456', N'01142229024', 0, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, N'45454545')
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ__Admins__C9F284567F4F8CB8]    Script Date: 11/19/2019 10:29:32 AM ******/
ALTER TABLE [dbo].[Admins] ADD UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ__tmp_ms_x__737584F6087D08FE]    Script Date: 11/19/2019 10:29:32 AM ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ__tmp_ms_x__A9D10534249AE863]    Script Date: 11/19/2019 10:29:32 AM ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Calls_Companies]  WITH CHECK ADD  CONSTRAINT [FK__Calls_Com__Count__6991A7CB] FOREIGN KEY([Country_ID])
REFERENCES [dbo].[Countries] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Calls_Companies] CHECK CONSTRAINT [FK__Calls_Com__Count__6991A7CB]
GO
ALTER TABLE [dbo].[Cars]  WITH CHECK ADD  CONSTRAINT [FK__Cars__City_ID__0A688BB1] FOREIGN KEY([City_ID])
REFERENCES [dbo].[Cities] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Cars] CHECK CONSTRAINT [FK__Cars__City_ID__0A688BB1]
GO
ALTER TABLE [dbo].[Cars_Companies]  WITH CHECK ADD FOREIGN KEY([Country_ID])
REFERENCES [dbo].[Countries] ([ID])
GO
ALTER TABLE [dbo].[Cities]  WITH CHECK ADD FOREIGN KEY([Country_ID])
REFERENCES [dbo].[Countries] ([ID])
GO
ALTER TABLE [dbo].[City_Photos]  WITH CHECK ADD  CONSTRAINT [FK__City_Phot__City___7226EDCC] FOREIGN KEY([City_ID])
REFERENCES [dbo].[Cities] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[City_Photos] CHECK CONSTRAINT [FK__City_Phot__City___7226EDCC]
GO
ALTER TABLE [dbo].[City_Rates]  WITH CHECK ADD  CONSTRAINT [FK__City_Rate__City___6E565CE8] FOREIGN KEY([City_ID])
REFERENCES [dbo].[Cities] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[City_Rates] CHECK CONSTRAINT [FK__City_Rate__City___6E565CE8]
GO
ALTER TABLE [dbo].[City_Rates]  WITH CHECK ADD  CONSTRAINT [FK__City_Rate__User___2E70E1FD] FOREIGN KEY([User_ID])
REFERENCES [dbo].[Users] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[City_Rates] CHECK CONSTRAINT [FK__City_Rate__User___2E70E1FD]
GO
ALTER TABLE [dbo].[City_Reports]  WITH CHECK ADD  CONSTRAINT [FK__City_Repo__City___7132C993] FOREIGN KEY([City_ID])
REFERENCES [dbo].[Cities] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[City_Reports] CHECK CONSTRAINT [FK__City_Repo__City___7132C993]
GO
ALTER TABLE [dbo].[Country_Photo_Comments]  WITH CHECK ADD  CONSTRAINT [FK__Country_P__Count__00CA12DE] FOREIGN KEY([Country_Photo_ID])
REFERENCES [dbo].[Country_Photos] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Country_Photo_Comments] CHECK CONSTRAINT [FK__Country_P__Count__00CA12DE]
GO
ALTER TABLE [dbo].[Country_Photo_Comments]  WITH CHECK ADD FOREIGN KEY([User_ID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Country_Photos]  WITH CHECK ADD  CONSTRAINT [FK__Country_P__Count__7DEDA633] FOREIGN KEY([Country_ID])
REFERENCES [dbo].[Countries] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Country_Photos] CHECK CONSTRAINT [FK__Country_P__Count__7DEDA633]
GO
ALTER TABLE [dbo].[Country_Video_Comments]  WITH CHECK ADD  CONSTRAINT [FK__Country_V__Count__0682EC34] FOREIGN KEY([Country_Video_ID])
REFERENCES [dbo].[Country_Videos] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Country_Video_Comments] CHECK CONSTRAINT [FK__Country_V__Count__0682EC34]
GO
ALTER TABLE [dbo].[Country_Video_Comments]  WITH CHECK ADD FOREIGN KEY([User_ID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Country_Videos]  WITH CHECK ADD  CONSTRAINT [FK__Country_V__Count__03A67F89] FOREIGN KEY([Country_ID])
REFERENCES [dbo].[Countries] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Country_Videos] CHECK CONSTRAINT [FK__Country_V__Count__03A67F89]
GO
ALTER TABLE [dbo].[Favorites]  WITH CHECK ADD  CONSTRAINT [FK__Favorites__Count__58BC2184] FOREIGN KEY([Country_ID])
REFERENCES [dbo].[Countries] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Favorites] CHECK CONSTRAINT [FK__Favorites__Count__58BC2184]
GO
ALTER TABLE [dbo].[Favorites]  WITH CHECK ADD  CONSTRAINT [FK__Favorites__User___57C7FD4B] FOREIGN KEY([User_ID])
REFERENCES [dbo].[Users] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Favorites] CHECK CONSTRAINT [FK__Favorites__User___57C7FD4B]
GO
ALTER TABLE [dbo].[Flys]  WITH CHECK ADD  CONSTRAINT [FK__Flys__City_ID__09746778] FOREIGN KEY([City_ID])
REFERENCES [dbo].[Cities] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Flys] CHECK CONSTRAINT [FK__Flys__City_ID__09746778]
GO
ALTER TABLE [dbo].[Hotels]  WITH CHECK ADD  CONSTRAINT [FK__Hotels__City_ID__0880433F] FOREIGN KEY([City_ID])
REFERENCES [dbo].[Cities] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Hotels] CHECK CONSTRAINT [FK__Hotels__City_ID__0880433F]
GO
ALTER TABLE [dbo].[Offers]  WITH CHECK ADD FOREIGN KEY([Country_ID])
REFERENCES [dbo].[Countries] ([ID])
GO
ALTER TABLE [dbo].[Place_Comments]  WITH CHECK ADD  CONSTRAINT [FK__Place_Com__Place__78D3EB5B] FOREIGN KEY([Place_ID])
REFERENCES [dbo].[Places] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Place_Comments] CHECK CONSTRAINT [FK__Place_Com__Place__78D3EB5B]
GO
ALTER TABLE [dbo].[Place_Comments]  WITH CHECK ADD  CONSTRAINT [FK__Place_Com__User___6F7F8B4B] FOREIGN KEY([User_ID])
REFERENCES [dbo].[Users] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Place_Comments] CHECK CONSTRAINT [FK__Place_Com__User___6F7F8B4B]
GO
ALTER TABLE [dbo].[Place_Likes]  WITH CHECK ADD  CONSTRAINT [FK__Place_Lik__Place__77DFC722] FOREIGN KEY([Place_ID])
REFERENCES [dbo].[Places] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Place_Likes] CHECK CONSTRAINT [FK__Place_Lik__Place__77DFC722]
GO
ALTER TABLE [dbo].[Place_Likes]  WITH CHECK ADD  CONSTRAINT [FK__Place_Lik__User___30592A6F] FOREIGN KEY([User_ID])
REFERENCES [dbo].[Users] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Place_Likes] CHECK CONSTRAINT [FK__Place_Lik__User___30592A6F]
GO
ALTER TABLE [dbo].[Places]  WITH CHECK ADD FOREIGN KEY([City_ID])
REFERENCES [dbo].[Cities] ([ID])
GO
ALTER TABLE [dbo].[Places_Photos]  WITH CHECK ADD  CONSTRAINT [FK__Places_Ph__Place__756D6ECB] FOREIGN KEY([Place_ID])
REFERENCES [dbo].[Places] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Places_Photos] CHECK CONSTRAINT [FK__Places_Ph__Place__756D6ECB]
GO
ALTER TABLE [dbo].[Places_Videos]  WITH CHECK ADD  CONSTRAINT [FK__Places_Vi__Place__76619304] FOREIGN KEY([Place_ID])
REFERENCES [dbo].[Places] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Places_Videos] CHECK CONSTRAINT [FK__Places_Vi__Place__76619304]
GO
ALTER TABLE [dbo].[Report_Comments]  WITH CHECK ADD  CONSTRAINT [FK__Report_Co__Repor__6ABAD62E] FOREIGN KEY([Report_ID])
REFERENCES [dbo].[City_Reports] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Report_Comments] CHECK CONSTRAINT [FK__Report_Co__Repor__6ABAD62E]
GO
ALTER TABLE [dbo].[Report_Comments]  WITH CHECK ADD  CONSTRAINT [FK__Report_Co__User___6BAEFA67] FOREIGN KEY([User_ID])
REFERENCES [dbo].[Users] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Report_Comments] CHECK CONSTRAINT [FK__Report_Co__User___6BAEFA67]
GO
ALTER TABLE [dbo].[Reservations]  WITH CHECK ADD FOREIGN KEY([User_ID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD FOREIGN KEY([FromManager])
REFERENCES [dbo].[Admins] ([ID])
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD FOREIGN KEY([ToAdmin])
REFERENCES [dbo].[Admins] ([ID])
GO
ALTER TABLE [dbo].[User_City]  WITH CHECK ADD FOREIGN KEY([City_ID])
REFERENCES [dbo].[Cities] ([ID])
GO
ALTER TABLE [dbo].[User_City]  WITH CHECK ADD FOREIGN KEY([User_ID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[User_Photos]  WITH CHECK ADD  CONSTRAINT [FK__Country_Phot__User___34C8D9D1] FOREIGN KEY([Country_ID])
REFERENCES [dbo].[Countries] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[User_Photos] CHECK CONSTRAINT [FK__Country_Phot__User___34C8D9D1]
GO
ALTER TABLE [dbo].[User_Photos]  WITH CHECK ADD  CONSTRAINT [FK__User_Phot__User___34C8D9D1] FOREIGN KEY([User_ID])
REFERENCES [dbo].[Users] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[User_Photos] CHECK CONSTRAINT [FK__User_Phot__User___34C8D9D1]
GO
ALTER TABLE [dbo].[User_Rates]  WITH CHECK ADD  CONSTRAINT [FK__User_Rate__FUser__324172E1] FOREIGN KEY([FUser_ID])
REFERENCES [dbo].[Users] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[User_Rates] CHECK CONSTRAINT [FK__User_Rate__FUser__324172E1]
GO
ALTER TABLE [dbo].[User_Rates]  WITH CHECK ADD  CONSTRAINT [FK_User_Rates_Users] FOREIGN KEY([TUser_ID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[User_Rates] CHECK CONSTRAINT [FK_User_Rates_Users]
GO
ALTER TABLE [dbo].[User_Videos]  WITH CHECK ADD  CONSTRAINT [FK__Country_Vide__User___37A5467C] FOREIGN KEY([Country_ID])
REFERENCES [dbo].[Countries] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[User_Videos] CHECK CONSTRAINT [FK__Country_Vide__User___37A5467C]
GO
ALTER TABLE [dbo].[User_Videos]  WITH CHECK ADD  CONSTRAINT [FK__User_Vide__User___37A5467C] FOREIGN KEY([User_ID])
REFERENCES [dbo].[Users] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[User_Videos] CHECK CONSTRAINT [FK__User_Vide__User___37A5467C]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD FOREIGN KEY([Country_ID])
REFERENCES [dbo].[Countries] ([ID])
GO
USE [master]
GO
ALTER DATABASE [Traveller] SET  READ_WRITE 
GO
