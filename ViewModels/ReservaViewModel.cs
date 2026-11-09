using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using AppHotel.Models;

namespace AppHotel.ViewModels;

public class ReservaViewModel : INotifyPropertyChanged
{
    private int _adultos = 1;
    private int _criancas;
    private Suite? _suiteSelecionada;
    private DateTime _checkIn = DateTime.Today;
    private DateTime _checkOut = DateTime.Today.AddDays(1);
    private string _mensagemOperacao = "Preencha os dados e registre sua reserva.";

    public event PropertyChangedEventHandler? PropertyChanged;

    public List<Suite> Suites { get; } = new()
    {
        new Suite { Nome = "Suíte Econômica", ValorDiaria = 180m, Descricao = "Conforto essencial para sua viagem." },
        new Suite { Nome = "Suíte Luxo", ValorDiaria = 290m, Descricao = "Mais espaço e comodidade." },
        new Suite { Nome = "Suíte Família", ValorDiaria = 390m, Descricao = "Ideal para viajar com a família." },
        new Suite { Nome = "Suíte Premium", ValorDiaria = 520m, Descricao = "Uma experiência especial de hospedagem." }
    };

    public List<Reserva> Reservas { get; } = new();
    public ObservableCollection<Reserva> ReservasExibidas { get; } = new();

    public ReservaViewModel()
    {
        SuiteSelecionada = Suites.FirstOrDefault();
    }

    public int Adultos
    {
        get => _adultos;
        set
        {
            int quantidade = Math.Clamp(value, 1, 8);
            if (Set(ref _adultos, quantidade))
                AtualizarResumo();
        }
    }

    public int Criancas
    {
        get => _criancas;
        set
        {
            int quantidade = Math.Clamp(value, 0, 8);
            if (Set(ref _criancas, quantidade))
                AtualizarResumo();
        }
    }

    public Suite? SuiteSelecionada
    {
        get => _suiteSelecionada;
        set
        {
            if (Set(ref _suiteSelecionada, value))
            {
                OnPropertyChanged(nameof(DescricaoSuite));
                AtualizarResumo();
            }
        }
    }

    public DateTime CheckIn
    {
        get => _checkIn;
        set
        {
            DateTime novaData = value.Date < DateTime.Today ? DateTime.Today : value.Date;
            if (Set(ref _checkIn, novaData))
            {
                if (_checkOut <= _checkIn)
                    CheckOut = _checkIn.AddDays(1);

                AtualizarResumo();
            }
        }
    }

    public DateTime CheckOut
    {
        get => _checkOut;
        set
        {
            DateTime novaData = value.Date <= _checkIn ? _checkIn.AddDays(1) : value.Date;
            if (Set(ref _checkOut, novaData))
                AtualizarResumo();
        }
    }

    public DateTime DataMinimaCheckIn => DateTime.Today;
    public DateTime DataMinimaSaida => CheckIn.AddDays(1);
    public int QuantidadeDiarias => Math.Max(1, (CheckOut.Date - CheckIn.Date).Days);
    public decimal Total => (SuiteSelecionada?.ValorDiaria ?? 0m) * QuantidadeDiarias;
    public string ValorDiariaFormatado => (SuiteSelecionada?.ValorDiaria ?? 0m).ToString("C", CultureInfo.GetCultureInfo("pt-BR"));
    public string TotalFormatado => Total.ToString("C", CultureInfo.GetCultureInfo("pt-BR"));
    public string DescricaoSuite => SuiteSelecionada?.Descricao ?? "Selecione uma acomodação.";
    public string ResumoHospedes => $"{Adultos} adulto(s) • {Criancas} criança(s)";
    public string ResumoPeriodo => $"{CheckIn:dd/MM/yyyy} a {CheckOut:dd/MM/yyyy} • {QuantidadeDiarias} diária(s)";
    public string MensagemOperacao
    {
        get => _mensagemOperacao;
        private set => Set(ref _mensagemOperacao, value);
    }

    public bool AdicionarReserva()
    {
        if (SuiteSelecionada is null)
        {
            MensagemOperacao = "Selecione uma acomodação antes de continuar.";
            return false;
        }

        try
        {
            var reserva = new Reserva(Adultos, Criancas, SuiteSelecionada, CheckIn, CheckOut);
            Reservas.Add(reserva);
            ReservasExibidas.Add(reserva);
            OnPropertyChanged(nameof(Reservas));
            MensagemOperacao = $"Reserva registrada. Total: {reserva.ValorTotalFormatado}.";
            return true;
        }
        catch (ArgumentException erro)
        {
            MensagemOperacao = erro.Message;
            return false;
        }
    }

    private void AtualizarResumo()
    {
        OnPropertyChanged(nameof(QuantidadeDiarias));
        OnPropertyChanged(nameof(Total));
        OnPropertyChanged(nameof(ValorDiariaFormatado));
        OnPropertyChanged(nameof(TotalFormatado));
        OnPropertyChanged(nameof(ResumoHospedes));
        OnPropertyChanged(nameof(ResumoPeriodo));
        OnPropertyChanged(nameof(DataMinimaCheckIn));
        OnPropertyChanged(nameof(DataMinimaSaida));
    }

    private bool Set<T>(ref T campo, T valor, [CallerMemberName] string? nome = null)
    {
        if (EqualityComparer<T>.Default.Equals(campo, valor))
            return false;

        campo = valor;
        OnPropertyChanged(nome);
        return true;
    }

    private void OnPropertyChanged([CallerMemberName] string? nome = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));
    }
}
