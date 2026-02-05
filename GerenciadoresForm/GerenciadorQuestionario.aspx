<%@ Page Title="Gerenciador de Questionário" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GerenciadorQuestionario.aspx.cs" Inherits="AvaliacaoTrends.GerenciadoresForm.GerenciadorQuestionario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function OnEndCallback(s, e) {
            if (s.cpMensagemSucesso) {
                alert(s.cpMensagemSucesso);
                delete s.cpMensagemSucesso;
            }
            if (s.cpMensagemErro) {
                alert(s.cpMensagemErro);
                delete s.cpMensagemErro;
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <dx:ASPxFormLayout ID="ASPxFormLayoutPrincipal" runat="server" Width="100%" Theme="Office365">
        <Items>
            <dx:LayoutGroup Caption="Cadastro de Questionário" ColCount="3" SettingsItemCaptions-Location="Top">
                <Items>
                     
                    <dx:LayoutItem Caption="Nome do Questionário">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox ID="tbxNomeQuestionario" runat="server" Width="100%">
                                    <ValidationSettings ValidationGroup="ValidaQuestionario" Display="Dynamic">
                                        <RequiredField IsRequired="True" ErrorText="Campo obrigatório!" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    
                    <dx:LayoutItem Caption="Tipo">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxComboBox ID="cmbTipoQuestionario" runat="server" Width="100%">
                                    <Items>
                                        <dx:ListEditItem Text="Avaliação" Value="A" />
                                        <dx:ListEditItem Text="Pesquisa" Value="P" />
                                        <dx:ListEditItem Text="Feedback" Value="F" />
                                    </Items>
                                    <ValidationSettings ValidationGroup="ValidaQuestionario" Display="Dynamic">
                                        <RequiredField IsRequired="True" ErrorText="Selecione um tipo!" />
                                    </ValidationSettings>
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    
                    <dx:LayoutItem Caption="Link de Instruções">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox ID="tbxLinkInstrucoes" runat="server" Width="100%">
                                    <ValidationSettings ValidationGroup="ValidaQuestionario" Display="Dynamic">
                                        <RequiredField IsRequired="True" ErrorText="O link é obrigatório!" />
                                        <RegularExpression ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?" 
                                            ErrorText="Formato de link inválido (Use http:// ou https://)" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    
                    <dx:LayoutItem Caption="" ColSpan="3">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxButton ID="btnSalvar" runat="server" Text="Cadastrar Questionário" AutoPostBack="true" 
                                    OnClick="BtnNovoQuestionario_Click" ValidationGroup="ValidaQuestionario" Theme="Office365" />
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
        </Items>
    </dx:ASPxFormLayout>

    <br />

    <dx:ASPxGridView ID="gvGerenciamentoQuestionarios" runat="server" Width="100%" 
        KeyFieldName="qst_id_questionario" AutoGenerateColumns="False" Theme="Office365"
        OnRowUpdating="gvGerenciamentoQuestionarios_RowUpdating" 
        OnRowDeleting="gvGerenciamentoQuestionarios_RowDeleting">
        
        <ClientSideEvents EndCallback="OnEndCallback" />
        
        <Columns>
            <dx:GridViewDataTextColumn FieldName="qst_id_questionario" Visible="false" />
            <dx:GridViewDataTextColumn FieldName="qst_nm_questionario" Caption="Nome" VisibleIndex="1" />
            <dx:GridViewDataComboBoxColumn FieldName="qst_tp_questionario" Caption="Tipo" VisibleIndex="2">
                <PropertiesComboBox>
                    <Items>
                        <dx:ListEditItem Text="Avaliação" Value="A" />
                        <dx:ListEditItem Text="Pesquisa" Value="P" />
                        <dx:ListEditItem Text="Feedback" Value="F" />
                    </Items>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataTextColumn FieldName="qst_ds_link_instrucoes" Caption="Link de Instruções" VisibleIndex="3" />
            <dx:GridViewCommandColumn ShowEditButton="True" ShowDeleteButton="True" VisibleIndex="4" Caption="Ações" />
        </Columns>

        <Settings ShowFilterRow="True" ShowGroupPanel="True" />
        <SettingsEditing Mode="Batch" />
    </dx:ASPxGridView>
</asp:Content>