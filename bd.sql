CREATE DATABASE IF NOT EXISTS Taller;
USE Taller;

-- 1. Categoría de Servicios / Productos
CREATE TABLE categoriaServicios (
    id_categoria INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    descripcion VARCHAR(255),
    estado TINYINT DEFAULT 1
);

-- 2. Productos (relacionados con categoría)
CREATE TABLE producto (
    id_productos INT AUTO_INCREMENT PRIMARY KEY,
    id_categoria INT NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    descripcion VARCHAR(255),
    precio DECIMAL(10,2) NOT NULL,
    estado TINYINT DEFAULT 1,
    CONSTRAINT fk_producto_categoria FOREIGN KEY (id_categoria) 
        REFERENCES categoriaServicios(id_categoria)
);

-- 3. Marca de Carro
CREATE TABLE marcaCarro (
    id_marca INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    estado TINYINT DEFAULT 1
);

-- 4. Cliente
CREATE TABLE clienteCarro (
    id_cliente INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    telefono VARCHAR(20),
    direccion VARCHAR(255),
    estado TINYINT DEFAULT 1
);

-- 5. Carro (Relacionado con Marca y Cliente - Un cliente puede tener muchos carros)
CREATE TABLE carro (
    id_carro INT AUTO_INCREMENT PRIMARY KEY,
    id_cliente INT NOT NULL,
    id_marca INT NOT NULL,
    modelo VARCHAR(50) NOT NULL,
    anio INT,
    placa VARCHAR(20) UNIQUE NOT NULL,
    estado TINYINT DEFAULT 1,
    CONSTRAINT fk_carro_cliente FOREIGN KEY (id_cliente) 
        REFERENCES clienteCarro(id_cliente),
    CONSTRAINT fk_carro_marca FOREIGN KEY (id_marca) 
        REFERENCES marcaCarro(id_marca)
);

-- 6. Cotización Cabecera
CREATE TABLE cotizacion (
    id_cotizacion INT AUTO_INCREMENT PRIMARY KEY,
    id_cliente INT NOT NULL,
    id_carro INT NOT NULL,
    fecha_emision DATETIME DEFAULT CURRENT_TIMESTAMP,
    total DECIMAL(10,2) NOT NULL,
    estado TINYINT DEFAULT 1, -- 1: Pendiente, 2: Aprobada, 3: Rechazada
    CONSTRAINT fk_cotizacion_cliente FOREIGN KEY (id_cliente) 
        REFERENCES clienteCarro(id_cliente),
    CONSTRAINT fk_cotizacion_carro FOREIGN KEY (id_carro) 
        REFERENCES carro(id_carro)
);

-- 7. Detalle de Cotización
CREATE TABLE detalleCotizacion (
    id_detalle_cotizacion INT AUTO_INCREMENT PRIMARY KEY,
    id_cotizacion INT NOT NULL,
    id_producto INT NOT NULL,
    cantidad INT NOT NULL,
    precio_unitario DECIMAL(10,2) NOT NULL,
    subtotal DECIMAL(10,2) NOT NULL,
    CONSTRAINT fk_det_cotizacion FOREIGN KEY (id_cotizacion) 
        REFERENCES cotizacion(id_cotizacion) ON DELETE CASCADE,
    CONSTRAINT fk_det_cot_producto FOREIGN KEY (id_producto) 
        REFERENCES producto(id_productos)
);

-- 8. Factura Cabecera
CREATE TABLE factura (
    id_factura INT AUTO_INCREMENT PRIMARY KEY,
    id_cliente INT NOT NULL,
    id_carro INT NOT NULL,
    id_cotizacion INT NULL, -- Opcional si se factura directo o desde una cotización
    fecha_emision DATETIME DEFAULT CURRENT_TIMESTAMP,
    total DECIMAL(10,2) NOT NULL,
    estado TINYINT DEFAULT 1, -- 1: Emitida, 2: Anulada
    CONSTRAINT fk_factura_cliente FOREIGN KEY (id_cliente) 
        REFERENCES clienteCarro(id_cliente),
    CONSTRAINT fk_factura_carro FOREIGN KEY (id_carro) 
        REFERENCES carro(id_carro),
    CONSTRAINT fk_factura_cotizacion FOREIGN KEY (id_cotizacion) 
        REFERENCES cotizacion(id_cotizacion)
);

-- 9. Detalle de Factura
CREATE TABLE detalleFactura (
    id_detalle_factura INT AUTO_INCREMENT PRIMARY KEY,
    id_factura INT NOT NULL,
    id_producto INT NOT NULL,
    cantidad INT NOT NULL,
    precio_unitario DECIMAL(10,2) NOT NULL,
    subtotal DECIMAL(10,2) NOT NULL,
    CONSTRAINT fk_det_factura FOREIGN KEY (id_factura) 
        REFERENCES factura(id_factura) ON DELETE CASCADE,
    CONSTRAINT fk_det_fac_producto FOREIGN KEY (id_producto) 
        REFERENCES producto(id_productos)
);