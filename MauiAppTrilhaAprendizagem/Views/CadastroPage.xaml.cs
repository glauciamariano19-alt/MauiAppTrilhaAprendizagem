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
            var usuario = new Usuario
            {
                Nome = NomeEntry.Text,
                Email = EmailEntry.Text,
                Senha = SenhaEntry.Text,
                Perfil = PerfilPicker.SelectedItem?.ToString()
            };

            // Salva no banco
            await _db.AddUsuarioAsync(usuario);

            // Se for aluno, cria o AlunoModel com idade digitada
            if (usuario.Perfil == "Aluno")
            {
                int idade = 0;
                int.TryParse(IdadeEntry.Text, out idade);

                var aluno = new AlunoModel
                {
                    Nome = usuario.Nome,
                    Idade = idade,
                    PontuacaoTotal = 0
                };

                // Navega para a Home do Aluno
                await Navigation.PushAsync(new AlunoHomePage(aluno));
            }
            else
            {
                await DisplayAlert("Sucesso", $"Usuário {usuario.Perfil} cadastrado com sucesso!", "OK");
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
