IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'UserDb')
BEGIN
    CREATE DATABASE UserDb;
END
GO

USE UserDb

CREATE TABLE roles (
    role_id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    role_name NVARCHAR(255) UNIQUE NOT NULL,
    created_at DATETIME DEFAULT SYSDATETIME(),
    updated_at DATETIME DEFAULT SYSDATETIME()
);

CREATE TABLE users (
    user_id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    email NVARCHAR(255) UNIQUE NOT NULL,
    password NVARCHAR(255) NOT NULL,
    auth_method NVARCHAR(50) NOT NULL,
    CONSTRAINT chk_auth_method CHECK (auth_method IN ('credentials', 'github', 'google')),
    is_active BIT NOT NULL DEFAULT 1,
    role_id UNIQUEIDENTIFIER FOREIGN KEY REFERENCES roles(role_id),
    created_at DATETIME DEFAULT SYSDATETIME(),
    updated_at DATETIME DEFAULT SYSDATETIME()
);

CREATE TABLE tokens (
    token_id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    user_id UNIQUEIDENTIFIER FOREIGN KEY REFERENCES users(user_id) ON DELETE CASCADE,
    token NVARCHAR(MAX) NOT NULL,
    expires_at DATETIME NOT NULL,
    created_at DATETIME DEFAULT SYSDATETIME()
);

CREATE TABLE permissions (
    permission_id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    permission_name VARCHAR(255) UNIQUE NOT NULL,
    created_at DATETIME DEFAULT SYSDATETIME(),
    updated_at DATETIME DEFAULT SYSDATETIME()
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

