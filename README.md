🖥️ Backend C# - Instrucciones de instalación y ejecución
--
Este es el backend del proyecto desarrollado en C# con .NET, utilizando PostgreSQL como base de datos. A continuación, se describen los pasos necesarios para compilar y ejecutar el proyecto.

✅ Requisitos
 - Visual Studio 2022

 - .NET SDK 7.0 o superior

 - PostgreSQL y pgAdmin

Archivo de respaldo (.backup) incluido en la carpeta BaseDatos

🚀 Instrucciones para compilar y ejecutar
--
1. Abrir el proyecto
 . Abre Visual Studio 2022.

Selecciona y abre la solución del proyecto (.sln).

2. Compilar
 . En el Explorador de soluciones, selecciona el proyecto principal.

 . Haz clic derecho > Compilar.

3. Restaurar la base de datos en pgAdmin
 . Abre pgAdmin.

 . Crea una nueva base de datos llamada: 🗃️ PruebaDevDB

 . Selecciona la base de datos PruebaDevDB en el panel izquierdo.

 . Haz clic derecho > Restore (Restaurar).

 . En el campo Filename, selecciona el archivo .backup ubicado en la carpeta:
 - /BasesDatos/NombreDelArchivo.backup
 . Haz clic en Restore para completar el proceso.

4. Ejecutar el proyecto
--
. Vuelve a Visual Studio.

 . Selecciona el proyecto como proyecto de inicio si aún no lo está.

 . Haz clic en el botón Iniciar (Play).
