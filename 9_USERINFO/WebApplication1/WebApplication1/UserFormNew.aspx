<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserFormNew.aspx.cs" Inherits="WebApplication1.UserFormNew" %>

<!DOCTYPE html>
<%@ Register Src="~/NotesUserControl.ascx" TagPrefix="uc" TagName="Note" %>
<%@ Register Src="~/DocumentsUserControl.ascx" TagPrefix="uc" TagName="Document" %>
<%@ Register Src="~/LogoutUserControl.ascx" TagPrefix="uc" TagName="Logout" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Form</title>
    <link rel="stylesheet" href="Content/userForm.css" />
    <link
        rel="stylesheet"
        href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css"
        integrity="sha512-iecdLmaskl7CVkqkXNQ/ZH/XLlvWZOJyj7Yy7tcenmpD1ypASozpmT/E0iPtmFIB46ZmdtAc9eNBvH0H/ZpiBw=="
        crossorigin="anonymous"
        referrerpolicy="no-referrer" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.4/jquery.min.js"></script>
</head>
<body>

    <form id="formUserRegister" runat="server">
        <asp:ScriptManager runat="server">
        </asp:ScriptManager>
        <uc:Logout ID="logoutUserControl" runat="server" />

        <div class="form-container">
            <div class="nav" id="navContainer" runat="server">
                <div id="detailsNav">
                    <i class="fa-solid fa-user"></i>&nbsp;Details
                </div>
                <div id="notesNav">
                    <i class="fa-solid fa-note-sticky"></i>&nbsp;Notes
                </div>
                <div id="documentsNav">
                    <i class="fa-solid fa-folder"></i>&nbsp;Documents
                </div>
            </div>
            <div class="container">
                <div id="detailsContainer">
                    <div class="form-heading">
                        <div class="form-heading-element">
                            <div class="form-heading-text" id="formHeader" runat="server">Register</div>
                            <div class="form-heading-graphic"></div>
                        </div>
                        <div class="form-heading-image">
                            <img
                                id="profileImage"
                                src="https://img.freepik.com/free-icon/user_318-159711.jpg?w=2000"
                                alt=""
                                runat="server" />
                        </div>
                    </div>
                    <div class="input-row">
                        <div class="input-container">
                            <div class="title" id="firstNameTitle">
                                First Name <span>*</span>
                            </div>
                            <div class="input-element">
                                <input
                                    type="text"
                                    id="firstName"
                                    name="firstName"
                                    isrequired="#firstName|#firstNameTitle|#firstNameTitle span|[A-Za-z ]+"
                                    showdetails="First Name|firstName|text"
                                    submitform="firstName"
                                    runat="server" />
                            </div>
                        </div>

                        <div class="input-container">
                            <div class="title" id="lastNameTitle">Last Name <span>*</span></div>
                            <div class="input-element">
                                <input
                                    type="text"
                                    id="lastName"
                                    name="lastName"
                                    isrequired="#lastName|#lastNameTitle|#lastNameTitle span|[A-Za-z ]+"
                                    submitform="lastName"
                                    runat="server"
                                    showdetails="Last Name|lastName|text" />
                            </div>
                        </div>
                    </div>
                    <div class="input-row">
                        <div class="input-container">
                            <div class="title" id="emailTitle">Email <span>*</span></div>
                            <div class="input-element">
                                <input
                                    type="text"
                                    id="email"
                                    name="email"
                                    submitform="email"
                                    isrequired="#email|#emailTitle|#emailTitle span|[a-z0-9]+@[a-z]+\.[a-z]{2,3}"
                                    runat="server" />
                            </div>
                        </div>

                        <div class="input-container">
                            <div class="title" id="genderTitle">Gender <span>*</span></div>
                            <div class="input-element" showdetails="Gender|gender|radio">
                                <input
                                    type="radio"
                                    id="male"
                                    name="gender"
                                    value="male"
                                    runat="server"
                                    isrequired="true" />
                                <label for="male">Male</label>
                                <input
                                    type="radio"
                                    id="female"
                                    name="gender"
                                    value="female"
                                    runat="server"
                                    isrequired="true" />
                                <label for="female">Female</label>
                            </div>
                        </div>
                    </div>
                    <div class="input-row">
                        <div class="input-container">
                            <div class="title" id="passwordTitle">Password <span>*</span></div>
                            <div class="input-element">
                                <input
                                    type="password"
                                    id="password"
                                    name="password"
                                    submitform="password"
                                    runat="server" />
                            </div>
                        </div>
                        <div class="input-container">
                            <div class="title" id="confirmPasswordTitle">Confirm Password <span>*</span></div>
                            <div class="input-element">
                                <input
                                    type="password"
                                    id="confirmPassword"
                                    name="confirmPassword"
                                    runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="input-row">
                        <div class="input-container">
                            <div class="title" id="dobTitle">Date of birth <span>*</span></div>
                            <div class="input-element">
                                <input
                                    type="date"
                                    id="date"
                                    name="date"
                                    max="2015-01-01"
                                    isrequired="#date|#dobTitle|#dobTitle span|."
                                    submitform="date"
                                    runat="server"
                                    showdetails="Date Of Birth|date|text" />
                            </div>
                        </div>

                        <div class="input-container">
                            <div class="title" id="hobbyTitle">Hobbies</div>
                            <div class="input-element">
                                <input
                                    type="text"
                                    list="hobbyList"
                                    id="hobby"
                                    name="hobby"
                                    autocomplete="off"
                                    submitform="hobby"
                                    showdetails="Hobbies|hobby|text"
                                    runat="server"
                                    multiple />
                                <datalist id="hobbyList">
                                    <option value="Singing"></option>
                                    <option value="Dancing"></option>
                                    <option value="Football"></option>
                                </datalist>
                            </div>
                        </div>
                    </div>
                    <div class="input-container-dp">
                        <div class="title" id="userRoleTitle">Select user roles <span>*</span></div>
                        <div class="input-element">
                            <asp:CheckBoxList ID="chklistUserRole" runat="server" Width="242px">
                            </asp:CheckBoxList>
                        </div>

                    </div>
                    <div class="input-container-dp">
                        <div class="profile-pic">
                            <div class="title" id="dpTitle">Profile picture <span>*</span></div>

                            <div class="input-element">
                                <label id="profilePic-label" for="profilePic">
                                    <div id="card" class="card">
                                        <i class="fa-sharp fa-solid fa-plus" id="iconForAddPic"></i>
                                    </div>
                                </label>
                                <div id="filename"></div>
                                <input
                                    type="file"
                                    name="dp"
                                    id="profilePic"
                                    showdetails="Profile Picture|profilePic|file"
                                    runat="server" />
                            </div>
                        </div>
                    </div>

                    <fieldset>
                        <legend>Present Address</legend>
                        <div id="errMsgPresentAddress"></div>
                        <div class="input-row">
                            <div class="input-container">
                                <div class="title" id="presentAddressLine1Title">
                                    Address Line 1 <span>*</span>
                                </div>
                                <div class="input-element">
                                    <input
                                        type="text"
                                        id="presentAddressLine1"
                                        name="presentAddressLine1"
                                        placeholder="Plot"
                                        showdetails="Address Line 1|presentAddressLine1|text"
                                        populateaddress="presentAddressLine1|permanentAddressLine1"
                                        isrequired="#presentAddressLine1|#errMsgPresentAddress|#presentAddressLine1Title span|."
                                        submitform="presentAddressLine1"
                                        runat="server" />
                                </div>
                            </div>

                            <div class="input-container">
                                <div class="title">Address Line 2</div>
                                <div class="input-element">
                                    <input
                                        type="text"
                                        id="presentAddressLine2"
                                        name="presentAddressLine2"
                                        placeholder="Street Area"
                                        showdetails="Address Line 2|presentAddressLine2|text"
                                        populateaddress="presentAddressLine2|permanentAddressLine2"
                                        runat="server" />
                                </div>
                            </div>
                        </div>
                        <asp:UpdatePanel ID="presentCountryUpdatePanel" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <div class="input-row">
                                    <div class="input-container">
                                        <div id="presentCountryTitle" class="title">
                                            Country <span>*</span>
                                        </div>
                                        <div class="input-element">
                                            <asp:DropDownList ID="presentCountry" addresstype="present"
                                                showdetails="Country|presentCountry|text"
                                                populateaddress="presentCountry|permanentCountry|permanent"
                                                isrequired="#presentCountry|#errMsgPresentAddress|#presentCountryTitle span|."
                                                runat="server" AutoPostBack="True" OnSelectedIndexChanged="PopulateStatesPresent">
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                    <div class="input-container">
                                        <div id="presentStateTitle" class="title">
                                            State <span>*</span>
                                        </div>
                                        <div class="input-element">

                                            <asp:DropDownList ID="presentState" showdetails="State|presentState|text"
                                                populateaddress="presentState|permanentState|state"
                                                isrequired="#presentState|#errMsgPresentAddress|#presentStateTitle span|."
                                                runat="server">
                                                <asp:ListItem Value="" Text=""></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="input-row">
                            <div class="input-container">
                                <div id="presentCityTitle" class="title">City <span>*</span></div>
                                <div class="input-element">
                                    <input
                                        type="text"
                                        id="presentCity"
                                        name="presentCity"
                                        showdetails="City|presentCity|text"
                                        populateaddress="presentCity|permanentCity"
                                        runat="server"
                                        isrequired="#presentCity|#errMsgPresentAddress|#presentCityTitle span|." />
                                </div>
                            </div>
                            <div class="input-container">
                                <div id="presentPinTitle" class="title">
                                    Postal code <span>*</span>
                                </div>
                                <div class="input-element">
                                    <input
                                        type="text"
                                        id="presentPin"
                                        name="presentPin"
                                        showdetails="Pin|presentPin|text"
                                        populateaddress="presentPin|permanentPin"
                                        runat="server"
                                        isrequired="#presentPin|#errMsgPresentAddress|#presentPinTitle span|." />
                                </div>
                            </div>
                        </div>
                    </fieldset>
                    <asp:UpdatePanel ID="permanentAddressUpdatePanel" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <fieldset id="permanentContainer">
                                <legend>Permanent Address</legend>
                                <div id="errMsgPermanentAddress"></div>
                                <div class="input-checkbox-address">
                                    <asp:CheckBox ID="ifPresentSameAsPermanent" AutoPostBack="True" Text="Same as present address" OnCheckedChanged="PopulatePermanentAsPresent" runat="server" />
                                </div>
                                <div class="input-row">
                                    <div class="input-container">
                                        <div id="permanentAddressLine1Title" class="title">
                                            Address Line 1 <span>*</span>
                                        </div>
                                        <div class="input-element">
                                            <input
                                                type="text"
                                                id="permanentAddressLine1"
                                                name="permanentAddressLine1"
                                                placeholder="Plot"
                                                showdetails="Address Line 1|permanentAddressLine1|text"
                                                runat="server"
                                                isrequired="#permanentAddressLine1|#errMsgPermanentAddress|#permanentAddressLine1Title span|." />
                                        </div>
                                    </div>

                                    <div class="input-container">
                                        <div class="title">Address Line 2</div>
                                        <div class="input-element">
                                            <input
                                                type="text"
                                                id="permanentAddressLine2"
                                                name="permanentAddressLine2"
                                                placeholder="Street Area"
                                                runat="server"
                                                showdetails="Address Line 2|permanentAddressLine2|text" />
                                        </div>
                                    </div>
                                </div>
                                <asp:UpdatePanel ID="permanentCountryUpdatePanel" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                        <div class="input-row">
                                            <div class="input-container">
                                                <div id="permanentCountryTitle" class="title">
                                                    Country <span>*</span>
                                                </div>
                                                <div class="input-element">
                                                    <asp:DropDownList ID="permanentCountry" addresstype="permanent"
                                                        showdetails="Country|permanentCountry|text"
                                                        isrequired="#permanentCountry|#errMsgPermanentAddress|#permanentCountryTitle span|."
                                                        runat="server" AutoPostBack="True" OnSelectedIndexChanged="PopulateStatesPermanent">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="input-container">
                                                <div id="permanentStateTitle" class="title">
                                                    State <span>*</span>
                                                </div>
                                                <div class="input-element">

                                                    <asp:DropDownList ID="permanentState" showdetails="State|permanentState|text"
                                                        isrequired="#permanentState|#errMsgPermanentAddress|#permanentStateTitle span|."
                                                        runat="server">
                                                        <asp:ListItem Value="" Text=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div class="input-row">
                                    <div class="input-container">
                                        <div id="permanentCityTitle" class="title">
                                            City <span>*</span>
                                        </div>
                                        <div class="input-element">
                                            <input
                                                type="text"
                                                id="permanentCity"
                                                name="permanentCity"
                                                showdetails="City|permanentCity|text"
                                                runat="server"
                                                isrequired="#permanentCity|#errMsgPermanentAddress|#permanentCityTitle span|." />
                                        </div>
                                    </div>
                                    <div class="input-container">
                                        <div id="permanentPinTitle" class="title">
                                            Postal code <span>*</span>
                                        </div>
                                        <div class="input-element">
                                            <input
                                                type="text"
                                                id="permanentPin"
                                                name="permanentPin"
                                                showdetails="Pin|permanentPin|text"
                                                runat="server"
                                                isrequired="#permanentPin|#errMsgPermanentAddress|#permanentPinTitle span|." />
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <%--<div class="input-checkbox">
                    <input
                        type="checkbox"
                        id="ifSubscribed"
                        name="ifSubscribed"
                        runat="server"
                        showdetails="Subscribed to News Letter|ifSubscribed|checkbox" /><label for="ifSubscribed">Subscribe to news letter</label>
                </div>--%>


                    <br />
                    <div class="submit">
                        <asp:Button ID="submitFormNew"
                            runat="server" />
                        <asp:Button ID="deleteForm" Text="Delete" OnClick="DeleteBtn" runat="server" />
                        <asp:Button ID="cancelForm" Text="Cancel" OnClick="CancelBtn" runat="server" />
                    </div>
                </div>

                <br />
                <asp:UpdatePanel ID="noteUpdatePanel" runat="server">
                    <ContentTemplate>
                        <uc:Note ID="notesUserControl" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>

                <uc:Document ID="documentsUserControl" runat="server" />
            </div>

        </div>
        <br />

        <script src="Scripts/userForm.js"></script>

    </form>
</body>
</html>

