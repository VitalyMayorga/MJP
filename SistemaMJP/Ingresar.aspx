<%@ Page Title="Ingresar" Language="C#" AutoEventWireup="true" CodeBehind="Ingresar.aspx.cs" Inherits="SistemaMJP.Ingresar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="Content/css/bootstrap.css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="Content/Ingreso.css" />
    <script src="/Scripts/jquery-1.10.2.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $('#btnIngresar').click(function (event) {
                var usuario = $("#txtUsuario").val();
                var contraseña = $("#txtContraseña").val();
                contraseña = contraseña.replace = (/\s+/g, '');
                usuario = usuario.replace(/\s+/g, '');
                if (usuario == "") {
                    $("#MsjErrorUsuario").css('display', 'block');
                    event.preventDefault();
                }
                else if (contraseña == "") {
                    $("#MsjErrorContraseña").css('display', 'block');
                    event.preventDefault();
                }
                else {
                    $.ajax({
                        type: "POST",
                        url: "Ingresar.aspx/login",
                        data: {
                            Usuario: usuario,
                            Contraseña: contraseña
                        },
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            
                        },
                        failure: function(data){
                            $("#MsjErrorLogin").css('display', 'block');
                            event.preventDefault();
                        }
                    });

                }
                //alert("Funciona la alerta ;D");
            });

        });

    </script>
    <script>
        $("#txtUsuario").keyup(function (event) {
            $("#MsjErrorUsuario").css('display', 'none');
        });

        $("#txtContraseña").keyup(function () {
            $("#MsjErrorContraseña").css('display', 'none');
        });
    </script>
   <%-- <script>
        $("#txtUsuario").keyup(function (event) {
            $("#MsjErrorUsuario").css('display', 'none');
        });

        $("#txtContraseña").keyup(function () {
            $("#MsjErrorContraseña").css('display', 'none');
        });
    </script>--%>
    <title>Inicio de Sesión</title>

</head>
<body>
    <header class="Encabezado"></header>
    <section>
        <form id="form1" runat="server" class="form-horizontal">
            <asp:ScriptManager runat="server" />
            <a href="">
                <div id="alertAlerta" class="alert alert-danger fade in" runat="server" hidden="hidden">
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                    <strong>
                        <asp:Label ID="labelTipoAlerta" runat="server" Text="Alerta! "></asp:Label></strong><asp:Label ID="labelAlerta" runat="server" Text="Mensaje de alerta"></asp:Label>
                </div>
            </a>
            <div class="Panel-Ingreso">
                <img src="/Images/LOGO_MJP.png" alt="LogoMJP" class="ImagenIngreso" />
                <h3 class="Titulo">Inicio de sesión</h3>
                <div class="form-group">
                    <label class="col-md-4 control-label">Usuario</label>

                    <div class="col-md-8">
                        <asp:TextBox ID="txtUsuario" runat="server" placeholder="Usuario" class="form-control"></asp:TextBox>
                    </div>
                </div>
                <div <%--style="display: none;"--%> id="MsjErrorUsuario" class="col-md-offset-4">
                    <p class="msjErroneo">Debe ingresar un usuario</p>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label">Contraseña</label>

                    <div class="col-md-8">
                        <asp:TextBox ID="txtContraseña" TextMode="Password" runat="server" placeholder="Contraseña" class="form-control"></asp:TextBox>
                    </div>
                </div>
                <div style="display: none;" id="MsjErrorContraseña" class="col-md-offset-4">
                    <label class="labelError">Debe ingresar una contraseña</label>
                </div>
                <div class="col-md-offset-5">
                    <a>Olvidé la contraseña</a>
                </div>
                <div style="display: none;" id="MsjErrorLogin" class="col-md-offset-5">
                    <label class="labelError">Usuario/Contraseña inválidos</label>
                </div>
                <div class="col-md-offset-1 col-md-10">
                    <div class="BotonIngreso">
                        <asp:Button ID="btnIngresar" class="btn btn-default" runat="server" Text="Ingresar" />
                    </div>

                </div>



            </div>

        </form>
    </section>
</body>
</html>
