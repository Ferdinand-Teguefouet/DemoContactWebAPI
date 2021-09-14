using System;
using System.Collections.Generic;

namespace ADOLibrary
{
    public class Command
    {   // Les différents champs
        internal readonly string _query;
        internal readonly Dictionary<string, object> _parameters;
        internal readonly bool _isStoredProcedure;

        // constructeur de la classe "Command"
        public Command(string query, bool isStoredProcedure = false)
        {
            _query = query;
            _isStoredProcedure = isStoredProcedure;
            _parameters = new Dictionary<string, object>();
        }

        // Méthode 
        public void AddParameter(string key, object value)
        {
            // On fait un ternaire pour tester si la valeur est nulle
            // C# ne comprend pas le null qui provient de la BDD
            _parameters.Add(key, value is null ? DBNull.Value : value);
        }
    }
}
