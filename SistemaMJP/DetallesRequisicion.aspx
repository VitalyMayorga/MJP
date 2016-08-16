<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetallesRequisicion.aspx.cs" Inherits="SistemaMJP.DetallesRequisicion" %>
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
                    <div class="col-md-1" style="margin-left: 3%;">
                        <div class="BotonIngreso">
                            <asp:Button ID="btnNuevoProducto" class="btn btn-default" runat="server" Text="Nuevo producto" OnClick="nuevoProducto" />
                        </div>

                    </div>
                </div>
                <h4 class="Encabezado">Productos de la requisición</h4>
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
                <div class="form-group" style="margin-top: 2%; margin-right: 5%">
                    <div class="col-md-10" style="text-align: right; margin-left: 7.8%">
                        <div class="BotonIngreso">
                            <asp:Button type="submit" name="btnEliminarRequisicion" Text="Eliminar requisición" id="btnEliminarReq" class="btn btn-default" runat="server" OnClick="btnEliminar"></asp:Button>
                        </div>
                    </div>
                    <div class="col-md-1" style="margin-left: 0.5%;">
                        <div class="BotonIngreso">
                            <asp:Button type="submit" name="btnEnviarAprobacion" Text="Enviar a aprobación" id="btnEnviarAprobacion" class="btn btn-default" runat="server" OnClick="btnEnviar"></asp:Button>
                        </div>


                    </div>
                </div>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="EliminarModal" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Confirmar Eliminado</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label id="productoEliminar"></label>
                        
                    </div>
                    
                </div>
                <div class="modal-footer">
                    <asp:Button id="btnAceptarM" runat="server" class="btn btn-default" UseSubmitBehavior="false" Text="Aceptar" OnClick="aceptarEliminado"></asp:Button>
                    <asp:Button id="btnCancelarM" runat="server" class="btn btn-default" data-dismiss="modal" Text="Cancelar"></asp:Button>
                </div>
            </div>

        </div>
    </div>
    <div id="AgregarProducto" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Nuevo producto a requisición</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label id="nombreProducto" runat="server"></label>
                    </div>
                    <div class="form-group">
                        <label>Descripción artículo</label>
                    </div>
                    <div class="form-group">
                        <label id="descripcionLabel" runat="server"></label>
                    </div>
                    <div class="form-group">
                        <label>Cantidad (Unidades)</label>
                        <asp:TextBox ID="txtCantidad" runat="server" placeholder="Ej:10" class="form-control text-box single-line"></asp:TextBox>
                        <div style="display: none;" id="MsjErrorPrograma" class="col-md-offset-2" runat="server">
                        <label class="msjErroneo" id="MensajeErrorTxt" runat="server"></label>
                    </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button id="btnAceptar" runat="server" class="btn btn-default" UseSubmitBehavior="false" Text="Aceptar" OnClick="aceptar"></asp:Button>
                    <asp:Button id="btnCancelar" runat="server" class="btn btn-default" data-dismiss="modal" Text="Cancelar"></asp:Button>
                </div>
            </div>

        </div>
    </div>
    <script type="text/javascript">
        function openModal() {
            $('#AgregarProducto').modal('show');
        }
        function openModalEliminar(value) {
            $('#EliminarModal').modal('show');
            document.getElementById('productoEliminar').innerHTML = value;
        }
</script>
</asp:Content>
