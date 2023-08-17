<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NotesUserControl.ascx.cs" Inherits="ManageUser.NotesUserControl" %>
<div class="form-heading-element">
    <div class="form-heading-text" runat="server">Notes</div>
    <div class="form-heading-graphic"></div>
</div>
<div id="noteDivUserControl">
    <div>
        Add Note:
    <br />
        <textarea id="NoteMessage" runat="server" placeholder="Enter Note"></textarea>
        <asp:CheckBox ID="ifPrivateCheck" runat="server" Text="Add as private" />
        <br />
        <div class="submit">
            <asp:Button ID="AddNote" runat="server" OnClick="AddNoteBtn" Text="Add" CssClass="btn btn-primary" />
        </div>
    </div>
    <asp:Label ID="noteHeader" runat="server">Notes</asp:Label>
    <br />
    <asp:DataList ID="NoteDataList" runat="server">
        <ItemTemplate>
            <asp:Label ID="noteLabel" runat="server" Text='<%# Eval("noteMessage") %>' />
        </ItemTemplate>
    </asp:DataList>
    <asp:Label ID="emptyNoteLabel" runat="server">No notes found!</asp:Label>
    <br />

    <asp:Label ID="notePrivateHeader" runat="server">Private Notes</asp:Label>
    <br />
    <asp:DataList ID="NotePrivateDataList" runat="server">
        <ItemTemplate>
            <asp:Label ID="notePrivateLabel" runat="server" Text='<%# Eval("noteMessage") %>' />
        </ItemTemplate>
    </asp:DataList>
    <asp:Label ID="emptyNotePrivateLabel" runat="server">No private notes found!</asp:Label>
    <br />
</div>

