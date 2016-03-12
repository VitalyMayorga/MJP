<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Administracion.aspx.cs" Inherits="SistemaMJP.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <title>Menu Administrador</title>
     <link rel="stylesheet" href="Content/reset.css"/>
     <%--<link rel="stylesheet" href="Content/style.css">--%>
    <link rel="stylesheet" href="css/boostrap.css"/>
    <link rel="stylesheet" href="Content/MenuPrincipal.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
      <asp:ScriptManager runat="server" />

                    <div class="Panel-Princiapl">
          
                        <div class ="container">
                              <h1 class="titulo" >Menu Administrador</h1>
                            <hr/>
                         </div>

                            <div class="form-group">
                                <div class="form-group2">                                    
                                       <div class="LabelMenu"> <label class="col-md-4 control-label" >Asignar Roles y Perfiles</label></div>
                                       <div class="BotonMenu">
                                            <asp:ImageButton id="imagebutton1" class="btn btn-success" runat="server" Height="147px" Width="154px"
                                               AlternateText="ImageButton 1"
                                               ImageAlign="middle"
                                               ImageUrl="http://bewustcommunicatie.nl/wp-content/uploads/2014/01/advies.png"
                                               PostBackUrl="http://localhost:62386/RolesPerfiles"/>
                                        </div> 
                                         <%--   <asp:Button ID="Button1" class="btn btn-success" runat="server" Height="109px" Width="133px" /> --%>                                 
                                </div>
                                 <div class="form-group2">
                                        <div class="LabelMenu"><label class="col-md-4 control-label" >Control de Bodegas</label></div>
                                        <div class="BotonMenu"> <asp:ImageButton id="imagebutton2" class="btn btn-success" runat="server" Height="147px" Width="154px"
                                               AlternateText="ImageButton 1"
                                               ImageAlign="middle"
                                               ImageUrl="http://www.siga.com.es/siga-sistema-gestion-integral-almacenes-img/modelos_gestion_inventarios_tipos2.png"
                                             PostBackUrl="http://localhost:62386/Bodegas"/>
                                        </div> 
                                 </div>
                                 </div>
                     </div>  
</asp:Content>