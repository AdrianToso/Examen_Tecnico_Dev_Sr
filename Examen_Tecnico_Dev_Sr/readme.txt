=====================================================
HABITACIONES COLOREADAS
=====================================================

Lenguaje de Programación Utilizado:
----------------------------------
C#

Versión de .NET / Herramientas Empleadas:
------------------------------------------
* **SDK de .NET:** .NET 8.0 
    * Este proyecto fue desarrollado utilizando el SDK de .NET 8.0, que permite la compilación y ejecución de la aplicación de consola.
    * Compatible con cualquier entorno que soporte .NET 8.0 (Windows, macOS, Linux).
* **Entorno de Desarrollo
    * Visual Studio 2022 o Visual Studio Code (con la extensión de C#).
    * La solución puede compilarse y ejecutarse desde la línea de comandos usando `dotnet run` si el SDK está instalado.

Instrucciones de Ejecución:
---------------------------
1.  Asegúrate de tener el SDK de .NET 8.0 instalado en tu sistema.
2.  Navega a la carpeta raíz del proyecto (donde se encuentra el archivo .csproj y los archivos .cs).
3.  Ejecuta el siguiente comando en tu terminal:
    `dotnet run`

Descripción Breve del Ejercicio:
--------------------------------
Este programa procesa un plano de planta ASCII (definido internamente para el ejemplo) y lo representa en la consola, coloreando cada habitación de forma única. 
Las paredes (`#`) y los espacios (` `) son identificados, y las puertas son usadas como barreras para delimitar correctamente las habitaciones. 
Se utiliza un algoritmo de relleno por inundación (flood-fill) para asignar IDs a cada habitación y luego se visualizan con distintos colores de fondo en la consola.

=====================================================