<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RevisionBajas.aspx.cs" Inherits="SistemaMJP.RevisionBajas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server"
     UpdateMode="Conditional">
        <ContentTemplate>
            <div class="form-horizontal">
                <asp:LinkButton runat="server" OnClick="regresarMP"><i class="glyphicon glyphicon-circle-arrow-left atras" style="font-size: 35px;
    margin-left: 2%;"></i></asp:LinkButton> 
                <h3 class="Encabezado">Bajas de Productos</h3>
                <div class="table-responsive tablaMJP">
                      
                    <asp:GridView ID="GridBajas" class="gridsFormat gridF" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="PageIndexChanging" OnRowCreated="gridBajas_RowCreated" Width="100%">
                       <Columns>
                             <asp:TemplateField >
                                <ItemTemplate>
                                  <asp:UpdatePanel ID="updateA" runat="server">
                                    <ContentTemplate>
                                        <asp:LinkButton ID="btnAceptar"  OnClick="aceptar" runat="server" class="btn btn-default" ToolTip="Aceptar Baja"><i class="glyphicon glyphicon-ok"></i></asp:LinkButton>
                                    </ContentTemplate>
                                     <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnAceptar" EventName="Click"/>
                                    </Triggers>
                                  </asp:UpdatePanel>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:UpdatePanel ID="updateR" runat="server">
                                        <ContentTemplate>
                                            <asp:LinkButton ID="btnRechazar"  OnClick="rechazar" runat="server" class="btn btn-default" ToolTip="Rechazar baja"><i class="glyphicon glyphicon-remove"></i></asp:LinkButton>
                                        </ContentTemplate>
                                         <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnRechazar" EventName="Click"/>
                                        </Triggers>
                                  </asp:UpdatePanel>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        
                    </asp:GridView>
                                  
                </div>

            </div>
        </ContentTemplate>          
    </asp:UpdatePanel>
</asp:Content>
