<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditarUsuario.aspx.cs" Inherits="SistemaMJP.EditarUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Content/RolesyPerfiles.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server"
     UpdateMode="Conditional">
        <ContentTemplate>
            <div class="form-horizontal">
                <asp:LinkButton runat="server" OnClick="regresarRP"><i class="glyphicon glyphicon-circle-arrow-left atras" style="font-size: 35px;
    margin-left: 2%;"></i></asp:LinkButton> 

                <h3 class="Encabezado">Editar Usuario</h3>                
                
                <div class="form-group" >
                    <label class="col-md-2 control-label" >Rol: </label>
                    <div class="col-md-10">
                        <asp:DropDownList ID = "ListRoles" class="form-control dropdown cmbsize" OnSelectedIndexChanged="mostrarListBox" AutoPostBack="true" runat="server">                                
                        </asp:DropDownList>
                    </div>
                    <div style="display: none;" id="MsjErrorListRol" class="col-md-offset-2" runat="server">
                        <label class="mensajeError">Debe seleccionar un Rol para el usuario</label>
                        <br/> 
                    </div>
                </div>

                 <div class="form-group" style="display: none;" id="labelPrograma" runat="server">
                     
                    <label class="col-md-4 control-label" >Programas Presupuestarios Disponibles: </label>
                    <label class="col-md-4 control-label" >Programas Presupuestarios Asignados: </label>                          
                        
                 </div>

                <div class="form-group" style="display: none;" id="listBoxPrograma" runat="server">

                     <div class="col-md-3" style="margin-left:9%;">
                        <asp:ListBox id="ListBoxProgramasDisponibles" Width="300px" runat="server" class="form-control item-center cmbsize" SelectionMode="Single">
                        </asp:ListBox>                              
                    </div>  

                    <div class="col-md-1">
                        <div class="form-group style-flecha">
                            <asp:LinkButton ID="asignarPP" runat="server" OnClick="asignarProgramas"><i class="glyphicon glyphicon-chevron-right"></i></asp:LinkButton>
                        </div>  
                        <div class="form-group style-flecha">
                            <asp:LinkButton ID="desAsignarPP" runat="server" OnClick="desasignarProgramas"><i class="glyphicon glyphicon-chevron-left"></i></asp:LinkButton> 
                        </div> 
                    </div> 

                    <div class="col-md-3">
                        <asp:ListBox id="ListBoxProgramasAsignados" Width="300px" runat="server" class="form-control item-center cmbsize" SelectionMode="Single">
                        </asp:ListBox>
                    </div>                    
                                   
                 </div>  
                <div style="display: none; margin-left:25%;" id="MsjErrorListBoxPrograma" class="form-group col-md-offset-2" runat="server">
                    <label class="mensajeError">Debe asignar al menos un Programa al Usuario</label>
                    <br/> 
                </div>              
                 
                <div class="form-group" style="display: none;" id="listBodega" runat="server">
                    <label class="col-md-2 control-label" >Bodega: </label>
                    <div class="col-md-10">
                        <asp:DropDownList ID = "ListBodegas" class="form-control dropdown cmbsize" runat="server" >                                
                        </asp:DropDownList>
                    </div>
                    <div style="display: none;" id="MsjErrorlistBodega" class="col-md-offset-2" runat="server">
                        <label class="mensajeError">Debe seleccionar una bodega para el usuario</label>
                        <br/> 
                    </div>
                </div>
                
                <div class="form-group" style="display: none;" id="labelBodegas" runat="server">
                     
                    <label class="col-md-3 control-label" style="margin-left:4%;" >Bodegas Disponibles: </label>
                    <label class="col-md-4 control-label" >Bodegas Asignados: </label>                          
                        
                 </div>

                <div class="form-group" style="display: none;" id="listBoxBodegas" runat="server">

                     <div class="col-md-3" style="margin-left:9%;">
                        <asp:ListBox id="ListBoxBodegasDisponibles" Width="300px" runat="server" class="form-control item-center cmbsize" SelectionMode="Single">
                        </asp:ListBox>                              
                    </div>  

                    <div class="col-md-1">
                        <div class="form-group style-flecha">
                            <asp:LinkButton ID="asignarB" runat="server" OnClick="asignarBodegas"><i class="glyphicon glyphicon-chevron-right"></i></asp:LinkButton>
                        </div>  
                        <div class="form-group style-flecha">
                            <asp:LinkButton ID="desAsignarB" runat="server" OnClick="desasignarBodegas"><i class="glyphicon glyphicon-chevron-left"></i></asp:LinkButton> 
                        </div> 
                    </div> 

                    <div class="col-md-3">
                        <asp:ListBox id="ListBoxBodegasAsignadas" Width="300px" runat="server" class="form-control item-center cmbsize" SelectionMode="Single">
                        </asp:ListBox>
                    </div> 
                            
                </div>
                <div style="display: none; margin-left:25%;" id="MsjErrorListBoxBodegas" class="form-group col-md-offset-2" runat="server">
                    <label class="mensajeError">Debe asignar al menos una Bodega al Usuario</label>
                    <br/> 
                 </div>
                           

                 <div class="form-group" style="display: none;" id="labelSubBodegas" runat="server">
                     
                    <label class="col-md-3 control-label" style="margin-left:4%;" >SubBodegas Disponibles: </label>
                    <label class="col-md-4 control-label" >Subbodegas Asignados: </label>                          
                        
                 </div>
                 
                <div class="form-group" style="display: none;" id="listBoxSubBodegas" runat="server">

                     <div class="col-md-3" style="margin-left:9%;">
                        <asp:ListBox id="ListBoxSubBodegasDisponibles" Width="300px" runat="server" class="form-control item-center cmbsize" SelectionMode="Single">
                        </asp:ListBox>                              
                    </div>  

                    <div class="col-md-1">
                        <div class="form-group style-flecha">
                            <asp:LinkButton ID="asignarSB" runat="server" OnClick="asignarSubBodegas"><i class="glyphicon glyphicon-chevron-right"></i></asp:LinkButton>
                        </div>  
                        <div class="form-group style-flecha">
                            <asp:LinkButton ID="desAsignarSB" runat="server" OnClick="desasignarSubBodegas"><i class="glyphicon glyphicon-chevron-left"></i></asp:LinkButton> 
                        </div> 
                     </div> 

                    <div class="col-md-3">
                        <asp:ListBox id="ListBoxSubBodegasAsignadas" Width="300px" runat="server" class="form-control item-center cmbsize" SelectionMode="Single">
                        </asp:ListBox>
                    </div>                     
                                   
                </div>
                <div style="display: none; margin-left:25%;" id="MsjErrorListBoxSubBodega" class="form-group col-md-offset-2" runat="server">
                    <label class="mensajeError">Debe asignar al menos una SubBodega al Usuario</label>
                    <br/> 
                </div>

                <div class="form-group">
                    <div class="BotonAgregar">
                        <asp:Button ID="btnEditar" class="btn btn-default" runat="server" Text="Editar Usuario" OnClick="editar" />
                    </div>
                </div>

             </div>    
        </ContentTemplate>
         <Triggers>
            <asp:AsyncPostBackTrigger ControlID="asignarPP" EventName="Click"/>
        </Triggers>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="desAsignarPP" EventName="Click"/>
        </Triggers>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="asignarB" EventName="Click"/>
        </Triggers>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="desAsignarB" EventName="Click"/>
        </Triggers>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="asignarSB" EventName="Click"/>
        </Triggers>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="desAsignarSB" EventName="Click"/>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
