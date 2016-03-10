<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Preingreso-Productos.aspx.cs" Inherits="SistemaMJP.Preingreso_Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <script>
        $(document).ready(function () {
            $("#txtSubPartida").keydown(function () {
                $("#MsjErrorSubPartida").css('display', 'none');
                
            });            
        });
    </script>
    <div class="Content">
        <h3 class="Encabezado">Ingreso de Mercadería</h3>
        <div class="form-group">
            <label class="col-md-4 control-label">Ubicación</label>
            <div class="col-md-8">
                <asp:DropDownList ID="ListaBodegas" runat="server" class="form-control"></asp:DropDownList>

            </div>
            <div style="display: none;" id="MsjErrorBodega" class="col-md-offset-4" runat="server">
                <label class="msjErroneo">Debe elegir una bodega</label>
            </div>
        </div>
        <div class="form-group">
            <label class="col-md-4 control-label">Programa Presupuestario</label>
            <div class="col-md-8">
                <asp:DropDownList ID="ListaProgramas" runat="server" class="form-control"></asp:DropDownList>

            </div>
            <div style="display: none;" id="MsjsErrorPrograma" class="col-md-offset-4" runat="server">
                <label class="msjErroneo">Debe elegir un programa</label>
            </div>
        </div>
        <div class="form-group" id="Subbodega" style="display: none;" runat="server">
            <label class="col-md-4 control-label">Departamento</label>
            <div class="col-md-8">
                <asp:DropDownList ID="ListaSubBodegas" runat="server" class="form-control"></asp:DropDownList>

            </div>
            <div style="display: none;" id="MsjErrorSubBodega" class="col-md-offset-4" runat="server">
                <label class="msjErroneo">Debe ingresar un departamento</label>
            </div>
        </div>
        <div class="form-group">
            <label class="col-md-4 control-label">SubPartida</label>

            <div class="col-md-8">
                <asp:TextBox ID="txtSubPartida" runat="server" placeholder="Subpartida" class="form-control"></asp:TextBox>
            </div>
            <div style="display: none;" id="MsjErrorSubPartida" class="col-md-offset-4" runat="server">
                <label class="msjErroneo">Debe ingresar una subpartida</label>
            </div>
        </div>

        <div class="form-group">

            <div class="col-md-8">
                Ingreso de Factura<input id="IngresoFactura" runat="server" name="Tipo" type="radio" />
                Mercadería Inicial<input id="MercaderiaInicial" runat="server" name="Tipo" type="radio" />
            </div>

        </div>

        <div class="col-md-offset-1 col-md-10">
            <div class="BotonIngreso">
                <asp:Button ID="btnAceptar" class="btn btn-default" runat="server" Text="Aceptar" OnClick="aceptar" />
            </div>

        </div>

    </div>
</asp:Content>
