<%@ Page Title="MenuPrincipal" Language="C#" AutoEventWireup="true" CodeBehind="MenuPrincipal.aspx.cs" Inherits="SistemaMJP.MenuPrincipal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Menu Principal</title>
     <link rel="stylesheet" href="Content/reset.css">
     <%--<link rel="stylesheet" href="Content/style.css">--%>
    <link rel="stylesheet" href="css/boostrap.css"/>
    <link rel="stylesheet" href="Content/MenuPrincipal.css" />
</head>
<body>
     <header style="background-color:blue;"></header>
    <section>
        <form id="form1" runat="server">
            <asp:ScriptManager runat="server" />

                    <div class="Panel-Princiapl">
          
                        <div class ="container">
                              <h1 class="Titulo">Menu Principal</h1>
                         </div>

                            <div class="form-group">
                                <div class="form-group2">                                    
                                       <div class="LabelMenu"> <label class="col-md-4 control-label" >Administracion</label></div>
                                       <div class="BotonMenu">
                                            <asp:ImageButton id="imagebutton1" class="btn btn-success" runat="server" Height="109px" Width="133px"
                                               AlternateText="ImageButton 1"
                                               ImageAlign="middle"
                                               ImageUrl="http://botanicalgardens.penang.gov.my/images/images/adun.png"
                                               PostBackUrl="http://localhost:62386/Administracion"/>
                                        </div> 
                                         <%--   <asp:Button ID="Button1" class="btn btn-success" runat="server" Height="109px" Width="133px" /> --%>                                 
                                </div>
                                 <div class="form-group2">
                                        <div class="LabelMenu"><label class="col-md-4 control-label" >Ingreso Facturas</label></div>
                                        <div class="BotonMenu"> <asp:ImageButton id="imagebutton2" class="btn btn-success" runat="server" Height="109px" Width="133px"
                                               AlternateText="ImageButton 1"
                                               ImageAlign="middle"
                                               ImageUrl="http://blog.facturalegal.com/wp-content/uploads/2011/11/talonario1.jpg"
                                             PostBackUrl="http://localhost:62386/IngresoFacturas"/>
                                        </div> 
                                 </div>
                                 <div class="form-group2">
                                       <div class="LabelMenu"> <label class="col-md-4 control-label" >Control Activos</label></div>
                                       <div class="BotonMenu">
                                           <asp:ImageButton id="imagebutton3" class="btn btn-success" runat="server" Height="109px" Width="133px"
                                               AlternateText="ImageButton 1"
                                               ImageAlign="middle"
                                               ImageUrl="http://4.bp.blogspot.com/-zHl3ZnPfUew/Vl3jK5-2L6I/AAAAAAAAEUc/dcg5GKwlm5Y/s200/The-Parts-of-a-Usability-Test-Plan.jpg"
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
                                               ImageUrl="http://site.b2bservicios.com/imagenes/REQUISICION.png"
                                                 PostBackUrl="http://localhost:62386/Requisiciones"/> </div>
                                        </div>

                                       <div class="form-group2">
                                           <div class="LabelMenu">  <label class="col-md-4 control-label" >Devolucion/Baja <br /> Mercaderia</label></div>
                                           <div class="BotonMenu"> <asp:ImageButton id="imagebutton5" class="btn btn-success" runat="server" Height="109px" Width="133px"
                                               AlternateText="ImageButton 1"
                                               ImageAlign="middle"
                                               ImageUrl="http://www.todojuguete.es/imagenes/b2b/post-venta.jpg"
                                                PostBackUrl="http://localhost:62386/DevolucionBajas"/></div>
                                       </div>

                                       <div class="form-group2">
                                           <div class="LabelMenu"> <label class="col-md-4 control-label" >Reportes</label></div>
                                           <div class="BotonMenu"><asp:ImageButton id="imagebutton6" class="btn btn-success" runat="server" Height="109px" Width="133px"
                                               AlternateText="ImageButton 1"
                                               ImageAlign="middle"
                                               ImageUrl="https://vencoasesores.files.wordpress.com/2014/11/reporte-comercial-imagen.jpg"
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
                                               ImageUrl="http://osmoscloud.com/img/home_components/home_0003s_0005s_0000_graphic_7.png"
                                               PostBackUrl="http://localhost:62386/Seguimiento"/>
                                         </div>
                                     </div>
                                </div>
                     </div>                          
        </form>
    </section>
</body>
</html>
