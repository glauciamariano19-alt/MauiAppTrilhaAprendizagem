using MauiAppTrilhaAprendizagem.Models;

namespace MauiAppTrilhaAprendizagem.Views
{
    public partial class CriarConteudoPage : ContentPage
    {
        public CriarConteudoPage()
        {
            InitializeComponent();
        }

        private async void OnSalvarConteudoClicked(
            object sender,
            EventArgs e)
        {
            if (pickerTrilha.SelectedIndex < 0 ||
                string.IsNullOrWhiteSpace(entryPergunta.Text) ||
                string.IsNullOrWhiteSpace(entryAltA.Text) ||
                string.IsNullOrWhiteSpace(entryAltB.Text) ||
                string.IsNullOrWhiteSpace(entryAltC.Text) ||
                pickerCorreta.SelectedIndex < 0)
            {
                await DisplayAlert("Atenção", "Preencha todos os campos.","OK");

                return;
            }

            await DisplayAlert("Sucesso", $"Novo conteúdo criado para a {pickerTrilha.SelectedItem}","OK");

            await Navigation.PopAsync();
        }
    }
}