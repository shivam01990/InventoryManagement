﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory.ViewModel
{
    public abstract class ProductsViewModelBase : ViewModelBase
    {

        public abstract string Name { get; }

    }
}
