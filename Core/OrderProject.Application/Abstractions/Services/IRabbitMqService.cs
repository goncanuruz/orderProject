﻿using OrderProject.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProject.Application.Abstractions.Services
{
    public interface IRabbitMqService
    {
        void SendEmailQueque(SendEmailQuequeDto data);
    }
}
