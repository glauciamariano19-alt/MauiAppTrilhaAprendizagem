using MauiAppTrilhaAprendizagem.Models;

namespace MauiAppTrilhaAprendizagem.Views.Aluno
{
    public partial class ProgressoPage : ContentPage
    {
        public ProgressoPage(AlunoModel aluno)
        {
            InitializeComponent();
            BindingContext = aluno; 
        }
    }
}
