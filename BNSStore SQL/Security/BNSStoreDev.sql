CREATE LOGIN [BNSStoreDev] WITH PASSWORD = 'BNSStoreDev'
GO

CREATE USER [BNSStoreDev] FOR LOGIN [BNSStoreDev]
GO

GRANT CONNECT TO [BNSStoreDev];
GO

EXEC sp_addrolemember 'db_owner', 'BNSStoreDev';
GO