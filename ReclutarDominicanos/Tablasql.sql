CREATE TABLE [dbo].[tblCandidatos]
(
    [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [TipoDocumento] INT NOT NULL, 
    [Documento] NVARCHAR(50) NOT NULL, 
    [Nombres] NVARCHAR(150) NOT NULL, 
    [Apellidos] NVARCHAR(150) NOT NULL, 
    [FechaNac] DATE NOT NULL, 
    [FechaIng] DATE NOT NULL DEFAULT (GETDATE()), 
    [Peso] DECIMAL(5, 2) NOT NULL, 
    [Estatura] DECIMAL(5, 2) NOT NULL, 
    [CantHijos] INT NOT NULL, 
    [CondFisica] NVARCHAR(150) NOT NULL, 
    [Estado] INT NOT NULL, 
    [FormAcad] NVARCHAR(150) NOT NULL
);

CREATE UNIQUE INDEX UX_tblCandidatos_Documento
ON tblCandidatos(Documento);
