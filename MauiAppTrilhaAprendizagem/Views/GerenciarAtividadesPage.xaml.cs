using MauiAppTrilhaAprendizagem.Data;
using MauiAppTrilhaAprendizagem.Models;

namespace MauiAppTrilhaAprendizagem.Views;

public partial class GerenciarAtividadesPage : ContentPage
{
    private List<Usuario> alunos;

    private Dictionary<string, List<string>> exerciciosPorTrilha =
        new()
        {
            {
                "Trilha dos Números",
                new List<string>
                {
                    "Quantas bolas tem na imagem?",
                    "Quantos carrinhos tem na imagem?",
                    "Quantas bonecas tem na imagem?"
                }
            },

            {
                "Trilha das Quantidades",
                new List<string>
                {
                    "Quantos balões faltam?",
                    "Quantas flores faltam?"
                }
            },

            {
                "Trilha das Cores",
                new List<string>
                {
                    "Qual é a cor do sol?",
                    "Qual é a cor do tomate?",
                    "Qual é a cor da borboleta?"
                }
            },

            {
                "Trilha das Formas",
                new List<string>
                {
                    "Qual é a forma da pizza?",
                    "Qual é a forma da bola?",
                    "Qual é a forma da televisão?"
                }
            }
        };

    public GerenciarAtividadesPage()
    {
        InitializeComponent();

        CarregarDados();
    }

    private async void CarregarDados()
    {
        var db = new DatabaseContext();

        alunos = await db.GetUsuariosAsync();

        alunos = alunos
            .Where(x => x.Perfil == "Aluno")
            .ToList();

        AlunoPicker.ItemsSource = alunos.Select(a => a.Nome).ToList();

        TrilhaPicker.ItemsSource = exerciciosPorTrilha.Keys.ToList();
    }

    private async void OnEnviarAtividadeClicked(object sender, EventArgs e)
    {
        if (AlunoPicker.SelectedIndex < 0 || TrilhaPicker.SelectedIndex < 0)
        {
            await DisplayAlert("Atenção", "Selecione o aluno e a trilha.", "OK");
            return;
        }

        // Pega o aluno selecionado na lista
        var aluno = alunos[AlunoPicker.SelectedIndex];

        // Grava o nome da trilha de reforço no cadastro dele
        aluno.AtividadeEspecifica = TrilhaPicker.SelectedItem.ToString();

        // Salva a alteração no SQLite
        var db = new DatabaseContext();
        await db.UpdateUsuarioAsync(aluno);

        await DisplayAlert("Sucesso!", $"Atividade de reforço ({aluno.AtividadeEspecifica}) enviada para {aluno.Nome}.", "OK");

        // Volta para a tela anterior
        await Navigation.PopAsync();
    }
}
