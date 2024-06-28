using _01Usuarios___EMANOSALVAS.Modelos;
using System.Data;
using System.Data.SqlClient;

namespace _01Usuarios___EMANOSALVAS.Comunes
{
    public static class ConexionBD
    {
        public static SqlConnection conexion;

       public static SqlConnection abrirConexion()
        {
            //conexion = new SqlConnection("Server=LT-EMANOSALVAS\\SQLEXPRESS;Database=PROYECTO_1;Trusted_Connection=True;");
            conexion = new SqlConnection("Server=LT-EMANOSALVAS\\SQLEXPRESS;Database=PROYECTO_1;User Id=sa;Password=Representaciones.2024;");
            conexion.Open();
            return conexion;
        }

        public static List<Usuario> GetUsuarios()
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = abrirConexion();
            cmd.CommandType= System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "SP_GET_USUARIOS";

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
            return fillUsuarios(ds);

        }

        private static List<Usuario> fillUsuarios(DataSet ds)
        {
            List<Usuario> lrespuesta = new List<Usuario>();
            Usuario objUsuario= new Usuario();
            for (int i = 0; i <= ds.Tables[0].Rows.Count-1; i++)
            {
                objUsuario = new Usuario();
                objUsuario.id_usuario =Convert.ToInt32(ds.Tables[0].Rows[i]["ID_USUARIO"].ToString());
                objUsuario.codigo = ds.Tables[0].Rows[i]["CODIGO"].ToString(); 
                objUsuario.nombres = ds.Tables[0].Rows[i]["NOMBRES"].ToString();
                objUsuario.apellidos = ds.Tables[0].Rows[i]["APELLIDOS"].ToString();
                lrespuesta.Add(objUsuario);
            }
            return lrespuesta; 
        }
    }
}
