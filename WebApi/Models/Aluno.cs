using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace WebApi.Models
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
        public string Data { get; set; }
        public int Ra { get; set; }


        public List<Aluno> ListarAlunos(int? id = null)
        {
            try
            {
                var alunoDao = new AlunoDAO();
                return alunoDao.ListarAlunosDB(id);
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao listar alunos: Erro => {ex.Message}");
            }
        }

        public bool ReescreverArquivo(List<Aluno> listaAlunos)
        {
            var pathFile = HostingEnvironment.MapPath(@"~/App_Data/Base.json");
            var json = JsonConvert.SerializeObject(listaAlunos, Formatting.Indented);
            File.WriteAllText(pathFile, json);

            return true;
        }

        public void Inserir(Aluno aluno)
        {
            try
            {
                var alunoDao = new AlunoDAO();
                alunoDao.InserirAlunoDB(aluno);
            }
            catch (Exception ex )
            {

                throw new Exception($"Erro ao inserir aluno: Erro => {ex.Message}");
            }
        }

        public void Atualizar( Aluno aluno)
        {
 
            try
            {
                var alunoDao = new AlunoDAO();
                alunoDao.AtualizarAlunoDB(aluno);
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao atualizar aluno: Erro => {ex.Message}");
            }
        }

        public void Deletar(int id)
        {
            try
            {
                var alunoDao = new AlunoDAO();
                alunoDao.DeletarAlunoDb(id);
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao deletar aluno: Erro => {ex.Message}");
            }
        }
    }
}