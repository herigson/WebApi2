using App.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace App.Repository
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

        public List<AlunoDTO> ListarAlunosDB(int? id)
        {
            var listaAlunos = new List<AlunoDTO>();
            try
            {
                IDbCommand selectCmd = conexao.CreateCommand();

                if (id == null)
                    selectCmd.CommandText = "select * from alunos";
                else
                    selectCmd.CommandText = $"select * from alunos where id = {id}";

                IDataReader resultado = selectCmd.ExecuteReader();
                while (resultado.Read())
                {
                    var aluno = new AlunoDTO()
                    {
                        Id = Convert.ToInt32(resultado["Id"]),
                        Nome = resultado["nome"].ToString(),
                        Sobrenome = resultado["sobrenome"].ToString(),
                        Telefone = resultado["telefone"].ToString(),
                        Ra = Convert.ToInt32(resultado["ra"]),
                    };
                    listaAlunos.Add(aluno);

                }
                return listaAlunos;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        public void InserirAlunoDB(AlunoDTO aluno)
        {
            try
            {
                IDbCommand insertCmd = conexao.CreateCommand();
                insertCmd.CommandText = "insert into Alunos (nome, sobrenome, telefone, ra) values (@nome, @sobrenome, @telefone, @ra)";

                IDbDataParameter paramNome = new SqlParameter("nome", aluno.Nome);
                IDbDataParameter paramSobrenome = new SqlParameter("sobrenome", aluno.Sobrenome);
                IDbDataParameter paramTelefone = new SqlParameter("telefone", aluno.Telefone);
                IDbDataParameter paramRa = new SqlParameter("ra", aluno.Ra);

                insertCmd.Parameters.Add(paramNome);
                insertCmd.Parameters.Add(paramSobrenome);
                insertCmd.Parameters.Add(paramTelefone);
                insertCmd.Parameters.Add(paramRa);

                insertCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        public void AtualizarAlunoDB(AlunoDTO aluno)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }

        }

        public void DeletarAlunoDb(int id)
        {
            try
            {
                IDbCommand deleteCmd = conexao.CreateCommand();
                deleteCmd.CommandText = "delete from Alunos where Id = @id";

                IDbDataParameter paramId = new SqlParameter("id", id);
                deleteCmd.Parameters.Add(paramId);

                deleteCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }
    }
}