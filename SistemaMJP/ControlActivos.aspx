<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ControlActivos.aspx.cs" Inherits="SistemaMJP.ControlActivos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="form-horizontal">
                <asp:LinkButton runat="server" OnClick="regresarMP"><i class="glyphicon glyphicon-circle-arrow-left atras" style="font-size: 35px;
    margin-left: 2%;"></i></asp:LinkButton> 
                <h3 class="Encabezado">Control de activos</h3>
                <div class="form-group">
                    <div class="col-md-1" style="margin-left:3%;">
                        <div class="BotonIngreso">
                            <asp:Button ID="IngresarActivo" class="btn btn-default" runat="server" Text="Ingresar Activo" OnClick="ingresar" />
                        </div>

                    </div>
                </div>
                <h4 class="Encabezado">Activos de la bodega</h4>
                <div class="table-responsive tablaMJP">
                    <asp:GridView ID="GridActivos" class="gridsFormat gridActivos" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="PageIndexChanging" OnRowCreated="gridActivos_RowCreated" Width="100%">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnModificar" runat="server" class="btn btn-default" OnClick="btnEditar_Click" ToolTip="Editar"><i class="glyphicon glyphicon-edit"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEliminar" runat="server" class="btn btn-default" OnClick="btnEliminar_Click" ToolTip="Eliminar"><i class="glyphicon glyphicon-remove"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        
                    </asp:GridView>
                </div>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="EliminarModal" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Confirmar Eliminado</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label id="productoEliminar"></label>
                        
                    </div>
                    
                </div>
                <div class="modal-footer">
                    <asp:Button id="btnAceptarM" runat="server" class="btn btn-default" UseSubmitBehavior="false" Text="Aceptar" OnClick="aceptarEliminado"></asp:Button>
                    <asp:Button id="btnCancelarM" runat="server" class="btn btn-default" data-dismiss="modal" Text="Cancelar"></asp:Button>
                </div>
            </div>

        </div>
    </div>
    <script type="text/javascript">
        function openModal(value) {
            $('#EliminarModal').modal('show');
            document.getElementById('productoEliminar').innerHTML = value;
        }
</script>
</asp:Content>
