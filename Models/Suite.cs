namespace AppHotel.Models;

public class Suite
{
    private string _nome = string.Empty;
    private decimal _valorDiaria;
    private string _descricao = string.Empty;

    public string Nome
    {
        get => _nome;
        set => _nome = string.IsNullOrWhiteSpace(value)
            ? throw new ArgumentException("Informe o nome da acomodação.", nameof(value))
            : value.Trim();
    }

    public decimal ValorDiaria
    {
        get => _valorDiaria;
        set => _valorDiaria = value > 0
            ? value
            : throw new ArgumentOutOfRangeException(nameof(value), "O valor da diária deve ser maior que zero.");
    }

    public string Descricao
    {
        get => _descricao;
        set => _descricao = value?.Trim() ?? string.Empty;
    }

    public override string ToString() => Nome;
}
