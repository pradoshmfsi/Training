<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Notes.ascx.cs" Inherits="ManageUser.Notes" %>
<div id="noteUpdatePanel">
    <div class="form-heading-element">
    <div class="form-heading-text" runat="server">Notes</div>
    <div class="form-heading-graphic"></div>
</div>
<div id="noteDivUserControl">
    <div>
        Add Note:
    <br />
        <textarea id="NoteTextArea" placeholder="Enter Note"></textarea>
        <asp:CheckBox ID="ifPrivateCheck" runat="server" Text="Add as private" />
        <br />
        <div class="submit">
                <input type="submit" id="AddNote" value="Add" />
        </div>
        <div class="note-container">
        </div>
    </div>

</div>
</div>

