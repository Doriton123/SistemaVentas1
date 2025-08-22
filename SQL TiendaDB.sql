CREATE DATABASE TiendaDB;
USE TiendaDB;

CREATE TABLE Categoria (
    CodigoCategoria INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL
);

CREATE TABLE Producto (
    CodigoProducto INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    CodigoCategoria INT NOT NULL,
    CONSTRAINT FK_Producto_Categoria 
        FOREIGN KEY (CodigoCategoria) REFERENCES Categoria(CodigoCategoria)
);

CREATE TABLE Venta (
    CodigoVenta INT IDENTITY(1,1) PRIMARY KEY,
    Fecha DATETIME NOT NULL,
    CodigoProducto INT NOT NULL,
    CONSTRAINT FK_Venta_Producto 
        FOREIGN KEY (CodigoProducto) REFERENCES Producto(CodigoProducto)
);

INSERT INTO Categoria (Nombre) VALUES
('Electrónicos'),
('Ropa'),
('Electrodomesticos'),
('Juegos'),
('Libros');

INSERT INTO Producto (Nombre, CodigoCategoria) VALUES
('Laptop Dell', 1),
('iPhone 14', 1),
('Camisa Arrow', 2),
('Pantalón Abercrombie', 2),
('Refrigeradora', 3),
('Lavadora', 3),
('Mario kart', 4),
('Fifa', 4),
('Crimen y castigo', 5),
('La odisea', 5);

INSERT INTO Venta (Fecha, CodigoProducto) VALUES
('2015-02-15', 1),
('2017-10-16', 3),
('2018-11-17', 5),
('2019-12-18', 2),
('2019-02-19', 7),
('2019-01-20', 9),
('2020-02-01', 4),
('2017-03-02', 6),
('2015-04-03', 8),
('2016-05-04', 10),
('2016-06-05', 1),  
('2019-12-21', 2); -- ultima venta mas reciente


SELECT TOP 1 
    c.Nombre AS 'Categoría del Producto de la Última Venta'
FROM Venta v
INNER JOIN Producto p ON v.CodigoProducto = p.CodigoProducto
INNER JOIN Categoria c ON p.CodigoCategoria = c.CodigoCategoria
ORDER BY v.Fecha DESC;

SELECT DISTINCT C.*
FROM Categoria C
JOIN Producto P ON C.CodigoCategoria = P.CodigoCategoria
JOIN Venta V ON P.CodigoProducto = V.CodigoProducto
WHERE YEAR(V.Fecha) = 2019;

SELECT *FROM Categoria;
SELECT *FROM Venta;
SELECT *FROM Producto