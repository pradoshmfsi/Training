﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="BookMyDoctor.Web.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="Content/Login.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.4/jquery.min.js"></script>
</head>
<body>
    <form id="formLogin" runat="server">
        <div>
            <div id="loading">
                <img src="Assets/ZZ5H.gif" />
            </div>
            <div class="form-container">
                <div class="form-heading">LOGIN</div>
                <div class="login-container">
                    <div id="lblError"></div>
                    <div class="input-container">
                        <div class="title" id="emailTitle">Email<span>*</span></div>
                        <div class="input-element">
                            <input
                                class="txt"
                                type="text"
                                id="txtEmail"
                                isrequired="Id:#txtEmail|TitleId:#emailTitle span|Regex:[a-z0-9]+@[a-z]+\.[a-z]{2,3}"
                                name="txtEmail" />
                        </div>
                    </div>
                    <div class="input-container">
                        <div class="title" id="passwordTitle">Password<span>*</span></div>
                        <div class="input-element">
                            <input
                                class="txt"
                                type="password"
                                id="txtPassword"
                                isrequired="Id:#txtPassword|TitleId:#passwordTitle span|Regex:."
                                name="txtPassword" />
                        </div>
                    </div>
                    <br />
                    <div class="submit">
                        <input type="submit" id="btnLogin"
                            class="btn-submit"
                            value="LOGIN"/>
                    </div>
                </div>
            </div>
        </div>
        <script src="Scripts/Login.js"></script>
    </form>
</body>
</html>