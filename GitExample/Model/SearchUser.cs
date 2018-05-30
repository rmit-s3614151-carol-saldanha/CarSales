using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Runtime.Serialization;
namespace WebAPIClient
{
    //Display searched information about a user
    [DataContract(Name = "repo")]
    public class SearchUser
    {
        [DataMember(Name = "id")]
        public string id { get; set; }

        [DataMember(Name = "name")]
        public Uri name { get; set; }

        [DataMember(Name = "full_name")]
        public Uri full_name { get; set; }

      
    }

}

