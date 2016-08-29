<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ingreso_Requisicion.aspx.cs" Inherits="SistemaMJP.Ingreso_Requisicion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="form-horizontal">
                <h3 class="Encabezado">Ingreso de productos</h3>

                <div class="form-group">
                    <label class="col-md-2 control-label">Producto a buscar:</label>
                    <div class="col-md-2">
                        <asp:Textbox id="productoBusqueda" runat="server" class="form-control text-box single-line"></asp:Textbox>
                    </div>
                    <div class="col-md-2">
                        <asp:LinkButton ID="btnModificar" runat="server" class="btn btn-default" OnClick="btnBuscar_Click" ToolTip="Buscar"><i class="glyphicon glyphicon-search"></i></asp:LinkButton>
                    </div>
                </div>
                <div class="table-responsive tablaMJP" id="tablaProductos" runat="server">
                    <asp:GridView ID="GridProductos" class="gridsFormat gridF" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="PageIndexChanging" OnRowCreated="gridProductos_RowCreated" Width="100%">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnSeleccionar" runat="server" class="btn btn-default" OnClick="btnSeleccionar_Click" ToolTip="Seleccionar"><i class="glyphicon glyphicon-plus"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                
                <div class="form-group">
                    <div class="alinearDerecha" style="margin-right:3%">
                        <div class="BotonIngreso">
                            <asp:Button ID="btnSalir" class="btn btn-default" runat="server" Text="Cancelar" OnClick="cancelar" />
                        </div>

                    </div>
                    
                </div>

            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnAceptar" EventName="Click"/>
        </Triggers>
    </asp:UpdatePanel>

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
                        <label id="nombreProducto"></label>
                    </div>
                    <div class="form-group">
                        <label>Presentación empaque</label>
                    </div>
                    <div class="form-group">
                        <label id="descripcionLabel"></label>
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
                    <asp:Button id="btnAceptarS" runat="server" class="btn btn-default" UseSubmitBehavior="false" Text="Aceptar y Salir" OnClick="aceptarYSalir"></asp:Button>
                    <asp:Button id="btnCancelar" runat="server" class="btn btn-default" data-dismiss="modal" Text="Cancelar"></asp:Button>
                </div>
            </div>

        </div>
    </div>
    <script type="text/javascript">
        function openModal(nombre, descripcion) {
            document.getElementById('nombreProducto').innerHTML = nombre;
            document.getElementById('descripcionLabel').innerHTML = descripcion;
            $('#AgregarProducto').modal('show');
        }
</script>
</asp:Content>

