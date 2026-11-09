namespace AppHotel.Models;

public class Reserva
{
    private int _adultos;
    private int _criancas;
    private DateTime _checkIn;
    private DateTime _checkOut;
    private Suite _suite;

    public int Adultos
    {
        get => _adultos;
        set
        {
            if (value < 1 || value > 8)
                throw new ArgumentOutOfRangeException(nameof(value), "A reserva deve ter entre 1 e 8 adultos.");

            _adultos = value;
        }
    }

    public int Criancas
    {
        get => _criancas;
        set
        {
            if (value < 0 || value > 8)
                throw new ArgumentOutOfRangeException(nameof(value), "A quantidade de crianças deve estar entre 0 e 8.");

            _criancas = value;
        }
    }

    public Suite Suite
    {
        get => _suite;
        set => _suite = value ?? throw new ArgumentNullException(nameof(value), "Selecione uma acomodação.");
    }

    public DateTime CheckIn
    {
        get => _checkIn;
        set
        {
            if (value.Date < DateTime.Today)
                throw new ArgumentException("O check-in não pode ser anterior à data atual.", nameof(value));

            _checkIn = value.Date;

            if (_checkOut <= _checkIn)
                _checkOut = _checkIn.AddDays(1);
        }
    }

    public DateTime CheckOut
    {
        get => _checkOut;
        set
        {
            if (value.Date <= _checkIn)
                throw new ArgumentException("O check-out deve ser posterior ao check-in.", nameof(value));

            _checkOut = value.Date;
        }
    }

    public DateTime DataCriacao { get; }
    public int QuantidadeDiarias => (CheckOut - CheckIn).Days;
    public decimal ValorTotal => Suite.ValorDiaria * QuantidadeDiarias;
    public string Periodo => $"{CheckIn:dd/MM/yyyy} a {CheckOut:dd/MM/yyyy}";
    public string ValorTotalFormatado => ValorTotal.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("pt-BR"));
    public string Identificacao => $"{Suite.Nome} • {Adultos} adulto(s) e {Criancas} criança(s)";

    public Reserva(int adultos, int criancas, Suite suite, DateTime checkIn, DateTime checkOut)
    {
        _checkIn = DateTime.Today;
        _checkOut = DateTime.Today.AddDays(1);
        _suite = suite ?? throw new ArgumentNullException(nameof(suite));

        Adultos = adultos;
        Criancas = criancas;
        CheckIn = checkIn;
        CheckOut = checkOut;
        DataCriacao = DateTime.Now;
    }
}
