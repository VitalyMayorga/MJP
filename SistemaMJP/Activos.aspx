﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Activos.aspx.cs" Inherits="SistemaMJP.Activos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="form-horizontal">
                <h3 class="Encabezado">Ingreso de Activo</h3>

                <div class="form-group">
                    <label class="col-md-2 control-label">Activo</label>

                    <div class="col-md-10">
                        <asp:DropDownList id="ListaActivos" runat="server" class="form-control dropdown cmbsize" OnSelectedIndexChanged="revisarActivo" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div style="display: none;" id="MsjErrorActivo" class="col-md-offset-2" runat="server">
                        <label class="msjErroneo">Debe elegir un Activo</label>
                    </div>
                </div>

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

                <div class="form-group">
                    <label class="col-md-2 control-label">Documento</label>

                    <div class="col-md-10">
                        <asp:TextBox ID="txtDocumento" runat="server" placeholder="Documento" class="form-control text-box single-line"></asp:TextBox>
                    </div>
                    <div style="display: none;" id="MsjErrorDocumento" class="col-md-offset-2" runat="server">
                        <label class="msjErroneo">Debe ingresar un documento</label>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-2 control-label">Fecha Documento</label>
                    <div class='input-group date col-md-10' style="max-width: 320px">

                        <asp:TextBox type='text' ID="txtFecha" class="form-control" runat="server"></asp:TextBox>


                    </div>
                    <div style="display: none;" id="MsjErrorFecha" class="col-md-offset-2" runat="server">
                        <label class="msjErroneo">Debe ingresar una fecha</label>
                    </div>
                </div>
                 <h4 class="Encabezado">Opcional</h4>
                <div class="form-group">
                <label class="col-md-2 control-label">Requisición</label>
                    <div class="col-md-10">
                        <asp:DropDownList id="ListaRequisiciones" runat="server" class="form-control dropdown cmbsize" AutoPostBack="true"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group" style="margin-top: 2%; margin-right: 4%;">
                    <div class="row" style="text-align: right;">
                            <asp:Button ID="btnAceptar" class="btn btn-default" runat="server" Text="Aceptar" OnClick="aceptar" />
                            <asp:Button ID="btnCancelar" class="btn btn-default" runat="server" style="margin-left:2%" Text="Cancelar" OnClick="cancelar" />

                    </div>

                </div>
            </div>
        </ContentTemplate>
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
