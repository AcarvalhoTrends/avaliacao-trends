using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AvaliacaoTrends.Models
{
    [Serializable]
    public class OpcaoResposta
    {
       public decimal opr_id_opcao_resposta { get; set; }
       public decimal opr_id_pergunta { get; set; }
       public string opr_ds_opcao_resposta { get; set; }
       public char opr_ch_resposta_correta { get; set; }
       public int opr_nu_ordem { get; set; }

        public OpcaoResposta (decimal idRespostaOpc,decimal idPerguntaOpr,string opcaoRespota,char respostaCorreta, int ordemNu)
        {
            this.opr_id_opcao_resposta = idRespostaOpc;
            this.opr_id_pergunta = idPerguntaOpr;
            this.opr_ds_opcao_resposta = opcaoRespota;
            this.opr_ch_resposta_correta = respostaCorreta;
            this.opr_nu_ordem = ordemNu;
        }
        OpcaoResposta() { }
    }
}