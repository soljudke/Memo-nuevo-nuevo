using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
namespace Memotest
{
    class Tarjetas
    {
        public string foto { get; set; }
        public int id { get; set; }
        public static int Actual { get; set; }
        public int pareja{ get; set; }
        public int nivel{ get; set; }
        private OleDbConnection Conn;
        public void AbrirConexion()
        {
            Conn = new OleDbConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Memotest.Properties.Settings.Database21ConnectionString"].ConnectionString);
            Conn.Open();
        }
        public List<Tarjetas> Traemelos()
        {
            AbrirConexion();
            OleDbCommand Consulta = Conn.CreateCommand();
            Consulta.CommandType = CommandType.StoredProcedure;
            Consulta.CommandText = "Traeme_Tarjetas";
            OleDbDataReader traido = Consulta.ExecuteReader();
            List<Tarjetas> lista = new List<Tarjetas>();
            while (traido.Read())
            {
                Tarjetas tarje = new Tarjetas();
                tarje.id = Convert.ToInt32(traido["Id"]);
                tarje.pareja= Convert.ToInt32(traido["Pareja"]);
                tarje.foto = traido["Foto"].ToString();
                tarje.nivel = Convert.ToInt32(traido["Nivel"]);
                lista.Add(tarje);
            }
            Conn.Close();
            return lista;
        }
        public List<Tarjetas> Traemelos2()
        {
            AbrirConexion();
            OleDbCommand Consulta = Conn.CreateCommand();
            Consulta.CommandType = CommandType.StoredProcedure;
            Consulta.CommandText = "Traeme_Tarjetas2";
            OleDbDataReader traido = Consulta.ExecuteReader();
            List<Tarjetas> lista = new List<Tarjetas>();
            while (traido.Read())
            {
                Tarjetas tarje = new Tarjetas();
                tarje.id = Convert.ToInt32(traido["Id"]);
                tarje.pareja = Convert.ToInt32(traido["Pareja"]);
                tarje.foto = traido["Foto"].ToString();
                tarje.nivel = Convert.ToInt32(traido["Nivel"]);
                lista.Add(tarje);
            }
            Conn.Close();
            return lista;
        }
        public List<Tarjetas> Traemelos3()
        {
            AbrirConexion();
            OleDbCommand Consulta = Conn.CreateCommand();
            Consulta.CommandType = CommandType.StoredProcedure;
            Consulta.CommandText = "Traeme_Tarjetas3";
            OleDbDataReader traido = Consulta.ExecuteReader();
            List<Tarjetas> lista = new List<Tarjetas>();
            while (traido.Read())
            {
                Tarjetas tarje = new Tarjetas();
                tarje.id = Convert.ToInt32(traido["Id"]);
                tarje.pareja = Convert.ToInt32(traido["Pareja"]);
                tarje.foto = traido["Foto"].ToString();
                tarje.nivel = Convert.ToInt32(traido["Nivel"]);
                lista.Add(tarje);
            }
            Conn.Close();
            return lista;
        }
    }
}
