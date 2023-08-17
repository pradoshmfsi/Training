<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form.aspx.cs" Inherits="ManageUser.Form" %>

<!DOCTYPE html>
<%@ Register Src="~/Notes.ascx" TagPrefix="uc" TagName="Note" %>
<%@ Register Src="~/DocumentsUserControl.ascx" TagPrefix="uc" TagName="Document" %>
<%@ Register Src="~/LogoutUserControl.ascx" TagPrefix="uc" TagName="Logout" %>
<%@ Register Src="~/Address.ascx" TagPrefix="uc" TagName="Address" %>
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
        <uc:Logout ID="logoutUserControl" runat="server" />
        <div id="loading">
            <img src="assets/ZZ5H.gif" />
        </div>
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
                                    isrequired="#password|#passwordTitle|#passwordTitle span|."
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
                                    isrequired="#password|#passwordTitle|#passwordTitle span|."
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
                                    id="dob"
                                    name="date"
                                    max="2015-01-01"
                                    isrequired="#dob|#dobTitle|#dobTitle span|." />
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
                            <table id="chklistUserRole">
                            </table>
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
                    <div id="presentAddress">
                        <uc:Address ID="present" AddressType="present" runat="server" />
                    </div>
                    <div id="permanentAddress">
                        
                        <uc:Address ID="permanent" AddressType="permanent" runat="server" />
                    </div>
                    <div class="submit">
                        <asp:Button ID="submitFormNew"
                            runat="server" />
                        <asp:Button ID="deleteForm" Text="Delete" OnClick="DeleteBtn" runat="server" />
                        <asp:Button ID="cancelForm" Text="Cancel" OnClick="CancelBtn" runat="server" />
                    </div>
                </div>


            </div>
            <uc:Note ID="notesUserControl" runat="server" ClientIDMode="Static" />

            <uc:Document ID="documentsUserControl" runat="server" />
        </div>

    </form>
    <script src="Scripts/formDetails.js"></script>
</body>
</html>
