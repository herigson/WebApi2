using App.Domain;
using App.Repository;
using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public class AlunoModel
    {

        public List<AlunoDTO> ListarAlunos(int? id = null)
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

        public void Inserir(AlunoDTO aluno)
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

        public void Atualizar(AlunoDTO aluno)
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