# App Hotel

Aplicativo de hospedagem desenvolvido em C# e .NET MAUI.

## Funcionalidades

- Seleção da quantidade de adultos e crianças.
- Escolha de acomodação e consulta do valor da diária.
- Seleção de check-in e check-out com validação do período.
- Cálculo automático da quantidade de diárias e do valor total.
- Registro de reservas durante a execução do aplicativo.
- Exibição das reservas registradas na sessão atual.
- Tela de informações do projeto.

## Organização do projeto

- `Models/Suite.cs`: classe que representa as acomodações.
- `Models/Reserva.cs`: classe que representa uma reserva e valida seus dados.
- `ViewModels/ReservaViewModel.cs`: propriedades da tela, atualização do resumo e lista de reservas.
- `Views/MainPage.xaml`: interface de reservas.
- `Views/SobrePage.xaml`: informações do aplicativo.

## Conceitos utilizados

O projeto utiliza classes, objetos, propriedades com `get` e `set`, encapsulamento, validação de dados, `List<Reserva>`, `ObservableCollection<Reserva>` e vinculação de dados.

## Execução no Windows

1. Instale o SDK .NET 8 e a carga de trabalho .NET MAUI.
2. Abra `AppHotel.csproj` no Visual Studio ou a pasta do projeto no ambiente de desenvolvimento.
3. Restaure os pacotes NuGet.
4. Compile e execute o projeto para Windows.

Os valores das acomodações são demonstrativos. As reservas são mantidas em memória e não são gravadas em banco de dados ou arquivo permanente.
