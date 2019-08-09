using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Salesforce.Common;
using Salesforce.Force;

namespace SalesForceLeadLogic
{
    public class SalesForceProcessor : ISalesForceProcessor
    {
        public async Task<bool> CreateSFObject<T>(T sfObject, string objectName, IConfiguration config)
        {
            var securityToken = config["SalesForceCredentials:SecurityToken"];
            var consumerKey = config["SalesForceCredentials:ConsumerKey"];
            var consumerSecret = config["SalesForceCredentials:ConsumerSecret"];
            var username = config["SalesForceCredentials:Username"];
            var password = config["SalesForceCredentials:Password"];
            var isSandboxUser = config["SalesForceCredentials:IsSandboxUser"];
            var url = config["SalesForceCredentials:URL"];
            bool result = false;

            try
            {
                var auth = new AuthenticationClient();

                // Authenticate with Salesforce                  
                await auth.UsernamePasswordAsync(consumerKey, consumerSecret, username, password + securityToken, url);
                var client = new ForceClient(auth.InstanceUrl, auth.AccessToken, auth.ApiVersion);
                ////create a lead from our form values
                var response = await client.CreateAsync(objectName, sfObject);
                if (response.Id == null)
                {
                    Trace.TraceWarning("Failed to create " + objectName);                   
                }
                else
                {
                    result = true;
                    Trace.TraceInformation("Created " + objectName + "Successfully!!!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                while (ex.InnerException != null)
                {
                    Trace.TraceInformation("Error While creating" + objectName);
                    Trace.TraceError(ex.Message + ex.InnerException);
                }
            }

            return result;
        }
    }
}
