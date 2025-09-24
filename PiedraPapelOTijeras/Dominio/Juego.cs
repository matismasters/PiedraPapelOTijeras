namespace PiedraPapelOTijeras.Dominio
{
    public class Juego
    {
        public enum Jugada
        {
            Piedra = 1,
            Papel = 2,
            Tijeras = 3
        }

        public Jugador Jugador1 { get; }
        public Jugador Jugador2 { get; }
        public int PuntajeParaGanar { get; }
        public bool HaTerminado => ObtenerGanador() != null;
        public List<ResultadoRonda> HistorialRondas { get; }

        public Juego(Jugador jugador1, Jugador jugador2, int puntajeParaGanar = 3)
        {
            Jugador1 = jugador1 ?? throw new ArgumentNullException(nameof(jugador1));
            Jugador2 = jugador2 ?? throw new ArgumentNullException(nameof(jugador2));
            
            if (puntajeParaGanar < 1)
                throw new ArgumentException("El puntaje para ganar debe ser mayor a 0", nameof(puntajeParaGanar));
            
            PuntajeParaGanar = puntajeParaGanar;
            HistorialRondas = new List<ResultadoRonda>();
        }

        public ResultadoRonda JugarRonda(Jugada jugadaJugador1, Jugada jugadaJugador2)
        {
            if (HaTerminado)
                throw new InvalidOperationException("El juego ya ha terminado");

            var resultado = new ResultadoRonda(Jugador1, jugadaJugador1, Jugador2, jugadaJugador2);
            
            if (!resultado.EsEmpate)
            {
                resultado.Ganador!.IncrementarPuntaje();
            }

            HistorialRondas.Add(resultado);
            return resultado;
        }

        public Jugador? ObtenerGanador()
        {
            if (Jugador1.Puntaje >= PuntajeParaGanar)
                return Jugador1;
            
            if (Jugador2.Puntaje >= PuntajeParaGanar)
                return Jugador2;
            
            return null;
        }

        public void Reiniciar()
        {
            Jugador1.ReiniciarPuntaje();
            Jugador2.ReiniciarPuntaje();
            HistorialRondas.Clear();
        }

        public string ObtenerEstadoJuego()
        {
            if (HaTerminado)
            {
                var ganador = ObtenerGanador()!;
                var puntajeOponente = ganador == Jugador1 ? Jugador2.Puntaje : Jugador1.Puntaje;
                return $"🎉 ¡{ganador.Nombre} ha ganado la partida! ({ganador.Puntaje}-{puntajeOponente})";
            }

            return $"{Jugador1} vs {Jugador2} | Primero en llegar a {PuntajeParaGanar} gana";
        }
    }
}