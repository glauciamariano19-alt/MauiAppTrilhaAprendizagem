using MauiAppTrilhaAprendizagem.Data;
using MauiAppTrilhaAprendizagem.Models;
using MauiAppTrilhaAprendizagem.Views.Aluno;

namespace MauiAppTrilhaAprendizagem.Views
{
    public partial class AlunoHomePage : ContentPage
    {
        private AlunoModel aluno;
        private Usuario usuarioAtual;
        private string trilhaReforcoPendente = string.Empty;

        public AlunoHomePage(AlunoModel alunoModel)
        {
            InitializeComponent();
            aluno = alunoModel ?? new AlunoModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await VerificarAtividadeReforcoAsync();
        }

        private async Task VerificarAtividadeReforcoAsync()
        {
            try
            {
                int idUsuario = Preferences.Default.Get("IdUsuarioLogado", 0);

                if (idUsuario != 0)
                {
                    var db = new DatabaseContext();
                    var usuarios = await db.GetUsuariosAsync();
                    usuarioAtual = usuarios.FirstOrDefault(u => u.IdUsuario == idUsuario);

                    if (usuarioAtual != null && !string.IsNullOrEmpty(usuarioAtual.AtividadeEspecifica))
                    {
                        trilhaReforcoPendente = usuarioAtual.AtividadeEspecifica;

                        // Ativa o botão em Laranja de destaque
                        ReforcoButton.Text = $"⚠️ Reforço Obrigatório: {trilhaReforcoPendente}";
                        ReforcoButton.BackgroundColor = Color.FromArgb("#D97706");
                        ReforcoButton.TextColor = Colors.White;
                        ReforcoButton.IsEnabled = true;

                        // Bloqueia as outras trilhas
                        MinhasTrilhasButton.IsEnabled = false;
                        MinhasTrilhasButton.Text = "🔒 Minhas Trilhas (Conclua o reforço primeiro)";
                    }
                    else
                    {
                        // Deixa o botão cinza desativado quando não há reforço
                        ReforcoButton.Text = "✅ Nenhuma atividade de reforço pendente";
                        ReforcoButton.BackgroundColor = Colors.LightGray;
                        ReforcoButton.TextColor = Colors.DarkGray;
                        ReforcoButton.IsEnabled = false;

                        // Libera as trilhas normalmente
                        MinhasTrilhasButton.IsEnabled = true;
                        MinhasTrilhasButton.Text = "📚 Minhas Trilhas";
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao verificar reforço: {ex.Message}");
            }
        }

        private async void OnReforcoClicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(trilhaReforcoPendente)) return;

                string trilhaParaAbrir = trilhaReforcoPendente;
                trilhaReforcoPendente = string.Empty;

                if (usuarioAtual != null)
                {
                    var db = new DatabaseContext();
                    usuarioAtual.AtividadeEspecifica = string.Empty;
                    await db.UpdateUsuarioAsync(usuarioAtual);
                }

                // Volta o botão para o estado neutro antes de abrir o jogo
                ReforcoButton.Text = "✅ Nenhuma atividade de reforço pendente";
                ReforcoButton.BackgroundColor = Colors.LightGray;
                ReforcoButton.TextColor = Colors.DarkGray;
                ReforcoButton.IsEnabled = false;

                MinhasTrilhasButton.IsEnabled = true;
                MinhasTrilhasButton.Text = "📚 Minhas Trilhas";

                int idTrilha = 1;
                int estagioInicial = 1;

                if (trilhaParaAbrir == "Trilha das Quantidades")
                {
                    idTrilha = 2;
                    estagioInicial = 2;
                }
                else if (trilhaParaAbrir == "Trilha das Cores")
                {
                    idTrilha = 3;
                }
                else if (trilhaParaAbrir == "Trilha das Formas")
                {
                    idTrilha = 4;
                }

                await Navigation.PushAsync(new TrilhaMatematicaPage(aluno, estagioInicial, idTrilha));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro no Reforço", ex.ToString(), "OK");
            }
        }

        private async void OnTrilhasClicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new TrilhasPage(aluno));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro nas Trilhas", ex.ToString(), "OK");
            }
        }

        private async void OnProgressoClicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new ProgressoPage(aluno));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro no Progresso", ex.ToString(), "OK");
            }
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            Preferences.Default.Remove("IdUsuarioLogado");
            await Navigation.PopToRootAsync();
        }
    }
}
