using MauiAppTrilhaAprendizagem.Data;
using MauiAppTrilhaAprendizagem.Models;

namespace MauiAppTrilhaAprendizagem.Views
{
    public partial class GerenciarAlunosPage : ContentPage
    {
        private readonly DatabaseContext _db;

        public GerenciarAlunosPage()
        {
            InitializeComponent();
            _db = new DatabaseContext();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await CarregarAlunosAsync();
        }

        private async Task CarregarAlunosAsync()
        {
            try
            {
                var todosUsuarios = await _db.GetUsuariosAsync();
                var apenasAlunos = todosUsuarios
                    .Where(u => u.Perfil != null && u.Perfil.Equals("Aluno", StringComparison.OrdinalIgnoreCase))
                    .ToList();

                AlunosCollectionView.ItemsSource = apenasAlunos;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", "Falha ao carregar alunos: " + ex.Message, "OK");
            }
        }

        private async void OnExcluirAlunoClicked(object sender, EventArgs e)
        {
            var botao = sender as Button;
            var alunoSelecionado = botao?.CommandParameter as Usuario;

            if (alunoSelecionado == null) return;

            bool confirmar = await DisplayAlert(
                "Confirmar Exclusão",
                $"Tem certeza que deseja excluir permanentemente o cadastro de {alunoSelecionado.Nome}?",
                "Sim, excluir",
                "Cancelar");

            if (confirmar)
            {
                // Usa o método DELETE que já está pronto no seu DatabaseContext
                await _db.DeleteUsuarioAsync(alunoSelecionado);
                await DisplayAlert("Sucesso", $"Cadastro de {alunoSelecionado.Nome} removido.", "OK");

                // Atualiza a lista na tela na hora
                await CarregarAlunosAsync();
            }
        }
    }
}
