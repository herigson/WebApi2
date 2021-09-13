using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApi.Models;

namespace WebApi.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/Aluno")]
    public class AlunoController : ApiController
    {
        // GET: api/Aluno
        [HttpGet]
        [Route("Recuperar")]
        public IHttpActionResult Recuperar()
        {
            try
            {
                Aluno aluno = new Aluno();

                return Ok(aluno.ListarAlunos());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        [HttpGet]
        [Route("Recuperar/{id:int}")]
        // GET: api/Aluno/5
        public Aluno Get(int id)
        {
            Aluno aluno = new Aluno();

            return aluno.ListarAlunos().Where(x => x.Id == id).FirstOrDefault();
        }
        [HttpGet]
        [Route(@"RecuperarPorDataNome/{data:regex([0-9]{4}\-[0-9]{2})}/{nome:minlength(5)}")]
        // GET: api/Aluno/5
        public IHttpActionResult Recuperar(string data, string nome)
        {
            try
            {
                Aluno aluno = new Aluno();
                IEnumerable<Aluno> alunos = aluno.ListarAlunos().Where(x => x.Nome == nome || x.Data == data);

                if (!alunos.Any())
                    return NotFound();

                return Ok(alunos);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }

        }

        // POST: api/Aluno
        public List<Aluno> Post(Aluno aluno)
        {
            Aluno _aluno = new Aluno();

            _aluno.Inserir(aluno);


            return _aluno.ListarAlunos();
        }

        // PUT: api/Aluno/5
        public Aluno Put(int id, [FromBody] Aluno aluno)
        {
            Aluno _aluno = new Aluno();

            return _aluno.Atualizar(id, aluno);
        }

        // DELETE: api/Aluno/5
        public void Delete(int id)
        {
            Aluno _aluno = new Aluno();

            _aluno.Deletar(id);
        }
    }
}
