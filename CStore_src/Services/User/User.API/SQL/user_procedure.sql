CREATE PROCEDURE dbo.InsertUser
    @Email NVARCHAR(255),
    @Password NVARCHAR(255),
    @AuthMethod NVARCHAR(50),
    @Role UNIQUEIDENTIFIER
AS
BEGIN
    INSERT INTO users (email,password,auth_method,role_id)
    VALUES (@Email , @Password , @AuthMethod,@Role);
END



/* DROP PROCEDURE dbo.InsertUser; */
