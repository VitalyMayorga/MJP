<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RevisionBajas.aspx.cs" Inherits="SistemaMJP.RevisionBajas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="form-horizontal">
                <asp:LinkButton runat="server" OnClick="regresarMP"><i class="glyphicon glyphicon-circle-arrow-left atras" style="font-size: 35px;
    margin-left: 2%;"></i></asp:LinkButton> 
                <h3 class="Encabezado">Bajas de Productos</h3>
                <div class="table-responsive tablaMJP">
                    <asp:GridView ID="GridBajas" class="gridsFormat gridF" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="PageIndexChanging" OnRowCreated="gridBajas_RowCreated" Width="100%">
                        <Columns>
                            <asp:TemplateField >
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnAceptar" OnClick="aceptar" runat="server" class="btn btn-default" ToolTip="Aceptar Baja"><i class="glyphicon glyphicon-ok"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnRechazar" OnClick="rechazar" runat="server" class="btn btn-default" ToolTip="Rechazar baja"><i class="glyphicon glyphicon-remove"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        
                    </asp:GridView>
                </div>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
