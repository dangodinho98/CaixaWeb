using AutoMapper;
using Caixa.Web.DTOs;
using Caixa.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Caixa.Web.Controllers.Api
{
    public class EstabelecimentoController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();

        //GET /api/estabelecimento
        public IEnumerable<EstabelecimentoDTO> GetEstabelecimento()
        {
            return db.Estabelecimento.ToList().Select(Mapper.Map<Estabelecimentos, EstabelecimentoDTO>);
        }

        //GET /api/estabelecimento/5
        public IHttpActionResult GetEstabelecimentos(int id)
        {
            var estabelecimento = db.Estabelecimento.Find(id);

            if (estabelecimento == null)
                return NotFound();

            return Ok(Mapper.Map<Estabelecimentos, EstabelecimentoDTO>(estabelecimento));
        }

        //POST /api/estabelecimento
        [HttpPost]
        public IHttpActionResult CreateEstabelecimentos(EstabelecimentoDTO estabelecimentosDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var estabelecimento = Mapper.Map<EstabelecimentoDTO, Estabelecimentos>(estabelecimentosDto);
            db.Estabelecimento.Add(estabelecimento);
            db.SaveChanges();

            estabelecimentosDto.Id = estabelecimento.Id;

            return Created(new Uri(Request.RequestUri + "/" + estabelecimento.Id.ToString()), estabelecimentosDto);
        }

        //PUT /api/estabelecimento/1
        [HttpPut]
        public void UpdateEstabelecimentos(int id, EstabelecimentoDTO estabelecimentosDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var estabelecimentoInDB = db.Estabelecimento.Find(id);

            if (estabelecimentoInDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            //AutoMapper
            Mapper.Map(estabelecimentosDto, estabelecimentoInDB);

            db.SaveChanges();
        }

        //DELETE /api/estabelecimento/1
        [HttpDelete]
        public void DeleteEstabelecimento(int id)
        {
            var estabelecimentoInDB = db.Estabelecimento.Find(id);

            if (estabelecimentoInDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            db.Estabelecimento.Remove(estabelecimentoInDB);
            db.SaveChanges();
        }
    }
}
