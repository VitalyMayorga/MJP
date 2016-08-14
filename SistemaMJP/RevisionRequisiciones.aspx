<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RevisionRequisiciones.aspx.cs" Inherits="SistemaMJP.RevisionRequisiciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdatePanel runat="server">
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
                                    <asp:LinkButton ID="btnObservacion" runat="server" class="btn btn-default" OnClick="btnVer_Click" ToolTip="Ver Observacion"><i class="glyphicon glyphicon-info-sign" style="color:black"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>                          
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDetalles" runat="server" class="btn btn-default" OnClick="btnDetalles_Click" ToolTip="Ver Detalles"><i class="glyphicon glyphicon-edit"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        
                    </asp:GridView>
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
    <script type="text/javascript">       
        function openModalObservacion(value) {
            $('#InfoModal').modal('show');
            document.getElementById('observacion').innerHTML = value;
        }
</script>
    
</asp:Content>
