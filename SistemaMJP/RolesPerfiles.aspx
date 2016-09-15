<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RolesPerfiles.aspx.cs" Inherits="SistemaMJP.RolesPerfiles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
     <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="form-horizontal">
                <asp:LinkButton runat="server" OnClick="regresarMA"><i class="glyphicon glyphicon-circle-arrow-left atras" style="font-size: 35px;
    margin-left: 2%;"></i></asp:LinkButton> 
                <h3 class="Encabezado">Roles y Perfiles</h3>
                <div class="form-group">
                    <div class="col-md-offset-1 col-md-2 alinearDerecha">
                        <div class="BotonIngreso">
                            <asp:Button ID="CrearUsuario" class="btn btn-default" runat="server" Text="Crear Usuario" OnClick="crear"/>
                        </div>

                    </div>
                </div>
                <h4 class="Encabezado">Usuarios del Sistema</h4>
                <div class="table-responsive tablaMJP">
                    <asp:GridView ID="GridUsuarios" class="gridsFormat gridDR" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="PageIndexChanging" OnRowCreated="gridUsuario_RowCreated" Width="100%">
                        <Columns>
                            <asp:TemplateField >
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEditar" OnClick="btnEditar_Click" runat="server" class="btn btn-default" ToolTip="Editar"><i class="glyphicon glyphicon-edit"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEliminar" runat="server" class="btn btn-default" OnClick="btnEliminar_Click" ToolTip="Eliminar"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        
                    </asp:GridView>
                </div>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
