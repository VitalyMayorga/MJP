<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Restablecer_contraseña.aspx.cs" Inherits="SistemaMJP.Restablecer_contraseña" %>

<!DOCTYPE html>

<html lang="en">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" type="text/css" />

    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>
    <webopt:BundleReference runat="server" Path="~/Content/css" />
    <link rel="stylesheet" href="Content/Master.css" />

    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.1/themes/base/jquery-ui.css" />
    <script src="http://code.jquery.com/jquery-1.10.2.js"></script>
    <script src="http://code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
    <script>

        $(document).ready(function () {
            $("#txtContraseña").keydown(function () {
                $("#MsjErrorContraseña").css('display', 'none');
            });

            $("#txtContraseña2").keydown(function () {
                $("#MsjErrorDifContraseña").css('display', 'none');
                $("#MsjErrorNoContraseña").css('display', 'none');
            });
        });
    </script>
</head>
<body>
    <form runat="server">
        <asp:ScriptManager runat="server">
            <Scripts>
                <%--To learn more about bundling scripts in ScriptManager see http://go.microsoft.com/fwlink/?LinkID=301884 --%>
                <%--Framework Scripts--%>
                <asp:ScriptReference Name="MsAjaxBundle" />
                <%--<asp:ScriptReference Name="jquery" />--%>
                <asp:ScriptReference Name="bootstrap" />
                <asp:ScriptReference Name="respond" />
                <asp:ScriptReference Name="WebForms.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebForms.js" />
                <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebUIValidation.js" />
                <asp:ScriptReference Name="MenuStandards.js" Assembly="System.Web" Path="~/Scripts/WebForms/MenuStandards.js" />
                <asp:ScriptReference Name="GridView.js" Assembly="System.Web" Path="~/Scripts/WebForms/GridView.js" />
                <asp:ScriptReference Name="DetailsView.js" Assembly="System.Web" Path="~/Scripts/WebForms/DetailsView.js" />
                <asp:ScriptReference Name="TreeView.js" Assembly="System.Web" Path="~/Scripts/WebForms/TreeView.js" />
                <asp:ScriptReference Name="WebParts.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebParts.js" />
                <asp:ScriptReference Name="Focus.js" Assembly="System.Web" Path="~/Scripts/WebForms/Focus.js" />
                <asp:ScriptReference Name="WebFormsBundle" />
                <%--Site Scripts--%>
            </Scripts>
        </asp:ScriptManager>
        <header>
            <div class="content-wrapper">
                <div class="logo">
                    <img src="/Images/LOGO_MJP.png" style="width: 100px" title="Ministerio de Justicia y Paz" />
                </div>

                <div>
                    <p style="font-size: 30px">MINISTERIO DE JUSTICIA Y PAZ</p>
                    <p style="font-size: 20px">República de Costa Rica</p>
                </div>

            </div>

        </header>
        <div class="form-horizontal">
            <h3 class="Encabezado">Restablecer contraseña</h3>
            <div class="form-group">
                <label class="col-md-2 control-label">Contraseña</label>

                <div class="col-md-8">
                    <asp:TextBox ID="txtContraseña" TextMode="Password" runat="server" placeholder="Contraseña" class="form-control"></asp:TextBox>
                </div>
            </div>

            <div style="display: none;" id="MsjErrorContraseña" class="col-md-offset-2" runat="server">
                <label class="msjErroneo">Debe ingresar una contraseña</label>
            </div>

            <div class="form-group">
                <label class="col-md-2 control-label">Repetir Contraseña</label>
                <div class="col-md-8">
                    <asp:TextBox ID="txtContraseña2" TextMode="Password" runat="server" placeholder="Repetir Contraseña" class="form-control"></asp:TextBox>
                </div>
            </div>
            <div style="display: none;" id="MsjErrorDifContraseña" class="col-md-offset-2" runat="server">
                <label class="msjErroneo">Contraseñas no coinciden</label>
            </div>
            <div style="display: none;" id="MsjErrorNoContraseña" class="col-md-offset-2" runat="server">
                <label class="msjErroneo">Debe repetir la contraseña</label>
            </div>
            <div style="display: none;" id="MsjErrorTamContraseña" class="col-md-offset-2" runat="server">
                <label class="msjErroneo">Contraseña debe tener un mínimo de 5 caracteres</label>
            </div>
            <div class="col-md-offset-2 col-md-8">
                <div class="BotonIngreso">
                    <asp:Button ID="btnIngresar" class="btn btn-default" runat="server" Text="Enviar" OnClick="restablecer" />
                </div>

            </div>
        </div>
    </form>
</body>
</html>

