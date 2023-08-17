<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="WebApplication1.Details" %>

<!DOCTYPE html>
<%@ Register Src="~/LogoutUserControl.ascx" TagPrefix="uc" TagName="Logout" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Form</title>
    <link rel="stylesheet" href="Content/userForm.css" />
</head>
<body>
    <form id="formUserDetails" runat="server">
        <%--<h1 id="userDetailTitle">User Details</h1>--%>
        <uc:Logout ID="logoutUserControl" runat="server" />
        <div class="form-heading-element">
            <div class="form-heading-text">User List</div>
            <div class="form-heading-graphic"></div>
        </div>
        <div class="submit">
            <asp:Button ID="addForm" Text="ADD USER" runat="server" OnClick="AddUserBtn" />
            <br />
            <%--<asp:Button ID="logout" Text="Logout" runat="server" OnClick="LogoutBtn" />
            <br />--%>
        </div>
        <br />
        <div id="userDetailsDiv">
            <asp:GridView ID="UserDetailsGrid" runat="server"
                AutoGenerateColumns="False" DataKeyNames="userId"
                OnRowCommand="EditRowCommand" EmptyDataText="No records has been added." GridLines="None">
                <Columns>
                    <asp:TemplateField HeaderText="User ID">
                        <ItemTemplate>
                            <asp:Label ID="UserIdLabel" runat="server" Text='<%# Eval("UserId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="First Name">
                        <ItemTemplate>
                            <asp:Label ID="fnameLabel" runat="server" Text='<%# Eval("FirstName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Last Name">
                        <ItemTemplate>
                            <asp:Label ID="lnameLabel" runat="server" Text='<%# Eval("LastName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Email">
                        <ItemTemplate>
                            <asp:Label ID="emailLabel" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="D.O.B">
                        <ItemTemplate>
                            <asp:Label ID="dobLabel" runat="server" Text='<%# Convert.ToDateTime(Eval("dob")).ToString("dd/MM/yyyy") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:ButtonField CommandName="Select" HeaderText="Edit" ShowHeader="True" Text="Edit" />
                </Columns>
            </asp:GridView>
        </div>

    </form>
</body>
</html>
