/*
배포 후 스크립트 템플릿							
--------------------------------------------------------------------------------------
 이 파일에는 빌드 스크립트에 추가될 SQL 문이 있습니다.		
 SQLCMD 구문을 사용하여 파일을 배포 후 스크립트에 포함합니다.			
 예:      :r .\myfile.sql								
 SQLCMD 구문을 사용하여 배포 후 스크립트의 변수를 참조합니다.		
 예:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

INSERT [dbo].[Authority] ([AuthorityLevel], [AuthorityName]) VALUES (0, N'관리자')
GO
INSERT [dbo].[Authority] ([AuthorityLevel], [AuthorityName]) VALUES (1, N'사용자')
GO


INSERT [dbo].[Users] ([UID], [UserName], [Password], [Email], [AuthorityLevel], [InsertDate], [UpdateDate], [LastLogin], [Status]) VALUES (N'secu', N'관리자', N'8a72d740d54d0fe0e13b61a72d33555f14d4a292fb1696ab64e48f08bff56ab2', N'', 0, CAST(N'2025-01-07 09:26:02.090' AS DateTime), NULL, NULL, 0)
GO


INSERT [dbo].[Team] ([TeamID], [TeamName]) VALUES (1, N'개발 1팀')
GO
INSERT [dbo].[Team] ([TeamID], [TeamName]) VALUES (2, N'개발 2팀')
GO



INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (1, 1, 1, N'진해기지사령부', NULL, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (2, 1, 2, N'1함대', NULL, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (3, 1, 3, N'2함대', NULL, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (4, 1, 4, N'3함대', NULL, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (5, 1, 5, N'해군작전사령부', NULL, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (6, 1, 6, N'7전단', NULL, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (7, 1, 1, N'진기사', 1, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (8, 1, 1, N'교육사령부', 1, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (9, 1, 1, N'잠수함사령부', 1, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (10, 1, 1, N'해군사관학교', 1, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (11, 1, 2, N'군항지구', 2, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (12, 1, 2, N'제108전대(양양)', 2, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (13, 1, 2, N'제118전대(울릉도)', 2, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (14, 1, 2, N'제214전진기지(울진)', 2, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (15, 1, 2, N'제215전진기지(울릉도)', 2, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (16, 1, 2, N'포방대대(포항)', 2, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (17, 1, 2, N'고성합작소', 2, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (18, 1, 3, N'사령부', 3, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (19, 1, 3, N'장안송신소', 3, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (20, 1, 4, N'무기지원대대', 4, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (21, 1, 4, N'사령부', 4, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (22, 1, 6, N'사령부', 6, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (23, 1, 6, N'615대대', 6, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (25, 1, 1, N'리더쉽센터', 8, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (27, 1, 1, N'정문', 8, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (28, 1, 1, N'후문', 8, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (29, 1, 1, N'정문', 9, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (30, 1, 1, N'시설전대', 7, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (31, 1, 1, N'정문', 10, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (32, 1, 2, N'정문', 11, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (33, 1, 2, N'정문', 12, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (34, 1, 2, N'후문', 12, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (35, 1, 2, N'정문', 13, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (36, 1, 2, N'정문', 14, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (37, 1, 2, N'정문', 15, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (38, 1, 2, N'정문', 16, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (39, 1, 2, N'정문', 17, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (40, 1, 3, N'1정문', 18, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (41, 1, 3, N'2정문', 18, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (42, 1, 3, N'3정문', 18, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (45, 1, 3, N'5정문', 18, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (46, 1, 3, N'정문', 19, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (47, 1, 4, N'정문', 20, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (48, 1, 4, N'1정문', 21, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (49, 1, 4, N'2정문', 21, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (50, 1, 4, N'3정문', 21, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (51, 1, 6, N'정문', 22, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (52, 1, 6, N'정문', 23, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (53, 1, 1, N'제1정문', 7, CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (54, 1, 2, N'제245R/S(울릉도)', 2, NULL)
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (55, 1, 2, N'정문', 54, NULL)
GO
INSERT [dbo].[Location] ([LocationID], [CustomerTypeID], [MasterLocationID], [LocationName], [ParentLocationID], [InsertDate]) VALUES (56, 1, 2, N'후문', 11, NULL)
GO


INSERT [dbo].[Software] ([SoftwareID], [SoftwareName], [InsertDate]) VALUES (1, N'차량관제', NULL)
GO
INSERT [dbo].[Software] ([SoftwareID], [SoftwareName], [InsertDate]) VALUES (2, N'휴대형리더기 앱', NULL)
GO
INSERT [dbo].[Software] ([SoftwareID], [SoftwareName], [InsertDate]) VALUES (3, N'인원용뷰어', NULL)
GO
INSERT [dbo].[Software] ([SoftwareID], [SoftwareName], [InsertDate]) VALUES (4, N'차량용뷰어', NULL)
GO
INSERT [dbo].[Software] ([SoftwareID], [SoftwareName], [InsertDate]) VALUES (5, N'프록시서버', NULL)
GO
INSERT [dbo].[Software] ([SoftwareID], [SoftwareName], [InsertDate]) VALUES (6, N'프록시클라이언트', NULL)
GO
INSERT [dbo].[Software] ([SoftwareID], [SoftwareName], [InsertDate]) VALUES (7, N'GateAgentW(국방망)', NULL)
GO
INSERT [dbo].[Software] ([SoftwareID], [SoftwareName], [InsertDate]) VALUES (8, N'GateAgentW(독립망)', NULL)
GO
INSERT [dbo].[Software] ([SoftwareID], [SoftwareName], [InsertDate]) VALUES (9, N'출입증발급용 키오스크', NULL)
GO
INSERT [dbo].[Software] ([SoftwareID], [SoftwareName], [InsertDate]) VALUES (10, N'반납용 키오스크', NULL)
GO


INSERT [dbo].[CustomerType] ([CustomerTypeID], [TeamID], [CustomerTypeName], [InsertDate]) VALUES (1, 2, N'해군', CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[CustomerType] ([CustomerTypeID], [TeamID], [CustomerTypeName], [InsertDate]) VALUES (2, NULL, N'병원', CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[CustomerType] ([CustomerTypeID], [TeamID], [CustomerTypeName], [InsertDate]) VALUES (3, NULL, N'기업', CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[CustomerType] ([CustomerTypeID], [TeamID], [CustomerTypeName], [InsertDate]) VALUES (4, NULL, N'학교', CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO


INSERT [dbo].[InstallType] ([InstallTypeID], [InstallTypeName], [InsertDate]) VALUES (1, N'S/W 업데이트', CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[InstallType] ([InstallTypeID], [InstallTypeName], [InsertDate]) VALUES (2, N'신규 설치', CAST(N'2025-01-06 00:00:00.000' AS DateTime))
GO