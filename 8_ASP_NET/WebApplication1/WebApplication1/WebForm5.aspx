<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm5.aspx.cs" Inherits="WebApplication1.WebForm5" %>

<!DOCTYPE html>
<%@ Register Src="~/WebUserControl1.ascx" TagPrefix="uc" TagName="Message" %> 
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>User Form</h1>
            <br />
            <div>
                <asp:TextBox ID="UserName" runat="server" placeholder="Enter User Name" ToolTip="Enter User Name" CssClass="form-control"></asp:TextBox>
            </div>
            <br />
            <div>
                <asp:TextBox ID="UserEmail" runat="server" placeholder="Enter UserEmail" ToolTip="Enter UserEmail" CssClass="form-control"></asp:TextBox>
            </div>
            <br />
            <uc:Message ID="messageControl" runat="server" /> 
            <br />

            <div>
                <asp:Button ID="AddUser" runat="server" OnClick="AddUserOnClick" Text="Submit" CssClass="btn btn-primary" />
            </div>
        </div>
    </form>
</body>
</html>
