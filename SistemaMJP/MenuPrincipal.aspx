<%@ Page Title="Ingresar" Language="C#" AutoEventWireup="true" CodeBehind="MenuPrincipal.aspx.cs" Inherits="SistemaMJP.Ingresar" %>

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
    <section>
        <form id="form1" runat="server">
            <asp:ScriptManager runat="server" />

                    <div class="Panel-Princiapl">
                      <%--  <img src="/Images/LOGO_MJP.png" alt="LogoMJP" class="ImagenIngreso"/> --%>
                        <div class ="container">
                        <h3 class="Titulo">Menu Principal</h3>
                            <div class="form-group">
                                <label class="col-md-2 control-label">Administracion</label>
                                <asp:Button ID="Button1" class="btn btn-success" runat="server" />
                            
                                 <label class="col-md-2 control-label">Ingreso Facturas</label>
                                <asp:Button ID="Button2" class="btn btn-success" runat="server" />

                                 <label class="col-md-2 control-label">Control Activos</label>
                                <asp:Button ID="Button3" class="btn btn-success" runat="server"/>

                                 <label class="col-md-2 control-label">Requisiciones</label>
                                <asp:Button ID="Button4" class="btn btn-success" runat="server" />

                                 <label class="col-md-2 control-label">Devolucion/Baja Mercaderia</label>
                                <asp:Button ID="Button5" class="btn btn-success" runat="server" />

                                 <label class="col-md-2 control-label">Reportes</label>
                                <asp:Button ID="Button6" class="btn btn-success" runat="server" />

                                 <label class="col-md-2 control-label">Seguimiento Requisiciones</label>
                                <asp:Button ID="Button7" class="btn btn-success" runat="server" />
                            </div>
                        
                       
                        
                       
                        </div>

                    </div>
                           
        </form>
    </section>
</body>
</html>
