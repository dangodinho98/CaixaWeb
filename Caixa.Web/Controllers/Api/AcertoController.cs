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
    public class AcertoController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();

        //GET /api/acertos
        public IEnumerable<AcertoDTO> GetAcertos()
        {
            return db.Acerto.ToList().Select(Mapper.Map<Acerto, AcertoDTO>);
        }

        //GET /api/acertos/5
        public IHttpActionResult GetAcerto(int id)
        {
            var acerto = db.Acerto.Find(id);

            if (acerto == null)
                return NotFound();

            return Ok(Mapper.Map<Acerto, AcertoDTO>(acerto));
        }

        //POST /api/acertos
        [HttpPost]
        public IHttpActionResult CreateAcerto(AcertoDTO acertoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var acerto = Mapper.Map<AcertoDTO, Acerto>(acertoDto);
            db.Acerto.Add(acerto);
            db.SaveChanges();

            acertoDto.Id = acerto.Id;

            return Created(new Uri(Request.RequestUri + "/" + acerto.Id.ToString()),acertoDto);
        }

        //PUT /api/acerto/1
        [HttpPut]
        public void UpdateAcerto(int id, AcertoDTO acertoDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var acertoInDB = db.Acerto.Find(id);

            if (acertoInDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            //AutoMapper
            Mapper.Map(acertoDto, acertoInDB);

            db.SaveChanges();
        }

        //DELETE /api/acertos/1
        [HttpDelete]
        public void DeleteAcerto(int id)
        {
            var acertoInDB = db.Acerto.Find(id);

            if (acertoInDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            db.Acerto.Remove(acertoInDB);
            db.SaveChanges();
        }
    }
}
