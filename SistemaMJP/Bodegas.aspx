<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Bodegas.aspx.cs" Inherits="SistemaMJP.Bodegas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Manejo de Bodegas</title>     
    <link rel="stylesheet" href="Content/Bodegas.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdatePanel runat="server">
    <ContentTemplate>
        <div class="form-horizontal">
                <asp:LinkButton runat="server" OnClick="regresarMA"><i class="glyphicon glyphicon-circle-arrow-left atras" style="font-size: 35px;
    margin-left: 2%;"></i></asp:LinkButton> 
               
                <h3 class="Encabezado">Agregar Bodegas y subBodegas</h3>
                
                 <hr/>

                <h2 class="subtitulo">Bodegas</h2>
                <div class="form-group" >
                    <label class="col-md-4 control-label">Escriba el nombre de la bodega que desea agregar al sistema</label>
                </div>    

                <div class="form-group" >
                    <div class="col-md-2">
                        <asp:RadioButton id="RbBodegas" runat="server" GroupName="GroupBodegas" OnCheckedChanged="rbEnable" AutoPostBack="true" name="bodega" value="bodega"></asp:RadioButton>
                     </div> 
                    <div class="col-md-4">                            
                        <asp:TextBox ID="txtBodega" class="form-control text-box single-line" runat="server" placeholder="Bodega"></asp:TextBox>
                    </div>
                    <div style="display: none;" id="MsjErrortextBodega" class="col-md-offset-2" runat="server">
                        <label class="mensajeError">Debe ingresar el nombre de la Bodega</label>
                            <br/> 
                    </div>
                </div>
                            
                <div class="form-group" >
                    <asp:TextBox ID="txtPrefijo"  class="form-control text-box single-line" runat="server" placeholder="Prefijo"></asp:TextBox>                             
                </div>                            
                        
                <br/>           
                <hr/>                

                <h2 class="subtitulo">SubBodegas</h2>
                <div class="form-group">                        
                    <label class="col-md-6 control-label">Selccione la bodega a la que pertenece la nueva subBodega y escriba dicho nombre para agregarla al sistema</label>
                </div>
                                      
                <div class="form-group" >
                    <div class="col-md-10">
                    <asp:RadioButton id="RbSubBodegas" runat="server" GroupName="GroupBodegas" OnCheckedChanged="rbEnable" AutoPostBack="true" name="subbodega" value="subbodega"></asp:RadioButton>   
                       
                        <asp:DropDownList ID = "ListProgramas" class="form-control dropdown cmbsize" runat="server">                                
                    </asp:DropDownList>
                    </div>
                    <div style="display: none;" id="MsjErrorListProgramas" class="col-md-offset-2" runat="server">
                        <label class="mensajeError">Debe elegir un programa</label>
                    </div>
                </div>

                <div class="form-group" >
                    <div class="col-md-10">
                        <asp:DropDownList ID = "ListBodegas"  class="form-control dropdown cmbsize" runat="server">                                
                        </asp:DropDownList>
                    </div>
                    <div style="display: none;" id="MsjErrorListBodegas" class="col-md-offset-2" runat="server">
                        <label class="mensajeError">Debe elegir una Bodega</label>
                    </div>
                </div>

                <div class="form-group" >
                    <div class="col-md-10">
                        <asp:TextBox ID="txtSubBodega" class="form-control text-box single-line" runat="server" placeholder="SubBodega" ></asp:TextBox>     
                    </div>
                        <div style="display: none;" id="MsjErrortextSubBodega" class="col-md-offset-2" runat="server">
                        <label class="mensajeError">Debe ingresar el nombre de la SubBodega</label>
                    </div>                        
                </div>
                                     
                <br/>
                <hr/>                
            
                <div class="form-group">
                    <div class="BotonIncluir">
                        <asp:Button ID="btnIncluir" class="btn btn-default" runat="server" Text="Incluir" OnClick="incluir" />
                    </div>
                </div>               

        </div>
    </ContentTemplate>
    </asp:UpdatePanel>    
</asp:Content>