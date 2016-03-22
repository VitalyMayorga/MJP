<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CrearUsuario.aspx.cs" Inherits="SistemaMJP.CrearUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Content/RolesyPerfiles.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
     <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="form-horizontal">
                <asp:LinkButton runat="server" OnClick="regresarRP"><i class="glyphicon glyphicon-circle-arrow-left atras" style="font-size: 35px;
    margin-left: 2%;"></i></asp:LinkButton> 

                <h3 class="Encabezado">Agregar Usuario</h3>
                <div class="form-group">
                    <label class="col-md-2 control-label" >Nombre: </label>
                    <div class="col-md-10">
                        <asp:TextBox ID="txtNombre" class="form-control text-box single-line" runat="server" placeholder="Nombre de Usuario"></asp:TextBox>
                    </div>
                    <div style="display: none;" id="MsjErrortextNombre" class="col-md-offset-2" runat="server">
                        <label class="mensjaeError">Debe ingresar el Nombre del Usuario</label>
                        <br/> 
                    </div>
                    
                </div>

                <div class="form-group">
                    <label class="col-md-2 control-label" >Apellidos: </label>
                    <div class="col-md-10">
                        <asp:TextBox ID="TextApellidos" class="form-control text-box single-line" runat="server" placeholder="Apellidos del usuario"></asp:TextBox>
                    </div>
                    <div style="display: none;" id="MsjErrortextApellidos" class="col-md-offset-2" runat="server">
                        <label class="mensjaeError">Debe ingresar los Apellidos del Usuario</label>
                        <br/> 
                    </div>
                    
                </div>

                <div class="form-group" >
                    <label class="col-md-2 control-label" >Correo Institucional: </label>
                    <div class="col-md-10">
                        <asp:TextBox ID="txtCorreo" class="form-control text-box single-line" runat="server" placeholder="Correo Institucional"></asp:TextBox>           
                    </div>
                    <div style="display: none;" id="MsjErrortextCorreo" class="col-md-offset-2" runat="server">
                        <label class="mensjaeError">Debe ingresar el correo Institucional</label>
                        <br/> 
                    </div>
                   
                </div>
                
                <div class="form-group" >
                    <label class="col-md-2 control-label" >Rol: </label>
                    <div class="col-md-10">
                        <asp:DropDownList ID = "ListRoles" class="form-control dropdown cmbsize" runat="server">                                
                        </asp:DropDownList>
                    </div>
                </div>

                 <div class="form-group" >
                     
                    <label class="col-md-4 control-label" >Programas Presupuestarios Disponibles: </label>
                    <label class="col-md-4 control-label" >Programas Presupuestarios Asignados: </label>                          
                        
                 </div>

                <div class="form-group">

                     <div class="col-md-3" style="margin-left:9%;">
                        <asp:ListBox id="ListBoxProgramasAsignados" Width="300px" runat="server" class="form-control item-center cmbsize" SelectionMode="Single">
                        </asp:ListBox>                              
                    </div>  

                    <div class="col-md-1">
                        <div class="form-group style-flecha">
                    <asp:LinkButton runat="server" OnClick="asignar"><i class="glyphicon glyphicon-chevron-right"></i></asp:LinkButton>
                         </div>  
                        <div class="form-group style-flecha">
                    <asp:LinkButton runat="server" OnClick="desasignar"><i class="glyphicon glyphicon-chevron-left"></i></asp:LinkButton> 
                        </div> 
                         </div> 
                    <div class="col-md-3">
                        <asp:ListBox id="ListBoxProgramas" Width="300px" runat="server" class="form-control item-center cmbsize" SelectionMode="Single">
                        </asp:ListBox>
                    </div> 
                                   
                </div>
                

             </div>    
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
