using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        public List<Aluno> ListarAlunos()
        {
            var pathFile = HostingEnvironment.MapPath(@"~/App_Data/Base.json");
            var json = System.IO.File.ReadAllText(pathFile);

            var listaAlunos = JsonConvert.DeserializeObject<List<Aluno>>(json);

            return listaAlunos;
        }

        public bool ReescreverArquivo(List<Aluno> listaAlunos)
        {
            var pathFile = HostingEnvironment.MapPath(@"~/App_Data/Base.json");
            var json = JsonConvert.SerializeObject(listaAlunos, Formatting.Indented);
            File.WriteAllText(pathFile, json);

            return true;
        }

        public Aluno Inserir(Aluno aluno)
        {
            var listaAlunos = this.ListarAlunos();
            var maxId = listaAlunos.Max(aln => aln.Id);
            aluno.Id = maxId + 1;
            listaAlunos.Add(aluno);

            ReescreverArquivo(listaAlunos);

            return aluno;
        }

        public Aluno Atualizar(int id, Aluno aluno)
        {
            var listaAlunos = this.ListarAlunos();

            var itemIndex = listaAlunos.FindIndex(a => a.Id == aluno.Id);
            if (itemIndex >= 0)
            {
                aluno.Id = id;
                listaAlunos[itemIndex] = aluno;
            }
            else
                return null;
            ReescreverArquivo(listaAlunos);

            return aluno;
        }

        public bool Deletar(int id)
        {
            var listaAlunos = this.ListarAlunos();
            var itemIndex = listaAlunos.FindIndex(a => a.Id == id);

            if (itemIndex >= 0)
                listaAlunos.RemoveAt(itemIndex);
            else
                return false;

            ReescreverArquivo(listaAlunos);
            return true;
        }
    }
}