CRATE DATABASE TrenScanner;
GO

-- Tabla: Rutas
CREATE TABLE [dbo].[Rutas] (
    [id_ruta] INT IDENTITY(1,1) NOT NULL,
    [origen] VARCHAR(100) NOT NULL,
    [destino] VARCHAR(100) NOT NULL,
    PRIMARY KEY ([id_ruta])
);
GO

-- Tabla: Tarifas
CREATE TABLE [dbo].[Tarifas] (
    [id_tarifa] INT IDENTITY(1,1) NOT NULL,
    [id_viaje] INT NOT NULL,
    [tarifa] VARCHAR(50) NOT NULL,
    [precio] DECIMAL(10, 2) NOT NULL,
    [ida_vuelta] VARCHAR(10) NOT NULL,
    PRIMARY KEY ([id_tarifa]),
    FOREIGN KEY ([id_viaje]) REFERENCES [dbo].[Viajes] ([id_viaje])
);
GO

-- Tabla: Usuarios
CREATE TABLE [dbo].[Usuarios] (
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Nombre] VARCHAR(100) NOT NULL,
    [Correo] VARCHAR(150) NOT NULL UNIQUE,
    [Contraseña] VARCHAR(255) NOT NULL,
    [Token] VARCHAR(500) NULL,
    [Rol] INT NOT NULL,
    PRIMARY KEY ([Id])
);
GO

-- Tabla: Viajes
CREATE TABLE [dbo].[Viajes] (
    [id_viaje] INT IDENTITY(1,1) NOT NULL,
    [salida] VARCHAR(100) NOT NULL,
    [llegada] VARCHAR(100) NOT NULL,
    [duracion] VARCHAR(50) NOT NULL,
    [tipo_transbordo] VARCHAR(50) NOT NULL,
    [tanda] VARCHAR(50) NULL,
    [fecha] VARCHAR(100) NOT NULL,
    [id_ruta] INT NOT NULL,
    PRIMARY KEY ([id_viaje]),
    FOREIGN KEY ([id_ruta]) REFERENCES [dbo].[Rutas] ([id_ruta])
);
GO
