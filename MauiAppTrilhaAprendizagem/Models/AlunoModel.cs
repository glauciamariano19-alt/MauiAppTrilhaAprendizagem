namespace MauiAppTrilhaAprendizagem.Models
{
    public class AlunoModel
    {
        public string Nome { get; set; }
        public int Idade { get; set; }
        public int PontuacaoTotal { get; set; }
        public List<ExercicioModel> Exercicios { get; set; } = new List<ExercicioModel>();
    }
}
