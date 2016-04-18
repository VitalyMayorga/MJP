<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PerfilUsuario.aspx.cs" Inherits="SistemaMJP.PerfilUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="form-horizontal">
                <asp:LinkButton runat="server" OnClick="regresarMP"><i class="glyphicon glyphicon-circle-arrow-left atras" style="font-size: 35px;
    margin-left: 2%;"></i></asp:LinkButton> 

                <h3 class="Encabezado">Editar Informacion</h3>
                <div class="form-group">
                    <label class="col-md-2 control-label" >Nombre: </label>
                    <div class="col-md-10">
                        <asp:TextBox ID="txtNombre" class="form-control text-box single-line" runat="server" ></asp:TextBox>
                    </div>
                    <div style="display: none;" id="MsjErrortextNombre" class="col-md-offset-2" runat="server">
                        <label class="mensajeError">Este campo no puede estar vacio</label>
                        <br/> 
                    </div>
                    
                </div>

                <div class="form-group">
                    <label class="col-md-2 control-label" >Apellidos: </label>
                    <div class="col-md-10">
                        <asp:TextBox ID="TextApellidos" class="form-control text-box single-line" runat="server" ></asp:TextBox>
                    </div>
                    <div style="display: none;" id="MsjErrortextApellidos" class="col-md-offset-2" runat="server">
                        <label class="mensajeError">Este campo no puede estar vacio</label>
                        <br/> 
                    </div>
                    
                </div>

                <div class="form-group" >
                    <label class="col-md-2 control-label" >Contraseña: </label>
                    <div class="col-md-10">
                        <asp:TextBox ID="txtPassword" TextMode="password" class="form-control text-box single-line" runat="server" ></asp:TextBox>           
                    </div>
                    <div style="display: none;" id="MsjErrortextPassword" class="col-md-offset-2" runat="server">
                        <label class="mensajeError">Ingrese una contraseña valida</label>
                        <br/> 
                    </div>
                   
                </div>

                <div class="form-group" >
                    <label class="col-md-2 control-label" >Reescriba la contraseña: </label>
                    <div class="col-md-10">
                        <asp:TextBox ID="txtPassword2" TextMode="password" class="form-control text-box single-line" runat="server" ></asp:TextBox>           
                    </div>
                    <div style="display: none;" id="MsjErrortextRevisarPassword" class="col-md-offset-2" runat="server">
                        <label class="mensajeError">El texto ingresado no coincide con el anterior, favor revisar la contraseña</label>
                        <br/> 
                    </div>
                   
                </div>

                <div class="form-group">
                    <div class="BotonAgregar">
                        <asp:Button ID="EditarInfo" class="btn btn-default" runat="server" Text="Editar Informacion" OnClick="editarInfo" />
                    </div>
                </div>

             </div>    
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
