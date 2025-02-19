﻿using OrderProject.Application.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProject.Application.ViewModels.Products
{
    public class CreateProductDto:ICreateDto
    {
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? Unit { get; set; }
        public decimal UnitPrice { get; set; }
        public bool Status { get; set; }
    }
}
