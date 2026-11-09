using System;

class HabitacionHotel
{
    private int numero;
    private string tipo;
    private double precioPorNoche;
    private bool disponible;
    private string clienteActual;

    // PROPIEDADES
    public int Numero
    {
        get { return numero; }
    }

    public string Tipo
    {
        get { return tipo; }
        set
        {
            if (value == "Individual" || value == "Doble" || value == "Suite")
            {
                tipo = value;
            }
            else
            {
                throw new ArgumentException(
                    "El tipo debe ser Individual, Doble o Suite.");
            }
        }
    }

    public double PrecioPorNoche
    {
        get { return precioPorNoche; }
        set
        {
            if (value > 0)
            {
                precioPorNoche = value;
            }
            else
            {
                throw new ArgumentException(
                    "El precio por noche debe ser mayor a cero.");
            }
        }
    }

    public bool Disponible
    {
        get { return disponible; }
    }

    public string ClienteActual
    {
        get { return clienteActual; }
    }

    // CONSTRUCTOR
    public HabitacionHotel(int numero, string tipo, double precioPorNoche)
    {
        if (numero <= 0)
        {
            throw new ArgumentException(
                "El número de habitación debe ser positivo.");
        }

        this.numero = numero;
        Tipo = tipo;
        PrecioPorNoche = precioPorNoche;
        disponible = true;
        clienteActual = "";
    }

    // METODO RESERVAR
    public double Reservar(string nombreCliente, int noches)
    {
        if (!disponible)
        {
            return -1;
        }

        if (string.IsNullOrWhiteSpace(nombreCliente) || noches <= 0)
        {
            throw new ArgumentException(
                "Debe ingresar un cliente y una cantidad de noches válida.");
        }

        disponible = false;
        clienteActual = nombreCliente;

        return precioPorNoche * noches;
    }

    // METODO LIBERAR
    public bool Liberar()
    {
        if (disponible)
        {
            return false;
        }

        disponible = true;
        clienteActual = "";

        return true;
    }

    // METODO MOSTRAR INFORMACION
    public string MostrarInfo()
    {
        string estado = disponible ? "Disponible" : "Ocupada";
        string cliente = disponible ? "Ninguno" : clienteActual;

        return $"Habitación: {numero}\n" +
               $"Tipo: {tipo}\n" +
               $"Precio por noche: {precioPorNoche:C0}\n" +
               $"Estado: {estado}\n" +
               $"Cliente actual: {cliente}\n";
    }

    // A. METODO APLICAR DESCUENTO
    public void AplicarDescuento(int porcentaje)
    {
        if (porcentaje < 0 || porcentaje >= 100)
        {
            throw new ArgumentException(
                "El descuento debe estar entre 0 y 99.");
        }

        double nuevoPrecio = precioPorNoche *
                             (1 - porcentaje / 100.0);

        if (nuevoPrecio <= 0)
        {
            throw new ArgumentException(
                "El precio resultante no puede ser cero o negativo.");
        }

        precioPorNoche = nuevoPrecio;
    }

    // B. NUEVO METODO OBTENER DATOS RESUMEN
    public string ObtenerDatosResumen()
    {
        string estado = disponible
            ? "Disponible"
            : "Ocupada por: " + clienteActual;

        return $"[{numero}] {tipo} - {precioPorNoche:C0} - {estado}";
    }
}

// CLASE PRINCIPAL
class Program
{
    static void Main(string[] args)
    {
        // CREACION DE HABITACIONES

        HabitacionHotel habitacion101 =
            new HabitacionHotel(101, "Individual", 50000);

        HabitacionHotel habitacion202 =
            new HabitacionHotel(202, "Doble", 80000);

        HabitacionHotel habitacion303 =
            new HabitacionHotel(303, "Suite", 150000);

        HabitacionHotel[] habitaciones =
        {
            habitacion101,
            habitacion202,
            habitacion303
        };

        // ESTADO INICIAL

        Console.WriteLine("=== ESTADO INICIAL ===");

        foreach (HabitacionHotel habitacion in habitaciones)
        {
            Console.WriteLine(habitacion.MostrarInfo());
        }

        // OPERACIONES

        Console.WriteLine("=== OPERACIONES ===");

        double costo1 = habitacion101.Reservar("Juana Pérez", 3);

        if (costo1 != -1)
        {
            Console.WriteLine(
                $"Reserva de Juana Pérez: ${costo1:N0}");
        }
        else
        {
            Console.WriteLine("La habitación 101 no está disponible.");
        }

        double costo2 = habitacion202.Reservar("Luis Gómez", 2);

        if (costo2 != -1)
        {
            Console.WriteLine(
                $"Reserva de Luis Gómez: ${costo2:N0}");
        }

        double costo3 = habitacion101.Reservar("Otro cliente", 2);

        if (costo3 == -1)
        {
            Console.WriteLine(
                "La segunda reserva de la habitación 101 falló.");
        }

        bool liberada = habitacion101.Liberar();

        Console.WriteLine(
            liberada
                ? "La habitación 101 fue liberada."
                : "La habitación 101 ya estaba disponible.");

        habitacion303.AplicarDescuento(10);

        Console.WriteLine(
            $"Nuevo precio de la habitación 303: " +
            $"${habitacion303.PrecioPorNoche:N0}");

        double costo4 = habitacion101.Reservar("Carlos Ruiz", 1);

        if (costo4 != -1)
        {
            Console.WriteLine(
                $"Reserva de Carlos Ruiz: ${costo4:N0}");
        }

        // MOSTRAR RESUMEN DE LAS HABITACIONES

        Console.WriteLine("\n=== RESUMEN DE HABITACIONES ===");

        foreach (HabitacionHotel habitacion in habitaciones)
        {
            Console.WriteLine(habitacion.ObtenerDatosResumen());
        }

        // A. CONTAR HABITACIONES LIBRES Y OCUPADAS

        int ocupadas = 0;
        int libres = 0;

        foreach (HabitacionHotel habitacion in habitaciones)
        {
            if (habitacion.Disponible)
            {
                libres++;
            }
            else
            {
                ocupadas++;
            }
        }

        Console.WriteLine("\n=== METRICAS DEL HOTEL ===");

        Console.WriteLine($"Habitaciones ocupadas: {ocupadas}");
        Console.WriteLine($"Habitaciones libres: {libres}");

        // B. CALCULAR PRECIO PROMEDIO

        double sumaPrecios = 0;

        foreach (HabitacionHotel habitacion in habitaciones)
        {
            sumaPrecios += habitacion.PrecioPorNoche;
        }

        double promedio = sumaPrecios / habitaciones.Length;

        Console.WriteLine(
            $"Precio promedio por noche: ${promedio:N0}");

        // ESTADO FINAL

        Console.WriteLine("\n=== ESTADO FINAL ===");

        foreach (HabitacionHotel habitacion in habitaciones)
        {
            Console.WriteLine(habitacion.MostrarInfo());
        }
    }
}
