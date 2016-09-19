<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RevisionRequisiciones.aspx.cs" Inherits="SistemaMJP.RevisionRequisiciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdatePanel id="PanelGrids" runat="server">
        <ContentTemplate>
            <div class="form-horizontal">
                <asp:LinkButton runat="server" OnClick="regresarMP"><i class="glyphicon glyphicon-circle-arrow-left atras" style="font-size: 35px;
    margin-left: 2%;"></i></asp:LinkButton> 
                <h3 class="Encabezado">Requisiciones Pendientes de Usuario</h3>
                <div class="table-responsive tablaMJP">
                    <asp:GridView ID="GridRequisicion" class="gridsFormat gridF" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="PageIndexChanging" OnRowCreated="gridRequisicion_RowCreated" Width="100%">
                        
                         <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnObservacionAlmacen" runat="server" class="btn btn-default" OnClick="btnVer_Click" ToolTip="Ver Observacion"><i class="glyphicon glyphicon-info-sign" style="color:black"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>                          
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDetallesAlmacen" runat="server" class="btn btn-default" OnClick="btnDetalles_Click" ToolTip="Ver Detalles"><i class="glyphicon glyphicon-edit"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        
                    </asp:GridView>
                </div>

                <div style="display: none;" id="GridDespacho" class="table-responsive tablaMJP" runat="server">
                   <h3 class="Encabezado">Requisiciones Despachadas</h3>
                     <asp:GridView ID="GridRequisicionDespacho" class="gridsFormat gridF" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="PageIndexChangingDespacho" OnRowCreated="gridRequisicion_RowCreated" Width="100%">
                        
                         <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnObservacionDespacho" runat="server" class="btn btn-default" OnClick="btnVer_Click" ToolTip="Ver Observacion"><i class="glyphicon glyphicon-info-sign" style="color:black"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>                          
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDetallesDespacho" runat="server" class="btn btn-default" OnClick="btnDetalles_Click" ToolTip="Ver Detalles"><i class="glyphicon glyphicon-edit"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        
                    </asp:GridView>
                </div>

                <div id="btnReqFinalizadas" runat="server" style="display: none;">
                    <div class="form-group" style="margin-top: 2%; margin-right: 4%;">
                        <div class="row" style="text-align: right;">
                            <asp:Button ID="btnVer" class="btn btn-default" runat="server" Text="Ver Requisiciones Finalizadas" OnClick="verRequisiciones" />
                        </div>
                    </div>               
                </div>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="InfoModal" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Observacion</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label id="observacion"></label>
                        
                    </div>
                    
                </div>
                <div class="modal-footer">                    
                    <asp:Button id="Button1" runat="server" class="btn btn-default" data-dismiss="modal" Text="Cerrar"></asp:Button>
                </div>
            </div>

        </div>
    </div>
    <div id="seguimientoReq" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Requisiciones Finalizadas</h4>
                </div>
                <div class="modal-body">
                    
                    <div class="table-responsive tablaMJP" id="divItems">
                        <asp:GridView ID="trackingGrid" class="gridsFormat2" runat="server" Width="100%">
                        </asp:GridView>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnCancelar" runat="server" class="btn btn-default" data-dismiss="modal" Text="Salir"></asp:Button>
                </div>
            </div>
        </ContentTemplate>
        </asp:UpdatePanel>
        </div>
    </div>
    <script type="text/javascript">       
        function openModalObservacion(value) {
            $('#InfoModal').modal('show');
            document.getElementById('observacion').innerHTML = value;
        }
        function openModal() {
            $('#seguimientoReq').modal('show');
        }
</script>
    
</asp:Content>
