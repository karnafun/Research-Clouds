<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ScholarDemo.aspx.cs" Inherits="assets_aspx_ScholarDemo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <asp:Image ID="img" runat="server" />
        <br />
        <asp:Label ID="lbl_name" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="lbl_interests" runat="server" Text="Label"></asp:Label>
        <br />
        <div id="div_articles" runat="server"> </div>
        <br />
        <div id="div_clusters" runat="server"> </div>
    </form>
</body>
</html>
