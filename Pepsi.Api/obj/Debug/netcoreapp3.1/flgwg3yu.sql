IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [Username] nvarchar(max) NOT NULL,
    [Email] nvarchar(450) NOT NULL,
    [MobileNumber] nvarchar(450) NOT NULL,
    [Points] int NOT NULL,
    [PasswordHash] varbinary(max) NOT NULL,
    [PasswordSalt] varbinary(max) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);

GO

CREATE UNIQUE INDEX [IX_Users_Email] ON [Users] ([Email]);

GO

CREATE UNIQUE INDEX [IX_Users_MobileNumber] ON [Users] ([MobileNumber]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200713210433_intialDB', N'3.1.5');

GO

ALTER TABLE [Users] ADD [RegisterDate] datetime2 NOT NULL DEFAULT '2020-07-13T23:11:36.0921241+02:00';

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200713211136_AddRegisterDate', N'3.1.5');

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'Username');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Users] ALTER COLUMN [Username] nvarchar(100) NOT NULL;

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'RegisterDate');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Users] ALTER COLUMN [RegisterDate] datetime2 NOT NULL;
ALTER TABLE [Users] ADD DEFAULT '2020-07-13T23:35:54.9832043+02:00' FOR [RegisterDate];

GO

DROP INDEX [IX_Users_MobileNumber] ON [Users];
DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'MobileNumber');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Users] ALTER COLUMN [MobileNumber] nvarchar(20) NOT NULL;
CREATE UNIQUE INDEX [IX_Users_MobileNumber] ON [Users] ([MobileNumber]);

GO

DROP INDEX [IX_Users_Email] ON [Users];
DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'Email');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Users] ALTER COLUMN [Email] nvarchar(100) NOT NULL;
CREATE UNIQUE INDEX [IX_Users_Email] ON [Users] ([Email]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200713213555_AddUserValidationsConfiguration', N'3.1.5');

GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'Username');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Users] DROP COLUMN [Username];

GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'RegisterDate');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Users] ALTER COLUMN [RegisterDate] datetime2 NOT NULL;
ALTER TABLE [Users] ADD DEFAULT '2020-07-27T22:42:43.6501344+02:00' FOR [RegisterDate];

GO

ALTER TABLE [Users] ADD [FirstName] nvarchar(100) NOT NULL DEFAULT N'';

GO

ALTER TABLE [Users] ADD [LastName] nvarchar(100) NOT NULL DEFAULT N'';

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200727204244_SeparateUserName', N'3.1.5');

GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'RegisterDate');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [Users] ALTER COLUMN [RegisterDate] nvarchar(max) NULL;
ALTER TABLE [Users] ADD DEFAULT N'getdate()' FOR [RegisterDate];

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200727204833_FixDateDefaultValue', N'3.1.5');

GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'RegisterDate');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [Users] ALTER COLUMN [RegisterDate] datetime2 NOT NULL;
ALTER TABLE [Users] ADD DEFAULT (getdate()) FOR [RegisterDate];

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200727211047_FixDateDefaultValueSql', N'3.1.5');

GO

ALTER TABLE [Users] ADD [IsEcouponsVerified] bit NOT NULL DEFAULT CAST(0 AS bit);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200728142831_UserVerifiedField', N'3.1.5');

GO

ALTER TABLE [Users] ADD [ResetPasswordOTP] int NOT NULL DEFAULT 0;

GO

ALTER TABLE [Users] ADD [ResetPasswordOTPDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200806115325_ResetPasswordOTP', N'3.1.5');

GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'ResetPasswordOTPDate');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [Users] ALTER COLUMN [ResetPasswordOTPDate] datetime2 NULL;

GO

DECLARE @var9 sysname;
SELECT @var9 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'ResetPasswordOTP');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var9 + '];');
ALTER TABLE [Users] ALTER COLUMN [ResetPasswordOTP] int NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200806121916_ResetPasswordOTPNullable', N'3.1.5');

GO

ALTER TABLE [Users] ADD [IsSubscribedMail] bit NOT NULL DEFAULT CAST(0 AS bit);

GO

ALTER TABLE [Users] ADD [IsSubscribedSms] bit NOT NULL DEFAULT CAST(0 AS bit);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200806174303_IsUserSubscribedToSmsEmail', N'3.1.5');

GO

ALTER TABLE [Users] ADD [LastModificationDate] datetime2 NOT NULL DEFAULT (getdate());

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200809162612_UserLastModificationDate', N'3.1.5');

GO

CREATE TABLE [UserTransactions] (
    [Id] int NOT NULL IDENTITY,
    [Date] datetime2 NOT NULL DEFAULT (getdate()),
    [Sku] nvarchar(50) NULL DEFAULT N'330ml',
    [PromoCode] nvarchar(50) NULL,
    [OldPointsBalance] int NOT NULL,
    [NewPointsBalance] int NOT NULL,
    [points] int NOT NULL,
    [UserEmail] nvarchar(100) NOT NULL,
    [ProductId] nvarchar(30) NULL DEFAULT N'Pepsi',
    [CategoryId] nvarchar(max) NULL,
    [Type] nvarchar(max) NULL,
    [CampaignId] nvarchar(50) NULL DEFAULT N'KSAPepsiPromo2020',
    [UserId] int NOT NULL,
    CONSTRAINT [PK_UserTransactions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserTransactions_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_UserTransactions_UserId] ON [UserTransactions] ([UserId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200810153644_UserTransactionTable', N'3.1.5');

GO

