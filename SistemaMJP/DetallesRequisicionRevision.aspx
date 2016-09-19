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
                <div style="display: none;" id="GridAprobadorPrograma" class="table-responsive tablaMJP" runat="server">
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

                <div style="display: none;" id="GridAprobadorAlmacen" class="table-responsive tablaMJP" runat="server">
                    <asp:GridView ID="GridProductosRequisicionAlmacen" class="gridsFormat gridPF" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="PageIndexChanging" OnRowCreated="gridProductosRequisicion_RowCreated" Width="50%">                       
                    </asp:GridView>
                </div>

                <div class="form-group" style="margin-top: 2%; margin-right: 5%">

                    <div style="display: none;" id="MsjErrorCantActivo" class="col-md-offset-2" runat="server">
                        <label class="mensajeError">Existen activos que aun no estan relacionados con esta requisicion, favor completar esta informacion antes de continuar</label>
                        <br/> 
                    </div>                        
                    
                    <div class="BotonIngreso" style="float:left">
                        <div class="form-group" style="display: none;" id="btn_boleta" runat="server">
                            <asp:Button type="submit" name="btnBoleta" Text="Boleta" id="btnBoleta"  runat="server" class="btn btn-default" OnClick="btnBoleta_Click"  style="margin-left: 1%; margin-right:-10%"></asp:Button>
                        </div>
                    </div>

                    <div class="row BotonIngreso" style="float:right">
                            <div class="form-group" style="display: block;" id="btn_devolver" runat="server">
                                <asp:Button  name="btnDevolver" Text="Devolver" id="btnDevolver"  runat="server" class="btn btn-default" OnClick="btnDevolver_Click"  style="margin-right: 1%;margin-left:-10%"></asp:Button>
                            </div>
                            <div class="form-group" style="display: none;" id="btn_aprobar" runat="server">
                                <asp:Button  name="btnAprobar" Text="Aprobar" id="btnAprobar"  runat="server" class="btn btn-default" OnClick="btnAprobar_Click" style="margin-right: 1%"></asp:Button>
                            </div>
                            <div class="form-group" style="display: none;" id="btn_rechazar" runat="server">
                                <asp:Button  name="btnRechazar" Text="Rechazar" id="btnRechazar"  runat="server" class="btn btn-default" OnClick="btnRechazar_Click" style="margin-right: 1%"></asp:Button>
                            </div>

                            <div class="form-group" style="display: none;" id="btn_despachar" runat="server">
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
                        <label id="producto"></label>      
                               
                    </div>
                    <textarea id="justificacionDevolucion" runat="server" style="max-width:300px;width:300px"></textarea>    
                </div>
                <div class="modal-footer">
                    <asp:Button id="btnAceptarJD" runat="server" class="btn btn-default" UseSubmitBehavior="false" Text="Aceptar" OnClick="aceptarDevolucion"></asp:Button>
                    <asp:Button id="btnCancelarJD" runat="server" class="btn btn-default" data-dismiss="modal" Text="Cancelar"></asp:Button>
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
                        <label id="idroweditar" style="display: none;" runat="server"></label>      
                               
                    </div>
                    <div class="form-group">
                        <label>Cantidad (Unidades)</label>
                        <asp:TextBox ID="txtCantidad" runat="server" class="form-control text-box single-line"></asp:TextBox>
                        <div style="display: none;" id="MsjErrorcantidad" class="col-md-offset-2" runat="server">
                        <label class="msjErroneo" id="MensajeErrorTxt" runat="server"></label>
                    </div>
                    </div>  
                </div>
                <div class="modal-footer">
                    <asp:Button id="btnAceptarEdicion" runat="server" class="btn btn-default" UseSubmitBehavior="false" Text="Aceptar" OnClick="aceptarEdicion"></asp:Button>
                    <asp:Button id="btnCancelarEdicion" runat="server" class="btn btn-default" data-dismiss="modal" Text="Cancelar"></asp:Button>
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
                        <label id="idroweliminar" style="display: none;" runat="server"></label> 
                        
                    </div>
                    
                </div>
                <div class="modal-footer">
                    <asp:Button id="btn_Acp" runat="server" class="btn btn-default" UseSubmitBehavior="false" Text="Aceptar" OnClick="aceptarEliminado"></asp:Button>
                    <asp:Button id="btn_Can" runat="server" class="btn btn-default" data-dismiss="modal" Text="Cancelar"></asp:Button>
                </div>
            </div>

        </div>
    </div>

    <div id="ModalDetallesRechazo" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Justificación del Rechazo</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label id="requisicion"></label>      
                               
                    </div>
                    <textarea id="justificacionRechazo" runat="server" style="max-width:300px;width:300px"></textarea>    
                </div>
                <div class="modal-footer">
                    <asp:Button id="btnAceptarJR" runat="server" class="btn btn-default" UseSubmitBehavior="false" Text="Aceptar" OnClick="aceptarRechazo"></asp:Button>
                    <asp:Button id="btnCancelarJR" runat="server" class="btn btn-default" data-dismiss="modal" Text="Cancelar"></asp:Button>
                </div>
            </div>

        </div>
    </div>

    <div id="ModalInfoDespacho" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Datos de Despacho</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label id="texto"></label> 
                        <label id="idRequisicion" style="display: none;" runat="server"></label>      
                               
                    </div>
                    <div class="form-group">
                        <label>Placa del Automobil</label>
                        <asp:TextBox ID="txtPlaca" runat="server" class="form-control text-box single-line"></asp:TextBox>
                        <div style="display: none;" id="MsjErrorPlaca" class="col-md-offset-2" runat="server">
                            <label class="msjErroneo" id="Label2" runat="server"></label>
                        </div>

                        <label>Conductor Designado</label>
                        <asp:TextBox ID="txtConductor" runat="server" class="form-control text-box single-line"></asp:TextBox>
                        <div style="display: none;" id="MsjErrorConductor" class="col-md-offset-2" runat="server">
                            <label class="msjErroneo" id="Label3" runat="server"></label>
                        </div>

                        <label>Destinatario</label>
                        <asp:TextBox ID="txtDestinatario" runat="server" class="form-control text-box single-line"></asp:TextBox>
                        <div style="display: none;" id="MsjErrorDestinatario" class="col-md-offset-2" runat="server">
                            <label class="msjErroneo" id="Label4" runat="server"></label>
                        </div>
                    </div>  
                </div>
                <div class="modal-footer">
                    <asp:Button id="btnAceptarInfo" runat="server" class="btn btn-default" UseSubmitBehavior="false" Text="Aceptar" OnClick="aceptarDespacho"></asp:Button>
                    <asp:Button id="btnCancelarInfo" runat="server" class="btn btn-default" data-dismiss="modal" Text="Cancelar"></asp:Button>
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

        function openModalRechazo(value) {
            $('#ModalDetallesRechazo').modal('show');
            document.getElementById('requisicion').innerHTML = value;
        }

        function openModalCompletarInfo(value) {
            $('#ModalInfoDespacho').modal('show');
            document.getElementById('texto').innerHTML = value;
        }

        $("td.col1").hide();
</script>
    
    
</asp:Content>
