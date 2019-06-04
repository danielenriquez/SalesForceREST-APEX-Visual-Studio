@isTest
private class tst_LeadRESTServices {
	@isTest
    static void Getlead_test() {  
       
        Lead lead = CreateLead();        
		Insert lead;
        
        RestRequest req = new RestRequest();
        RestResponse res = new RestResponse();
        req.requestURI = '/services/apexrest/restfull-leads/' + lead.Id; //Request URL
        req.httpMethod = 'GET';
    
        RestContext.request = req;
        RestContext.response= res;
                     
        Test.startTest();
        Lead resul = LeadRESTServices.getLeadById(); 
        Test.stopTest();         
    } 
    @isTest 
    static void Createlead_test() {
        Lead lead = CreateLead();    
		Insert lead;        
        String JsonMsg=JSON.serialize(lead);
        
        RestRequest req = new RestRequest();
        RestResponse res = new RestResponse();
        req.requestURI = '/services/apexrest/restfull-leads/';
        req.httpMethod = 'POST';
        req.requestBody = Blob.valueof(JsonMsg);
    
        RestContext.request = req;
        RestContext.response= res;
                     
        Test.startTest();
        LeadRESTServices.Response resul = LeadRESTServices.CreateLead(); 
        Test.stopTest(); 
    }
    
    @isTest 
    static void DeleteLead_test() {
        Lead lead = CreateLead();    
		Insert lead; 
        // Set up a test request
        RestRequest request = new RestRequest();
        request.requestUri = '/services/apexrest/restfull-leads/' + lead.Id;
        request.httpMethod = 'GET';
        RestContext.request = request;
        // Call the method to test
        LeadRESTServices.Deletelead();
    }    
    
    static Lead CreateLead(){
        Lead lead = new Lead();
		lead.FirstName = 'FirstName';
		lead.LastName = 'LastName';         
		lead.Phone = '123456789'; 
		lead.MobilePhone = '123456789';                 
		lead.Email = 'mail@mail.com';           
		lead.Nationality__c = 'ENG'; 
		lead.Country = 'UK';             
		lead.CurrencyIsoCode = 'GBP';        
        return lead;
    }    
}