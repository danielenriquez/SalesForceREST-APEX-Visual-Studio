# SalesForceREST-APEX-Visual-Studio
SalesForce REST APEX Web Services. Connections from C# Visual Studio
### In this example we are going to show how to create APEX REST web services and request it from C# class.

1- Create APEX class using SalesForce developer console or Visual Studio code.</br>
<b><a href="https://github.com/danielenriquez/SalesForceREST-APEX-Visual-Studio/blob/master/LeadRESTServices.cls">LeadRESTServices.cls</a></b></br></br>
2-Create Lead object class on Visial Studio.</br>
<b><a href="https://github.com/danielenriquez/SalesForceREST-APEX-Visual-Studio/blob/master/Lead.cs">Lead.cs</a></b></br></br>
3-Create SalesForceHelperPartner class on Visual Studio.</br>
<b><a href="https://github.com/danielenriquez/SalesForceREST-APEX-Visual-Studio/blob/master/SalesForceHelperPartner.cs">SalesForceHelperPartner.cs</a></b></br></br>
In this class we have to refer SalesForce WSDL xml squema using <b>Project Web Refernces</b></br>
-First we have to download from SalesForce the API Parner WSDL xml file.</br></br>
![sf-api](https://user-images.githubusercontent.com/8003697/58895366-a2e71300-86eb-11e9-9cef-4730b89c0d39.jpg)</br>
![sf-api-WSDL](https://user-images.githubusercontent.com/8003697/58895395-b2fef280-86eb-11e9-8474-eac55a36b5e5.jpg)</br>
Then from our Visual Studio project, we have to create a Web Reference to this xml file.</br>
For more information about how to create Web references check this link:</br>
https://www.c-sharpcorner.com/uploadfile/anavijai/add-web-reference-in-visual-studio-2010/</br></br>
![vs-sf-helper](https://user-images.githubusercontent.com/8003697/58895811-b5158100-86ec-11e9-8dec-cfc44c04d2ea.jpg)</br>
(Look at the Web Reference URL path)</br></br>
![vs-sf-helper-WSDL](https://user-images.githubusercontent.com/8003697/58895877-d4aca980-86ec-11e9-8b52-973258237f6d.jpg)</br></br>
4- Invoque Web Service from aspx c# extention, <b><a href="https://github.com/danielenriquez/SalesForceREST-APEX-Visual-Studio/blob/master/Create.aspx.cs">Create.aspx.cs</a></b> using <b>SalesForceHelperPartner</b> class to connect to SalesForce and call the <b>APEX REST services</b>.</br></br> 

Note: <b><a href="https://github.com/danielenriquez/SalesForceREST-APEX-Visual-Studio/blob/master/tst_LeadRESTServices.cls">tst_LeadRESTServices.cls</a></b> this is an example of SalesForce APEX Unit Test</br>
