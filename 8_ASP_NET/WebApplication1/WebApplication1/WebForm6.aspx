<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm6.aspx.cs" Inherits="WebApplication1.WebForm6" %>

<!DOCTYPE html>
<%@ Register Src="~/WebUserControl1.ascx" TagPrefix="uc" TagName="Message" %> 
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Company Form</h1>
            <br />
            <div>
                <asp:TextBox ID="CompanyName" runat="server" placeholder="Enter Company Name" ToolTip="Enter Company Name" CssClass="form-control"></asp:TextBox>
            </div>
            <br />
            <div>
                <asp:TextBox ID="CompanyRevenue" runat="server" placeholder="Enter CompanyRevenue" ToolTip="Enter CompanyRevenue" CssClass="form-control"></asp:TextBox>
            </div>
            <br />
            <uc:Message ID="messageControl" runat="server" /> 
            <br />
            <div>
                <asp:Button ID="AddCompany" runat="server" OnClick="AddCompanyOnClick" Text="Submit" CssClass="btn btn-primary" />
            </div>
        </div>
    </form>
</body>
</html>
