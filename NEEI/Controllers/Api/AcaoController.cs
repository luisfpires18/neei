﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NEEI.Models;

namespace NEEI.Controllers.Api
{
    public class AcaoController : ApiController
    {
        /// Base de Dados;
        private ApplicationDbContext _context;

        public AcaoController()
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
            // Procura Acção por Id;
            var acao = _context.Acao.SingleOrDefault(a => a.Id == id);
            // Põe o valor a 0;
            acao.Total = 0;

            // Atualiza o Total do Relatorio de Contas;
            var rc = _context.Relatorio.SingleOrDefault(r => r.Id == acao.RelatorioId);
            var acoes = _context.Acao.SqlQuery("SELECT * FROM Acaos a WHERE a.RelatorioId = @id", new SqlParameter("@id", acao.RelatorioId)).ToList();

            rc.Valor = 0;
            foreach (var a in acoes)
            {
                rc.Valor += a.Total;
            }

            _context.Acao.Remove(acao);
            _context.SaveChanges();

            return Ok();
        }
    }
}
