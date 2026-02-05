using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Configuration;
using AvaliacaoTrends.Models;

namespace AvaliacaoTrends.DAO
{
    public class PerguntasDAO
    {
        SqlCommand ioQuery;
        SqlConnection ioConexao;

        public BindingList<Perguntas> BuscaPerguntas(decimal? idPergunta = null)
        {
            BindingList<Perguntas> loPerguntasLista = new BindingList<Perguntas>();
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    if (idPergunta != null)
                    {
                        ioQuery = new SqlCommand("Select * from PER_PERGUNTA_Acarvalho WHERE per_id_pergunta = @idPer", ioConexao);
                        ioQuery.Parameters.Add(new SqlParameter("@idPer", idPergunta));
                    }
                    else
                    {
                        ioQuery = new SqlCommand("Select * from PER_PERGUNTA_Acarvalho", ioConexao);
                    }

                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            decimal id = loReader.GetDecimal(0);
                            decimal idQuestionario = loReader.GetDecimal(1);
                            string dsPergunta = loReader.GetString(2);
                            char tpPergunta = Convert.ToChar(loReader.GetString(3));
                            char respostaObrigatoria = Convert.ToChar(loReader.GetString(4));
                            int nuOrdem = loReader.GetInt32(5);

                            Perguntas loNovaPergunta = new Perguntas(id, idQuestionario, dsPergunta, tpPergunta, respostaObrigatoria, nuOrdem);
                            loPerguntasLista.Add(loNovaPergunta);
                        }
                        loReader.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return loPerguntasLista;
        }

        public int InserePergunta(Perguntas aoNovaPergunta)
        {
            if (aoNovaPergunta == null) throw new NullReferenceException();
            int liQtdRegistrosInseridos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("INSERT INTO PER_PERGUNTA_Acarvalho(per_id_pergunta, per_id_questionario, per_ds_pergunta, per_tp_pergunta, per_ch_resposta_obrigatoria, per_nu_ordem) VALUES (@idPer, @idQuest, @dsPer, @tpPer, @respObri, @nuOrdem)", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idPer", aoNovaPergunta.per_id_pergunta));
                    ioQuery.Parameters.Add(new SqlParameter("@idQuest", aoNovaPergunta.per_id_questionario));
                    ioQuery.Parameters.Add(new SqlParameter("@dsPer", aoNovaPergunta.per_ds_pergunta));
                    ioQuery.Parameters.Add(new SqlParameter("@tpPer", aoNovaPergunta.per_tp_pergunta));
                    ioQuery.Parameters.Add(new SqlParameter("@respObri", aoNovaPergunta.per_ch_resposta_obrigatoria));
                    ioQuery.Parameters.Add(new SqlParameter("@nuOrdem", aoNovaPergunta.per_nu_ordem));
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return liQtdRegistrosInseridos;
        }

        public int RemovePergunta(Perguntas aoPergunta)
        {
            if (aoPergunta == null) throw new NullReferenceException();
            int liQtdRegistrosExcluidos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("DELETE FROM PER_PERGUNTA_Acarvalho WHERE per_id_pergunta = @idPer", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idPer", aoPergunta.per_id_pergunta));
                    liQtdRegistrosExcluidos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return liQtdRegistrosExcluidos;
        }

        public int AtualizarPergunta(Perguntas aoPergunta)
        {
            if (aoPergunta == null) throw new ArgumentNullException();
            int liQtdRegistrosAtualizados = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("UPDATE PER_PERGUNTA_Acarvalho SET per_id_questionario = @idQuest, per_ds_pergunta = @dsPer, per_tp_pergunta = @tpPer, per_ch_resposta_obrigatoria = @respObri, per_nu_ordem = @nuOrdem WHERE per_id_pergunta = @idPer", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idPer", aoPergunta.per_id_pergunta));
                    ioQuery.Parameters.Add(new SqlParameter("@idQuest", aoPergunta.per_id_questionario));
                    ioQuery.Parameters.Add(new SqlParameter("@dsPer", aoPergunta.per_ds_pergunta));
                    ioQuery.Parameters.Add(new SqlParameter("@tpPer", aoPergunta.per_tp_pergunta));
                    ioQuery.Parameters.Add(new SqlParameter("@respObri", aoPergunta.per_ch_resposta_obrigatoria));
                    ioQuery.Parameters.Add(new SqlParameter("@nuOrdem", aoPergunta.per_nu_ordem));
                    liQtdRegistrosAtualizados = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return liQtdRegistrosAtualizados;
        }
    }
}