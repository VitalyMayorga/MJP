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
                            <asp:Button ID="btnNuevoProducto" class="btn btn-default" runat="server" Text="Nuevo producto" OnClick="nuevoProducto" />
                        </div>

                    </div>
                </div>
                <h4 class="Encabezado">Productos de la factura</h4>
                <div class="table-responsive tablaMJP">
                    <asp:GridView ID="GridProductos" class="gridsFormat gridPF" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="PageIndexChanging" OnRowCreated="gridProductos_RowCreated" Width="100%">
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
                                    <asp:LinkButton ID="btnEliminar" runat="server" class="btn btn-default" OnClick="btnEliminar_Click" ToolTip="Eliminar"><i class="glyphicon glyphicon-remove"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="form-group" style="margin-top:2%;margin-right:5%">
                    <div class="col-md-10" style="text-align:right;margin-left:7.8%">
                        <div class="BotonIngreso">
                            <input type="submit" name="btnVistaPrevia" value="Vista Previa" id="btnVistaPrevia" class="btn btn-default">
                        </div>
                      </div>
					<div class="col-md-1" style="margin-left:0.5%;">
                        <div class="BotonIngreso">
                            <input type="submit" name="btnEnviarAprobacion" value="Enviar a aprobación" id="btnEnviarAprobacion" class="btn btn-default">
                        </div>

                    
                    </div>
                </div>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
