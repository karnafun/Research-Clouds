<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserSettings.aspx.cs" Inherits="assets_aspx_UserSettings" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div runat="server" id="div_info"></div>
        <asp:Label runat="server" ID="lbl_info"> Personal Info</asp:Label>
        <br />
        <label>Image</label>
        <asp:Image  runat="server" ID="img_user"/>
        <label> summery: </label>        
        <asp:TextBox runat="server" ID="txt_summery"></asp:TextBox>
        <br />
        
        <br />
        <label>Email</label>
        <asp:TextBox runat="server" ID="txt_email"></asp:TextBox>
    </div>


        <div runat="server" id="div_articles">
            <asp:DropDownList runat="server" ID="ddl_articles">

            </asp:DropDownList>
        </div>
    </form>
</body>
</html>
