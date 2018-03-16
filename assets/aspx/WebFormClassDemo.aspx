<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WebFormClassDemo.aspx.cs" Inherits="assets_html_WebFormClassDemo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        td {
            border:1px solid black;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <div id="userInformation">
            <asp:Table runat="server">
                <asp:TableHeaderRow>
                    <asp:TableCell>ID </asp:TableCell>
                    <asp:TableCell>First Name </asp:TableCell>
                    <asp:TableCell>Middle Name </asp:TableCell>
                    <asp:TableCell>Last Name</asp:TableCell>
                    <asp:TableCell>Degree</asp:TableCell>
                    <asp:TableCell>Image Path</asp:TableCell>
                    <asp:TableCell>Birth Date</asp:TableCell>
                    <asp:TableCell>Registration Date </asp:TableCell>
                    <asp:TableCell>Administrator</asp:TableCell>
                    <asp:TableCell>Email </asp:TableCell>
                    <asp:TableCell>Hash</asp:TableCell>
                    <asp:TableCell>Salt</asp:TableCell>
                    <asp:TableCell>Summery</asp:TableCell>
                </asp:TableHeaderRow>
                <asp:TableRow>
                    <asp:TableCell runat="server" id="ID" >ID </asp:TableCell>
                    <asp:TableCell runat="server" id="txt_firstNAme">First Name </asp:TableCell>
                    <asp:TableCell runat="server" id="txt_middleName">Middle Name </asp:TableCell>
                    <asp:TableCell runat="server" id="txt_lastName">Last Name</asp:TableCell>
                    <asp:TableCell runat="server" id="txt_degree">Degree</asp:TableCell>
                    <asp:TableCell runat="server" id="txt_imagePath">Image Path</asp:TableCell>
                    <asp:TableCell runat="server" id="txt_birthDate">Birth Date</asp:TableCell>
                    <asp:TableCell runat="server" id="txt_registrationDate">Registration Date </asp:TableCell>
                    <asp:TableCell runat="server" id="txt_administrator">Administrator</asp:TableCell>
                    <asp:TableCell runat="server" id="txt_email">Email </asp:TableCell>
                    <asp:TableCell runat="server" id="txt_hash">Hash</asp:TableCell>
                    <asp:TableCell runat="server" id="txt_salt">Salt</asp:TableCell>
                    <asp:TableCell runat="server" id="txt_summery">Summery</asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>
    <div id="ph" runat="server">
    <button runat="server" id="btn_update">Update User</button>
    </div>
    </form>
</body>
</html>
