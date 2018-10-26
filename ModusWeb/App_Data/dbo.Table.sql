CREATE TABLE [dbo].[Source]
(
	[SourceId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Url] VARCHAR(200) NOT NULL, 
    [Description] VARCHAR(200) NULL
)
