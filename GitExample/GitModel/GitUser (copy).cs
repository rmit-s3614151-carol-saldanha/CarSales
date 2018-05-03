using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Runtime.Serialization;
namespace WebAPIClient

//namespace GitExample.GitModel
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    [DataContract(Name = "repo")]
    public class GitUser
    {


        [DataMember(Name = "id")]
        public string id { get; set; }

        [DataMember(Name = "name")]
        public Uri name { get; set; }

        [DataMember(Name = "full_name")]
        public Uri full_name { get; set; }

        [DataMember(Name = "followers_url")]
        public Uri followers_url { get; set; }
    }

}

