﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Runtime.Serialization;
namespace WebAPIClient
{
    //Model for all the Products from the API
    [DataContract(Name = "repo")]
    public class Product
    {

   
        [DataMember(Name = "Make")]
        public string Make { get; set; }

        [DataMember(Name = "Model")]
        public string Model { get; set; }

        [DataMember(Name = "Title")]
        public string Title { get; set; }

        [DataMember(Name = "Price")]
        public int Price { get; set; }

        [DataMember(Name = "Thumbnail")]
        public Uri Thumbnail { get; set; }

    }

}

