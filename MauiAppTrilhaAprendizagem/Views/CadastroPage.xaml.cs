using MauiAppTrilhaAprendizagem.Data;
using MauiAppTrilhaAprendizagem.Models;

namespace MauiAppTrilhaAprendizagem.Views
{
    public partial class CadastroPage : ContentPage
    {
        private readonly DatabaseContext _db;

        public CadastroPage()
        {
            InitializeComponent();
            _db = new DatabaseContext();
            _db.InitializeAsync();
        }

        private async void OnCadastrarClicked(object sender, EventArgs e)
        {
            int idade = 0;
            int.TryParse(IdadeEntry.Text, out idade);

            var usuario = new Usuario
            {
                Nome = NomeEntry.Text?.Trim(),
                Email = EmailEntry.Text?.Trim(),
                Senha = SenhaEntry.Text?.Trim(),
                Perfil = PerfilPicker.SelectedItem?.ToString(),
                Idade = idade
            };

            if (string.IsNullOrEmpty(usuario.Nome) || string.IsNullOrEmpty(usuario.Email) ||
                string.IsNullOrEmpty(usuario.Senha) || string.IsNullOrEmpty(usuario.Perfil))
            {
                await DisplayAlert("Atenção", "Preencha todos os campos e selecione o perfil.", "OK");
                return;
            }

            // Salva no banco (o SQLite preenche automaticamente o usuario.IdUsuario logo após essa linha)
            await _db.AddUsuarioAsync(usuario);

            // 🌟 CORREÇÃO: Atualiza a memória com o ID do novo usuário cadastrado
            Preferences.Default.Set("IdUsuarioLogado", usuario.IdUsuario);

            if (usuario.Perfil == "Aluno")
            {
                var aluno = new AlunoModel
                {
                    Nome = usuario.Nome,
                    Idade = idade,
                    PontuacaoTotal = 0
                };

                await Navigation.PushAsync(new AlunoHomePage(aluno));
            }
            else
            {
                await DisplayAlert("Sucesso", $"Usuário {usuario.Perfil} cadastrado com sucesso!", "OK");
                await Navigation.PopAsync();
            }

            // Limpa os campos após o cadastro
            NomeEntry.Text = string.Empty;
            IdadeEntry.Text = string.Empty;
            EmailEntry.Text = string.Empty;
            SenhaEntry.Text = string.Empty;
            PerfilPicker.SelectedItem = null;
        }
    }
}
