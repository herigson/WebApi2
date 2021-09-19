using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    
    public class AlunoDAO
    {
        private string stringConexao = ConfigurationManager.ConnectionStrings["ConnectionDev"].ConnectionString;
        private IDbConnection conexao;
        

        public AlunoDAO()
        {
            conexao = new SqlConnection(stringConexao);
            conexao.Open();
            
        }

        public List<Aluno> ListarAlunosDB()
        {
            var listaAlunos = new List<Aluno>();

            IDbCommand selectCmd = conexao.CreateCommand();
            selectCmd.CommandText = "select * from alunos";

            IDataReader resultado = selectCmd.ExecuteReader();
            while (resultado.Read())
            {
                var aluno = new Aluno();
                aluno.Id = Convert.ToInt32(resultado["Id"]);
                aluno.Nome = resultado["nome"].ToString();
                aluno.Sobrenome = resultado["sobrenome"].ToString();
                aluno.Telefone = resultado["telefone"].ToString();
                aluno.Ra = Convert.ToInt32(resultado["ra"]);

                listaAlunos.Add(aluno);

            }
            conexao.Close();

            return listaAlunos;
        }

        public void InserirAlunoDB(Aluno aluno)
        {
            IDbCommand insertCmd = conexao.CreateCommand();
            insertCmd.CommandText = "insert into Alunos (nome, sobrenome, telefone, ra) values (@nome, @sobrenome, @telefone, @ra)";

            IDbDataParameter paramNome = new SqlParameter("nome", aluno.Nome);
            insertCmd.Parameters.Add(paramNome);

            IDbDataParameter paramSobrenome = new SqlParameter("sobrenome", aluno.Sobrenome);
            insertCmd.Parameters.Add(paramSobrenome);

            IDbDataParameter paramTelefone = new SqlParameter("telefone", aluno.Telefone);
            insertCmd.Parameters.Add(paramTelefone);

            IDbDataParameter paramRa = new SqlParameter("ra", aluno.Ra);
            insertCmd.Parameters.Add(paramRa);

            insertCmd.ExecuteNonQuery();


        }

        public void AtualizarAlunoDB(Aluno aluno)
        {
            IDbCommand updateCmd = conexao.CreateCommand();
            updateCmd.CommandText = "update Alunos set nome = @nome, sobrenome = @sobrenome, telefone = @telefone, ra = @ra where Id = @id";

            IDbDataParameter paramNome = new SqlParameter("nome", aluno.Nome);
            IDbDataParameter paramSobrenome = new SqlParameter("sobrenome", aluno.Sobrenome);
            IDbDataParameter paramTelefone = new SqlParameter("telefone", aluno.Telefone);
            IDbDataParameter paramRa = new SqlParameter("ra", aluno.Ra);
            IDbDataParameter paramId = new SqlParameter("id", aluno.Id);
            updateCmd.Parameters.Add(paramNome);
            updateCmd.Parameters.Add(paramSobrenome);
            updateCmd.Parameters.Add(paramTelefone);
            updateCmd.Parameters.Add(paramRa);
            updateCmd.Parameters.Add(paramId);

            updateCmd.ExecuteNonQuery();
        }

        public void DeletarAlunoDb(int id)
        {
            IDbCommand deleteCmd = conexao.CreateCommand();
            deleteCmd.CommandText = "delete from Alunos where Id = @id";

            IDbDataParameter paramId = new SqlParameter("id", id);
            deleteCmd.Parameters.Add(paramId);

            deleteCmd.ExecuteNonQuery();

        }
    }
}