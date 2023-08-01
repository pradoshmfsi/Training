<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="WebApplication1.WebForm2" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="form1" runat="server">
        <div>
            <div>
                <asp:Label ID="labelId" runat="server">User Name</asp:Label>
                <asp:TextBox ID="UserName" runat="server" ToolTip="Enter User Name"></asp:TextBox>
            </div>

            <p>
                <asp:Button ID="SubmitButton" runat="server" Text="Submit" OnClick="SubmitButton_Click" />
            </p>
            <asp:Label ID="userInput" runat="server"></asp:Label>
            <br />
            <br />

            <asp:HyperLink ID="HyperLink1" runat="server" Text="JavaTpoint" NavigateUrl="https://www.javatpoint.com"></asp:HyperLink>

            <br />
            <br />
            <div>
                <asp:RadioButton ID="RadioButton1" runat="server" Text="Male" GroupName="gender" />
                <asp:RadioButton ID="RadioButton2" runat="server" Text="Female" GroupName="gender" />
            </div>
            <p>
                <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" Style="width: 61px" />
            </p>
            <asp:Label runat="server" ID="genderId"></asp:Label>
            <br />
            <br />
            <div>
                <asp:Calendar ID="Calendar1" runat="server"
                    OnSelectionChanged="Calendar1_SelectionChanged"></asp:Calendar>
            </div>
            <asp:Label runat="server" ID="ShowDate"></asp:Label>
            <br />
            <br />
            <div>
                <p>Browse to Upload File</p>
                <asp:FileUpload ID="FileUpload1" runat="server" />
            </div>
            <div>
                <asp:Button ID="Button2" runat="server" Text="Upload File" OnClick="Button2_Click" />
            </div>
            <asp:Label runat="server" ID="FileUploadStatus"></asp:Label>
            <br />
            <br />
            <p>
                Click the button to download a file
            </p>
            <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Download" />
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Text="Select Brand Preferences"></asp:Label>
            <br />
            <br />
            <asp:CheckBox ID="apple" runat="server" Text="Apple" />
            <br />
            <asp:CheckBox ID="dell" runat="server" Text="Dell" />
            <br />
            <asp:Button ID="Button4" runat="server" OnClick="Cookie_Click" Text="Submit" />
            <p>
                <asp:Label ID="Label3" runat="server"></asp:Label>
            </p>
        </div>

        <asp:Label ID="Label1" runat="server"></asp:Label>
        <asp:DataList ID="DataList1" runat="server" DataKeyField="countryId">
            <ItemTemplate>
                countryId:
                <asp:Label ID="countryIdLabel" runat="server" Text='<%# Eval("countryId") %>' />
                <br />
                countryName:
                <asp:Label ID="countryNameLabel" runat="server" Text='<%# Eval("countryName") %>' />
                <br />
                <br />
            </ItemTemplate>
        </asp:DataList>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>


        <br />
        <br />
        <h2>Trying a custom edit and delete gridview
            <asp:GridView ID="GridView2" runat="server">
            </asp:GridView>
            <asp:EntityDataSource ID="EntityDataSource1" runat="server">
            </asp:EntityDataSource>
        </h2>
        
        <asp:DataGrid ID="DataGrid1" runat="server">
        </asp:DataGrid>
    </div>

</asp:Content>

