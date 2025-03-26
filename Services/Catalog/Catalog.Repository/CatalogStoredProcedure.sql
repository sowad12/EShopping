EXEC(N'CREATE OR ALTER PROC PRODUCT_INSERT_OR_UPDATE
(
   @Id BIGINT=null,   
   @ProductBrandId BIGINT,
   @ProductTypeId BIGINT,
   @Name NVARCHAR(MAX),
   @Summary NVARCHAR(MAX),
   @Description NVARCHAR(MAX),
   @ImageFile NVARCHAR(MAX),
   @Price DECIMAL(10, 2)
)
AS
BEGIN
    SET NOCOUNT ON;
    IF ISNULL(@Id,0)<=0
    BEGIN       
        INSERT INTO Product (
           
            ProductBrandId,
            ProductTypeId,
            Name,
            Summary,
            Description,
            ImageFile,
            Price,
            IsDeleted
        )
        VALUES (
           
            @ProductBrandId,
            @ProductTypeId,
            @Name,
            @Summary,
            @Description,
            @ImageFile,
            @Price,
            0
        );
    END
    ELSE
    BEGIN
        UPDATE Product 
        SET 
            ProductBrandId=@ProductBrandId,
            ProductTypeId=@ProductTypeId,                        
            Name=@Name,  
            Summary=@Summary, 
            Description=@Description,
            ImageFile=@ImageFile, 
            Price=@Price,          
            IsDeleted=0

       WHERE Id = @Id;

        SELECT @Id;
    END;
END')

EXEC(N'CREATE OR ALTER PROC PRODUCT_BRAND_INSERT
(
   @Id BIGINT=null,      
   @Name NVARCHAR(MAX)
  
)
AS
BEGIN
    SET NOCOUNT ON;
    IF ISNULL(@Id,0)<=0
    BEGIN       
        INSERT INTO ProductBrand (
                     
            Name,         
            IsDeleted
        )
        VALUES (
                   
            @Name,       
            0
        );

        SELECT SCOPE_IDENTITY();
    END
    ELSE
    BEGIN
        UPDATE ProductBrand 
        SET                                
            Name=@Name,                 
            IsDeleted=0

       WHERE Id = @Id;

        SELECT @Id;
    END;
END')

EXEC(N'CREATE OR ALTER PROC PRODUCT_TYPE_INSERT
(
   @Id BIGINT=null,      
   @Name NVARCHAR(MAX)
  
)
AS
BEGIN
    SET NOCOUNT ON;
    IF ISNULL(@Id,0)<=0
    BEGIN       
        INSERT INTO ProductType (
                       
            Name,         
            IsDeleted
        )
        VALUES (
                   
            @Name,       
            0
        );
       SELECT SCOPE_IDENTITY();
    END
    ELSE
    BEGIN
        UPDATE ProductType 
        SET 
                                
            Name=@Name,                 
            IsDeleted=0

       WHERE Id = @Id;

        SELECT @Id;
    END;
END')
EXEC(N'
CREATE OR ALTER PROC PRODUCT_SELECT_BY_ID
(
   @Id BIGINT    
)
AS
BEGIN
 SELECT * FROM Product WHERE IsDeleted = 0 AND Id=@Id;
END
')

EXEC(N'
CREATE OR ALTER PROC PRODUCT_SELECT_BY_BRAND_NAME
(
   @Name NVARCHAR(MAX)
)
AS
BEGIN
  SELECT a.*
    FROM Product AS a
    INNER JOIN ProductBrand AS b ON a.ProductBrandId = b.Id 
    WHERE a.IsDeleted = 0 AND b.Name LIKE ''%'' + @Name + ''%'';
END
')

EXEC(N'
CREATE OR ALTER PROC PRODUCT_SELECT_BY_PRODUCT_NAME
(
   @Name NVARCHAR(MAX)   
)
AS
BEGIN
 SELECT * FROM Product WHERE IsDeleted = 0 AND  Name LIKE ''%'' + @Name + ''%'';
END
')


EXEC(N'
CREATE OR ALTER PROC PRODUCT_SELECT_ALL
(
 @BrandId BIGINT=NULL,
 @TypeId BIGINT=NULL,
 @SearchQuery VARCHAR(MAX)=NULL,
 @StartRow INT=0,
 @EndRow INT=0,
 @OrderBy VARCHAR(MAX)=NULL
)
AS
BEGIN
SELECT * FROM
(
    SELECT W.*,
        ROW_NUMBER() OVER (
         ORDER BY 
                CASE WHEN @OrderBy IS NULL THEN W.CreatedAt END DESC,        
                CASE WHEN @OrderBy = ''name asc'' THEN W.Name END ASC,
                CASE WHEN @OrderBy = ''name desc'' THEN W.Name END DESC,
                CASE WHEN @OrderBy = ''price asc'' THEN W.Price END ASC,
                CASE WHEN @OrderBy = ''price desc'' THEN W.Price END DESC
        ) SL
     FROM
     (
         SELECT COUNT(*) OVER () TotalRow,       
           * FROM Product 
         WHERE 0=0
         AND IsDeleted=0
         AND CASE WHEN @SearchQuery IS NULL THEN 1 WHEN @SearchQuery='''' THEN 1 WHEN Name LIKE ''%''+@SearchQuery+''%'' THEN 1 ELSE 0 END =1
         AND CASE WHEN @BrandId IS NULL THEN 1 WHEN @BrandId=0 THEN 1 WHEN ProductBrandId=@BrandId THEN 1 ELSE 0 END =1
         AND CASE WHEN @TypeId IS NULL THEN 1 WHEN @TypeId=0 THEN 1 WHEN ProductTypeId=@TypeId THEN 1 ELSE 0 END =1
     )W
 )TBL
 WHERE 0=0
 AND CASE WHEN @EndRow IS NULL THEN 1 WHEN @EndRow=0 THEN 1 WHEN TBL.SL BETWEEN (@StartRow+1) AND (@StartRow+@EndRow) THEN 1 ELSE 0 END =1
END
')

EXEC(N'
CREATE OR ALTER PROC PRODUCT_BRAND_SELECT_ALL

AS
BEGIN
  SELECT * FROM ProductBrand WHERE IsDeleted=0;
END
')

EXEC(N'
CREATE OR ALTER PROC PRODUCT_TYPE_SELECT_ALL

AS
BEGIN
 SELECT * FROM ProductType WHERE IsDeleted=0;
END
')


EXEC(N'
CREATE OR ALTER PROC PRODUCT_DELETE_BY_ID
(
 @Id BIGINT  
)
AS
BEGIN
 UPDATE PRODUCT SET IsDeleted=1 WHERE IsDeleted=0 AND Id=@Id;
 
END
')
EXEC(N'
CREATE OR ALTER PROC PRODUCT_BRAND_DELETE_BY_ID
(
 @Id BIGINT  
)
AS
BEGIN
 UPDATE ProductBrand SET IsDeleted=1 WHERE IsDeleted=0 AND Id=@Id;
 
END
')
EXEC(N'
CREATE OR ALTER PROC PRODUCT_TYPE_DELETE_BY_ID
(
 @Id BIGINT  
)
AS
BEGIN
 UPDATE ProductType SET IsDeleted=1 WHERE IsDeleted=0 AND Id=@Id;
 
END
')

