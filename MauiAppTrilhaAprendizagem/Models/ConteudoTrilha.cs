using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MauiAppTrilhaAprendizagem.Models
{
    public class ConteudoTrilha
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Trilha { get; set; } = string.Empty;

        public string Pergunta { get; set; } = string.Empty;

        public string Imagem { get; set; } = string.Empty;

        public string AlternativaA { get; set; } = string.Empty;

        public string AlternativaB { get; set; } = string.Empty;

        public string AlternativaC { get; set; } = string.Empty;

        public string RespostaCorreta { get; set; } = string.Empty;
    }
}