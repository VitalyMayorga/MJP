<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Administracion.aspx.cs" Inherits="SistemaMJP.Administracion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <title>Menu Administrador</title>
     <link rel="stylesheet" href="Content/reset.css" />
    <%--<link rel="stylesheet" href="Content/style.css">--%>
    <link rel="stylesheet" href="Content/boostrap.css" />
    <link rel="stylesheet" href="Content/MenuPrincipal.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">      

                    <div class="Panel-Princiapl">
          
                        <div class ="container">
                              <h1 class="titulo" >Menu Administrador</h1>
                            <hr/>
                         </div>

                            <div class="form-group">
                                <div class="form-group2">                                    
                                       <div class="LabelMenu"> <label>Asignar Roles y Perfiles</label></div>
                                       <div class="BotonMenu">
                                            <asp:ImageButton id="imagebutton1"  runat="server" Height="147px" Width="154px"
                                               AlternateText="Roles y Oerfiles"
                                               ImageAlign="middle"
                                               ImageUrl="/Images/rolesperfiles.png"
                                               PostBackUrl="http://localhost:62386/RolesPerfiles"/>
                                        </div>                                
                                </div>
                                 <div class="form-group2">
                                        <div class="LabelMenu"><label>Control de Bodegas</label></div>
                                        <div class="BotonMenu"> <asp:ImageButton id="imagebutton2" runat="server" Height="147px" Width="154px"
                                               AlternateText="Bodegas"
                                               ImageAlign="middle"
                                               ImageUrl="/Images/bodega.png"
                                             PostBackUrl="http://localhost:62386/Bodegas"/>
                                        </div> 
                                 </div>
                                 </div>
                     </div>  
</asp:Content>