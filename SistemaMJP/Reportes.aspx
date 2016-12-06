<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reportes.aspx.cs" Inherits="SistemaMJP.Reportes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <link rel="stylesheet" href="Content/Reporte.css"/>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server"
     UpdateMode="Conditional">
        <ContentTemplate>
            <div class="form-horizontal">
                <asp:LinkButton runat="server" OnClick="regresarMP"><i class="glyphicon glyphicon-circle-arrow-left atras" style="font-size: 35px;
    margin-left: 2%;"></i></asp:LinkButton> 

                <h3 class="Encabezado">Reportes</h3>
                <div class="form-group" style="display: none;" id="rTodos" runat="server">
                    <label class="col-md-2 control-label" >Reportes: </label>
                    <div class="col-md-10">
                        <asp:DropDownList ID = "ListaReporte1" class="form-control dropdown cmbsize" OnSelectedIndexChanged="mostrarReporte" AutoPostBack="true" runat="server">                                
                            <asp:ListItem Value="0">Seleccione un Reporte</asp:ListItem>
                            <asp:ListItem Value="1">Reporte de existencias</asp:ListItem>
                            <asp:ListItem Value="2">Reporte de articulos general</asp:ListItem>
                            <asp:ListItem Value="3">Reporte de articulos por destino y subPartida</asp:ListItem>
                            <asp:ListItem Value="4">Reporte de trazabilidad</asp:ListItem>
                            <asp:ListItem Value="5">Reporte de requisiciones</asp:ListItem>
                        </asp:DropDownList>
                    </div>                    
                </div>

                <div class="form-group" style="display: none;" id="rAlgunos" runat="server">
                    <label class="col-md-2 control-label" >Reportes: </label>
                    <div class="col-md-10">
                        <asp:DropDownList ID = "ListaReporte2" class="form-control dropdown cmbsize" OnSelectedIndexChanged="mostrarReporte2" AutoPostBack="true" runat="server">                                
                            <asp:ListItem Value="0">Seleccione un Reporte</asp:ListItem>                          
                            <asp:ListItem Value="5">Reporte de requisiciones</asp:ListItem>
                        </asp:DropDownList>
                    </div>                    
                </div>

                <div class="form-group" id="DropProgramas" runat="server">
                    <label class="col-md-2 control-label" >Programas: </label>
                    <div class="col-md-10">
                        <asp:DropDownList ID = "ListProgramas" class="form-control dropdown cmbsize" AutoPostBack="true" runat="server">                                
                            <asp:ListItem Value="0">Seleccione un programa</asp:ListItem>
                        </asp:DropDownList>
                    </div>                    
                </div>

                 <div class="form-group" style="display: none;" id="rExistencias" runat="server">                    
                   <div class="BotonAgregar">
                        <asp:Button ID="Btn_reporteExistencia" class="btn btn-default botonAgregar" runat="server" Text="Generar Reporte" OnClick="existencia" />
                    </div>                     
                </div>

                <div class="form-group" style="display: none;" id="rGeneral" runat="server">
                   <div class="BotonAgregar">
                        <asp:Button ID="Btn_articulosGeneral" class="btn btn-default botonAgregar" runat="server" Text="Generar Reporte" OnClick="general" />
                   </div>   
                </div>

                 <div class="form-group" style="display: none;" id="rDestinoSubPartida1" runat="server">
                    <label class="col-md-2 control-label" >Destino: </label>
                   <div class="col-md-10">
                        <asp:DropDownList ID = "ListDestino1" class="form-control dropdown cmbsize" AutoPostBack="true" runat="server">                                
                        </asp:DropDownList>
                    </div>
                    <div style="display: none;" id="MsjErrorDestino1" class="col-md-offset-2" runat="server">
                        <label class="mensajeError">Debe seleccionar un destino</label>
                        <br/> 
                    </div>
                  </div>

                <div class="form-group" style="display: none;" id="rDestinoSubPartida2" runat="server">
                    <label class="col-md-2 control-label" >SubPartida: </label>
                     <div class="col-md-10">
                        <asp:DropDownList ID = "ListSubPartida1" class="form-control dropdown cmbsize spaceb" AutoPostBack="true" runat="server">                                
                        </asp:DropDownList>
                    </div>
                    <div style="display: none;" id="MsjErrorSubPartida1" class="col-md-offset-2" runat="server">
                        <label class="mensajeError">Debe seleccionar una SubPartida</label>
                        <br/> 
                    </div>
                     <div class="BotonAgregar">
                        <asp:Button ID="Btn_articulosDestinoSubpartida" class="btn btn-default botonAgregar" runat="server" Text="Generar Reporte" OnClick="destinoSubPartida" />
                    </div>                    
                </div>

                 <div class="form-group" style="display: none;" id="rTrazabilidad1" runat="server">
                    <label class="col-md-2 control-label" >SubPartida: </label>
                   <div class="col-md-10">
                        <asp:DropDownList ID = "ListSubPartida2" class="form-control dropdown cmbsize" AutoPostBack="true" runat="server">                                
                        </asp:DropDownList>
                    </div>
                    <div style="display: none;" id="MsjErrorSubPartida2" class="col-md-offset-2" runat="server">
                        <label class="mensajeError">Debe seleccionar una SubPartida</label>
                        <br/> 
                    </div>
                     </div>

                     <div class="form-group" style="display: none;" id="rTrazabilidad2" runat="server">
                     <label class="col-md-2 control-label" >Periodo: </label>
                     <div class="col-md-10">
                        <asp:DropDownList ID = "ListPeriodo" class="form-control dropdown cmbsize spaceb" AutoPostBack="true" runat="server">                                
                            <asp:ListItem Value="12">12</asp:ListItem>
                            <asp:ListItem Value="24">24</asp:ListItem>
                            <asp:ListItem Value="36">36</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div style="display: none;" id="MsjErrorPeriodo" class="col-md-offset-2" runat="server">
                        <label class="mensajeError">Debe seleccionar un Periodo</label>
                        <br/> 
                    </div>
                     <div class="BotonAgregar">
                        <asp:Button ID="Btn_trazabilidad" class="btn btn-default botonAgregar" runat="server" Text="Generar Reporte" OnClick="trazabilidad" />
                    </div>                    
                </div>

                <div class="form-group" style="display: none;" id="rRequisicion" runat="server">
                    <label class="col-md-2 control-label" >Destino: </label>
                   <div class="col-md-10">
                        <asp:DropDownList ID = "ListDestino2" class="form-control dropdown cmbsize spaceb" AutoPostBack="true" runat="server">                                
                        </asp:DropDownList>
                    </div>
                    <div style="display: none;" id="MsjErrordestino2" class="col-md-offset-2" runat="server">
                        <label class="mensajeError">Debe seleccionar un Destino</label>
                        <br/> 
                    </div>
                     <div class="BotonAgregar">
                        <asp:Button ID="Btn_requisicion" class="btn btn-default botonAgregar" runat="server" Text="Generar Reporte" OnClick="requisiciones" />
                    </div>                    
                </div>

             </div>    
        </ContentTemplate>
      
    </asp:UpdatePanel>
</asp:Content>