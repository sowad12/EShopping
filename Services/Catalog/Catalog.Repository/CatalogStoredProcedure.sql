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
  SELECT b.Name 
    FROM Product AS a
    INNER JOIN ProductBrand AS b ON a.ProductBrandId = b.Id 
    WHERE a.IsDeleted = 0 AND b.Name = @Name;
END
')

EXEC(N'
CREATE OR ALTER PROC PRODUCT_SELECT_BY_PRODUCT_NAME
(
   @Name NVARCHAR(MAX)   
)
AS
BEGIN
 SELECT * FROM Product WHERE IsDeleted = 0 AND  Name = @Name;
END
')

EXEC(N'
CREATE OR ALTER PROC PRODUCT_SELECT_ALL

AS
BEGIN
 SELECT * FROM Product WHERE IsDeleted=0;
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

