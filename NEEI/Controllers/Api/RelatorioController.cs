using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NEEI.Models;

namespace NEEI.Controllers.Api
{
    public class RelatorioController : ApiController
    {
        /// Base de Dados;
        private ApplicationDbContext _context;

        public RelatorioController()
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
            // Procura Relatorio por Id;
            var relatorio = _context.Relatorio.SingleOrDefault(r => r.id == id);

            if (relatorio == null)
                return NotFound();

            _context.Relatorio.Remove(relatorio);
            _context.SaveChanges();

            return Ok();
        }
    }
}
