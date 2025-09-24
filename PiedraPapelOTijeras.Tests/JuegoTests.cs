using PiedraPapelOTijeras.Dominio;

namespace PiedraPapelOTijeras.Tests
{
    public class JuegoTests
    {
        private readonly Jugador _jugador1;
        private readonly Jugador _jugador2;

        public JuegoTests()
        {
            _jugador1 = new Jugador("Ana");
            _jugador2 = new Jugador("Luis");
        }

    }
}
