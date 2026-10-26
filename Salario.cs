using System;
using System.Collections.Generic;

// Una empresa de desarrollo de software gestiona dos tipos de empleados.
// Con distintas estructuras salariales:
// Dev: Salario base + bono variable por cada proyecto completado.
// Gerentes: Salario base + bono fijo por gestion directiva.
// Objetivo: Calcular el salario total y mostrar la información consolidada utilizando Herencia y polimorfismo.

public class helloworld
{
    public class Empleado
    {
        private string nombre;
        private int edad;
        protected decimal salarioBase;

        public Empleado(string nombre, int edad, decimal salarioBase)
        {
            this.nombre = nombre;
            this.edad = edad;
            this.salarioBase = salarioBase;
        }

        public string Nombre
        {
            get { return nombre; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El nombre no debe de estar vacío.");

                nombre = value;
            }
        }

        public int Edad
        {
            get { return edad; }
            set
            {
                if (value < 18 || value > 65)
                    throw new ArgumentException("¡Edad no válida para trabajar!");

                edad = value;
            }
        }

        public decimal SalarioBase
        {
            get { return salarioBase; }
            set { salarioBase = value; }
        }

        public virtual decimal CalcularSalario()
        {
            return SalarioBase;
        }

        public virtual string MostrarInf()
        {
            return $"Nombre: {nombre}, Edad: {edad}, Salario total: {CalcularSalario():c}";
        }
    }

    public class Gerente : Empleado
    {
        private const decimal BonoGestion = 800000;

        public Gerente(string nombre, int edad, decimal salarioBase)
            : base(nombre, edad, salarioBase)
        {
        }

        public override decimal CalcularSalario()
        {
            return SalarioBase + BonoGestion;
        }

        public override string MostrarInf()
        {
            return base.MostrarInf() + " | Rol: Gerente";
        }
    }

    public static void Main(string[] args)
    {
        List<Empleado> empleados = new List<Empleado>
        {
            new Gerente("Dayanna Peña", 19, 4000000)
        };

        foreach (Empleado e in empleados)
        {
            Console.WriteLine(e.MostrarInf());
        }
    }
}
