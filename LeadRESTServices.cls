@RestResource(urlMapping='/restfull-leads/*')
global class LeadRESTServices {

    //REST get action
    @HttpGet
    global static Lead getLeadById() {
        RestRequest request = RestContext.request;
        // grab the caseId from the end of the URL
        String leadId = request.requestURI.substring(
          request.requestURI.lastIndexOf('/')+1);

        Lead result =  [SELECT Id,Title,FirstName,LastName,
                       Phone,MobilePhone,Email,Nationality__c,
                       Country__c,Client_Comments__c,Country FROM Lead WHERE Id = :leadId];

        return result; 
    }
    //REST post action
 	@HttpPost
    global static Response CreateLead() {        
        //Get response body JSON parameters
		RestRequest req = RestContext.request;
    	Blob body = req.requestBody;
    	String requestString = body.toString();                
        
        //Parse JOSN paramters to Class object
        LeadObject param = (LeadObject)JSON.deserialize(requestString, LeadObject.class);

        String objectId = InsertLead(param);
                        
        //Response back info to the client
        Response response = new Response();
        if (objectId != null){
            response.Id = objectId;
            response.action = 'Post';
            response.sfObject = 'Lead';
            response.status = 'ok';
        } else {
            //response.Id = objectId;
            response.action = 'Post';
            response.sfObject = 'Lead';
            response.status = 'error';
        }
        return response;
    }
    //REST delete action
    @HttpDelete
    global static void DeleteLead() {
        RestRequest request = RestContext.request;
        String leadId = request.requestURI.substring(
            request.requestURI.lastIndexOf('/')+1);
            DeleteLead(leadId);
    }  
    //REST upsert action
    @HttpPut
    global static Response UpsertLead() {
        //Get response body JSON parameters
		RestRequest req = RestContext.request;
    	Blob body = req.requestBody;
    	String requestString = body.toString();          
        //Parse JOSN paramters to Class object
        LeadObject param = (LeadObject)JSON.deserialize(requestString, LeadObject.class);

        String objectId = UpsertLead(param); 
  
        //Response back info to the client
        Response response = new Response();
        response.Id = objectId;
        response.action = 'Put';
        response.sfObject = 'Lead';
       
        return response;
    }  
    //REST update action
    @HttpPatch
    global static Response updateLeadFields() {
        RestRequest request = RestContext.request;
        String leadId = request.requestURI.substring(
            request.requestURI.lastIndexOf('/')+1);

        String objectId = UpdateLeadFields(leadId, request);

        //Response back info to the client
        Response response = new Response();
        response.Id = objectId;
        response.action = 'Patch';
        response.sfObject = 'Lead';
       
        return response;
    } 

    //=======================================================================================
    //CRUD actions on SalesForce Lead object
    //=======================================================================================
    public static ID InsertLead(LeadObject lead){        
        Lead newLead = new Lead(
            Title = lead.Title,
            FirstName = lead.FirstName,
            LastName = lead.LastName,         
            Phone = lead.Phone,
            MobilePhone = lead.Phone,                  
            Email = lead.Email,           
            Nationality__c = lead.Language,      
            Country__c = lead.Country,
            Client_Comments__c = lead.Comments,
            Country = lead.Country                    
        );        
        try{            
            Insert newLead;
            return newLead.Id;
         } catch(DmlException ex){
             system.debug('SignalliaRestFunctions. Insert Lead: ' + ex.getMessage());
             return null;
         } finally{}                
    }
            
    public static void DeleteLead(ID leadId) {
         try{  
            Lead thisLead = [SELECT Id FROM Lead WHERE Id = :leadId];
            delete thisLead;
        } catch(DmlException ex){
             system.debug('SignalliaRestFunctions. Delete Lead: ' + ex.getMessage()); 
         } finally{}         
    }     

    public static ID UpsertLead(LeadObject lead){        
        Lead newLead = new Lead(
            Id = lead.Id,
            Title = lead.Title,
            FirstName = lead.FirstName, 
            LastName = lead.LastName,         
            Phone = lead.Phone,
            MobilePhone = lead.Phone,                  
            Email = lead.Email,           
            Nationality__c = lead.Language,      
            Country__c = lead.Country,
            Client_Comments__c = lead.Comments,
            Country = lead.Country        
        );        
        try{       
            // Match lead by Id, if present.
            // Otherwise, create new lead.            
            Upsert newLead;
            return newLead.Id;
         } catch(DmlException ex){
             system.debug('SignalliaRestFunctions. Upsert Lead: ' + ex.getMessage());
             return null;
         } finally{}                         
    } 


    public static ID UpdateLeadFields(ID leadId, RestRequest request) {
        try{     
            Lead thisLead = [SELECT Id FROM Lead WHERE Id = :leadId]; 
            // Deserialize the JSON string into name-value pairs
            Map<String, Object> params = (Map<String, Object>)JSON.deserializeUntyped(request.requestbody.tostring());
            // Iterate through each parameter field and value
            for(String fieldName : params.keySet()) {
                // Set the field and value on the Case sObject
                thisLead.put(fieldName, params.get(fieldName)); 
            }
            update thisLead;
            return thisLead.Id;
         } catch(DmlException ex){
             system.debug('SignalliaRestFunctions. UpdateLeadFields Lead: ' + ex.getMessage());
             return null;
         } finally{}           
    }                   
    
    //Lead object model
    public class LeadObject{
        public String Id {get; set;}
        public String Title {get; set;}
 		public String FirstName {get; set;} 
        public String LastName {get; set;}         
        public String Email {get; set;}
        public String Phone {get; set;}
        public String MobilePhone {get; set;}        
        public String Language {get; set;}
        public String Country {get; set;}
 		public String Comments {get; set;}              
    }   

    //Response back model
    global class Response{
        String id {get; set;}
        String action {get; set;}
        String sfObject {get; set;} 
        String status {get; set;}  
    }    
}