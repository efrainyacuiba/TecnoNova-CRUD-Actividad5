-- Ejecutar dentro de la base de datos tecnonova_crud.
-- Solo necesitamos 2 tablas para esta actividad.

CREATE TABLE IF NOT EXISTS categorias (
    id INTEGER PRIMARY KEY,
    nombre VARCHAR(60) NOT NULL UNIQUE
);

CREATE TABLE IF NOT EXISTS productos (
    id INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    sku VARCHAR(30) NOT NULL UNIQUE,
    nombre VARCHAR(100) NOT NULL,
    precio NUMERIC(12,2) NOT NULL CHECK (precio > 0),
    stock INTEGER NOT NULL CHECK (stock >= 0),
    categoria_id INTEGER NOT NULL REFERENCES categorias(id)
);

INSERT INTO categorias (id, nombre) VALUES
    (1, 'Laptops'),
    (2, 'Celulares'),
    (3, 'Accesorios'),
    (4, 'Componentes')
ON CONFLICT (id) DO NOTHING;

-- Cuatro ejemplos adaptados del catálogo de TecnoNova Bolivia.
INSERT INTO productos (sku, nombre, precio, stock, categoria_id) VALUES
    ('TN-LAP-1001', 'Lenovo Laptop A101', 4255.92, 5, 1),
    ('TN-CEL-1002', 'ASUS Smartphone B102', 3405.84, 26, 2),
    ('TN-ACC-1012', 'Logitech Hub USB L112', 308.00, 18, 3),
    ('TN-COM-1005', 'Logitech GPU E105', 4332.24, 14, 4)
ON CONFLICT (sku) DO NOTHING;
