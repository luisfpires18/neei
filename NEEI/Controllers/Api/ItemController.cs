using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NEEI.Models;

namespace NEEI.Controllers.Api
{
    public class ItemController : ApiController
    {
        /// Base de Dados;
        private ApplicationDbContext _context;

        public ItemController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            // Procura Item por Id;
            var item = _context.Item.SingleOrDefault(a => a.Id == id);
            

            _context.Item.Remove(item);
            _context.SaveChanges();

            return Ok();
        }
    }
}
