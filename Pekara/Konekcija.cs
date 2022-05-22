using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Pekara
{
    class Konekcija
    {
        static public SqlConnection Connect()
        {
            string CS;
            CS = @"Data Source=DESKTOP;Initial Catalog=Pekara;Integrated Security=True;MultipleActiveResultSets=True";
            SqlConnection veza = new SqlConnection(CS);
            return veza;
        }
    }
}
