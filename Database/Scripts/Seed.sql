USE [KASPORT]
GO
INSERT [dbo].[Applications]
    ([Id], [Name])
VALUES
    (N'2af50454-63fa-4aa2-baa6-6ba3c5815310', N'App')
INSERT [dbo].[Applications]
    ([Id], [Name])
VALUES
    (N'8c76ab5c-dd13-46d2-9a2f-ae1df3e89957', N'App2')
INSERT [dbo].[Memberships]
    ([Id], [ApplicationId], [UserId])
VALUES
    (N'9da0b151-2749-409b-b7f5-05bf03dd1a53', N'2af50454-63fa-4aa2-baa6-6ba3c5815310', N'0fa95abc-d121-4538-91ee-81f5223a6da6')
INSERT [dbo].[Memberships]
    ([Id], [ApplicationId], [UserId])
VALUES
    (N'5966a125-2167-4abc-8f46-092096ddc188', N'2af50454-63fa-4aa2-baa6-6ba3c5815310', N'e224a76b-fecf-4a6e-b3ef-d194b45496c7')
INSERT [dbo].[Memberships]
    ([Id], [ApplicationId], [UserId])
VALUES
    (N'82b8035f-a837-4224-8626-43c2aa13958c', N'2af50454-63fa-4aa2-baa6-6ba3c5815310', N'578e8abd-e657-423d-ab1d-26d01fee97ca')
INSERT [dbo].[Memberships]
    ([Id], [ApplicationId], [UserId])
VALUES
    (N'a736f1c4-09d0-429f-ad6b-66cfee8a71ce', N'8c76ab5c-dd13-46d2-9a2f-ae1df3e89957', N'f4d3e6af-5e01-4a75-b3fa-5637923086a1')
INSERT [dbo].[Memberships]
    ([Id], [ApplicationId], [UserId])
VALUES
    (N'b4fe3eba-f187-4354-b67e-76f900edb61e', N'2af50454-63fa-4aa2-baa6-6ba3c5815310', N'be64b27b-59b6-43bb-a824-51c16d328ffb')
INSERT [dbo].[Memberships]
    ([Id], [ApplicationId], [UserId])
VALUES
    (N'5b73e710-6094-4b67-8d14-972e08d43677', N'2af50454-63fa-4aa2-baa6-6ba3c5815310', N'ac600b20-44a3-44c0-bac2-b3ff53577d3c')
INSERT [dbo].[Memberships]
    ([Id], [ApplicationId], [UserId])
VALUES
    (N'f9020f82-c7ab-4b9b-8bf9-9adb2451bdba', N'2af50454-63fa-4aa2-baa6-6ba3c5815310', N'67fe0392-146f-4cb8-a04f-80803a5e9c1b')
INSERT [dbo].[Memberships]
    ([Id], [ApplicationId], [UserId])
VALUES
    (N'58a7cb24-3a70-4071-b5af-aa034663980f', N'2af50454-63fa-4aa2-baa6-6ba3c5815310', N'f4d3e6af-5e01-4a75-b3fa-5637923086a1')
INSERT [dbo].[Claims]
    ([Id], [ApplicationId], [ClaimType], [ClaimValue])
VALUES
    (N'a4311e4e-6c12-4d93-9921-07505413b1f9', N'8c76ab5c-dd13-46d2-9a2f-ae1df3e89957', N'Role', N'BMAccount Admin')
INSERT [dbo].[Claims]
    ([Id], [ApplicationId], [ClaimType], [ClaimValue])
VALUES
    (N'b78494ae-6c43-49b7-962a-122b2e3bdfd5', N'8c76ab5c-dd13-46d2-9a2f-ae1df3e89957', N'Role', N'BMDistrict Admin')
INSERT [dbo].[Claims]
    ([Id], [ApplicationId], [ClaimType], [ClaimValue])
VALUES
    (N'5d508531-6bb3-4b2d-bd59-8129b182b5dc', N'2af50454-63fa-4aa2-baa6-6ba3c5815310', N'Role', N'User')
INSERT [dbo].[Claims]
    ([Id], [ApplicationId], [ClaimType], [ClaimValue])
VALUES
    (N'c568b7fb-41cb-461e-9dcb-8e9f93b56505', N'2af50454-63fa-4aa2-baa6-6ba3c5815310', N'Role', N'Manager')
INSERT [dbo].[Claims]
    ([Id], [ApplicationId], [ClaimType], [ClaimValue])
VALUES
    (N'4c7a37c2-3c76-4299-8c54-954c5bb8ca76', N'2af50454-63fa-4aa2-baa6-6ba3c5815310', N'Role', N'Admin')
INSERT [dbo].[UserClaims]
    ([Id], [ClaimId], [UserId])
VALUES
    (N'3a570804-ccbb-4c68-980d-029a933f958b', N'c568b7fb-41cb-461e-9dcb-8e9f93b56505', N'ac600b20-44a3-44c0-bac2-b3ff53577d3c')
INSERT [dbo].[UserClaims]
    ([Id], [ClaimId], [UserId])
VALUES
    (N'757b0e6c-f677-43c3-a31c-1c716fc13050', N'4c7a37c2-3c76-4299-8c54-954c5bb8ca76', N'67fe0392-146f-4cb8-a04f-80803a5e9c1b')
INSERT [dbo].[UserClaims]
    ([Id], [ClaimId], [UserId])
VALUES
    (N'3c15bd6c-46cb-41ec-a327-1c7b2c59f9a2', N'4c7a37c2-3c76-4299-8c54-954c5bb8ca76', N'be64b27b-59b6-43bb-a824-51c16d328ffb')
INSERT [dbo].[UserClaims]
    ([Id], [ClaimId], [UserId])
VALUES
    (N'906a5fb1-6a5a-413d-ad26-5d5f8b12d529', N'5d508531-6bb3-4b2d-bd59-8129b182b5dc', N'0fa95abc-d121-4538-91ee-81f5223a6da6')
INSERT [dbo].[UserClaims]
    ([Id], [ClaimId], [UserId])
VALUES
    (N'6be4d3aa-9fd6-46fa-bdde-66826142e2bc', N'4c7a37c2-3c76-4299-8c54-954c5bb8ca76', N'f4d3e6af-5e01-4a75-b3fa-5637923086a1')
INSERT [dbo].[UserClaims]
    ([Id], [ClaimId], [UserId])
VALUES
    (N'03dad80d-c62f-48ae-bd76-9adf9f5ce084', N'c568b7fb-41cb-461e-9dcb-8e9f93b56505', N'578e8abd-e657-423d-ab1d-26d01fee97ca')
INSERT [dbo].[UserClaims]
    ([Id], [ClaimId], [UserId])
VALUES
    (N'dfed4258-2e80-4bf5-9c45-cae070b0de6e', N'c568b7fb-41cb-461e-9dcb-8e9f93b56505', N'e224a76b-fecf-4a6e-b3ef-d194b45496c7')
INSERT [dbo].[UserClaims]
    ([Id], [ClaimId], [UserId])
VALUES
    (N'192c3f55-cf07-42fe-a900-d5fcd931a42f', N'a4311e4e-6c12-4d93-9921-07505413b1f9', N'f4d3e6af-5e01-4a75-b3fa-5637923086a1')
INSERT [dbo].[UserClaims]
    ([Id], [ClaimId], [UserId])
VALUES
    (N'67d4536e-3e97-48e6-aabb-f859c5c5c39a', N'b78494ae-6c43-49b7-962a-122b2e3bdfd5', N'f4d3e6af-5e01-4a75-b3fa-5637923086a1')
