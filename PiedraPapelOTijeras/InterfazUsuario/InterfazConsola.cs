using PiedraPapelOTijeras.Dominio;
using PiedraPapelOTijeras.Servicios;

namespace PiedraPapelOTijeras.InterfazUsuario
{
    public class InterfazConsola
    {
        private readonly ServicioJuego _servicioJuego;

        public InterfazConsola(ServicioJuego servicioJuego)
        {
            _servicioJuego = servicioJuego ?? throw new ArgumentNullException(nameof(servicioJuego));
        }

        public void Ejecutar()
        {
            MostrarBienvenida();
            
            bool continuar = true;
            while (continuar)
            {
                try
                {
                    var juego = ConfigurarNuevaPartida();
                    JugarPartida(juego);
                    continuar = PreguntarSiJugarOtraPartida();
                }
                catch (Exception ex)
                {
                    MostrarError($"Error inesperado: {ex.Message}");
                    continuar = false;
                }
            }

            MostrarDespedida();
        }

        private void MostrarBienvenida()
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════════╗");
            Console.WriteLine("║        🎮 PIEDRA, PAPEL O TIJERAS 🎮        ║");
            Console.WriteLine("╚══════════════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine("¡Bienvenido al clásico juego de Piedra, Papel o Tijeras!");
            Console.WriteLine();
        }

        private Juego ConfigurarNuevaPartida()
        {
            Console.WriteLine("=== CONFIGURACIÓN DE LA PARTIDA ===");
            Console.WriteLine();

            string nombreJugador1 = SolicitarNombreJugador("Ingrese el nombre del Jugador 1: ");
            string nombreJugador2 = SolicitarNombreJugador("Ingrese el nombre del Jugador 2: ");
            int puntajeParaGanar = SolicitarPuntajeParaGanar();

            Console.WriteLine();
            Console.WriteLine($"¡Perfecto! {nombreJugador1} vs {nombreJugador2}");
            Console.WriteLine($"Primero en llegar a {puntajeParaGanar} puntos gana la partida.");
            Console.WriteLine();
            Console.WriteLine("Presione cualquier tecla para comenzar...");
            Console.ReadKey();

            return _servicioJuego.IniciarNuevaPartida(nombreJugador1, nombreJugador2, puntajeParaGanar);
        }

        private string SolicitarNombreJugador(string mensaje)
        {
            string nombre;
            do
            {
                Console.Write(mensaje);
                nombre = Console.ReadLine()?.Trim() ?? "";
                
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    Console.WriteLine("❌ El nombre no puede estar vacío. Intente nuevamente.");
                }
            }
            while (string.IsNullOrWhiteSpace(nombre));

            return nombre;
        }

        private int SolicitarPuntajeParaGanar()
        {
            int puntaje;
            do
            {
                Console.Write("¿A cuántos puntos quieren jugar? (por defecto 3): ");
                string entrada = Console.ReadLine()?.Trim() ?? "";
                
                if (string.IsNullOrWhiteSpace(entrada))
                    return 3; // Valor por defecto

                if (!int.TryParse(entrada, out puntaje) || puntaje < 1)
                {
                    Console.WriteLine("❌ Debe ingresar un número mayor a 0. Intente nuevamente.");
                    puntaje = 0;
                }
            }
            while (puntaje < 1);

            return puntaje;
        }

