using System;
using Microsoft.Maui.Controls;

namespace MauiAppTrilhaAprendizagem.Views
{
    public partial class DesignerHomePage : ContentPage
    {
        public DesignerHomePage()
        {
            InitializeComponent();
        }

        private async void OnCriarConteudosClicked(object sender, EventArgs e)
        {
            // Navega para a página correta de criar conteúdo
            await Navigation.PushAsync(new CriarConteudoPage());
        }

        private async void OnGerenciarAlunosClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GerenciarAlunosPage());
        }
        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            // Volta para a tela anterior (Ex: Login)
            await Navigation.PopAsync();
        }
    }
}
