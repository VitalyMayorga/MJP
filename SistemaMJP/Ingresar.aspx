<%@ Page Title="Ingresar" Language="C#" AutoEventWireup="true" CodeBehind="Ingresar.aspx.cs" Inherits="SistemaMJP.Ingresar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Inicio de Sesión</title>
     <link rel="stylesheet" href="Content/reset.css">
     <%--<link rel="stylesheet" href="Content/style.css">--%>
    <link rel="stylesheet" href="css/boostrap.css"/>
    <link rel="stylesheet" href="Content/Ingreso.css"/>
    
</head>
<body>
    <section>
        <form id="form1" runat="server">
            <asp:ScriptManager runat="server" />

                    <div class="Panel-Ingreso">
                        <img src="/Images/LOGO_MJP.png" alt="LogoMJP" class="ImagenIngreso"/>
                        <div class ="container">
                        <h3 class="TituloIngreso">Inicio de sesión</h3>
                        <div class="form-group">
                            <label class="col-md-2 control-label">Usuario</label>
                        
                            <div class="col-md-10">
                                <asp:TextBox ID="txtUsuario" runat="server" placeholder="Usuario" class="input-txt"></asp:TextBox>
                            </div>
                        </div>
                        <h5>Contraseña</h5>
                        <asp:TextBox ID="txtContraseña" TextMode="Password" runat="server" placeholder="Contraseña" class="input-txt"></asp:TextBox>
                        <asp:Label ID="Label1" runat="server" Text="Label">Olvidé la contraseña</asp:Label>
                        <asp:Button ID="btnIngresar" class="btn btn-success" runat="server" Text="Iniciar sesión"/>
                        </div>

                    </div>
                           
        </form>
    </section>
</body>
</html>
