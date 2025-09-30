using PiedraPapelOTijeras.Dominio;

namespace PiedraPapelOTijeras.Tests
{
    public class ResultadoRondaTests
    {
        private readonly Jugador _jugador1;
        private readonly Jugador _jugador2;

        public ResultadoRondaTests()
        {
            _jugador1 = new Jugador("Jugador1");
            _jugador2 = new Jugador("Jugador2");
        }

        [Theory]
        [InlineData(Juego.Jugada.Piedra, Juego.Jugada.Tijeras)]
        [InlineData(Juego.Jugada.Tijeras, Juego.Jugada.Papel)]
        [InlineData(Juego.Jugada.Papel, Juego.Jugada.Piedra)]
        public void TestReglasBasicas(Juego.Jugada jugadaGanadora, Juego.Jugada jugadaPerdedora)
        {
            // Arrange
            ResultadoRonda resultadoRonda = new ResultadoRonda(
                _jugador1,
                jugadaGanadora,
                _jugador2,
                jugadaPerdedora
            );

            // Act & Assert
            Assert.Equal(_jugador1.Nombre, resultadoRonda.Ganador.Nombre);
            Assert.False(resultadoRonda.EsEmpate);
        }

        [Theory]
        [InlineData(Juego.Jugada.Piedra)]
        [InlineData(Juego.Jugada.Tijeras)]
        [InlineData(Juego.Jugada.Papel)]
        public void TestReglasEmpate(Juego.Jugada jugada)
        {
            // Arrange
            ResultadoRonda resultadoRonda = new ResultadoRonda(
                _jugador1,
                jugada,
                _jugador2,
                jugada
            );

            // Act & Assert
            Assert.Null(resultadoRonda.Ganador);
            Assert.True(resultadoRonda.EsEmpate);
        }
    }
}
