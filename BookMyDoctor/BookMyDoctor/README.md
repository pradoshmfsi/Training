# Book My Doctor 
## Deploy using IIS
### Step 1
```Go to C:\Windows\System32\drivers\etc```
Open hosts file with any program

![Screenshot 2023-08-24 100949](https://github.com/pradoshmfsi/Training/assets/138674240/03523240-6c9e-4448-97a5-f0c67cf49c96)

Replace your custom domain name with "yourdomainame".

### Step 2
Open IIS by typing ```inetmgr``` in Run Dialog Box

### Step 3

![image](https://github.com/pradoshmfsi/Training/assets/138674240/6343636c-fa38-4a6a-8aa1-a251b5b7c999)

Right Click on sites and select add website

### Step 4

![image](https://github.com/pradoshmfsi/Training/assets/138674240/de51681f-867a-453d-8df4-da5b0d1b4b64)

Add the required physical path(BookMyDoctor.Web Project Location), add your custom name you mentioned in hosts file above for the host name and site name.
Click OK!

### Step 5
For "https" you can go to bindings and add https binding to the website

![image](https://github.com/pradoshmfsi/Training/assets/138674240/96359f18-7753-4b8c-afe1-9e6a674994c3)

After selecting Add and selecting "https" from dropdown

![image](https://github.com/pradoshmfsi/Training/assets/138674240/0a122152-b426-43e3-a404-d084114a8236)

Add Host Name to be the same custom name mentioned above, select ```IIS Express Development Certificate``` for SSL Certificate.
Click OK

### Step 6
Now select your website from Sites.
Click Browse.

![image](https://github.com/pradoshmfsi/Training/assets/138674240/df8821f9-43b4-43fd-b8d1-24185a949abb)

If it does not work, try opening the same URL in a private window.

### Step 7
Do the necesssary changes to the Web.config file that is changing the SQL server username and password

```
<connectionStrings>
		<add name="BookMyDoctorEntities" connectionString="metadata=res://*/Model1.csdl|res://*/Model1.ssdl|res://*/Model1.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=.\SQLEXPRESS;initial catalog=BookMyDoctor;user id=[your-Sql-Username];password=[your-Sql-Password];MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />
</connectionStrings>
```

Here, in the connection string property find "id" and "password" and replace it with you actual SQL credentials!
Load the provided schema!

### Step 8
After browsing the URL, go to 
```
http://[www.yourdomainname.com]/Initialize
```

![image](https://github.com/pradoshmfsi/Training/assets/138674240/9a14b14c-676a-4b32-b697-c0b91e890f53)

Click the ```Inititalize Data```

This button will populate the default data to the SQL server.

You have successfully deployed the website!



