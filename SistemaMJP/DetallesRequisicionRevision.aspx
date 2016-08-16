<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetallesRequisicionRevision.aspx.cs" Inherits="SistemaMJP.DetallesRequisicionRevision" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="form-horizontal">
                <asp:LinkButton runat="server" OnClick="regresarMP" ToolTip="Menu Principal"><i class="glyphicon glyphicon-home atras" style="font-size: 35px;
    margin-left: 2%;"></i></asp:LinkButton>
                <h3 class="Encabezado" id="labelRequisicion" runat="server"></h3>
                <div class="table-responsive tablaMJP" id="GridAprobadorPrograma" style="display: none;">
                    <asp:GridView ID="GridProductosRequisicion" class="gridsFormat gridPF" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="PageIndexChanging" OnRowCreated="gridProductosRequisicion_RowCreated" Width="50%">
                                            
                        <Columns>
                            <asp:TemplateField visible="true">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEditar" runat="server" class="btn btn-default col1" OnClick="btnEditar_Click" ToolTip="Editar"><i class="glyphicon glyphicon-edit" style="color:black"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEliminar" runat="server" class="btn btn-default col1" OnClick="btnEliminar_Click" ToolTip="Eliminar"><i class="glyphicon glyphicon-trash" style="color:red"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        
                    </asp:GridView>
                </div>

                <div class="table-responsive tablaMJP" id="GridAprobadorAlmacen" style="display: none;">
                    <asp:GridView ID="GridProductosRequisicionAlmacen" class="gridsFormat gridPF" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="PageIndexChanging" OnRowCreated="gridProductosRequisicion_RowCreated" Width="50%">                       
                    </asp:GridView>
                </div>

                <div class="form-group" style="margin-top: 2%; margin-right: 5%">

                    <div style="display: none;" id="MsjErrorCantActivo" class="col-md-offset-2" runat="server">
                        <label class="mensajeError">Existen activos que aun no estan relacionados con esta requisicion, favor completar esta informacion antes de continuar</label>
                        <br/> 
                    </div>                        
                    
                    <div class="row BotonIngreso" style="float:right">
                            <asp:Button  name="btnDevolver" Text="Devolver" id="btnDevolver"  runat="server" class="btn btn-default" OnClick="btnDevolver_Click"  style="margin-right: 1%;margin-left:-10%"></asp:Button>
                            
                            <div class="form-group" style="display: none;" id="btnAprobador" runat="server">
                                <asp:Button  name="btnAprobar" Text="Aprobar" id="btnAprobar"  runat="server" class="btn btn-default" OnClick="btnAprobar_Click" style="margin-right: 1%"></asp:Button>
                            </div>

                            <div class="form-group" style="display: none;" id="btnAlmacen" runat="server">
                                <asp:Button  name="btnDespachar" Text="Despachar" id="btnDespachar"  runat="server" class="btn btn-default" OnClick="btnDespachar_Click"></asp:Button>
                            </div>
                        </div>

                </div>
            </div>

            
        </ContentTemplate>

    </asp:UpdatePanel>
    <div id="ModalDetalles" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Justificación de la Devolución</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label id="producto">Justificación</label>      
                               
                    </div>
                    <textarea id="justificacionDevolucion" runat="server" style="max-width:300px;width:300px"></textarea>    
                </div>
                <div class="modal-footer">
                    <asp:Button id="btnAceptarR" runat="server" class="btn btn-default" UseSubmitBehavior="false" Text="Aceptar" OnClick="aceptarDevolucion"></asp:Button>
                    <asp:Button id="btnCancelarR" runat="server" class="btn btn-default" data-dismiss="modal" Text="Cancelar"></asp:Button>
                </div>
            </div>

        </div>
    </div>

    <div id="ModalEditar" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Edicion de linea</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label id="linea"></label>      
                               
                    </div>
                    <asp:textbox id="cantidad" runat="server" style="max-width:300px;width:300px"></asp:textbox>    
                </div>
                <div class="modal-footer">
                    <asp:Button id="Button1" runat="server" class="btn btn-default" UseSubmitBehavior="false" Text="Aceptar" OnClick="aceptarEdicion"></asp:Button>
                    <asp:Button id="Button2" runat="server" class="btn btn-default" data-dismiss="modal" Text="Cancelar"></asp:Button>
                </div>
            </div>

        </div>
    </div>

    
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
                    <asp:Button id="btn_Acp" runat="server" class="btn btn-default" UseSubmitBehavior="false" Text="Aceptar" OnClick="aceptarEliminado"></asp:Button>
                    <asp:Button id="btn_Can" runat="server" class="btn btn-default" data-dismiss="modal" Text="Cancelar"></asp:Button>
                </div>
            </div>

        </div>
    </div>
    
    <script type="text/javascript">
        function openModal(value) {
            $('#ModalDetalles').modal('show');
            document.getElementById('producto').innerHTML = value;
        }

        function openModalEdicion(value) {
            $('#ModalEditar').modal('show');
            document.getElementById('linea').innerHTML = value;
        }

        function openModalDelete(value) {
            $('#EliminarModal').modal('show');
            document.getElementById('productoEliminar').innerHTML = value;
        }

        $("td.col1").hide();
</script>
    
    
</asp:Content>
