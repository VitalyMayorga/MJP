<%@ Page Title="MenuPrincipal" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MenuPrincipal.aspx.cs" Inherits="SistemaMJP.MenuPrincipal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Menu Principal</title>
     <link rel="stylesheet" href="Content/reset.css"/>
     <%--<link rel="stylesheet" href="Content/style.css">--%>
    <link rel="stylesheet" href="css/boostrap.css"/>
    <link rel="stylesheet" href="Content/MenuPrincipal.css" />
</asp:Content>


            <asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
            <asp:ScriptManager runat="server" />

                    <div class="Panel-Princiapl">
          
                        <div class ="container">
                              <h1 class="titulo" >Menu Principal</h1>
                            <hr/>
                         </div>

                            <div class="form-group">
                                <div class="form-group2">                                    
                                       <div class="LabelMenu"> <label class="col-md-4 control-label" >Administracion</label></div>
                                       <div class="BotonMenu">
                                            <asp:ImageButton id="imagebutton1" class="btn btn-success" runat="server" Height="109px" Width="133px"
                                               AlternateText="ImageButton 1"
                                               ImageAlign="middle"
                                               ImageUrl="/Images/admin.png"
                                               PostBackUrl="http://localhost:62386/Administracion"/>
                                        </div> 
                                         <%--   <asp:Button ID="Button1" class="btn btn-success" runat="server" Height="109px" Width="133px" /> --%>                                 
                                </div>
                                 <div class="form-group2">
                                        <div class="LabelMenu"><label class="col-md-4 control-label" >Ingreso Facturas</label></div>
                                        <div class="BotonMenu"> <asp:ImageButton id="imagebutton2" class="btn btn-success" runat="server" Height="109px" Width="133px"
                                               AlternateText="ImageButton 1"
                                               ImageAlign="middle"
                                               ImageUrl="/Images/facturas.jpg"
                                             PostBackUrl="http://localhost:62386/IngresoFacturas"/>
                                        </div> 
                                 </div>
                                 <div class="form-group2">
                                       <div class="LabelMenu"> <label class="col-md-4 control-label" >Control Activos</label></div>
                                       <div class="BotonMenu">
                                           <asp:ImageButton id="imagebutton3" class="btn btn-success" runat="server" Height="109px" Width="133px"
                                               AlternateText="ImageButton 1"
                                               ImageAlign="middle"
                                               ImageUrl="/Images/check.jpg"
                                                PostBackUrl="http://localhost:62386/ControlActivos"/>
                                       </div>
                                 </div>   
                            </div>

                                <div class="form-group">                                    
                                        <div class="form-group2">
                                            <div class="LabelMenu"> <label class="col-md-4 control-label" >Requisiciones</label></div>
                                            <div class="BotonMenu"> <asp:ImageButton id="imagebutton4" class="btn btn-success" runat="server" Height="109px" Width="133px"
                                               AlternateText="ImageButton 1"
                                               ImageAlign="middle"
                                               ImageUrl="/Images/requisicion.png"
                                                 PostBackUrl="http://localhost:62386/Requisiciones"/> </div>
                                        </div>

                                       <div class="form-group2">
                                           <div class="LabelMenu">  <label class="col-md-4 control-label" >Devolucion/Baja <br /> Mercaderia</label></div>
                                           <div class="BotonMenu"> <asp:ImageButton id="imagebutton5" class="btn btn-success" runat="server" Height="109px" Width="133px"
                                               AlternateText="ImageButton 1"
                                               ImageAlign="middle"
                                               ImageUrl="/Images/devolucion.jpg"
                                                PostBackUrl="http://localhost:62386/DevolucionBajas"/></div>
                                       </div>

                                       <div class="form-group2">
                                           <div class="LabelMenu"> <label class="col-md-4 control-label" >Reportes</label></div>
                                           <div class="BotonMenu"><asp:ImageButton id="imagebutton6" class="btn btn-success" runat="server" Height="109px" Width="133px"
                                               AlternateText="ImageButton 1"
                                               ImageAlign="middle"
                                               ImageUrl="/Images/reportes.jpg"
                                                PostBackUrl="http://localhost:62386/Reportes"/></div>
                                       </div>                                                                          
                               </div>
                                    
                                 <div class="form-group">
                                     <div class="form-group2">
                                         <div class="LabelMenu">
                                            <label class="col-md-4 control-label">Seguimiento Requisiciones</label>
                                         </div>
                                         <div class="BotonMenu">
                                            <asp:ImageButton id="imagebutton7" class="btn btn-success" runat="server" Height="109px" Width="133px"
                                               AlternateText="ImageButton 1"
                                               ImageAlign="middle"
                                               ImageUrl="/Images/seguimiento.png"
                                               PostBackUrl="http://localhost:62386/Seguimiento"/>
                                         </div>
                                     </div>
                                </div>
                     </div>    
            </asp:Content>                      

