<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DocumentsUserControl.ascx.cs" Inherits="WebApplication1.DocumentsUserControl" %>
<div id="documentDivUserControl">
    <div class="form-heading-element">
        <div class="form-heading-text" runat="server">Documents</div>
        <div class="form-heading-graphic"></div>
    </div>
    <div id="documentDivContainer">
        <div>
            Add Document:
    <br />
            <div class="input-element">
                <label id="documentFile-label" for="documentFile">
                    <div id="card" class="card">
                        <i class="fa-sharp fa-solid fa-plus" id="iconForAddDocument"></i>
                    </div>
                </label>
                <div id="documentName"></div>
                <input
                    type="file"
                    name="documentFile"
                    id="documentFile" />
            </div>
            <div class="submit">
                <input type="submit" id="AddDocumentBtn" value="Add" />
            </div>
        </div>
        <br />
        <div id="documentList"></div>
        <br />
    </div>

</div>

