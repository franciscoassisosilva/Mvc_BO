using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace BLL
{
    public class AlunoBLL
    {
        private static string _strConexao = ConfigurationManager.ConnectionStrings["conSQLServer"].ConnectionString;

        public IList<Aluno> getAlunos()
        {
            IList<Aluno> lista = new List<Aluno>();

            using (SqlConnection conn = new SqlConnection(_strConexao))
            {
                SqlCommand cmd = new SqlCommand("GET_ALUNOS", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
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
            
            return lista;
        }
    }
}
