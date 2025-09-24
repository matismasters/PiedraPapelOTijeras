using PiedraPapelOTijeras.InterfazUsuario;
using PiedraPapelOTijeras.Servicios;

namespace PiedraPapelOTijeras
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Configurar la consola para mejor experiencia de usuario
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                Console.Title = "Piedra, Papel o Tijeras";

                // Crear las instancias de los servicios
                var servicioJuego = new ServicioJuego();
                var interfazConsola = new InterfazConsola(servicioJuego);

                // Iniciar el juego
                interfazConsola.Ejecutar();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error crítico en el programa: {ex.Message}");
                Console.WriteLine("Presione cualquier tecla para salir...");
                Console.ReadKey();
            }
        }
    }
}
