# SalesForceREST-APEX-Visual-Studio
SalesForece REST APEX Web Services. Connections from C# Visual Studio
### In this example we are going to show how to create APEX REST web services and request it from C# class.

1- Create APEX class using SalesForce developer console or Visual Studio code.</br>
<b>LeadRESTServices.cls</b></br></br>
2-Create Lead object class on Visial Studio.</br>
<b>Lead.cs</b></br></br>
3-Create SalesForceHelperPartner class on Visial Studio.</br>
<b>SalesForceHelperPartner.cs</b></br></br>
In this class we have to refer SalesForce WSDL xml squema using <b>Project Web Refernces</b></br>
-First we have to download from SalesForce the API Parner WSDL xml file.</br></br>
![sf-api](https://user-images.githubusercontent.com/8003697/58895366-a2e71300-86eb-11e9-9cef-4730b89c0d39.jpg)</br>
![sf-api-WSDL](https://user-images.githubusercontent.com/8003697/58895395-b2fef280-86eb-11e9-8474-eac55a36b5e5.jpg)</br>
Then from our Visual Studio project, we have to create a Web Reference to this xml file.</br></br>
![vs-sf-helper](https://user-images.githubusercontent.com/8003697/58895811-b5158100-86ec-11e9-8dec-cfc44c04d2ea.jpg)</br>
(Look at the Web Reference URL path)</br>
![vs-sf-helper-WSDL](https://user-images.githubusercontent.com/8003697/58895877-d4aca980-86ec-11e9-8b52-973258237f6d.jpg)

