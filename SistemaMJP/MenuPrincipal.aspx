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
                                       <div class="BotonMenu"> <asp:Button ID="Button1" class="btn btn-success" runat="server" Height="109px" Width="133px" /> </div>
                                 </div>
                                 <div class="form-group2">
                                        <div class="LabelMenu"><label class="col-md-4 control-label" >Ingreso Facturas</label></div>
                                        <div class="BotonMenu"> <asp:Button ID="Button2" class="btn btn-success" runat="server" Height="109px" Width="133px" />   </div> 
                                 </div>
                                 <div class="form-group2">
                                       <div class="LabelMenu"> <label class="col-md-4 control-label" >Control Activos</label></div>
                                       <div class="BotonMenu">  <asp:Button ID="Button3" class="btn btn-success" runat="server" Height="109px" Width="133px" /></div>
                                 </div>   
                            </div>

                                <div class="form-group">                                    
                                        <div class="form-group2">
                                            <div class="LabelMenu"> <label class="col-md-4 control-label" >Requisiciones</label></div>
                                            <div class="BotonMenu"> <asp:Button ID="Button4" class="btn btn-success" runat="server" Height="109px" Width="133px" /> </div>
                                        </div>

                                       <div class="form-group2">
                                           <div class="LabelMenu">  <label class="col-md-4 control-label" >Devolucion/Baja <br /> Mercaderia</label></div>
                                           <div class="BotonMenu"> <asp:Button ID="Button5" class="btn btn-success" runat="server" Height="109px" Width="133px" /></div>
                                       </div>

                                       <div class="form-group2">
                                           <div class="LabelMenu"> <label class="col-md-4 control-label" >Reportes</label></div>
                                           <div class="BotonMenu"><asp:Button ID="Button6" class="btn btn-success" runat="server" Height="109px" Width="133px" /></div>
                                       </div>                                                                          
                               </div>
                                    
                                 <div class="form-group">
                                     <div class="form-group2">
                                         <div class="LabelMenu">
                                            <label class="col-md-4 control-label">Seguimiento Requisiciones</label>
                                         </div>
                                         <div class="BotonMenu">
                                            <asp:Button ID="Button7" class="btn btn-success" runat="server" Height="109px" Width="133px" />
                                         </div>
                                     </div>
                                </div>
                     </div>                          
        </form>
    </section>
</body>
</html>
