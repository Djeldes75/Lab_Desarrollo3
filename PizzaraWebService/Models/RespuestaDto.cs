using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaraWebService.Models
{
    public class RespuestaDto
    {
            public string Estado { get; set; }
            public string Mensaje { get; set; }
            public object Data { get; set; }
    }
}