        private void JugarPartida(Juego juego)
        {
            Console.Clear();
            
            while (!juego.HaTerminado)
            {
                MostrarEstadoPartida(juego);
                var resultadoRonda = JugarRonda(juego);
                MostrarResultadoRonda(resultadoRonda);
                
                if (!juego.HaTerminado)
                {
                    Console.WriteLine();
                    Console.WriteLine("Presione cualquier tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }

            MostrarGanadorPartida(juego);
        }

        private void MostrarEstadoPartida(Juego juego)
        {
            Console.WriteLine("╔══════════════════════════════════════════════╗");
            Console.WriteLine("║              ESTADO DE LA PARTIDA            ║");
            Console.WriteLine("╚══════════════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine(juego.ObtenerEstadoJuego());
            Console.WriteLine();
            Console.WriteLine(_servicioJuego.ObtenerOpcionesJugada());
            Console.WriteLine();
        }

        private ResultadoRonda JugarRonda(Juego juego)
        {
            var jugadaJugador1 = SolicitarJugada(juego.Jugador1.Nombre);
            var jugadaJugador2 = SolicitarJugada(juego.Jugador2.Nombre);

            return _servicioJuego.JugarRonda(jugadaJugador1, jugadaJugador2);
        }

        private Juego.Jugada SolicitarJugada(string nombreJugador)
        {
            Juego.Jugada jugada;
            bool jugadaValida;
            
            do
            {
                Console.Write($"{nombreJugador}, elige tu jugada: ");
                string entrada = Console.ReadLine() ?? "";
                
                jugadaValida = _servicioJuego.ValidarJugada(entrada, out jugada);
                
                if (!jugadaValida)
                {
                    Console.WriteLine("❌ Jugada inválida. Use: 1 (Piedra), 2 (Papel) o 3 (Tijeras)");
                }
            }
            while (!jugadaValida);

            return jugada;
        }

        private void MostrarResultadoRonda(ResultadoRonda resultado)
        {
            Console.WriteLine();
            Console.WriteLine("═══════════════════════════════════════════════");
            Console.WriteLine("           RESULTADO DE LA RONDA");
            Console.WriteLine("═══════════════════════════════════════════════");
            Console.WriteLine();
            
            Console.WriteLine($"{resultado.Jugador1.Nombre}: {resultado.JugadaJugador1}");
            Console.WriteLine($"{resultado.Jugador2.Nombre}: {resultado.JugadaJugador2}");
            Console.WriteLine();
            
            if (resultado.EsEmpate)
            {
                Console.WriteLine("🤝 " + resultado.Descripcion);
            }
            else
            {
                Console.WriteLine("🎉 " + resultado.Descripcion);
                Console.WriteLine($"   {resultado.Ganador!.Nombre} gana 1 punto!");
            }
            
            Console.WriteLine();
            Console.WriteLine("--- Puntajes actuales ---");
            Console.WriteLine($"{resultado.Jugador1.Nombre}: {resultado.Jugador1.Puntaje} puntos");
            Console.WriteLine($"{resultado.Jugador2.Nombre}: {resultado.Jugador2.Puntaje} puntos");
        }

        private void MostrarGanadorPartida(Juego juego)
        {
            Console.WriteLine();
            Console.WriteLine("╔══════════════════════════════════════════════╗");
            Console.WriteLine("║                PARTIDA TERMINADA             ║");
            Console.WriteLine("╚══════════════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine(juego.ObtenerEstadoJuego());
            Console.WriteLine();
            
            if (juego.HistorialRondas.Count > 0)
            {
                Console.WriteLine("--- Resumen de la partida ---");
                Console.WriteLine($"Rondas jugadas: {juego.HistorialRondas.Count}");
                Console.WriteLine($"Empates: {juego.HistorialRondas.Count(r => r.EsEmpate)}");
                Console.WriteLine();
            }
        }

        private bool PreguntarSiJugarOtraPartida()
        {
            Console.WriteLine();
            Console.Write("¿Desean jugar otra partida? (s/n): ");
            string respuesta = Console.ReadLine()?.Trim().ToLower() ?? "";
            
            return respuesta == "s" || respuesta == "si" || respuesta == "sí" || respuesta == "y" || respuesta == "yes";
        }

        private void MostrarDespedida()
        {
            Console.WriteLine();
            Console.WriteLine("╔══════════════════════════════════════════════╗");
            Console.WriteLine("║            ¡GRACIAS POR JUGAR!               ║");
            Console.WriteLine("║         Piedra, Papel o Tijeras              ║");
            Console.WriteLine("╚══════════════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine("¡Hasta la próxima! 👋");
        }

        private void MostrarError(string mensaje)
        {
            Console.WriteLine();
            Console.WriteLine($"❌ {mensaje}");
            Console.WriteLine();
        }
    }
}
