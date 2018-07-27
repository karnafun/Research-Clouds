<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeveloperPanel.aspx.cs" Inherits="assets_aspx_DeveloperPanel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div runat="server" id="div_controls">
                <asp:DropDownList runat="server" ID="ddl_users" AutoPostBack="true" OnTextChanged="Ddl_users_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div runat="server" id="div_info">
                <asp:Image runat="server" ID="img_user" />
                <br />
                <asp:Label runat="server" ID="lbl_name"></asp:Label>
                <br />
                <asp:Label runat="server" ID="lbl_email"></asp:Label>
                <br />
                <asp:Label runat="server" ID="lbl_summery"></asp:Label>
                <br />
                <asp:Label runat="server" ID="lbl_interests"></asp:Label>
            </div>
            <div runat="server" id="div_articles"></div>
            <div runat="server" id="div_keywords"></div>
        </div>
    </form>
</body>
</html>
