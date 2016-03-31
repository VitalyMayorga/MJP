<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Detalles_Factura.aspx.cs" Inherits="SistemaMJP.Detalles_Factura" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="form-horizontal">
                <asp:LinkButton runat="server" OnClick="regresarMP" ToolTip="Menu Principal"><i class="glyphicon glyphicon-home atras" style="font-size: 35px;
    margin-left: 2%;"></i></asp:LinkButton> 
                <h3 class="Encabezado" id="labelFactura" runat="server"></h3>
                <div class="form-group">
                    <div class="col-md-1" style="margin-left:3%;">
                        <div class="BotonIngreso">
                            <asp:Button ID="btnVistaPrevia" class="btn btn-default" runat="server" Text="Vista Previa" OnClick="vistaPrevia" />
                        </div>

                    </div>
                </div>
                <h4 class="Encabezado">Productos de la factura</h4>
                <div class="table-responsive tablaMJP">
                    <asp:GridView ID="GridProductos" class="gridsFormat gridF" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="PageIndexChanging" OnRowCreated="gridProductos_RowCreated" Width="100%">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnModificar" runat="server" class="btn btn-default" OnClick="btnEditar_Click" ToolTip="Editar"><i class="glyphicon glyphicon-edit"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnModificar" runat="server" class="btn btn-default" OnClick="btnEliminar_Click" ToolTip="Eliminar"><i class="glyphicon glyphicon-remove"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
