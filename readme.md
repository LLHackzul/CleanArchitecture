# PruebaClean

Este proyecto es una API para gestionar productos y órdenes, construida con **.NET Core** y configurada para ejecutarse en contenedores **Docker** junto con una base de datos **MySQL**.

## Requisitos Previos

1. Tener instalado **Docker** y **Docker Compose**:
   - [Instalar Docker](https://docs.docker.com/get-docker/)
   - [Instalar Docker Compose](https://docs.docker.com/compose/install/)
2. Clonar este repositorio:
   ```bash
   git clone <URL_DEL_REPOSITORIO>
   cd PruebaClean
   ```

## Archivos Importantes

- **docker-compose.yml**: Orquesta la API y la base de datos MySQL.
- **appsettings.json**: Configuración de la aplicación, como cadenas de conexión y claves de JWT.

## Instrucciones de Ejecución

Sigue estos pasos para levantar el proyecto:

1. **Construir los contenedores:**

   ```bash
   docker-compose build
   ```

2. **Levantar los servicios:**

   ```bash
   docker-compose up
   ```

   Esto iniciará:

   - La API en el puerto **5000**.
   - La base de datos MySQL en el puerto **3306**.

3. **Acceso a la API:**

   Una vez que los contenedores estén en ejecución, puedes acceder a la API desde:

   ```
   http://localhost:5000
   ```

4. **Detener los contenedores:**

   Para detener y eliminar los contenedores:

   ```bash
   docker-compose down
   ```

## Configuración appsettings.json

El archivo `appsettings.json` incluye los valores necesarios para la autenticación JWT y la conexion a BD:

```json
"ConnectionStrings": {
  "prueba": "Server=127.0.0.1;Port=3306;Database=testdb;User=root;Password=db123;"
},
"Jwt": {
  "Key": "x1Qe9zLmW8Rv2Pk4Yo7Bt6CgV3HnJd5Xo",
  "Issuer": "PruebaApiIssuer",
  "Audience": "PruebaApiAudience"
}
```

## Endpoints de la API

1. **Autenticación JWT**:

   - Generar token: `POST /api/Auth/token`

2. **Productos**:

   - Obtener todos los productos: `GET /api/Products`
   - Crear un producto: `POST /api/Products`
   - Actualizar un producto: `PUT /api/Products`
   - Eliminar un producto: `DELETE /api/Products`

3. **Órdenes**:

   - Crear una orden: `POST /api/Orders`

#