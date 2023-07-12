

-- SP that Generate random barcoads

USE imagesdb;
ALTER PROCEDURE GenerateRandomCharacters
    @numBarcodes INT,
    @barcodeLength INT
AS
BEGIN
    CREATE TABLE #tempBarcodes (barcode VARCHAR(255));

    DECLARE @characters VARCHAR(36) = '0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ';
    DECLARE @i INT = 1;

    WHILE @i <= @numBarcodes
    BEGIN
        DECLARE @barcode VARCHAR(255) = '';
        DECLARE @j INT = 1;

        WHILE @j <= @barcodeLength
        BEGIN
            SET @barcode = @barcode + SUBSTRING(@characters, CAST((RAND() * 36) + 1 AS INT), 1);
            SET @j = @j + 1;
        END;
		
		-- Exist Check	
		WHILE EXISTS (SELECT 1 FROM #tempBarcodes WHERE barcode = @barcode)
        BEGIN
            SET @barcode = '';
            SET @j = 1;

            WHILE @j <= @barcodeLength
            BEGIN
                SET @barcode = @barcode + SUBSTRING(@characters, CAST((RAND() * 36) + 1 AS INT), 1);
                SET @j = @j + 1;
            END;
        END;

        INSERT INTO #tempBarcodes (barcode) VALUES (@barcode);
        SET @i = @i + 1;
    END;

    SELECT barcode FROM #tempBarcodes;
    DROP TABLE #tempBarcodes;
END;

EXEC GenerateRandomCharacters @numBarcodes = 10000, @barcodeLength = 12;

-----------BEST WAY-----------

ALTER PROCEDURE GenerateRandomCharacters
    @numBarcodes INT,
    @barcodeLength INT
AS
BEGIN
    CREATE TABLE #tempBarcodes (barcode VARCHAR(255));

    DECLARE @characters VARCHAR(36) = '0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ';
    DECLARE @barcodeCount INT = 0; 

    WHILE @barcodeCount < @numBarcodes
    BEGIN
        DECLARE @barcode VARCHAR(255) = '';
        DECLARE @j INT = 1;

        WHILE @j <= @barcodeLength
        BEGIN
            SET @barcode = @barcode + SUBSTRING(@characters, CAST((RAND() * 36) + 1 AS INT), 1);
            SET @j = @j + 1;
        END;

        IF NOT EXISTS (SELECT 1 FROM #tempBarcodes WHERE barcode = @barcode)
        BEGIN
            INSERT INTO #tempBarcodes (barcode) VALUES (@barcode);
            SET @barcodeCount = @barcodeCount + 1;
        END;
    END;

    SELECT barcode FROM #tempBarcodes;
    DROP TABLE #tempBarcodes;
END;

EXEC GenerateRandomCharacters @numBarcodes = 50, @barcodeLength = 1;

