CREATE FUNCTION [dbo].[fnSplit]
(      
		@TEXT       VARCHAR(MAX)  
    ,	@DELIMITER	CHAR(1)  
)  
RETURNS @STRINGS TABLE 
(  
    POSITION INT IDENTITY(1,1) PRIMARY KEY,   
    VALUE VARCHAR(200)
)  
--WITH ENCRYPTION
AS  
BEGIN     
    DECLARE @INDEX  INT 
      
   SET @INDEX = -1  
      
    WHILE (LEN(@text) > 0)  
    BEGIN 
        SET @INDEX = CHARINDEX(@DELIMITER , @TEXT)  
          
        IF (@INDEX = 0) AND (LEN(@TEXT) > 0)  
        BEGIN 
            INSERT INTO @STRINGS VALUES (@TEXT)  
            BREAK  
        END 
          
        IF (@INDEX > 1)  
        BEGIN 
            INSERT INTO @STRINGS VALUES (LEFT(@TEXT, @INDEX - 1))  
            SET @TEXT = RIGHT(@TEXT, (LEN(@TEXT) - @INDEX))  
        END 
        ELSE 
            SET @TEXT = RIGHT(@TEXT, (LEN(@TEXT) - @INDEX))   
    END  
    
    RETURN  
END