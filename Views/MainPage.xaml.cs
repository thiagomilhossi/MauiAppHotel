using AppHotel.ViewModels;

namespace AppHotel.Views;

public partial class MainPage : ContentPage
{
    private ReservaViewModel ViewModel => (ReservaViewModel)BindingContext;

    public MainPage()
    {
        InitializeComponent();
        BindingContext = new ReservaViewModel();
    }

    private void OnAdultosMenosClicked(object sender, EventArgs e)
    {
        if (ViewModel.Adultos > 1)
            ViewModel.Adultos--;
    }

    private void OnAdultosMaisClicked(object sender, EventArgs e)
    {
        if (ViewModel.Adultos < 8)
            ViewModel.Adultos++;
    }

    private void OnCriancasMenosClicked(object sender, EventArgs e)
    {
        if (ViewModel.Criancas > 0)
            ViewModel.Criancas--;
    }

    private void OnCriancasMaisClicked(object sender, EventArgs e)
    {
        if (ViewModel.Criancas < 8)
            ViewModel.Criancas++;
    }

    private async void OnSalvarReservaClicked(object sender, EventArgs e)
    {
        if (ViewModel.AdicionarReserva())
            await DisplayAlert("Reserva registrada", "A reserva foi adicionada à lista desta sessão.", "OK");
        else
            await DisplayAlert("Não foi possível registrar", ViewModel.MensagemOperacao, "Entendi");
    }
}
