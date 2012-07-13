<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Gestión de Archivos
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <form action="/Manage/Upload" method="post" enctype="multipart/form-data"/>
    <label>Filename:<input type ="file" name="file"/></label>
    <input type="submit" value="Guardar"/>
    
    <%: Html.ActionLink("Descarga de documentos", "DownloadFile", "Manage")%>

</asp:Content>
