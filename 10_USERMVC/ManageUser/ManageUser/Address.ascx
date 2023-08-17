<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Address.ascx.cs" Inherits="ManageUser.Address" %>
<fieldset>
    <legend></legend>
    <div class="input-row">
        <div class="input-container">
            <div class="title" id="AddressLine1Title">
                Address Line 1 <span>*</span>
            </div>
            <div class="input-element">
                <input
                    type="text"
                    id="AddressLine1"
                    name="AddressLine1"
                    placeholder="Plot"
                    runat="server" />
            </div>
        </div>

        <div class="input-container">
            <div class="title">Address Line 2</div>
            <div class="input-element">
                <input
                    type="text"
                    id="AddressLine2"
                    name="AddressLine2"
                    placeholder="Street Area"
                    runat="server" />
            </div>
        </div>
    </div>
    <div class="input-row">
        <div class="input-container">
            <div id="CountryTitle" class="title">
                Country <span>*</span>
            </div>
            <div class="input-element">
                <select id="Country" runat="server">
                    <option value="">Select Country</option>
                </select>
            </div>
        </div>
        <div class="input-container">
            <div id="StateTitle" class="title">
                State <span>*</span>
            </div>
            <div class="input-element">

                <select id="State" runat="server">
                    <option value="">Select State</option>
                </select>
            </div>
        </div>
    </div>
    <div class="input-row">
        <div class="input-container">
            <div id="CityTitle" class="title">City <span>*</span></div>
            <div class="input-element">
                <input
                    type="text"
                    id="City"
                    name="City"
                    runat="server" />
            </div>
        </div>
        <div class="input-container">
            <div id="PinTitle" class="title">
                Postal code <span>*</span>
            </div>
            <div class="input-element">
                <input
                    type="text"
                    id="Pin"
                    name="Pin"
                    runat="server"/>
            </div>
        </div>
    </div>
</fieldset>
