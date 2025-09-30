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

        [Theory]
        [InlineData(2)]
        [InlineData(99)]
        public void TestReglasGanadorFinal(int totalJugadasParaGanar)
        {
            // Arrange
            Juego juego = new Juego(_jugador1, _jugador2, totalJugadasParaGanar);

            // Act
            for (int i = 1; i <= totalJugadasParaGanar; i++)
            {
                juego.JugarRonda(Juego.Jugada.Piedra, Juego.Jugada.Tijeras);
            }

            // Assert
            Assert.Equal(_jugador1, juego.ObtenerGanador());
        }
    }
}
