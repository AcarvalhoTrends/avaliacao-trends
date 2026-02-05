
using DevExpress.Web.Data;
using DevExpress.Web;
using AvaliacaoTrends.DAO;
using AvaliacaoTrends.Models;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AvaliacaoTrends.GerenciadoresForm
{
    public partial class GerenciadorQuestionario : System.Web.UI.Page
    {
        QuestionarioDAO loQuestionarioDAO = new QuestionarioDAO();

        public BindingList<Questionario> ListaQuestionario
        {
            get
            {
                if ((BindingList<Questionario>)ViewState["ViewStateListaQuestionario"] == null)
                    this.CarregaDados();
                return (BindingList<Questionario>)ViewState["ViewStateListaQuestionario"];
            }
            set
            {
                ViewState["ViewStateListaQuestionario"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CarregaDados();
        }

        private void CarregaDados()
        {
            try
            {
                this.ListaQuestionario = loQuestionarioDAO.BuscaQuestinario();
                this.gvGerenciamentoQuestionarios.DataSource = ListaQuestionario;
                this.gvGerenciamentoQuestionarios.DataBind();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected void BtnNovoQuestionario_Click(object sender, EventArgs e)
        {
            try
            {
                decimal idQuestionario = 1;
                if (this.ListaQuestionario != null && this.ListaQuestionario.Count > 0)
                    idQuestionario = this.ListaQuestionario.OrderByDescending(a => a.qst_id_questionario).First().qst_id_questionario + 1;
                string nomeQuestionario = this.tbxNomeQuestionario.Text;
                char tipoQuestionario = ' ';
                if (!string.IsNullOrEmpty(this.cmbTipoQuestionario.Text))
                    tipoQuestionario = this.cmbTipoQuestionario.Text[0];
                string linkInstrucao = this.tbxLinkInstrucoes.Text;

                Questionario loQuest = new Questionario(idQuestionario, nomeQuestionario, tipoQuestionario, linkInstrucao);
                this.loQuestionarioDAO.InsereQuest(loQuest);
                ViewState["ViewStateListaQuestionario"] = null;
                this.CarregaDados();
                HttpContext.Current.Response.Write("<script>alert('Questionario cadastrado com sucesso!');</script>");
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Erro no cadastro.');</script>");
            }

            this.tbxNomeQuestionario.Text = String.Empty;
            this.cmbTipoQuestionario.Text = String.Empty;
            this.tbxLinkInstrucoes.Text = String.Empty;
        }

        protected void gvGerenciamentoQuestionarios_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            e.Cancel = true;
            try
            {
                decimal idQuestionario = Convert.ToDecimal(e.Keys["qst_id_questionario"]);
                string nomeQuestionario = Convert.ToString(e.NewValues["qst_nm_questionario"]);
                string linkInstrucao = Convert.ToString(e.NewValues["qst_ds_link_instrucoes"]);
                string tipoStr = Convert.ToString(e.NewValues["qst_tp_questionario"]);
                char tipoQuestionario;
                if (string.IsNullOrEmpty(tipoStr) || !char.TryParse(tipoStr.Substring(0, 1), out tipoQuestionario))
                {
                    if (!char.TryParse(tipoStr, out tipoQuestionario))
                        throw new Exception("Tipo inválido.");
                }

                if (string.IsNullOrEmpty(nomeQuestionario))
                    throw new Exception("Informe o nome do questionário");
                if (string.IsNullOrEmpty(linkInstrucao))
                    throw new Exception("Informe o link de instruções");

                Questionario Questi = new Questionario()
                {
                    qst_id_questionario = idQuestionario,
                    qst_nm_questionario = nomeQuestionario,
                    qst_tp_questionario = tipoQuestionario,
                    qst_ds_link_instrucoes = linkInstrucao
                };

                this.loQuestionarioDAO.atualizarQuestionario(Questi);
                this.gvGerenciamentoQuestionarios.CancelEdit();
                ViewState["ViewStateListaQuestionario"] = null;
                CarregaDados();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar: " + ex.Message);
            }
        }

        protected void gvGerenciamentoQuestionarios_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {
            e.Cancel = true;
            try
            {
                decimal idQuestionario = Convert.ToDecimal(e.Keys["qst_id_questionario"]);
                loQuestionarioDAO.RemoveQuest(idQuestionario);
                this.gvGerenciamentoQuestionarios.CancelEdit();
                ViewState["ViewStateListaQuestionario"] = null;
                CarregaDados();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir: " + ex.Message);
            }
        }

        private void ShowMessage(string msg)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{msg}');", true);
        }
    }
}