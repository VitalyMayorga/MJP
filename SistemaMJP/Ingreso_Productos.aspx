﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ingreso_Productos.aspx.cs" Inherits="SistemaMJP.Ingreso_Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="form-horizontal">
                <h3 class="Encabezado">Ingreso de Mercadería</h3>

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
                        <asp:TextBox ID="TextBox1" runat="server" placeholder="Ej: 10" class="form-control text-box single-line"></asp:TextBox>
                    </div>
                    <div style="display: none;" id="Div1" class="col-md-offset-2" runat="server">
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
                    <label class="col-md-2 control-label">Precio Total</label>
                    <div class="col-md-10">
                        <asp:TextBox ID="txtPrecioT" runat="server" class="form-control text-box single-line"></asp:TextBox>

                    </div>
                </div>
                <div class="form-group">

                    <div class="col-md-3 alinearDerecha">
                        <asp:RadioButton ID="esActivo" runat="server" GroupName="tipoP" OnCheckedChanged="rbNo" AutoPostBack="true" />
                        <label class="control-label" for="ingresoF">No es un Activo</label>
                    </div>
                    <div class="col-md-5">
                        <asp:RadioButton ID="noActivo" runat="server" GroupName="tipoP" OnCheckedChanged="rbSi" AutoPostBack="true" />
                        <label class="control-label" for="mercaderiaI">Es un Activo</label>
                    </div>
                </div>
                <h4 class="Encabezado">Datos opcionales</h4>
                <div class="row">
                    <div class='col-sm-6'>
                        <div class="form-group">
                            <label class="col-md-2 control-label">Fecha Vencimiento</label>
                            <div class='input-group date' id='datetimepicker1'>
                                <input type='text' class="form-control" runat="server" />
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </div>
                    </div>
                    <script type="text/javascript">
                        $(function () {
                            $('#datetimepicker1').datetimepicker();
                        });
                    </script>
                </div>
                <div id="formActivo" runat="server">
                    <div class="form-group">
                        <label class="col-md-2 control-label">Número de Activo</label>

                        <div class="col-md-10">
                            <asp:TextBox ID="TextBox3" runat="server" placeholder="Número de Activo" class="form-control text-box single-line"></asp:TextBox>
                        </div>
                        <div style="display: none;" id="MensajeErrorNumActivo" class="col-md-offset-2" runat="server">
                            <label class="msjErroneo">Debe ingresar un número de Activo</label>
                        </div>

                    </div>
                    <div>
                        <label class="col-md-2 control-label">Funcionario Asignado</label>

                        <div class="col-md-10">
                            <asp:TextBox ID="TextBox4" runat="server" placeholder="Funcionario" class="form-control text-box single-line"></asp:TextBox>
                        </div>
                        <div style="display: none;" id="MensajeErrorFuncionario" class="col-md-offset-2" runat="server">
                            <label class="msjErroneo">Debe ingresar un funcionario</label>
                        </div>
                    </div>

                    <div>
                        <label class="col-md-2 control-label">Cédula Funcionario</label>

                        <div class="col-md-10">
                            <asp:TextBox ID="TextBox2" runat="server" placeholder="Cédula" class="form-control text-box single-line"></asp:TextBox>
                        </div>
                        <div style="display: none;" id="MensajeErrorCedula" class="col-md-offset-2" runat="server">
                            <label class="msjErroneo">Debe ingresar una cedula</label>
                        </div>
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
                            <asp:Button ID="btnAyS" class="btn btn-default" runat="server" Text="Aceptar" OnClick="aceptarYSalir" />
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

    
</asp:Content>