namespace PiedraPapelOTijeras.Dominio
{
    public class Jugador
    {
        public string Nombre { get; }
        public int Puntaje { get; private set; }

        public Jugador(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del jugador no puede estar vacío", nameof(nombre));
            
            Nombre = nombre;
            Puntaje = 0;
        }

        public void IncrementarPuntaje()
        {
            Puntaje++;
        }

        public void ReiniciarPuntaje()
        {
            Puntaje = 0;
        }

        public override string ToString()
        {
            return $"{Nombre} (Puntaje: {Puntaje})";
        }
    }
}
