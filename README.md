Ver capturas de pantalla del proyecto:
Sistema Ventas.png
Categoria.png
Filtro.png
Filtrar.png

Explicación sobre el sql de mi base de datos:

SELECT TOP 1 
    c.Nombre AS 'Categoría del Producto de la Última Venta'
FROM Venta v
INNER JOIN Producto p ON v.CodigoProducto = p.CodigoProducto
INNER JOIN Categoria c ON p.CodigoCategoria = c.CodigoCategoria
ORDER BY v.Fecha DESC; 

-Obtenemos nuestros datos en una sola fila, tomamos el campo nombre de la tabla categoria y colocamos el nombre de nuestra columna. 
-La tabla principal es Venta con un alias "v" 
-Utilizamos INNER JOIN porque es un tipo de union en sql que usamos para combinar celdas siempre que haya coincidencias entre las columnas relacionadas, en este caso CodigoProducto/CodigoCategoria. 
Esta condicion concecta cada Venta con el producto que se vendio. 
-Seguimos utilizando INNER JOIN y unimos la tabla Producto que el id o llave foranea tiene como nombre=CodigoCategoria que esta relacionada con la tabla Categoria son su llave primaria CodigoCategoria. 
-Por ultimo el ORDER BY utilizamos para ordenar resultados por fecha mas reciente de venta en orden descente.

En conclusion esta consulta devuelve el nombre de la categoría del producto que fue vendido más recientemente.

Segundo script de mi sql: 
SELECT DISTINCT C.*
FROM Categoria C
JOIN Producto P ON C.CodigoCategoria = P.CodigoCategoria
JOIN Venta V ON P.CodigoProducto = V.CodigoProducto
WHERE YEAR(V.Fecha) = 2019;

-Iniciamos indicando que columnas queremos obtener de nuestra base de datos, el C.* significa que queremos traer todas las columnas de la tabla Categoria con un alias "C" . 
-El DISTINCT lo utilizamos para evitar duplicados, por ejemplo si una categoria aparece varias veces porque tiene varios productos o varias ventas solo aparecerá una vez. 
-Indicamos la tabla principal que es Categoria con el alias ya desginado "C".
-Unimos las tablas categoria y producto. La condicion ON indica que se relacionan por la columna CodigoCategoria asi cada categoria se vincula con los productos. 
-Unimos las tablas producto y ventas, unimos los productos con sus ventas atravez de CodigoProducto. 
-Por ultimo filtramos resultados y extraemos solamente el año de la columna fecha en la tabla Venta con un año especifico = 2019. 

En conclusion el script devuelve todas las categorias que tuvieron al menos un producto vendido en 2019. Sin duplicarlos. Aunque haya dos productos vendidos con el mismo nombre de categoria. 
