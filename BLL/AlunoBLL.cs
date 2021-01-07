﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace BLL
{
    public class AlunoBLL
    {
        private static string _strConexao = ConfigurationManager.ConnectionStrings["conSQLServer"].ConnectionString;

        public IList<Aluno> GetAlunos()
        {
            IList<Aluno> lista = new List<Aluno>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_strConexao))
                {                    
                    SqlCommand cmd = new SqlCommand("GET_ALUNOS", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Aluno aluno = new Aluno()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Nome = reader["Nome"].ToString(),
                            Email = reader["Email"].ToString(),
                            Idade = Convert.ToInt32(reader["Idade"]),
                            DataInscricao = Convert.ToDateTime(reader["DataInscricao"]),
                            Sexo = reader["Sexo"].ToString()
                        };

                        lista.Add(aluno);
                    }
                }
            }
            catch
            {                
                throw;
            }
            
            return lista;
        }

        public void InserirAluno(Aluno aluno)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_strConexao))
                {
                    SqlCommand cmd = new SqlCommand("INCLUIR_ALUNO", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter paramNome = new SqlParameter();
                    paramNome.ParameterName = "@Nome";                    
                    paramNome.Value = aluno.Nome;
                    cmd.Parameters.Add(paramNome);

                    SqlParameter paramEmail = new SqlParameter();
                    paramEmail.ParameterName = "@Email";
                    paramEmail.Value = aluno.Email;
                    cmd.Parameters.Add(paramEmail);

                    SqlParameter paramIdade = new SqlParameter();
                    paramIdade.ParameterName = "@Idade";
                    paramIdade.Value = aluno.Idade;
                    cmd.Parameters.Add(paramIdade);

                    SqlParameter paramDataInscricao = new SqlParameter();
                    paramDataInscricao.ParameterName = "@DataInscricao";
                    paramDataInscricao.Value = aluno.DataInscricao;
                    cmd.Parameters.Add(paramDataInscricao);

                    cmd.Parameters.Add("@Sexo", SqlDbType.Char).Value = aluno.Sexo;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
        }
    
        
    }
}
