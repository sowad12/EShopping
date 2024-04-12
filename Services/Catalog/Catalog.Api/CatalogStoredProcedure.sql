EXEC(N'CREATE OR ALTER PROC CREATE_COUNTRY_INFO
(
   @Id bigint=null,
   @CountryName nvarchar(MAX),
   @CountryCode nvarchar(MAX),
   @CreatedAt datetime2 = NULL,
   @UpdatedAt datetime2 = NULL,
   @CreatedBy bigint = NULL,
   @UpdatedBy bigint = NULL,
   @IsDeleted bit
)
AS
BEGIN
    IF ISNULL(@Id,0)>0
    BEGIN
        SET IDENTITY_INSERT Country ON;
        INSERT INTO Country (
            Id,
            CountryName,
            CountryCode,
            CreatedAt,
            UpdatedAt,
            CreatedBy,
            UpdatedBy,
            IsDeleted
        )
        VALUES (
            @Id,
            @CountryName,
            @CountryCode,
            @CreatedAt,
            @UpdatedAt,
            @CreatedBy,
            @UpdatedBy,
            @IsDeleted
        );
    END
    ELSE
    BEGIN
        SET IDENTITY_INSERT Country OFF;
        INSERT INTO Country (
            CountryName,
            CountryCode,
            CreatedAt,
            UpdatedAt,
            CreatedBy,
            UpdatedBy,
            IsDeleted
        )
        VALUES (
            @CountryName,
            @CountryCode,
            @CreatedAt,
            @UpdatedAt,
            @CreatedBy,
            @UpdatedBy,
            @IsDeleted
        )
    
        SELECT SCOPE_IDENTITY();

    END
END')

EXEC(N'CREATE OR ALTER PROC COUNTRY_INFO_SELECT
(
 
   @CountryName nvarchar(MAX)=NULL,
   @CountryCode nvarchar(MAX)=NULL
  
)
AS
BEGIN

SELECT Id FROM Country
  WHERE IsDeleted = 0 AND (CountryCode=@CountryCode OR CountryName=@CountryName)
END')
