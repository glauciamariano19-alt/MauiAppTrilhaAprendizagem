using MauiAppTrilhaAprendizagem.Models;

namespace MauiAppTrilhaAprendizagem.Views.Aluno
{
    public partial class TrilhasPage : ContentPage
    {
        private AlunoModel aluno;

        public TrilhasPage(AlunoModel alunoModel) 
        {
            InitializeComponent();
            aluno = alunoModel; 
        }

        private async void OnTrilhaNumerosClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ExerciciosPage(aluno));
        }

        private async void OnTrilhaQuantidadesClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ExerciciosPage(aluno));
        }

        private async void OnTrilhaCoresFormasClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ExerciciosPage(aluno));
        }

        private async void OnTrilhaDesafiosClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ExerciciosPage(aluno));
        }
    }
}
