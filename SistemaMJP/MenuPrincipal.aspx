<%@ Page Title="MenuPrincipal" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MenuPrincipal.aspx.cs" Inherits="SistemaMJP.MenuPrincipal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Menu Principal</title>
    <link rel="stylesheet" href="Content/reset.css" />
    <%--<link rel="stylesheet" href="Content/style.css">--%>
    <link rel="stylesheet" href="Content/boostrap.css" />
    <link rel="stylesheet" href="Content/MenuPrincipal.css" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <div class="Panel-Princiapl">

        <div>
            <h1 class="titulo">Menu Principal</h1>
            <hr />
        </div>

        <div class="form-group1">
            <div class="form-group2">
                <div class="LabelMenu">
                    <label class="control-label">Administracion</label></div>
                <div class="BotonMenu">
                    <asp:ImageButton ID="imagebutton1" runat="server" Height="109px" Width="133px"
                        AlternateText="Administracion"
                        ImageAlign="middle"
                        ImageUrl="/Images/admin.png"
                        OnClick="ingresarMenuAdministracion" />
                </div>
                <%--   <asp:Button ID="Button1" class="btn btn-success" runat="server" Height="109px" Width="133px" /> --%>
            </div>
            <div class="form-group2">
                <div class="LabelMenu">
                    <label class="control-label">Ingreso Facturas</label></div>
                <div class="BotonMenu">
                    <asp:ImageButton ID="imagebutton2" runat="server" Height="109px" Width="133px"
                        AlternateText="Facturas"
                        ImageAlign="middle"
                        ImageUrl="/Images/facturas.jpg"
                        OnClick="ingresarFacturas" />
                </div>
            </div>
            <div class="form-group2">
                <div class="LabelMenu">
                    <label class="control-label">Control Activos</label></div>
                <div class="BotonMenu">
                    <asp:ImageButton ID="imagebutton3" runat="server" Height="109px" Width="133px"
                        AlternateText="Activos"
                        ImageAlign="middle"
                        ImageUrl="/Images/check.jpg"
                        OnClick="ingresarControlActivos" />
                </div>
            </div>
        </div>

        <div class="form-group1">
            <div class="form-group2">
                <div class="LabelMenu">
                    <label class="control-label">Requisiciones</label></div>
                <div class="BotonMenu">
                    <asp:ImageButton ID="imagebutton4" runat="server" Height="109px" Width="133px"
                        AlternateText="Requicisiones"
                        ImageAlign="middle"
                        ImageUrl="/Images/requisicion.png"
                        OnClick="ingresarRequisiciones" />
                </div>
            </div>

            <div class="form-group2">
                <div class="LabelMenu">
                    <label class="control-label">Devolucion/Baja
                        <br />
                        Mercaderia</label></div>
                <div class="BotonMenu">
                    <asp:ImageButton ID="imagebutton5"  runat="server" Height="109px" Width="133px"
                        AlternateText="Devoluciones"
                        ImageAlign="middle"
                        ImageUrl="/Images/devolucion.jpg"
                        OnClick="ingresarDevolucionBajas" />
                </div>
            </div>

            <div class="form-group2">
                <div class="LabelMenu">
                    <label class="control-label">Reportes</label></div>
                <div class="BotonMenu">
                    <asp:ImageButton ID="imagebutton6"  runat="server" Height="109px" Width="133px"
                        AlternateText="Reportes"
                        ImageAlign="middle"
                        ImageUrl="/Images/reportes.jpg"
                        OnClick="ingresarReportes" />
                </div>
            </div>
        </div>

        <div class="form-group1">
            <div class="form-group2">
                <div class="LabelMenu">
                    <label class="control-label">Seguimiento Requisiciones</label>
                </div>
                <div class="BotonMenu">
                    <asp:ImageButton ID="imagebutton7" runat="server" Height="109px" Width="133px"
                        AlternateText="Seguimiento"
                        ImageAlign="middle"
                        ImageUrl="/Images/seguimiento.png"
                        OnClick="ingresarSeguimiento" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>


