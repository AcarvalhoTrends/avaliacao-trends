using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Configuration;
using AvaliacaoTrends.Models;

namespace AvaliacaoTrends.DAO
{
    public class QuestionarioDAO
    {
        SqlCommand ioQuery;
        SqlConnection ioConexao;

        public BindingList<Questionario> BuscaQuestinario(decimal? idQuestionario = null)
        {
            BindingList<Questionario> loQuestionarioLista = new BindingList<Questionario>();

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();

                    
                    if (idQuestionario != null)
                    {
                        ioQuery = new SqlCommand("SELECT qst_id_questionario, qst_nm_questionario, qst_tp_questionario, qst_ds_link_instrucoes FROM QST_QUESTIONARIO_Acarvalho WHERE qst_id_questionario = @idQuest", ioConexao);
                        ioQuery.Parameters.Add(new SqlParameter("@idQuest", idQuestionario));
                    }
                    else
                    {
                        ioQuery = new SqlCommand("SELECT qst_id_questionario, qst_nm_questionario, qst_tp_questionario, qst_ds_link_instrucoes FROM QST_QUESTIONARIO_Acarvalho", ioConexao);
                    }

                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                       
                            decimal id = loReader.GetDecimal(0);

                         
                            string nomeQuestionario = loReader.GetString(1);

                            
                            char tipoQuestionario;

                            
                            if (loReader.IsDBNull(2))
                            {
                                tipoQuestionario = ' '; 
                            }
                            else
                            {
                                
                                string valorTemp = loReader.GetString(2);

                                
                                if (!string.IsNullOrEmpty(valorTemp))
                                {
                                    tipoQuestionario = valorTemp[0];
                                }
                                else
                                {
                                    tipoQuestionario = ' '; 
                                }
                            }

                            
                            string linkInstrucao = loReader.IsDBNull(3) ? "" : loReader.GetString(3);

                          
                            Questionario loNovoQuest = new Questionario(id, nomeQuestionario, tipoQuestionario, linkInstrucao);
                            loQuestionarioLista.Add(loNovoQuest);
                        }
                        loReader.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao buscar questionários: " + ex.Message);
                }
            }
            return loQuestionarioLista;
        }

        public int InsereQuest(Questionario aoNovoQuest)
        {
            if (aoNovoQuest == null) throw new NullReferenceException();

            int liQtdRegistrosInseridos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("INSERT INTO QST_QUESTIONARIO_Acarvalho(qst_id_questionario, qst_nm_questionario, qst_tp_questionario, qst_ds_link_instrucoes) VALUES (@idQuest, @nomeQuestionario, @tpQuest, @linkInstr)", ioConexao);

                    ioQuery.Parameters.Add(new SqlParameter("@idQuest", aoNovoQuest.qst_id_questionario));
                    ioQuery.Parameters.Add(new SqlParameter("@nomeQuestionario", aoNovoQuest.qst_nm_questionario));
                    ioQuery.Parameters.Add(new SqlParameter("@tpQuest", aoNovoQuest.qst_tp_questionario));
                    ioQuery.Parameters.Add(new SqlParameter("@linkInstr", aoNovoQuest.qst_ds_link_instrucoes));

                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return liQtdRegistrosInseridos;
        }

        public int RemoveQuest(Questionario aoQuest)
        {
            if (aoQuest == null) throw new NullReferenceException();

            int liQtdRegistrosExcluidos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("DELETE FROM QST_QUESTIONARIO_Acarvalho WHERE qst_id_questionario = @idQuest", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idQuest", aoQuest.qst_id_questionario));
                    liQtdRegistrosExcluidos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return liQtdRegistrosExcluidos;
        }

         internal void RemoveQuest(decimal idQuestionario)
        {
            Questionario objTemp = new Questionario();
            objTemp.qst_id_questionario = idQuestionario;
            RemoveQuest(objTemp);
        }

        public int atualizarQuestionario(Questionario aoQuest)
        {
            if (aoQuest == null)
                throw new ArgumentNullException();
            int liQtdRegistrosAtulizados = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("UPDATE QST_QUESTIONARIO_Acarvalho SET  qst_nm_questionario = @nomeQuestionario, qst_tp_questionario = @tpQuest, qst_ds_link_instrucoes = @linkInstr WHERE qst_id_questionario = @idQuest", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@nomeQuestionario", aoQuest.qst_id_questionario));
                    ioQuery.Parameters.Add(new SqlParameter("@nomeQuestionario", aoQuest.qst_nm_questionario));
                    ioQuery.Parameters.Add(new SqlParameter("@tpQuest", aoQuest.qst_tp_questionario));
                    ioQuery.Parameters.Add(new SqlParameter("@linkInstr", aoQuest.qst_ds_link_instrucoes));
                    liQtdRegistrosAtulizados = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return liQtdRegistrosAtulizados;
        }
    }
}