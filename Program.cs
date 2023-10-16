using System;
using System.Collections.Generic;
using System.Linq;

class Carta
{
    public string Nombre { get; set; }
    public string Pinta { get; set; }
    public int Valor { get; set; }

    public void Print()
    {
        Console.WriteLine($"Nombre: {Nombre}, Valor: {Valor}, Pinta: {Pinta}");
    }
}

class Mazo
{
    public List<Carta> Cartas { get; set; }

    public Mazo()
    {
        InicializarMazo();
    }

    private void InicializarMazo()
    {
        Cartas = new List<Carta>();

        string[] pintas = { "Tréboles", "Picas", "Corazones", "Diamantes" };

        foreach (var pinta in pintas)
        {
            for (int valor = 1; valor <= 13; valor++)
            {
                Cartas.Add(new Carta { Nombre = ObtenerNombreCarta(valor), Valor = valor, Pinta = pinta });
            }
        }
    }

    private string ObtenerNombreCarta(int valor)
    {
        switch (valor)
        {
            case 1:
                return "As";
            case 11:
                return "J";
            case 12:
                return "Reina";
            case 13:
                return "Rey";
            default:
                return valor.ToString();
        }
    }

    public void Barajar()
    {
        Random random = new Random();
        Cartas = Cartas.OrderBy(carta => random.Next()).ToList();
    }

    public Carta Repartir()
    {
        if (Cartas.Count > 0)
        {
            Carta cartaRepartida = Cartas[Cartas.Count - 1];
            Cartas.RemoveAt(Cartas.Count - 1);
            return cartaRepartida;
        }
        else
        {
            Console.WriteLine("El mazo está vacío. Reinicia el mazo.");
            return null;
        }
    }

    public void Reiniciar()
    {
        InicializarMazo();
    }
}

class Jugador
{
    public string Nombre { get; set; }
    public List<Carta> Mano { get; set; }

    public Jugador(string nombre)
    {
        Nombre = nombre;
        Mano = new List<Carta>();
    }

    public Carta Robar(Mazo mazo)
    {
        Carta cartaRobada = mazo.Repartir();
        if (cartaRobada != null)
        {
            Mano.Add(cartaRobada);
        }
        return cartaRobada;
    }

    public Carta Descartar(int indice)
    {
        if (indice >= 0 && indice < Mano.Count)
        {
            Carta cartaDescartada = Mano[indice];
            Mano.RemoveAt(indice);
            return cartaDescartada;
        }
        else
        {
            Console.WriteLine("Índice de descarte no válido.");
            return null;
        }
    }

    public void ImprimirMano()
    {
        Console.WriteLine($"{Nombre}'s Mano:");
        foreach (var carta in Mano)
        {
            carta.Print();
        }
    }
}

class Program
{
    static void Main()
    {
        Carta cartaEjemplo = new Carta { Nombre = "As", Valor = 1, Pinta = "Corazones" };
        cartaEjemplo.Print();

        Mazo mazo = new Mazo();
        mazo.Barajar();
        mazo.Reiniciar();
        mazo.Barajar();

        Jugador jugador1 = new Jugador("Jugador1");
        Jugador jugador2 = new Jugador("Jugador2");

        jugador1.Robar(mazo);
        jugador1.Robar(mazo);
        jugador1.Robar(mazo);

        jugador1.ImprimirMano();

        jugador1.Descartar(1);

        jugador1.ImprimirMano();
    }
}
