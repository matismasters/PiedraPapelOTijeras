namespace PiedraPapelOTijeras.Dominio
{
    public class ResultadoRonda
    {
        public Jugador Jugador1 { get; }
        public Jugador Jugador2 { get; }
        public Juego.Jugada JugadaJugador1 { get; }
        public Juego.Jugada JugadaJugador2 { get; }
        public Jugador? Ganador { get; }
        public bool EsEmpate { get; }
        public string Descripcion { get; }

        public ResultadoRonda(Jugador jugador1, Juego.Jugada jugadaJugador1, Jugador jugador2, Juego.Jugada jugadaJugador2)
        {
            Jugador1 = jugador1 ?? throw new ArgumentNullException(nameof(jugador1));
            Jugador2 = jugador2 ?? throw new ArgumentNullException(nameof(jugador2));
            JugadaJugador1 = jugadaJugador1;
            JugadaJugador2 = jugadaJugador2;

            // Determinar el ganador
            Ganador = DeterminarGanador(jugadaJugador1, jugadaJugador2, jugador1, jugador2);
            EsEmpate = Ganador == null;
            Descripcion = GenerarDescripcion();
        }

        private Jugador? DeterminarGanador(Juego.Jugada jugada1, Juego.Jugada jugada2, Jugador jugador1, Jugador jugador2)
        {
            if (jugada1 == jugada2)
                return null; // Empate

            return jugada1 switch
            {
                Juego.Jugada.Piedra => jugada2 == Juego.Jugada.Tijeras ? jugador1 : jugador2,
                Juego.Jugada.Papel => jugada2 == Juego.Jugada.Piedra ? jugador1 : jugador2,
                Juego.Jugada.Tijeras => jugada2 == Juego.Jugada.Papel ? jugador1 : jugador2,
                _ => throw new InvalidOperationException($"Jugada inválida: {jugada1}")
            };
        }

        private string GenerarDescripcion()
        {
            if (EsEmpate)
                return $"¡Empate! Ambos eligieron {JugadaJugador1}";

            var jugadaGanadora = Ganador == Jugador1 ? JugadaJugador1 : JugadaJugador2;
            var jugadaPerdedora = Ganador == Jugador1 ? JugadaJugador2 : JugadaJugador1;

            var accion = jugadaGanadora switch
            {
                Juego.Jugada.Piedra => "aplasta",
                Juego.Jugada.Papel => "envuelve",
                Juego.Jugada.Tijeras => "corta",
                _ => "vence"
            };

            return $"{Ganador!.Nombre} gana: {jugadaGanadora} {accion} a {jugadaPerdedora}";
        }
    }
}
