<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Bajas.aspx.cs" Inherits="SistemaMJP.Bajas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Realizar Bajas</title>     
    <link rel="stylesheet" href="Content/Bodegas.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="form-horizontal">
                <asp:LinkButton runat="server" OnClick="regresarDB"><i class="glyphicon glyphicon-circle-arrow-left atras" style="font-size: 35px;
    margin-left: 2%;"></i></asp:LinkButton> 

                <h3 class="Encabezado">Realizar baja de Producto</h3>
                
                 <div class="form-group" id="listPrograma" runat="server">
                    <label class="col-md-2 control-label" >Programa presupuestario: </label>
                    <div class="col-md-10">
                        <asp:DropDownList ID = "DropDownPrograma" class="form-control dropdown cmbsize" OnSelectedIndexChanged="llenarSubBodegas" AutoPostBack="true" runat="server" >                                
                        </asp:DropDownList>
                    </div>
                    <div style="display: none;" id="MsjErrorlistPrograma" class="col-md-offset-2" runat="server">
                        <label class="mensajeError">Debe seleccionar un Programa para realizar la baja</label>
                        <br/> 
                    </div>
                </div>  
                
                <div class="form-group" id="listSubBodegas" runat="server">
                    <label class="col-md-2 control-label" >SubBodega: </label>
                    <div class="col-md-10">
                        <asp:DropDownList ID = "DropDownSubBodegas" class="form-control dropdown cmbsize" runat="server" >                                
                        </asp:DropDownList>
                    </div>
                    <div style="display: none;" id="MsjErrorlistSubBodega" class="col-md-offset-2" runat="server">
                        <label class="mensajeError">Debe seleccionar una subBodega para realizar la baja</label>
                        <br/> 
                    </div>
                </div>                           

                 <div class="form-group" id="listProducto" runat="server">
                    <label class="col-md-2 control-label" >Producto: </label>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtProducto" placeholder="Producto" class="form-control text-box single-line" runat="server"></asp:TextBox>
                    </div>
                     <div class="col-md-3">
                        <asp:Button ID="btnEmpaques" class="btn btn-default" runat="server" Text="Actualizar" OnClick="llenarCantidades"/>
                   </div>
                          <div style="display: none;" id="MsjErrortextProducto" class="col-md-offset-2" runat="server">
                        <label class="mensajeError">Debe escribir el producto para realizar la baja</label>
                        <br/> 
                    </div>
                </div>

                <div class="form-group" id="listEmpaques" runat="server">
                    <label class="col-md-2 control-label" >Cantidad por Empaque: </label>
                    <div class="col-md-10">
                        <asp:DropDownList ID = "DropDownEmpaques" class="form-control dropdown cmbsize" AutoPostBack="true" runat="server" >                                
                        </asp:DropDownList>
                    </div>
                    <div style="display: none;" id="MsjErrorlistEmpaques" class="col-md-offset-2" runat="server">
                        <label class="mensajeError">Debe seleccionar la cantidad de productos que vienen por empaque</label>
                        <br/> 
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-2 control-label" >Cantidad de Unidades: </label>
                    <div class="col-md-10">
                        <asp:TextBox ID="TextCantidad" class="form-control text-box single-line" runat="server" placeholder="Cantidad"></asp:TextBox>
                    </div>
                    <div style="display: none;" id="MsjErrortextCantidad" class="col-md-offset-2" runat="server">
                        <label class="mensajeError">Debe ingresar la cantidad de unidades para realizar la baja</label>
                        <br/> 
                    </div>
                    <div style="display: none;" id="MsjErrorCantidadNegativa" class="col-md-offset-2" runat="server">
                        <label class="mensajeError">La cantidad introducidad es mayor que la cantidad del producto en la bodega </label>
                        <br/> 
                    </div>
                    
                </div>

                <div class="form-group" >
                    <label class="col-md-2 control-label" >Justificacion: </label>
                    <div class="col-md-10">
                        <asp:TextBox ID="txtJustificacion" class="form-control text-box single-line" runat="server" placeholder="Justificacion"></asp:TextBox>           
                    </div>
                    <div style="display: none;" id="MsjErrortextJustificacion" class="col-md-offset-2" runat="server">
                        <label class="mensajeError">Ingrese una justificacion para realizar la baja</label>
                        <br/> 
                    </div>
                   
                </div>

                <div class="form-group">
                    <div class="RealizarBaja" style="margin-left: 25%;">
                        <asp:Button ID="btnBajar" class="btn btn-default" runat="server" Text="Aceptar" OnClick="bajar" />
                    </div>
                </div>

             </div>    
        </ContentTemplate>
    </asp:UpdatePanel>
     <script type="text/javascript">
         $(document).ready(function () {
             $("[id$=<%= txtProducto.ClientID %>]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Bajas.aspx/getProductosBodegaProgramaSubBodega") %>',
                        data: "{ 'prefix': '" + request.term + "', 'programa': '" + document.getElementById('BodyContent_DropDownPrograma').value + "', 'subBodega': '" + document.getElementById('BodyContent_DropDownSubBodegas').value + "'}",
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
            $("[id$=<%= txtProducto.ClientID %>]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Bajas.aspx/getProductosBodegaProgramaSubBodega") %>',
                        data: "{ 'prefix': '" + request.term + "', 'programa': '" + document.getElementById('BodyContent_DropDownPrograma').value + "', 'subBodega': '" + document.getElementById('BodyContent_DropDownSubBodegas').value + "'}",
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

</asp:Content>