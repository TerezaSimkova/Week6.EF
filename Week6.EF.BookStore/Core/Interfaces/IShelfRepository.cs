﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week6.EF.BookStore.Core.Models;

namespace Week6.EF.BookStore.Core.Interfaces
{
    public interface IShelfRepository :IRepository<Shelf>
    {
        //eventualmento i metodi legati a shelf
        Shelf GetByCode(string code);
    }
}
