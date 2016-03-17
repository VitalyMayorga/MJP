<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Bodegas.aspx.cs" Inherits="SistemaMJP.Bodegas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Manejo de Bodegas</title>
     <link rel="stylesheet" href="Content/reset.css" />
    <%--<link rel="stylesheet" href="Content/style.css">--%>
    <link rel="stylesheet" href="Content/boostrap.css" />
    <link rel="stylesheet" href="Content/Bodegas.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdatePanel runat="server">
    <ContentTemplate>
            <div class="Panel-Bodegas">
               
                <h3 class="titulo">Agregar Bodegas y subBodegas</h3>
                <div class="form-group">
                        <hr/>

                        <h2 class="subtitulo">Bodegas</h2>
                        <div><label class="msjAgregar">Escriba el nombre de la bodega que desea agregar al sistema</label> </div> 
                        
                        <div class="radioGroup"> 
                            <asp:RadioButton id="RbBodegas" runat="server" GroupName="GroupBodegas" OnCheckedChanged="rbEnable" AutoPostBack="true" name="bodega" value="bodega"></asp:RadioButton>
                                                  
                            <asp:TextBox ID="txtBodega" runat="server" placeholder="Bodega"></asp:TextBox>
                            <asp:TextBox ID="txtPrefijo" runat="server" placeholder="Prefijo"></asp:TextBox> 
                            <div style="display: none;" id="MsjErrortextBodega" runat="server">
                                <label class="mensjaeError">Debe ingresar el nombre de la Bodega</label>
                                 <br/> 
                             </div>
                            
                        </div> 
                            <br/>           
                        <hr/>

                        <h2 class="subtitulo">SubBodegas</h2>
                        <div><label class="msjAgregar">Selccione la bodega a la que pertenece la nueva subBodega y escriba dicho nombre para agregarla al sistema</label></div> 
                         
                        <div class="radioGroup"> 
                            <asp:RadioButton id="RbSubBodegas" runat="server" GroupName="GroupBodegas" OnCheckedChanged="rbEnable" AutoPostBack="true" name="subbodega" value="subbodega"></asp:RadioButton>   
                       
                             <asp:DropDownList ID = "ListProgramas" OnSelectedIndexChanged="guardardatosPrograma" autopostback="false" runat="server">                                
                            </asp:DropDownList>
                            <asp:DropDownList ID = "ListBodegas" OnSelectedIndexChanged="guardardatosBodega" autopostback="false" runat="server">                                
                            </asp:DropDownList>

                            <asp:TextBox ID="txtSubBodega" runat="server" placeholder="SubBodega" ></asp:TextBox>
                            <div style="display: none;" id="MsjErrortextSubBodega" runat="server">
                                <label class="mensjaeError">Debe ingresar el nombre de la SubBodega</label>
                                <br/>  
                            </div>
                        </div>
                            
                </div>
                 <br/>
                <hr/>

            </div>
           
            <div class="BotonIncluir">
                <asp:Button ID="btnIncluir" class="btn btn-default" runat="server" Text="Incluir" OnClick="incluir" />
            </div>
    </ContentTemplate>
    </asp:UpdatePanel>    
</asp:Content>
