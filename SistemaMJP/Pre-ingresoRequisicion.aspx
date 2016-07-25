<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pre-ingresoRequisicion.aspx.cs" Inherits="SistemaMJP.Pre_ingresoRequisicion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="form-horizontal">
                <h3 class="Encabezado">Módulo de Requisiciones</h3>
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
                    <label class="col-md-2 control-label">SubBodega</label>
                    <div class="col-md-10">
                        <asp:DropDownList ID="ListaSubBodegas" runat="server" class="form-control dropdown cmbsize" OnSelectedIndexChanged="revisarSubB" AutoPostBack="true"></asp:DropDownList>

                    </div>
                    <div style="display: none;" id="MsjErrorSubBodega" class="col-md-offset-2" runat="server">
                        <label class="msjErroneo">Debe ingresar una subBodega</label>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-2 control-label">Unidad Solicitante</label>
                    <div class="col-md-10">
                        <asp:DropDownList ID="ListaUnidadSol" runat="server" class="form-control dropdown cmbsize" OnSelectedIndexChanged="revisarUnidad" AutoPostBack="true"></asp:DropDownList>

                    </div>
                    <div style="display: none;" id="MsjErrorUnidad" class="col-md-offset-2" runat="server">
                        <label class="msjErroneo">Debe elegir una unidad solicitante</label>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-2 control-label">Destino/label>

                    <div class="col-md-10">
                        <asp:TextBox ID="txtDestino" runat="server" mode="Multiline" placeholder="Destino" class="form-control text-box multi-line"></asp:TextBox>
                    </div>
                    <div style="display: none;" id="MsjErrorDestino" class="col-md-offset-2" runat="server">
                        <label class="msjErroneo">Debe ingresar un destino</label>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-2 control-label">Observaciones/label>

                    <div class="col-md-10">
                        <asp:TextBox ID="observaciones" runat="server" mode="Multiline" placeholder="Destino" class="form-control text-box multi-line"></asp:TextBox>
                    </div>                    
                </div>
                
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>    
</asp:Content>
