using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AvaliacaoTrends.Models
{
    [Serializable]
    public class Perguntas
    {
        public decimal per_id_pergunta { get; set; }

        public decimal per_id_questionario { get; set; }
        public string per_ds_pergunta { get; set; }
        public char per_tp_pergunta { get; set; }
        public char per_ch_resposta_obrigatoria { get; set; }
        public int per_nu_ordem { get; set; }

        public Perguntas(decimal idPergunta,decimal idQuestionario,string dsPergunta,char tipoPergunta, char RespostaObrigatoria,int NuOrdem) 
        {
            this.per_id_pergunta = idPergunta;
            this.per_id_questionario = idQuestionario;
            this.per_ds_pergunta = dsPergunta;
            this.per_tp_pergunta = tipoPergunta;
            this.per_ch_resposta_obrigatoria = RespostaObrigatoria;
            this.per_nu_ordem = NuOrdem;
        }
        Perguntas() { }
    }
}