<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Preingreso-Productos.aspx.cs" Inherits="SistemaMJP.Preingreso_Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="form-horizontal">
                <h3 class="Encabezado">Ingreso de Mercadería</h3>
                <div class="form-group">
                    <label class="col-md-2 control-label">Ubicación</label>
                    <div class="col-md-10">
                        <select id="ListaBodegas" runat="server" class="form-control dropdown cmbsize"></select>

                    </div>
                    <div style="display: none;" id="MsjErrorBodega" class="col-md-offset-2" runat="server">
                        <label class="msjErroneo">Debe elegir una bodega</label>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-2 control-label">Programa Presupuestario</label>
                    <div class="col-md-10">
                        <asp:DropDownList id="ListaProgramas" runat="server" class="form-control dropdown cmbsize" OnSelectedIndexChanged="revisarPrograma" autopostback="true"></asp:DropDownList>

                    </div>
                    <div style="display: none;" id="MsjErrorPrograma" class="col-md-offset-2" runat="server">
                        <label class="msjErroneo">Debe elegir un programa</label>
                    </div>
                </div>
                <div class="form-group" id="Subbodega" style="display: none;" runat="server">
                    <label class="col-md-2 control-label">Departamento</label>
                    <div class="col-md-10">
                        <select id="ListaSubBodegas" runat="server" class="form-control dropdown cmbsize"></select>

                    </div>
                    <div style="display: none;" id="MsjErrorSubBodega" class="col-md-offset-2" runat="server">
                        <label class="msjErroneo">Debe ingresar un departamento</label>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-2 control-label">SubPartida</label>

                    <div class="col-md-10">
                        <select id="ListaSubPartidas" runat="server" class="form-control dropdown cmbsize"></select>
                    </div>
                    <div style="display: none;" id="MsjErrorSubPartida" class="col-md-offset-2" runat="server">
                        <label class="msjErroneo">Debe elegir una subpartida</label>
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

</asp:Content>
