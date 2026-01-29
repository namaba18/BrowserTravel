BrowserTravel - Prueba tecnica 

Es un sistema de búsqueda de vehículos, diseñado con Clean Arquitecture, CQRS en .Net 9.

Esta aplicación expone una Web API que permite buscar vehículos disponibles según:
- Localidad de recogida
- Localidad de devolución
- Fecha y hora de recogida
- Fecha y hora de devolución
El sistema aplica reglas reales de negocio como disponibilidad por localidad, mercado, rango de fechas y estado del vehículo.

Utiliza dos bases de datos
MySQL que se utiliza para la informacion transaccional (vehiculos, reservas, localidades)
MongoDB que se usa para informacion no transaccional (tipo de vehículos)

En esta prueba se utilizaron patrones y principios como:
- Clean Architecture
- CQRS (Queries / Commands)
- MediatR
- Repository Pattern
- Domain Events
- Dependency Injection
- SOLID / DRY / KISS
- Testing (Unit + Integration)

Configuracion:

En appsettings.json se deben agregar las cadenas de conexion a las bases de datos respectivamente. 
por ejemplo:

"ConnectionStrings": {
  "Database": "server=localhost;Port=3307;Database=browsertravel;User=browsertravel;Pwd=12345"
}

Se usa EF core + Pomelo para la base de datos de MySQL 

El principal Endpoint es GET /cars en el cual se van a consultar los vehiculos disponibles segun la localidad y el rango de fechas dados por esto necesita los siguientes parametros:
Parámetro	  !  Tipo
'''''''''''''''''''''''
locationId	!  Guid
startDate	  !  DateTime
endDate	    !  DateTime

para ejecutar en postman se puede usar la siguiente ruta en el metodo get:

http://localhost:5226/cars?LocationId=08de5e1d-8810-4c34-8859-404b98cbedce&StartDate=2026-03-10T10:00:00&EndDate=2026-03-14T18:00:00

La api ya tiene unos datos precargados en un seed. 

En la aplicación queda pendiente implementar logs y un mejor manejo de errores ya que por cuestiones de tiempo no se pudo desarrollar 

Natalia Mantilla
Backend Developer – .NET
Prueba técnica – Browser Travel 
