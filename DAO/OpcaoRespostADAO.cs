using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Configuration;
using AvaliacaoTrends.Models;

namespace AvaliacaoTrends.DAO
{
    public class OpcaoRespostADAO
    { 
  
        SqlCommand ioQuery;
        //Instanciando o objeto SqlConnection para abrir a conexão com o banco de dados.
        SqlConnection ioConexao;

        public BindingList<OpcaoResposta> BuscaOpcaoResposta(decimal? idRespostaOpc = null)
        {
            //Criando uma lista de Autores que serão retornadas pela função.
            BindingList<OpcaoResposta> loListRespOpc = new BindingList<OpcaoResposta>();
             
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    //Abrindo a conexão com o servidor.
                    ioConexao.Open();
                    if (idRespostaOpc != null)
                    {
                      
                        ioQuery = new SqlCommand("SELECT * FROM OPR_OPCAO_RESPOSTA_Acarvalho WHERE opr_id_opcao_resposta = @idRespostaOp", ioConexao);
                       
                        ioQuery.Parameters.Add(new SqlParameter("@idRespostaOp", idRespostaOpc));
                    }
                    else
                    {
                        
                        ioQuery = new SqlCommand("SELECT * FROM OPR_OPCAO_RESPOSTA_Acarvalho", ioConexao);
                    }
                  
                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                       
                        while (loReader.Read())
                        {
                            decimal id = loReader.GetDecimal(0);
                            decimal idPergunta = loReader.GetDecimal(1);
                            string opcaoRespota = loReader.GetString(2);
                            char opcaoCorreta = Convert.ToChar(loReader.GetString(3));
                            int ordemNu = loReader.GetInt32(3);
                            OpcaoResposta loNovoRespOpc = new OpcaoResposta(id, idPergunta, opcaoRespota, opcaoCorreta, ordemNu);
                            loListRespOpc.Add(loNovoRespOpc);
                            
                        }
                       
                        loReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao tentar buscar o(s) autor(s).");
                }
            }
            return loListRespOpc;
        }
        public int InsereOpcaoResposta(OpcaoResposta aoRespostaOp)
        {
       
            if (aoRespostaOp == null)
                throw new NullReferenceException();

            int liQtdRegistrosInseridos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("INSERT INTO OPR_OPCAO_RESPOSTA_Acarvalho(opr_id_opcao_resposta, opr_id_pergunta, opr_ds_opcao_resposta,opr_ch_resposta_correta ,opr_nu_ordem,) VALUES (@idRespostaOp, @idPergunta, @dsOpcaoResposta,@dsOpcaoResCorreta, @opNuOrdem)", ioConexao);

                    ioQuery.Parameters.Add(new SqlParameter("@idRespostaOp", aoRespostaOp.opr_id_opcao_resposta));
                    ioQuery.Parameters.Add(new SqlParameter("@idPergunta", aoRespostaOp.opr_id_pergunta));
                    ioQuery.Parameters.Add(new SqlParameter("@dsOpcaoResposta", aoRespostaOp.opr_ds_opcao_resposta));
                    ioQuery.Parameters.Add(new SqlParameter("@dsOpcaoResCorreta", aoRespostaOp.opr_ch_resposta_correta));
                    ioQuery.Parameters.Add(new SqlParameter("@opNuOrdem", aoRespostaOp.opr_nu_ordem));

                   
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return liQtdRegistrosInseridos;
        }

        public int RemoveOpcResposta(OpcaoResposta aoOpcResposta)
        {
            if (aoOpcResposta == null)
                throw new NullReferenceException();

            int liQtdRegistrosExcluidos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("DELETE FROM OPR_OPCAO_RESPOSTA_Acarvalho WHERE opr_id_opcao_resposta = @idRespostaOp", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idRespostaOp", aoOpcResposta.opr_id_opcao_resposta));
                    liQtdRegistrosExcluidos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return liQtdRegistrosExcluidos;
        }


        public int atualizarOpcResposta(OpcaoResposta aoOpcResposta)
        {
            if (aoOpcResposta == null)
                throw new ArgumentNullException();

            int liQtdRegistrosAtulizados = 0;

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();

                    ioQuery = new SqlCommand("UPDATE OPR_OPCAO_RESPOSTA_Acarvalho SET opr_ds_opcao_resposta = @dsOpcaoResposta, opr_ch_resposta_correta = @dsOpcaoResCorreta, opr_nu_ordem = @opNuOrdem WHERE opr_id_opcao_resposta = @idRespostaOp AND opr_id_pergunta = @idPergunta", ioConexao);

                    ioQuery.Parameters.Add(new SqlParameter("@idRespostaOp", aoOpcResposta.opr_id_opcao_resposta));
                    ioQuery.Parameters.Add(new SqlParameter("@idPergunta", aoOpcResposta.opr_id_pergunta));
                    ioQuery.Parameters.Add(new SqlParameter("@dsOpcaoResposta", aoOpcResposta.opr_ds_opcao_resposta));
                    ioQuery.Parameters.Add(new SqlParameter("@dsOpcaoResCorreta", aoOpcResposta.opr_ch_resposta_correta));
                    ioQuery.Parameters.Add(new SqlParameter("@opNuOrdem", aoOpcResposta.opr_nu_ordem));

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