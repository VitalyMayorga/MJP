<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetallesFacturaRevision.aspx.cs" Inherits="SistemaMJP.DetallesFacturaRevision" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="form-horizontal">
                <asp:LinkButton runat="server" OnClick="regresarMP" ToolTip="Menu Principal"><i class="glyphicon glyphicon-home atras" style="font-size: 35px;
    margin-left: 2%;"></i></asp:LinkButton>
                <h3 class="Encabezado" id="labelFactura" runat="server"></h3>
                <div class="table-responsive tablaMJP">
                    <asp:GridView ID="GridProductos" class="gridsFormat gridPF" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="PageIndexChanging" OnRowCreated="gridProductos_RowCreated" Width="100%">
                        <%--<Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnAprobar" runat="server" class="btn btn-default" OnClick="btnAprobar_Click" ToolTip="Aprobar"><i class="glyphicon glyphicon-ok" style="color:green"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnRechazar" runat="server" class="btn btn-default" OnClick="btnRechazar_Click" ToolTip="Rechazar"><i class="glyphicon glyphicon-remove"  style="color:red"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>--%>
                    </asp:GridView>
                </div>
                <div class="form-group" style="margin-top: 2%; margin-right: 5%">
                        <%--<div class="col-md-10" style="text-align: right; margin-left: 7.8%">
                        <div class="BotonIngreso">
                            <asp:Button  name="btnVolver" Text="Volver" id="btnVolver"  runat="server" class="btn btn-default" OnClick="volver"></asp:Button>
                        </div>--%>
                        <div class="row BotonIngreso" style="float:right">
                            <asp:Button  name="btnVolver" Text="Volver" id="btnVolver"  runat="server" class="btn btn-default" OnClick="volver"  style="margin-right: 1%;margin-left:-10%"></asp:Button>
                            <asp:Button  name="btnAprobar" Text="Aprobar" id="btnAprobar"  runat="server" class="btn btn-default" OnClick="btnAprobar_Click" style="margin-right: 1%"></asp:Button>
                            <asp:Button  name="btnRechazar" Text="Rechazar" id="btnRechazar"  runat="server" class="btn btn-default" OnClick="btnRechazar_Click"></asp:Button>

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
                    <h4 class="modal-title">Confirmar Justificación</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label id="producto">Justificación</label>      
                               
                    </div>
                    <textarea id="justificacionRechazo" runat="server" style="max-width:300px;width:300px"></textarea>    
                </div>
                <div class="modal-footer">
                    <asp:Button id="btnAceptarM" runat="server" class="btn btn-default" UseSubmitBehavior="false" Text="Aceptar" OnClick="aceptar"></asp:Button>
                    <asp:Button id="btnCancelarM" runat="server" class="btn btn-default" data-dismiss="modal" Text="Cancelar"></asp:Button>
                </div>
            </div>

        </div>
    </div>

    <div id="ModalRD" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Fecha Recepción definitiva</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label>Fecha Recepción Definitiva</label>
                        
                    </div>
                    <div class="form-group">
                        <asp:TextBox runat="server" ID="txtFecha" class="form-control text-box single-line"></asp:TextBox>
                        
                    </div>
                    
                </div>
                <div class="modal-footer">
                    <asp:Button id="btnAP" runat="server" class="btn btn-default" UseSubmitBehavior="false" Text="Aceptar" OnClick="aceptarAprobado"></asp:Button>
                    <asp:Button id="btnC" runat="server" class="btn btn-default" data-dismiss="modal" Text="Cancelar"></asp:Button>
                </div>
            </div>

        </div>
    </div>
    <script type="text/javascript">
        function openModal(value) {
            $('#ModalDetalles').modal('show');
            document.getElementById('producto').innerHTML = value;
        }

        function openModalRD() {
            $('#ModalRD').modal('show');
            $("#<%= txtFecha.ClientID %>").datepicker({
                showmonth: true,
                autoSize: true,
                showAnim: 'slideDown',
                duration: 'fast'
            });
        }
</script>
     <script type="text/javascript">//Convertimos el calendario a español
         $.datepicker.regional['es'] = {
             closeText: 'Cerrar',
             prevText: '<Ant',
             nextText: 'Sig>',
             currentText: 'Hoy',
             monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
             monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
             dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
             dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
             dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
             weekHeader: 'Sm',
             dateFormat: 'dd/mm/yy',
             firstDay: 1,
             isRTL: false,
             showMonthAfterYear: false,
             yearSuffix: ''
         };
         $.datepicker.setDefaults($.datepicker.regional['es']);
         $(document).ready(function () {
             $("#<%= txtFecha.ClientID %>").datepicker({
                showmonth: true,
                autoSize: true,
                showAnim: 'slideDown',
                duration: 'fast'
            });
            

        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_endRequest(function () {
            $("#<%= txtFecha.ClientID %>").datepicker({
                showmonth: true,
                autoSize: true,
                showAnim: 'slideDown',
                duration: 'fast'
            });


        });

    </script>
    
</asp:Content>
