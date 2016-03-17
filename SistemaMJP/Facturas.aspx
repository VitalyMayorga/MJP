<%@ Page Title="Facturas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Facturas.aspx.cs" Inherits="SistemaMJP.Facturas1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="form-horizontal">
                <asp:LinkButton runat="server" OnClick="regresarMP"><i class="glyphicon glyphicon-circle-arrow-left atras" style="font-size: 35px;
    margin-left: 2%;"></i></asp:LinkButton> 
                <h3 class="Encabezado">Mercadería</h3>
                <div class="form-group">
                    <div class="col-md-offset-1 col-md-2 alinearDerecha">
                        <div class="BotonIngreso">
                            <asp:Button ID="IngresarProductos" class="btn btn-default" runat="server" Text="Ingresar Productos" OnClick="ingresar" />
                        </div>

                    </div>
                </div>
                <h4 class="Encabezado">Facturas de la bodega</h4>
                <div class="table-responsive">
                    <asp:GridView ID="GridFacturas" class="table-hover" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="PageIndexChanging" Width="100%">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnConsultar" OnClick="btnVer_Click" runat="server" class="btn btn-default" ToolTip="Consultar"><i class="glyphicon glyphicon-search"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnModificar" runat="server" class="btn btn-default" OnClick="btnEditar_Click" ToolTip="Editar"><i class="glyphicon glyphicon-edit"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        
                    </asp:GridView>
                </div>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
