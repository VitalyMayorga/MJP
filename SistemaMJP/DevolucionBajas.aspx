<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DevolucionBajas.aspx.cs" Inherits="SistemaMJP.DevolucionBajas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Menu devoluciones y bajas</title>    
    <link rel="stylesheet" href="Content/MenuPrincipal.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    
                    <div class="Panel-Princiapl">
                <asp:LinkButton runat="server" OnClick="regresarMP"><i class="glyphicon glyphicon-circle-arrow-left atras" style="font-size: 35px;
    margin-left: 2%;"></i></asp:LinkButton> 
                        <div class ="container">
                              <h1 class="titulo" >Menu Devoluciones y Bajas</h1>
                            <hr/>
                         </div>

                            <div class="form-group">
                                <div class="form-group2">                                    
                                       <div class="LabelMenu"> <label>Realizar Devoluciones</label></div>
                                       <div class="BotonMenu">
                                            <asp:ImageButton id="imagebutton1"  runat="server" Height="147px" Width="154px"
                                               AlternateText="Roles y Oerfiles"
                                               ImageAlign="middle"
                                               ImageUrl="/Images/devoluciones.png"
                                               OnClick="ingresarDevoluciones"/>
                                        </div>                                
                                </div>
                                 <div class="form-group2">
                                        <div class="LabelMenu"><label>Realizar Bajas</label></div>
                                        <div class="BotonMenu"> <asp:ImageButton id="imagebutton2" runat="server" Height="147px" Width="154px"
                                               AlternateText="Bodegas"
                                               ImageAlign="middle"
                                               ImageUrl="/Images/bajas.png"
                                             OnClick="ingresarBajas"/>
                                        </div> 
                                 </div>
                                 </div>
                     </div>  
</asp:Content>