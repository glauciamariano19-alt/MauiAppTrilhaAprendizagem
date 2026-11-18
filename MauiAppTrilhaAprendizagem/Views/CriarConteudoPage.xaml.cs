using System;
using MauiAppTrilhaAprendizagem.Models;
using Microsoft.Maui.Controls;

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
            // Validação atualizada: agora verifica se o campo da imagem também foi preenchido
            if (pickerTrilha.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(entryPergunta.Text) ||
                string.IsNullOrWhiteSpace(entryImagem.Text) || // Linha adicionada para a imagem
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

            // Criando o objeto com todas as propriedades do seu modelo atualizado
            var conteudo = new ConteudoTrilha
            {
                Trilha = pickerTrilha.SelectedItem.ToString(),
                Pergunta = entryPergunta.Text,

                // Linhas adicionadas para guardar o nome do arquivo que o Designer digitou
                Imagem = entryImagem.Text,
                NomeImagem = entryImagem.Text,

                AlternativaA = entryAltA.Text,
                AlternativaB = entryAltB.Text,
                AlternativaC = entryAltC.Text,
                RespostaCorreta = pickerCorreta.SelectedItem.ToString()
            };

            // Salva no banco SQLite que você configurou no projeto
            await App.Database.AddConteudoAsync(conteudo);

            await DisplayAlert(
                "Sucesso!",
                "Conteúdo publicado com sucesso.",
                "OK");

            // Fecha a página e retorna para o menu inicial do Designer
            await Navigation.PopAsync();
        }
    }
}
