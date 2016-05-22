<%@ Page Title="PreIngresoProductos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Preingreso-Productos.aspx.cs" Inherits="SistemaMJP.Preingreso_Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="form-horizontal">
                <h3 class="Encabezado">Ingreso de Mercadería</h3>
                <div class="form-group">
                    <label class="col-md-2 control-label">Ubicación</label>
                    <div class="col-md-10">
                        <asp:DropDownList ID="ListaBodegas" runat="server" class="form-control dropdown cmbsize" OnSelectedIndexChanged="revisarBodega" AutoPostBack="true"></asp:DropDownList>

                    </div>
                    <div style="display: none;" id="MsjErrorBodega" class="col-md-offset-2" runat="server">
                        <label class="msjErroneo">Debe elegir una bodega</label>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-2 control-label">Programa Presupuestario</label>
                    <div class="col-md-10">
                        <asp:DropDownList ID="ListaProgramas" runat="server" class="form-control dropdown cmbsize" OnSelectedIndexChanged="revisarPrograma" AutoPostBack="true"></asp:DropDownList>

                    </div>
                    <div style="display: none;" id="MsjErrorPrograma" class="col-md-offset-2" runat="server">
                        <label class="msjErroneo">Debe elegir un programa</label>
                    </div>
                </div>
                <div class="form-group" id="Subbodega" style="display: none;" runat="server">
                    <label class="col-md-2 control-label">Departamento</label>
                    <div class="col-md-10">
                        <asp:DropDownList ID="ListaSubBodegas" runat="server" class="form-control dropdown cmbsize" OnSelectedIndexChanged="revisarSubB" AutoPostBack="true"></asp:DropDownList>

                    </div>
                    <div style="display: none;" id="MsjErrorSubBodega" class="col-md-offset-2" runat="server">
                        <label class="msjErroneo">Debe ingresar un departamento</label>
                    </div>
                </div>

                <div class="form-group">

                    <div class="col-md-3 alinearDerecha">
                        <asp:RadioButton ID="ingresoF" runat="server" GroupName="tipoP" OnCheckedChanged="rbIngresoF" AutoPostBack="true" />
                        <label class="control-label" for="ingresoF">Ingreso de Factura</label>
                    </div>
                    <div class="col-md-5">
                        <asp:RadioButton ID="mercaderiaI" runat="server" GroupName="tipoP" OnCheckedChanged="rbMercaderiaI" AutoPostBack="true" />
                        <label class="control-label" for="mercaderiaI">Inventario Inicial</label>
                    </div>
                </div>

                <div class="form-group" id="formFacturas" runat="server">
                    <label class="col-md-2 control-label">Número Factura</label>

                    <div class="col-md-10">
                        <asp:TextBox ID="txtNumFactura" runat="server" placeholder="Número de Factura" class="form-control text-box single-line"></asp:TextBox>
                    </div>
                    <div style="display: none;" id="MsjErrorFactura" class="col-md-offset-2" runat="server">
                        <label class="msjErroneo">Debe ingresar un número de factura</label>
                    </div>
                </div>

                <div class="form-group" id="formFacturaFecha" runat="server">
                    <label class="col-md-2 control-label">Fecha Factura</label>
                    <div class='input-group date col-md-10' style="max-width: 320px">

                        <asp:TextBox type='text' ID="txtFechaF" class="form-control" runat="server"></asp:TextBox>


                    </div>
                    <div style="display: none;" id="MsjErrorFecha" class="col-md-offset-2" runat="server">
                        <label class="msjErroneo">Debe ingresar una fecha a la factura</label>
                    </div>
                </div>
                <div class="form-group" id="formProveedor" runat="server">
                    <label class="col-md-2 control-label">Proveedor</label>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ListaProveedores" runat="server" class="form-control dropdown cmbsize" OnSelectedIndexChanged="revisarProveedores" AutoPostBack="true"></asp:DropDownList>

                    </div>
                    <div class="col-md-7">
                        <asp:Button ID="nuevoProveedor" class="btn btn-default" runat="server" Text="Nuevo Proveedor" data-toggle="modal" data-target="#ProveedorModal" />

                    </div>

                </div>
                <div class="form-group">
                    <div style="display: none;" id="MsjErrorProveedor" class="col-md-offset-2" runat="server">
                        <label class="msjErroneo">Debe elegir un Proveedor</label>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-md-offset-1 col-md-2 alinearDerecha">
                        <div class="BotonIngreso">
                            <asp:Button ID="btnAceptar" class="btn btn-default" runat="server" Text="Aceptar" OnClick="aceptar" />
                        </div>

                    </div>
                    <div class="col-md-3">
                        <div class="BotonIngreso">
                            <asp:Button ID="btnCancelar" class="btn btn-default" runat="server" Text="Cancelar" OnClick="cancelar" />
                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="ProveedorModal" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Datos Proveedor</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label>Nombre</label>
                        <asp:TextBox ID="txtNombreProveedor" runat="server" placeholder="Nombre" class="form-control text-box single-line"></asp:TextBox>

                    </div>
                    <div class="form-group">
                        <label>Correo</label>
                        <asp:TextBox ID="txtCorreo" runat="server" placeholder="ejemplo@correo.com" class="form-control text-box single-line"></asp:TextBox>

                    </div>
                    <div class="form-group">
                        <label>Teléfonos(Separado por comas)</label>
                        <asp:TextBox ID="txtTelefonos" runat="server" placeholder="22222222,8888888" class="form-control text-box single-line"></asp:TextBox>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnAceptarM" runat="server" class="btn btn-default" UseSubmitBehavior="false" Text="Aceptar" OnClick="aceptarProveedor"></asp:Button>
                    <asp:Button ID="btnCancelarM" runat="server" class="btn btn-default" data-dismiss="modal" Text="Cancelar"></asp:Button>
                </div>
            </div>

        </div>
    </div>
    <script type="text/javascript">
        function openModal() {
            $('#ProveedorModal').modal('show');
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
            $("#<%= txtFechaF.ClientID %>").datepicker({
                showmonth: true,
                autoSize: true,
                showAnim: 'slideDown',
                duration: 'fast'
            });


        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_endRequest(function () {
            $("#<%= txtFechaF.ClientID %>").datepicker({
                showmonth: true,
                autoSize: true,
                showAnim: 'slideDown',
                duration: 'fast'
            });


        });

    </script>
</asp:Content>
