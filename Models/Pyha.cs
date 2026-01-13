using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace project.Models
{
    public class Pyha
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Nimetus on kohustulik")]
        public string Nimetus { get; set; }
        [DataType(DataType.Date)]
        public DateTime Kuupaev { get; set; }
    }
}