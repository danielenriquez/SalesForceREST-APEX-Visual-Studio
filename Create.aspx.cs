using SalesForcePartnerCnn;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SalesForceREST_Lead_Create : Page {
    protected void Page_Load(object sender, EventArgs e) {

        string sfUser = "username@salesforce.com";
        string sfPassw = "password";
        string sfToken = "sales-force-token_here";

        //Populate Lead model    
        Lead leadToInsert = AssignLeadParameters(Context);

        try {
            string jsonString = new JavaScriptSerializer().Serialize(leadToInsert);
            //Set secure protocol. static SalesForceHelperPartner class
            SalesForceHelperPartner.SfProtocolConn();
            //Login to SalesForce. static SalesForceHelperPartner class
            if (SalesForceHelperPartner.Login(sfUser, sfPassw, sfToken)) {
                //Post data. static SalesForceHelperPartner class
                var result = SalesForceHelperPartner.PostLeadRestServiceAsync(jsonString).Result;
                Tools.FormatToJson(Context, result);
            } else {
                Tools.FormatToJson(Context, "SalesForce Login error!");
            }
        } catch (Exception ex) {
            Console.WriteLine("Failed to execute query successfully. Error: " + ex.Message);
        }
    
    }

    //Assign http context parameters
    private Lead AssignLeadParameters(HttpContext context) {
        Lead lead = new Lead {
            Title = context.Request.Params["title"] ?? "",
            Email = context.Request.Params["email"] ?? "",
            Phone = context.Request.Params["phone"] ?? "",
            FirstName = context.Request.Params["firstname"] ?? "",
            LastName = context.Request.Params["lastname"] ?? "",
            Language = context.Request.Params["language"] ?? "",
            Country = context.Request.Params["country"] ?? "",
            Comments = context.Request.Params["comments"] ?? "",
        };
        return lead;
    }
}