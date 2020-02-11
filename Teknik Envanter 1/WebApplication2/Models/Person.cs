using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class Person
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("tckn")]
        public string Tckn { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [JsonProperty("birthPlace")]
        public string BirthPlace { get; set; }
        [JsonProperty("orderNo")]
        public string OrderNo { get; set; }

        public List<Person> People { get; set; }
    }

}
