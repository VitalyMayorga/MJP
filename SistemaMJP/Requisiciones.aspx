<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Requisiciones.aspx.cs" Inherits="SistemaMJP.Requisiciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="form-horizontal">
                <asp:LinkButton runat="server" OnClick="regresarMP"><i class="glyphicon glyphicon-circle-arrow-left atras" style="font-size: 35px;
    margin-left: 2%;"></i></asp:LinkButton>
                <h3 class="Encabezado">Requisiciones</h3>
                <div class="form-group">
                    <div class="col-md-1" style="margin-left: 3%;">
                        <div class="BotonIngreso">
                            <asp:Button ID="IngresarRequisicion" class="btn btn-default" runat="server" Text="Nueva Requisición" OnClick="ingresar" />
                        </div>

                    </div>
                </div>
                <h4 class="Encabezado">Requisiciones del Usuario</h4>
                <div class="table-responsive tablaMJP">
                    <asp:GridView ID="GridRequisiciones" class="gridsFormat gridPF" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="PageIndexChanging" OnRowCreated="gridRequisiciones_RowCreated" Width="100%">
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
                                    <asp:LinkButton ID="btnVer" runat="server" class="btn btn-default" OnClick="btnVer_Click" ToolTip="Historial"><i class="glyphicon glyphicon-list-alt"></i></asp:LinkButton>
                              
                                         </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="seguirTrack" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Tracking de la Requisicón</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label id="numeroRequisicion"></label>
                    </div>
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
        function openModal(numero) {
            document.getElementById('numeroRequisicion').innerHTML = numero;

            $('#seguirTrack').modal('show');
        }
    </script>
</asp:Content>
