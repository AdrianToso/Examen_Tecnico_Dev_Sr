class Program
{
    static char[,] mapaOriginal;
    static int[,] idHabitacion;
    static int alto, ancho;

   static Dictionary<int, ConsoleColor> coloresPorIdHabitacion = new Dictionary<int, ConsoleColor>();

    static void Main()
    {
        var plano = ObtenerPlanoDeEjemplo();

        CargarMapaDesdePlano(plano);
        IdentificarPuertas();
        AsignarIdAHabitaciones();
        MostrarMapaColoreadoEnConsola();

        Console.WriteLine("\nPresiona cualquier tecla para salir.");
        Console.ReadKey();
    }

    static string[] ObtenerPlanoDeEjemplo() => new[]
    {
        "##########",
        "#   #    #",
        "#   #    #",
        "## #### ##",
        "#        #",
        "#        #",
        "#  #######",
        "#  #  #  #",
        "#        #",
        "##########"
    };

    /// <summary>
    /// Carga el plano ASCII en un array 2D de caracteres.
    /// Calcula el alto y ancho del mapa.
    /// Rellena las líneas más cortas con espacios para mantener un ancho uniforme.
    /// Inicializa el array idHabitacion con ceros.
    /// </summary>
    /// <param name="plano">Array de strings que representa el plano ASCII.</param>
    static void CargarMapaDesdePlano(string[] plano)
    {
        alto = plano.Length;
        // Calcula el ancho máximo basándose en la línea más larga.
        ancho = plano.Length > 0 ? plano.Max(linea => linea.Length) : 0;
        mapaOriginal = new char[alto, ancho];
        idHabitacion = new int[alto, ancho];

        for (int fila = 0; fila < alto; fila++)
        {
            var linea = plano[fila];
            for (int columna = 0; columna < ancho; columna++)
            {
                if (columna < linea.Length)
                {
                    mapaOriginal[fila, columna] = linea[columna];
                }
                else
                {
                    mapaOriginal[fila, columna] = ' ';
                }
            }
        }
    }

    /// <summary>
    /// Identifica las "puertas" en el plano y las marca en el array idHabitacion con -1.
    /// Una puerta es un espacio (' ') que divide dos paredes colineales.
    /// </summary>
    static void IdentificarPuertas()
    {
        for (int fila = 0; fila < alto; fila++)
        {
            for (int columna = 0; columna < ancho; columna++)
            {
                if (mapaOriginal[fila, columna] == ' ')
                {
                    bool esPuertaHorizontal = false;
                    bool esPuertaVertical = false;

                    if (columna > 0 && columna < ancho - 1 &&
                        mapaOriginal[fila, columna - 1] == '#' && mapaOriginal[fila, columna + 1] == '#')
                    {
                        esPuertaHorizontal = true;
                    }

                    if (fila > 0 && fila < alto - 1 &&
                        mapaOriginal[fila - 1, columna] == '#' && mapaOriginal[fila + 1, columna] == '#')
                    {
                        esPuertaVertical = true;
                    }
                    if (esPuertaHorizontal || esPuertaVertical)
                    {
                        idHabitacion[fila, columna] = -1;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Recorre el mapa e identifica cada habitación única utilizando un algoritmo de "flood-fill".
    /// Asigna un ID numérico secuencial a cada habitación encontrada.
    /// Las puertas (-1 en idHabitacion) actúan como barreras para el flood-fill.
    /// </summary>
    static void AsignarIdAHabitaciones()
    {
        int proximoId = 1; 

        for (int fila = 0; fila < alto; fila++)
        {
            for (int columna = 0; columna < ancho; columna++)
            {
                if (mapaOriginal[fila, columna] == ' ' && idHabitacion[fila, columna] == 0)
                {
                    RellenarHabitacion(fila, columna, proximoId);
                    proximoId++;
                }
            }
        }
    }

    /// <summary>
    /// Implementa el algoritmo de "flood-fill" para marcar todos los espacios conectados
    /// de una habitación con el mismo ID. Se detiene en paredes ('#'), en puertas (-1),
    /// o en celdas ya visitadas.
    /// </summary>
    /// <param name="fila">Fila actual.</param>
    /// <param name="columna">Columna actual.</param>
    /// <param name="id">ID de la habitación actual.</param>
    static void RellenarHabitacion(int fila, int columna, int id)
    {
        // Condiciones de parada:
        // 1. La celda está fuera de los límites del mapa.
        // 2. La celda es una pared ('#').
        // 3. La celda ya ha sido asignada a una habitación (idHabitacion != 0, lo que incluye puertas -1).
        if (!EstaDentroDelMapa(fila, columna) ||
            mapaOriginal[fila, columna] == '#' ||
            idHabitacion[fila, columna] != 0) 
        {
            return;
        }

        // Asigna el ID de la habitación a la celda actual
        idHabitacion[fila, columna] = id;

        RellenarHabitacion(fila + 1, columna, id); // Abajo
        RellenarHabitacion(fila - 1, columna, id); // Arriba
        RellenarHabitacion(fila, columna + 1, id); // Derecha
        RellenarHabitacion(fila, columna - 1, id); // Izquierda
    }

    /// <summary>
    /// Verifica si una celda dada por sus coordenadas está dentro de los límites del mapa.
    /// </summary>
    /// <param name="fila">Fila a verificar.</param>
    /// <param name="columna">Columna a verificar.</param>
    /// <returns>True si la celda está dentro del mapa, False en caso contrario.</returns>
    static bool EstaDentroDelMapa(int fila, int columna)
    {
        return fila >= 0 && fila < alto && columna >= 0 && columna < ancho;
    }

    /// <summary>
    /// Muestra el mapa en la consola, aplicando colores de fondo a cada habitación.
    /// Las paredes se muestran como '#' y los espacios de las habitaciones se muestran
    /// como espacios con un color de fondo. 
    /// Las puertas se muestran como espacios en blanco sin color de fondo.
    /// </summary>
    static void MostrarMapaColoreadoEnConsola()
    {
        ConsoleColor[] coloresDisponibles = {
            ConsoleColor.Red, ConsoleColor.Green, ConsoleColor.Blue,
            ConsoleColor.Yellow, ConsoleColor.Magenta, ConsoleColor.Cyan,
            ConsoleColor.DarkRed, ConsoleColor.DarkGreen, ConsoleColor.DarkBlue,
            ConsoleColor.DarkYellow, ConsoleColor.DarkMagenta, ConsoleColor.DarkCyan,
            ConsoleColor.Gray, ConsoleColor.White 
        };
        int colorIndex = 0;

        Console.ResetColor();

        for (int fila = 0; fila < alto; fila++)
        {
            for (int columna = 0; columna < ancho; columna++)
            {
                char caracterOriginal = mapaOriginal[fila, columna];
                int currentRoomId = idHabitacion[fila, columna];

                if (caracterOriginal == '#')
                {
                    Console.ResetColor();
                    Console.Write('#');
                }
                else if (currentRoomId > 0) 
                {
                    if (!coloresPorIdHabitacion.ContainsKey(currentRoomId))
                    {
                        coloresPorIdHabitacion[currentRoomId] = coloresDisponibles[colorIndex % coloresDisponibles.Length];
                        colorIndex++;
                    }
                    Console.BackgroundColor = coloresPorIdHabitacion[currentRoomId]; 
                    Console.Write(' ');
                    Console.ResetColor();                 
                }
                else
                {
                    Console.ResetColor(); 
                    Console.Write(' '); 
                }
            }
            Console.WriteLine(); 
        }
    }
}
