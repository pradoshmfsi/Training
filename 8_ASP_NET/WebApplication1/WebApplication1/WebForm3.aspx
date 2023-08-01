<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebForm3.aspx.cs" Inherits="WebApplication1.WebForm3" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <%--<asp:TextBox ID="ViewStateText" runat="server" EnableViewState="False"></asp:TextBox>
    <br />
    <asp:Button ID="ViewStateButton" runat="server" Text="Submit" CssClass="btn btn-primary" />--%>
    <%--<div>  
        User Name:-<asp:textbox id="TextBox1" runat="server"></asp:textbox>  
        <br />  
        Password  :-<asp:textbox id="TextBox2" runat="server"></asp:textbox>  
        <br />  
        <asp:button id="Button1" runat="server" onclick="Button1_Click" text="Submit" />  
         <asp:button id="Button3" runat="server" onclick="Button3_Click" text="Restore" />  

        <asp:textbox id="TextBox3" runat="server"></asp:textbox>
    </div> --%>
    <asp:Label ID="Label1" runat="server" Font-Size='50px' ></asp:Label>
    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            
            <h6 id="titleForInsertion">Insert Data</h6>
            <%--<div>
                <asp:TextBox ID="CountryId" placeholder="Enter Country ID" runat="server" ToolTip="Enter Country Id" CssClass="form-control"></asp:TextBox>
            </div>--%>

            <br />
            <div>
                <asp:TextBox ID="CountryName" runat="server" placeholder="Enter Country Name" ToolTip="Enter Country Name" CssClass="form-control"></asp:TextBox>
            </div>
            
            <br />
            <div>
                <asp:Button ID="AddCountry" runat="server" OnClick="AddCountryOnClick" Text="Submit" CssClass="btn btn-primary" />
            </div>

            <br />
            <br />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="countryId" OnRowEditing="OnRowEditing" OnRowCancelingEdit="OnRowCancelingEdit"
                OnRowUpdating="OnRowUpdating" OnRowDeleting="OnRowDeleting" OnRowCommand="GridView1_RowCommand" EmptyDataText="No records has been added." CellPadding="4" ForeColor="#333333" GridLines="None" Width="825px">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="CountryId">
                        <ItemTemplate>
                            <asp:Label ID="CountryIdLabel" runat="server" Text='<%# Eval("countryId") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="CountryIdText" runat="server" Text='<%# Eval("countryId") %>'></asp:TextBox>
                        </EditItemTemplate>

                        <ItemStyle Width="50px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CountryName" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="CountryNameLabel" runat="server" Text='<%# Eval("countryName") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="CountryNameText" runat="server" Text='<%# Eval("countryName") %>'></asp:TextBox>
                        </EditItemTemplate>

                        <ItemStyle Width="150px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true" ItemStyle-Width="150" AccessibleHeaderText="Actions" HeaderText="Actions">
                        <ItemStyle Width="150px"></ItemStyle>
                    </asp:CommandField>
                    <asp:ButtonField CommandName="Select" HeaderText="States"  ShowHeader="True" Text="View" />
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
