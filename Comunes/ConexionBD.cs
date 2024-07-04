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

        public static Usuario GetUsuarios(int id)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = abrirConexion();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "SP_GET_USUARIO";
            cmd.Parameters.AddWithValue("PI_ID_USUARIO", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
            return fillUsuarios(ds)[0];
        }

        public static void PostUsuario(Usuario objUsuario)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = abrirConexion();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "SP_INS_USUARIO";
            cmd.Parameters.AddWithValue("PV_CODIGO", objUsuario.codigo);
            cmd.Parameters.AddWithValue("PV_NOMBRES", objUsuario.nombres);
            cmd.Parameters.AddWithValue("PV_APELLIDOS", objUsuario.apellidos);
            cmd.Parameters.AddWithValue("PV_MAIL", objUsuario.mail);
            cmd.Parameters.AddWithValue("PD_FECHA_NACIMIENTO", objUsuario.fecha_nacimiento);
            cmd.Parameters.AddWithValue("PV_CONTRASENA", objUsuario.contrasena);
            cmd.Parameters.AddWithValue("PI_USUARIO_CREACION", objUsuario.usuario_creacion);

            cmd.ExecuteNonQuery();
        
        }

        public static void PutUsuario(int usuarioModificacion, Usuario objUsuario)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = abrirConexion();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "SP_UPD_USUARIO";
            cmd.Parameters.AddWithValue("PI_ID_USUARIO", objUsuario.id_usuario);
            cmd.Parameters.AddWithValue("PV_CODIGO", objUsuario.codigo);
            cmd.Parameters.AddWithValue("PV_NOMBRES", objUsuario.nombres);
            cmd.Parameters.AddWithValue("PV_APELLIDOS", objUsuario.apellidos);
            cmd.Parameters.AddWithValue("PV_MAIL", objUsuario.mail);
            cmd.Parameters.AddWithValue("PD_FECHA_NACIMIENTO", objUsuario.fecha_nacimiento);
            cmd.Parameters.AddWithValue("PV_CONTRASENA", objUsuario.contrasena);
            cmd.Parameters.AddWithValue("PI_USUARIO_MODIFICACION", usuarioModificacion);

            cmd.ExecuteNonQuery();
        }

        public static void DeleteUsuario(int idUsuario, int idUsuarioModificacion)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = abrirConexion();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "SP_DEL_USUARIO";
            cmd.Parameters.AddWithValue("PI_ID_USUARIO", idUsuario);
            cmd.Parameters.AddWithValue("PI_USUARIO_MODIFICACION", idUsuarioModificacion);

            cmd.ExecuteNonQuery();
        }

        private static List<Usuario> fillUsuarios(DataSet ds)
        {
            List<Usuario> lrespuesta = new List<Usuario>();
            Usuario objUsuario= new Usuario();
            for (int i = 0; i <= ds.Tables[0].Rows.Count-1; i++)
            {
                objUsuario = new Usuario();
                objUsuario.id_usuario = Convert.ToInt32(ds.Tables[0].Rows[i]["ID_USUARIO"].ToString());
                objUsuario.codigo = ds.Tables[0].Rows[i]["CODIGO"].ToString();
                objUsuario.nombres = ds.Tables[0].Rows[i]["NOMBRES"].ToString();
                objUsuario.apellidos = ds.Tables[0].Rows[i]["APELLIDOS"].ToString();
                objUsuario.mail = ds.Tables[0].Rows[i]["MAIL"].ToString();
                objUsuario.fecha_nacimiento = Convert.ToDateTime(ds.Tables[0].Rows[i]["FECHA_NACIMIENTO"].ToString());
                objUsuario.contrasena = ds.Tables[0].Rows[i]["CONTRASENA"].ToString();
                objUsuario.estado = ds.Tables[0].Rows[i]["ESTADO"].ToString();
                objUsuario.fecha_ultima_conexion = Convert.ToDateTime(ds.Tables[0].Rows[i]["FECHA_ULTIMA_CONEXION"].ToString());
                objUsuario.usuario_creacion = Convert.ToInt32(ds.Tables[0].Rows[i]["USUARIO_CREACION"].ToString());
                objUsuario.fecha_creacion = Convert.ToDateTime(ds.Tables[0].Rows[i]["FECHA_CREACION"].ToString());
                objUsuario.usuario_modificacion = Convert.ToInt32(ds.Tables[0].Rows[i]["USUARIO_MODIFICACION"].ToString());
                objUsuario.fecha_modificacion = Convert.ToDateTime(ds.Tables[0].Rows[i]["FECHA_MODIFICACION"].ToString());
                lrespuesta.Add(objUsuario);
            }
            return lrespuesta; 
        }
    }
}
