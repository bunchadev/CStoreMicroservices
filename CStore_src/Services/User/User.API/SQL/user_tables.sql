IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'UserDb')
BEGIN
    CREATE DATABASE UserDb;
END
GO

CREATE TABLE roles (
    role_id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    role_name NVARCHAR(255) UNIQUE NOT NULL,
    created_at DATETIME2 DEFAULT SYSDATETIME(),
    updated_at DATETIME2 DEFAULT SYSDATETIME()
);

CREATE TABLE users (
    user_id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    username NVARCHAR(255) UNIQUE NOT NULL,
    email NVARCHAR(255) UNIQUE NOT NULL,
    password NVARCHAR(255) NOT NULL,
    role_id UNIQUEIDENTIFIER FOREIGN KEY REFERENCES roles(role_id),
    created_at DATETIME2 DEFAULT SYSDATETIME(),
    updated_at DATETIME2 DEFAULT SYSDATETIME()
);

CREATE TABLE tokens (
    token_id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    user_id UNIQUEIDENTIFIER FOREIGN KEY REFERENCES users(user_id) ON DELETE CASCADE,
    token NVARCHAR(MAX) NOT NULL,
    expires_at DATETIME2 NOT NULL,
    created_at DATETIME2 DEFAULT SYSDATETIME()
);

CREATE TABLE permissions (
    permission_id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    permission_name VARCHAR(255) UNIQUE NOT NULL,
    created_at DATETIME2 DEFAULT SYSDATETIME(),
    updated_at DATETIME2 DEFAULT SYSDATETIME()
);

CREATE TABLE role_permissions (
    role_id UNIQUEIDENTIFIER NOT NULL,
    permission_id UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT PK_role_permissions PRIMARY KEY (role_id, permission_id),
    CONSTRAINT FK_role_permissions_roles FOREIGN KEY (role_id) REFERENCES roles(role_id) ON DELETE CASCADE,
    CONSTRAINT FK_role_permissions_permissions FOREIGN KEY (permission_id) REFERENCES permissions(permission_id) ON DELETE CASCADE
);

INSERT INTO roles (role_id, role_name)
VALUES 
    (NEWID(), 'Admin'),
    (NEWID(), 'User');

DECLARE @AdminRoleId UNIQUEIDENTIFIER;
DECLARE @UserRoleId UNIQUEIDENTIFIER;

SELECT @AdminRoleId = role_id FROM roles WHERE role_name = 'Admin';
SELECT @UserRoleId = role_id FROM roles WHERE role_name = 'User';

INSERT INTO users (user_id, username, email, password, role_id)
VALUES 
    (NEWID(), 'trungnguyen', 'tn0888888@gmail.com', '1232003', @AdminRoleId),
    (NEWID(), 'bunchadev', 'trungvjppro1232003@gmail.com', '1232003', @UserRoleId);
