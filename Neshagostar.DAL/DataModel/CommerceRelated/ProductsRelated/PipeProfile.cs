﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neshagostar.DAL.DataModel.CommerceRelated.ProductsRelated
{
    public class PipeProfile
    {
        public Guid Id { get; set; }
        [Display(Name="نوع پروفیل")]
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
