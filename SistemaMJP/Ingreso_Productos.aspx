<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ingreso_Productos.aspx.cs" Inherits="SistemaMJP.Ingreso_Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="form-horizontal">
                <h3 class="Encabezado">Ingreso de Mercadería</h3>

                <div class="form-group">
                    <label class="col-md-2 control-label">SubPartida</label>

                    <div class="col-md-10">
                        <asp:DropDownList id="ListaSubPartidas" runat="server" class="form-control dropdown cmbsize" OnSelectedIndexChanged="revisarSubPartida" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div style="display: none;" id="MsjErrorSubPartida" class="col-md-offset-2" runat="server">
                        <label class="msjErroneo">Debe elegir una subpartida</label>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-2 control-label">Descripción del Artículo</label>
                    <div class="col-md-10">
                        <asp:TextBox ID="txtDescripcion" runat="server" class="form-control text-box single-line"></asp:TextBox>

                    </div>
                    <div style="display: none;" id="MsjErrorDescripcion" class="col-md-offset-2" runat="server">
                        <label class="msjErroneo">Debe dar una descripción</label>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-2 control-label">Presentación empaque</label>

                    <div class="col-md-10">
                        <asp:TextBox ID="txtPresentacion" runat="server" placeholder="Ej: Cajas" class="form-control text-box single-line"></asp:TextBox>
                    </div>
                    <div style="display: none;" id="MsjErrorPresentacion" class="col-md-offset-2" runat="server">
                        <label class="msjErroneo">Debe escribir una presentación del empaque</label>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-2 control-label">Cantidad por empaque</label>

                    <div class="col-md-10">
                        <asp:TextBox ID="txtCantidadE" runat="server" placeholder="Ej: 10" class="form-control text-box single-line"></asp:TextBox>
                    </div>
                    <div style="display: none;" id="MsjErrorCantEmp" class="col-md-offset-2" runat="server">
                        <label class="msjErroneo">Debe ingresar la cantidad por empaque</label>
                    </div>

                </div>

                <div class="form-group">
                    <label class="col-md-2 control-label">Cantidad total(Unidades)</label>

                    <div class="col-md-10">
                        <asp:TextBox ID="txtCantidadT" runat="server" placeholder="Ej: 10" class="form-control text-box single-line"></asp:TextBox>
                    </div>
                    <div style="display: none;" id="MsjErrorCantidad" class="col-md-offset-2" runat="server">
                        <label class="msjErroneo">Debe ingresar una cantidad</label>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-2 control-label">Precio Unitario</label>
                    <div class="col-md-10">
                        <asp:TextBox ID="txtPrecioT" runat="server" class="form-control text-box single-line"></asp:TextBox>

                    </div>
                    <div style="display: none;" id="MsjErrorPrecio" class="col-md-offset-2" runat="server">
                        <label class="msjErroneo">Debe ingresar el precio Unitario</label>
                    </div>
                </div>
                <div class="form-group">

                    <div class="col-md-3 alinearDerecha">
                        <asp:RadioButton ID="noActivo" runat="server" GroupName="tipoP" OnCheckedChanged="rbNo" AutoPostBack="true" />
                        <label class="control-label" for="noActivo">No es un Activo</label>
                    </div>
                    <div class="col-md-5">
                        <asp:RadioButton ID="esActivo" runat="server" GroupName="tipoP" OnCheckedChanged="rbSi" AutoPostBack="true" />
                        <label class="control-label" for="esActivo">Es un Activo</label>
                    </div>
                </div>
                <h4 class="Encabezado">Datos opcionales</h4>

                <div class="form-group">
                    <label class="col-md-2 control-label">Fecha Vencimiento</label>
                    <div class='input-group date col-md-10' style="max-width: 320px">

                        <asp:TextBox type='text' ID="txtFechaV" class="form-control" runat="server"></asp:TextBox>


                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-2 control-label">Fecha Caducidad</label>
                    <div class='input-group date col-md-10' style="max-width: 320px">

                        <asp:TextBox type='text' ID="txtFechaC" class="form-control" runat="server"></asp:TextBox>


                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-2 control-label">Fecha Garantia</label>
                    <div class='input-group date col-md-10' style="max-width: 320px">

                        <asp:TextBox type='text' ID="txtFechaG" class="form-control" runat="server"></asp:TextBox>


                    </div>


                </div>
                <div id="formActivo" runat="server" style="display: none">
                    <div class="form-group">
                        <label class="col-md-2 control-label">Número de Activo</label>

                        <div class="col-md-10">
                            <asp:TextBox ID="txtNumActivo" runat="server" placeholder="Número de Activo" class="form-control text-box single-line"></asp:TextBox>
                        </div>
                        <div style="display: none;" id="MsjErrorNumActivo" class="col-md-offset-2" runat="server">
                            <label class="msjErroneo">Debe ingresar un número de Activo</label>
                        </div>

                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">Funcionario Asignado</label>

                        <div class="col-md-10">
                            <asp:TextBox ID="txtFuncionario" runat="server" placeholder="Funcionario" class="form-control text-box single-line"></asp:TextBox>
                        </div>
                        <div style="display: none;" id="MsjErrorFuncionario" class="col-md-offset-2" runat="server">
                            <label class="msjErroneo">Debe ingresar un funcionario</label>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-2 control-label">Cédula Funcionario</label>

                        <div class="col-md-10">
                            <asp:TextBox ID="txtCedula" runat="server" placeholder="Cédula" class="form-control text-box single-line"></asp:TextBox>
                        </div>
                        <div style="display: none;" id="MsjErrorCedula" class="col-md-offset-2" runat="server">
                            <label class="msjErroneo">Debe ingresar una cedula</label>
                        </div>
                    </div>
                </div>
                <div class="form-group" style="margin-top: 2%; margin-right: 4%;">
                    <div class="row" style="text-align: right;">
                            <asp:Button ID="btnAceptar" class="btn btn-default" runat="server" Text="Aceptar" OnClick="aceptar" />
                            <asp:Button ID="btnAyS" class="btn btn-default" runat="server" Text="Aceptar y Salir" OnClick="aceptarYSalir" style="margin-left:2%"/>
                            <asp:Button ID="btnCancelar" class="btn btn-default" runat="server" Text="Cancelar" OnClick="cancelar" style="margin-left:2%" />

                    </div>

                </div>
                
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnAceptar" EventName="Click"/>
        </Triggers>
    </asp:UpdatePanel>

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
            $("#<%= txtFechaV.ClientID %>").datepicker({
                showmonth: true,
                autoSize: true,
                showAnim: 'slideDown',
                duration: 'fast'
            });

            $("#<%= txtFechaC.ClientID %>").datepicker({
                showmonth: true,
                autoSize: true,
                showAnim: 'slideDown',
                duration: 'fast'
            });

            $("#<%= txtFechaG.ClientID %>").datepicker({
                showmonth: true,
                autoSize: true,
                showAnim: 'slideDown',
                duration: 'fast'
            });

        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_endRequest(function () {
            $("#<%= txtFechaV.ClientID %>").datepicker({
                showmonth: true,
                autoSize: true,
                showAnim: 'slideDown',
                duration: 'fast'
            });
            $("#<%= txtFechaC.ClientID %>").datepicker({
                showmonth: true,
                autoSize: true,
                showAnim: 'slideDown',
                duration: 'fast'
            });

            $("#<%= txtFechaG.ClientID %>").datepicker({
                showmonth: true,
                autoSize: true,
                showAnim: 'slideDown',
                duration: 'fast'
            });

        });

    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("[id$=<%= txtDescripcion.ClientID %>]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Ingreso_Productos.aspx/getProductos") %>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('-')[0],
                                    val: item.split('-')[1]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },

                minLength: 1
            });
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_endRequest(function () {
            $("[id$=<%= txtDescripcion.ClientID %>]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Ingreso_Productos.aspx/getProductos") %>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('-')[0],
                                    val: item.split('-')[1]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },

                minLength: 1
            });
        });

    </script>

    <script>
        $(document).ready(function () {
            $("#<%= txtDescripcion.ClientID %>").keydown(function () {
                $("#<%= MsjErrorDescripcion.ClientID %>").css('display', 'none');

            });

            $("#<%= txtPresentacion.ClientID %>").keydown(function () {
                $("#<%= MsjErrorPresentacion.ClientID %>").css('display', 'none');

            });

            $("#<%= txtCantidadE.ClientID %>").keydown(function () {
                $("#<%= MsjErrorCantEmp.ClientID %>").css('display', 'none');

            });

            $("#<%= txtCantidadT.ClientID %>").keydown(function () {
                $("#<%= MsjErrorCantidad.ClientID %>").css('display', 'none');

            });

            $("#<%= txtPrecioT.ClientID %>").keydown(function () {
                $("#<%= MsjErrorPrecio.ClientID %>").css('display', 'none');

            });
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_endRequest(function () {
            $("#<%= txtDescripcion.ClientID %>").keydown(function () {
                $("#<%= MsjErrorDescripcion.ClientID %>").css('display', 'none');

            });

            $("#<%= txtPresentacion.ClientID %>").keydown(function () {
                $("#<%= MsjErrorPresentacion.ClientID %>").css('display', 'none');

            });

            $("#<%= txtCantidadE.ClientID %>").keydown(function () {
                $("#<%= MsjErrorCantEmp.ClientID %>").css('display', 'none');

            });

            $("#<%= txtCantidadT.ClientID %>").keydown(function () {
                $("#<%= MsjErrorCantidad.ClientID %>").css('display', 'none');

            });

            $("#<%= txtPrecioT.ClientID %>").keydown(function () {
                $("#<%= MsjErrorPrecio.ClientID %>").css('display', 'none');

            });
        });
    </script>
</asp:Content>
