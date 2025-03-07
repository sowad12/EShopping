﻿using Order.Library.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Library.Seeders
{
    public interface ISeeder
    {
        void Seed(ApplicationDbContext context);
    }
}
