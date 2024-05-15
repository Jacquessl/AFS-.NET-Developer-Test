CREATE TABLE Translation(
	IdTranslation int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Text nvarchar(200) NOT NULL,
	Translation nvarchar(200) NOT NULL,
)