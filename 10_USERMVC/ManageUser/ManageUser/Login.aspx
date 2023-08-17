<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ManageUser.Login" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="Content/login.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.4/jquery.min.js"></script>
</head>
<body>
    <form id="loginForm" runat="server">
        <div>
            <div class="form-container">

                <div class="login form-heading-element">
                    <div class="form-heading-text">LOGIN</div>
                    <div class="form-heading-graphic"></div>
                </div>

                <div class="login container">
                    <asp:label ID="loginErrorLabel" runat="server"></asp:label>
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
                    <br />
                    <div class="submit">
                        <asp:Button ID="loginBtn"
                            OnClick="LoginUserBtn"
                            OnClientClick="return validate();"
                            Text="Login"
                            runat="server" />
                    </div>
                </div>
            </div>
        </div>
        <script src="Scripts/login.js"></script>
    </form>
</body>
</html>
