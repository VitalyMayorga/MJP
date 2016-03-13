<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Bodegas.aspx.cs" Inherits="SistemaMJP.Bodegas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Manejo de Bodegas</title>
     <link rel="stylesheet" href="Content/reset.css" />
    <%--<link rel="stylesheet" href="Content/style.css">--%>
    <link rel="stylesheet" href="Content/boostrap.css" />
    <link rel="stylesheet" href="Content/Bodegas.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
             
            <div class="Panel-Bodegas">
               
                <h3 class="titulo">Agregar Bodegas y subBodegas</h3>
                <div class="form-group">
                    <hr/>

                     <h2 class="subtitulo">Bodegas</h2>
                    <div>  <label class="msjAgregar">Escriba el nombre de la bodega que desea agregar al sistema</label> </div> 
                    <br/>
                        <div> 
                            <asp:RadioButton id="RbBodegas" runat="server" GroupName="GroupBodegas" name="bodega" value="bodega"></asp:RadioButton>
                                                  
                            <asp:TextBox ID="txtBodega" runat="server" placeholder="Bodega" class="form-control"></asp:TextBox>
                        </div>
                   
                    <hr/>

                    <h2 class="subtitulo">SubBodegas</h2>
                   <div>  <label class="msjAgregar">Selccione la bodega a la que pertenece la nueva subBodega y escriba dicho nombre para agregarla al sistema</label></div> 
                        <br/>   
                     <div> 
                         <asp:RadioButton id="RbSubBodegas" runat="server" GroupName="GroupBodegas" name="subbodega" value="subbodega"></asp:RadioButton>   
                       
                            <asp:DropDownList ID = "ListBodegas" runat="server">                                
                            </asp:DropDownList>

                        <asp:TextBox ID="txtSubBodega" runat="server" placeholder="SubBodega" class="form-control"></asp:TextBox>
                        </div>

                            
                        </div>
                    <hr/>

                </div>
                
                <div>
                    <div class="BotonIncluir">
                        <asp:Button ID="btnIncluir" runat="server" Text="Incluir" OnClick="incluir" />
                    </div>

                </div>



            </div>

</asp:Content>
