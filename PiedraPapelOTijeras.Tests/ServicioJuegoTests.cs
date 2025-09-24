using PiedraPapelOTijeras.Dominio;
using PiedraPapelOTijeras.Servicios;

namespace PiedraPapelOTijeras.Tests
{
    public class ServicioJuegoTests
    {
        private readonly ServicioJuego _servicio;

        public ServicioJuegoTests()
        {
            _servicio = new ServicioJuego();
        }

        [Fact]
        public void IniciarNuevaPartida_ConParametrosValidos_DeberiaCrearJuego()
        {
            // Arrange
            string nombre1 = "Pedro";
            string nombre2 = "Maria";
            int puntaje = 5;

            // Act
            var juego = _servicio.IniciarNuevaPartida(nombre1, nombre2, puntaje);

            // Assert
            Assert.NotNull(juego);
            Assert.Equal(nombre1, juego.Jugador1.Nombre);
            Assert.Equal(nombre2, juego.Jugador2.Nombre);
            Assert.Equal(puntaje, juego.PuntajeParaGanar);
        }
    }
}
