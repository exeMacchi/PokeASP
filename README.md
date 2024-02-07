# Pokedex Web
### Descripción del Proyecto
Pokedex web desarrollada utilizando ASP.NET Web Forms para la gestión de información detallada sobre diferentes Pokémon.

### Características Principales
1. **ASP.NET Web Forms:** la aplicación está construida utilizando ASP.NET Web Forms.
2. **Bootstrap para estilos:** se ha utilizado el framework Bootstrap para aplicar estilos consistentes y responsivos a la interfaz de usuario de la Pokedex.
3. **Conexión a Base de Datos SQL Server:** la aplicación se integra con una base de datos SQL Server para almacenar y gestionar la información de los Pokémon.

### Funcionalidades
- **CRUD completo**: esta aplicación web te permite Crear, Leer, Modificar y Eliminar un Pokémon en la base de datos. La eliminación se podrá realizar de dos formas:
	- *Eliminación lógica*: los Pokémon eliminados desde la tabla de administrador se inactivarán en la base de datos, por lo que no se mostrarán desde el lado del cliente, pero seguirán existiendo en la base de datos.
	- *Eliminación física*: el administrador igualmente podrá eliminar el Pokémon definitivamente de la base de datos en una tabla especial.
- Búsqueda y filtrado básico de Pokémon según su nombre.
- Búsqueda y filtrado avanzado de un Pokémon según su nombre, número o tipo.
