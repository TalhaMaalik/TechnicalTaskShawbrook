﻿using BusinessLayer.Models.Base;
using BusinessLayer.Processors.Strategies;

namespace BusinessLayer.Models.Items
{
    public class Book : PhysicalProduct
    {
        public Book(Guid id, string? name, decimal cost) : base(id, name, cost){}
    }
}
