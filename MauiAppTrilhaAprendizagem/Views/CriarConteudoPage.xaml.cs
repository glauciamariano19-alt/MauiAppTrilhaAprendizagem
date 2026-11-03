using MauiAppTrilhaAprendizagem.Models;

namespace MauiAppTrilhaAprendizagem.Views
{
    public partial class CriarConteudoPage : ContentPage
    {
        public CriarConteudoPage()
        {
            InitializeComponent();
        }

        private async void OnSalvarConteudoClicked(object sender, EventArgs e)
        {
            if (pickerTrilha.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(entryPergunta.Text) ||
                string.IsNullOrWhiteSpace(entryAltA.Text) ||
                string.IsNullOrWhiteSpace(entryAltB.Text) ||
                string.IsNullOrWhiteSpace(entryAltC.Text) ||
                pickerCorreta.SelectedIndex == -1)
            {
                await DisplayAlert(
                    "Atenção",
                    "Preencha todos os campos.",
                    "OK");

                return;
            }

            var conteudo = new ConteudoTrilha
            {
                Trilha = pickerTrilha.SelectedItem.ToString(),

                Pergunta = entryPergunta.Text,

                AlternativaA = entryAltA.Text,
                AlternativaB = entryAltB.Text,
                AlternativaC = entryAltC.Text,

                RespostaCorreta =
                    pickerCorreta.SelectedItem.ToString()
            };

            await App.Database.AddConteudoAsync(conteudo);

            await DisplayAlert(
                "Sucesso!",
                "Conteúdo publicado com sucesso.",
                "OK");

            await Navigation.PopAsync();
        }
    }
}