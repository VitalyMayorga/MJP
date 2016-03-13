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

        <div class="form-group">
            <div class="form-group2">
                <div class="LabelMenu">
                    <label class="control-label">Administracion</label></div>
                <div class="BotonMenu">
                    <asp:ImageButton ID="imagebutton1" runat="server" Height="109px" Width="133px"
                        AlternateText="Administracion"
                        ImageAlign="middle"
                        ImageUrl="/Images/admin.png"
                        PostBackUrl="http://localhost:62386/Administracion" />
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
                        PostBackUrl="http://localhost:62386/ControlActivos" />
                </div>
            </div>
        </div>

        <div class="form-group">
            <div class="form-group2">
                <div class="LabelMenu">
                    <label class="control-label">Requisiciones</label></div>
                <div class="BotonMenu">
                    <asp:ImageButton ID="imagebutton4" runat="server" Height="109px" Width="133px"
                        AlternateText="Requicisiones"
                        ImageAlign="middle"
                        ImageUrl="/Images/requisicion.png"
                        PostBackUrl="http://localhost:62386/Requisiciones" />
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
                        PostBackUrl="http://localhost:62386/DevolucionBajas" />
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
                        PostBackUrl="http://localhost:62386/Reportes" />
                </div>
            </div>
        </div>

        <div class="form-group">
            <div class="form-group2">
                <div class="LabelMenu">
                    <label class="control-label">Seguimiento Requisiciones</label>
                </div>
                <div class="BotonMenu">
                    <asp:ImageButton ID="imagebutton7" runat="server" Height="109px" Width="133px"
                        AlternateText="Seguimiento"
                        ImageAlign="middle"
                        ImageUrl="/Images/seguimiento.png"
                        PostBackUrl="http://localhost:62386/Seguimiento" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>


