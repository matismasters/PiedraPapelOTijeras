using PiedraPapelOTijeras.Dominio;

namespace PiedraPapelOTijeras.Servicios
{
    public class ServicioJuego
    {
        private Juego? _juegoActual;

        public Juego IniciarNuevaPartida(string nombreJugador1, string nombreJugador2, int puntajeParaGanar = 3)
        {
            var jugador1 = new Jugador(nombreJugador1);
            var jugador2 = new Jugador(nombreJugador2);
            
            _juegoActual = new Juego(jugador1, jugador2, puntajeParaGanar);
            return _juegoActual;
        }

        public Juego? ObtenerJuegoActual()
        {
            return _juegoActual;
        }

        public bool ValidarJugada(string entrada, out Juego.Jugada jugada)
        {
            jugada = Juego.Jugada.Piedra; // Valor por defecto

            if (string.IsNullOrWhiteSpace(entrada))
                return false;

            entrada = entrada.Trim().ToLower();

            return entrada switch
            {
                "1" or "piedra" => TrySetJugada(Juego.Jugada.Piedra, out jugada),
                "2" or "papel" => TrySetJugada(Juego.Jugada.Papel, out jugada),
                "3" or "tijeras" => TrySetJugada(Juego.Jugada.Tijeras, out jugada),
                _ => false
            };
        }

        private static bool TrySetJugada(Juego.Jugada jugadaValida, out Juego.Jugada jugada)
        {
            jugada = jugadaValida;
            return true;
        }

        public ResultadoRonda JugarRonda(Juego.Jugada jugadaJugador1, Juego.Jugada jugadaJugador2)
        {
            if (_juegoActual == null)
                throw new InvalidOperationException("No hay partida iniciada");

            return _juegoActual.JugarRonda(jugadaJugador1, jugadaJugador2);
        }

        public void ReiniciarPartida()
        {
            if (_juegoActual == null)
                throw new InvalidOperationException("No hay partida iniciada");

            _juegoActual.Reiniciar();
        }

        public bool HayPartidaIniciada()
        {
            return _juegoActual != null;
        }

        public string ObtenerOpcionesJugada()
        {
            return "1. Piedra  |  2. Papel  |  3. Tijeras";
        }
    }
}
