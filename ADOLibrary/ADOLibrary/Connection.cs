using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ADOLibrary
{
    public class Connection
    {
        // déclaration d'un champ pour la connectionString
        private readonly string _connectionString;

        // Constructeur qui prend en paramètre une connectionString
        public Connection(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Méthode générique (Qui peut traiter tout type d'objet) qui retourne un IEnumérable
        // "Func<SqlDataReader, TEntity> convert" est un delegate qui permettra de faire des conversions
        public IEnumerable<TEntity> ExecuteReader<TEntity>(Command cmd, Func<SqlDataReader, TEntity> convert)
        {
            using(SqlConnection c = createConnection())
            {
                using(SqlCommand command = createCommand(cmd, c))
                {
                    c.Open();
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {                        
                        while (dataReader.Read())
                        {
                            yield return convert(dataReader);
                        }
                    }
                }
            }
        }

        // Méthode permettant de récupérer une valeur(ex: la donnée d'un champ spécifique)
        public object ExecuteScalar(Command cmd)
        {
            using(SqlConnection c = createConnection())
            {
                using (SqlCommand command = createCommand(cmd, c))
                {
                    c.Open();
                    object O = command.ExecuteScalar();
                    return (O is DBNull) ? null : O; 
                }
            }
        }

        // Méthode permettant de créer un enregistrement dans la BDD
        public int ExecuteNonQuery(Command cmd)
        {
            using (SqlConnection c = createConnection())
            {
                using (SqlCommand command = createCommand(cmd, c))
                {
                    c.Open();
                    return command.ExecuteNonQuery();  
                }
            }
        }

        // Méthode utilisée uniquement dans cette classe et qui retourne un objet de type SqlConnection
        private SqlConnection createConnection()
        {
            return new SqlConnection(_connectionString);
        }

        // méthode permettant de retourner un objet de type SqlCommand
        private SqlCommand createCommand(Command cmd, SqlConnection connection)
        {
            SqlCommand sqlCmd = connection.CreateCommand();
            sqlCmd.CommandText = cmd._query;
            // Les procédures stockées pour plutard
            sqlCmd.CommandType = cmd._isStoredProcedure ? System.Data.CommandType.StoredProcedure : System.Data.CommandType.Text;
            // Teste si cmd._parameter (dictionaire constitué où chaque clé correspond à un objet) n'est pas null
            if (!(cmd._parameters is null))
            {
                // On parcourt le dictionaire avec une boucle foreach
                foreach (KeyValuePair<string, object> param in cmd._parameters)
                {
                    // pour chaque élément, on crée une instance de type SqlParameter
                    SqlParameter parameter = sqlCmd.CreateParameter();
                    // on passe à l'objet "parameter" la clé de l'élément
                    parameter.ParameterName = param.Key;
                    // on passe à l'objet "parameter" la valeur de l'élément
                    parameter.Value = param.Value;
                    // On ajoute (Add(parameter)) l'objet "parameter" à la collection 
                    sqlCmd.Parameters.Add(parameter);                  
                }
            }
            // on retourne l'objet SqlCommand
            return sqlCmd;
        }
    }
}
