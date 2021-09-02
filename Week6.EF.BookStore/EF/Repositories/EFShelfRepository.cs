using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week6.EF.BookStore.Core.Interfaces;
using Week6.EF.BookStore.Core.Models;

namespace Week6.EF.BookStore.EF.Repositories
{
    public class EFShelfRepository : IShelfRepository
    {

        private readonly BookContext  Ctx;
        public EFShelfRepository()
        {
            Ctx = new BookContext();
        }
        public bool Add(Shelf item)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Shelf item)
        {
            throw new NotImplementedException();
        }

        public List<Shelf> Fetch()
        {
            return Ctx.Shelves.ToList();
        }

        public Shelf GetByCode(string code)
        {
            //validazione
            var shelf = Ctx.Shelves.Where(s => s.Code == code).FirstOrDefault(); // se non trova da null
            return shelf;
        }

        public Shelf GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Shelf item)
        {
            throw new NotImplementedException();
        }
    }
}
