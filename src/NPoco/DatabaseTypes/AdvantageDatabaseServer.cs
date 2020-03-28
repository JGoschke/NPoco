using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace NPoco.DatabaseTypes
{
    public class AdvantageDatabaseServer : DatabaseType
    {
        public override string GetParameterPrefix(string connectionString)
        {
            return ":";
        }
        private void AdjustSqlInsertCommandText(DbCommand cmd)
        {
            cmd.CommandText += ";\nSELECT LASTAUTOINC(CONNECTION) from system.iota;";
        }
        public override object ExecuteInsert<T>(Database db, DbCommand cmd, string primaryKeyName, bool useOutputClause, T poco, object[] args)
        {
            if (primaryKeyName != null)
            {
                AdjustSqlInsertCommandText(cmd);
                return db.ExecuteScalarHelper(cmd);
            }

            db.ExecuteNonQueryHelper(cmd);
            return -1;
        }

        public override string GetProviderName()
        {
            return "Advantage.Data.Provider";
        }
    }
}

