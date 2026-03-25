IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260325143310_InitialCreate'
)
BEGIN
    CREATE TABLE [RenewableEnergyProjects] (
        [Id] int NOT NULL IDENTITY,
        [OldRefId] nvarchar(max) NULL,
        [RefId] int NOT NULL,
        [RecordLastUpdated] datetime2 NULL,
        [Operator] nvarchar(max) NULL,
        [SiteName] nvarchar(max) NULL,
        [TechnologyType] nvarchar(max) NULL,
        [StorageType] nvarchar(max) NULL,
        [StorageCoLocationRepdRefId] int NULL,
        [InstalledCapacityMWe] decimal(18,4) NULL,
        [ShareCommunityScheme] nvarchar(max) NULL,
        [ChpEnabled] bit NOT NULL,
        [CfdAllocationRound] int NULL,
        [RoBandingRocPerMWh] decimal(18,4) NULL,
        [FitTariffPencePerKWh] decimal(18,4) NULL,
        [CfdCapacityMW] decimal(18,4) NULL,
        [TurbineCapacityMW] decimal(18,4) NULL,
        [NumberOfTurbines] int NULL,
        [HeightOfTurbinesMetres] decimal(18,4) NULL,
        [MountingTypeForSolar] nvarchar(max) NULL,
        [DevelopmentStatus] nvarchar(max) NULL,
        [DevelopmentStatusShort] nvarchar(max) NULL,
        [ReApplyingNewRepdRef] nvarchar(max) NULL,
        [ReApplyingOldRepdRef] nvarchar(max) NULL,
        [Address] nvarchar(max) NULL,
        [County] nvarchar(max) NULL,
        [Region] nvarchar(max) NULL,
        [Country] nvarchar(max) NULL,
        [PostCode] nvarchar(max) NULL,
        [XCoordinate] int NULL,
        [YCoordinate] int NULL,
        [PlanningAuthority] nvarchar(max) NULL,
        [PlanningApplicationReference] nvarchar(max) NULL,
        [AppealReference] nvarchar(max) NULL,
        [SecretaryOfStateReference] nvarchar(max) NULL,
        [TypeOfSecretaryOfStateIntervention] nvarchar(max) NULL,
        [JudicialReview] nvarchar(max) NULL,
        [OffshoreWindRound] nvarchar(max) NULL,
        [PlanningApplicationSubmitted] datetime2 NULL,
        [PlanningApplicationWithdrawn] datetime2 NULL,
        [PlanningPermissionRefused] datetime2 NULL,
        [AppealLodged] datetime2 NULL,
        [AppealWithdrawn] datetime2 NULL,
        [AppealRefused] datetime2 NULL,
        [AppealGranted] datetime2 NULL,
        [PlanningPermissionGranted] datetime2 NULL,
        [SecretaryOfStateIntervened] datetime2 NULL,
        [SecretaryOfStateRefusal] datetime2 NULL,
        [SecretaryOfStateGranted] datetime2 NULL,
        [PlanningPermissionExpired] datetime2 NULL,
        [UnderConstruction] datetime2 NULL,
        [Operational] datetime2 NULL,
        [HeatNetworkRef] nvarchar(max) NULL,
        [SolarSiteAreaSqm] decimal(18,4) NULL,
        CONSTRAINT [PK_RenewableEnergyProjects] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260325143310_InitialCreate'
)
BEGIN
    CREATE UNIQUE INDEX [IX_RenewableEnergyProjects_RefId] ON [RenewableEnergyProjects] ([RefId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260325143310_InitialCreate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260325143310_InitialCreate', N'8.0.12');
END;
GO

COMMIT;
GO

