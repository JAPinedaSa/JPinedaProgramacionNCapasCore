using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Poliza
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnection()))
                {
                    string query = "PolizaGetAll";
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable tablaPoliza = new DataTable("Tabla de Polizas");
                            da.Fill(tablaPoliza);

                            if (tablaPoliza.Rows.Count > 0)
                            {
                                result.Objects = new List<object>();
                                foreach (DataRow row in tablaPoliza.Rows)
                                {
                                    ML.Poliza poliza = new ML.Poliza();

                                    poliza.IdPoliza = int.Parse(row[0].ToString());
                                    poliza.Nombre = row[1].ToString();
                                    poliza.SubPoliza = new ML.SubPoliza();
                                    poliza.SubPoliza.IdSubPoliza = byte.Parse(row[2].ToString());
                                    poliza.SubPoliza.Nombre = row[3].ToString();
                                    poliza.NumeroPoliza = row[4].ToString();
                                    poliza.FechaCreacion = row[5].ToString();
                                    poliza.FechaModificacion = row[6].ToString();
                                    poliza.Usuario = new ML.Usuario();
                                    poliza.Usuario.IdUsuario = int.Parse(row[7].ToString());
                                    poliza.Usuario.Nombre = row[8].ToString();

                                    result.Objects.Add(poliza);
                                }
                                result.Correct = true;
                            }
                            else
                            {
                                result.Correct = false;
                            }
                        }
                        cmd.Connection.Open();
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;

        }

        public static ML.Result Add(ML.Poliza poliza)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnection()))
                {
                    string query = "PolizaAdd";
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;
                        cmd.CommandType = CommandType.StoredProcedure;

                       // Arreglo para almacenar los datos que fueron solicitados
                        SqlParameter[] collection = new SqlParameter[4];

                        collection[0] = new SqlParameter("Nombre", SqlDbType.VarChar);
                        collection[0].Value = poliza.Nombre;


                        collection[1] = new SqlParameter("NombrePoliza", SqlDbType.VarChar);
                        collection[1].Value = poliza.SubPoliza.Nombre;

                        collection[2] = new SqlParameter("NumeroPoliza", SqlDbType.VarChar);
                        collection[2].Value = poliza.NumeroPoliza;

                        collection[3] = new SqlParameter("IdUsuario", SqlDbType.VarChar);
                        collection[3].Value = poliza.Usuario.IdUsuario;

                        cmd.Parameters.AddRange(collection);

                        cmd.Connection.Open();

                        int RowsAffected = cmd.ExecuteNonQuery(); //0 -no se insertó //>=1 se insertó

                        if (RowsAffected >= 1)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Ocurrió un error al ingresar la materia";
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;


        }

        public static ML.Result GetById(int idPoliza)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnection()))
                {
                    string query = "PolizaGetById";
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;
                        cmd.CommandType = CommandType.StoredProcedure;


                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("IdPoliza", SqlDbType.VarChar);
                        collection[0].Value = idPoliza;

                        cmd.Parameters.AddRange(collection);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable tablaPoliza = new DataTable("Tabla Poliza");

                            da.Fill(tablaPoliza);

                            if (tablaPoliza.Rows.Count > 0)
                            {

                                DataRow row = tablaPoliza.Rows[0];
                                ML.Poliza poliza = new ML.Poliza();
                               // Luego para agregar la nueva fila al DataTable, utilizamos la fila y el nombre de la columna.
                                poliza.IdPoliza = int.Parse(row[0].ToString());
                                poliza.Nombre = row[1].ToString();
                                poliza.SubPoliza = new ML.SubPoliza();
                                poliza.SubPoliza.IdSubPoliza = byte.Parse(row[2].ToString());
                                poliza.SubPoliza.Nombre = row[3].ToString();
                                poliza.NumeroPoliza = row[4].ToString();
                                poliza.FechaCreacion = row[5].ToString();
                                poliza.FechaModificacion = row[6].ToString();
                                poliza.Usuario = new ML.Usuario();
                                poliza.Usuario.IdUsuario = int.Parse(row[7].ToString());
                                poliza.Usuario.Nombre = row[8].ToString();


                                result.Object = poliza;


                                result.Correct = true;
                            }
                            else
                            {
                                result.Correct = false;
                                result.ErrorMessage = "No jalo este pedo banda ";
                            }


                        }

                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;

        }

        public static ML.Result Update(ML.Poliza poliza)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnection()))
                {
                    string query = "PolizaUpdate";
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Arreglo para almacenar los datos que fueron solicitados
                        SqlParameter[] collection = new SqlParameter[5];

                        collection[0] = new SqlParameter("IdPoliza", SqlDbType.VarChar);
                        collection[0].Value = poliza.IdPoliza;

                        collection[1] = new SqlParameter("Nombre", SqlDbType.VarChar);
                        collection[1].Value = poliza.Nombre;


                        collection[2] = new SqlParameter("NombrePoliza", SqlDbType.VarChar);
                        collection[2].Value = poliza.SubPoliza.Nombre;

                        collection[3] = new SqlParameter("NumeroPoliza", SqlDbType.VarChar);
                        collection[3].Value = poliza.NumeroPoliza;

                        collection[4] = new SqlParameter("IdUsuario", SqlDbType.VarChar);
                        collection[4].Value = poliza.Usuario.IdUsuario;


                        cmd.Parameters.AddRange(collection);

                        cmd.Connection.Open();

                        int RowsAffected = cmd.ExecuteNonQuery(); //0 -no se insertó //>=1 se insertó

                        if (RowsAffected >= 1)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Ocurrió un error al modificar el usuario";
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;

        }



    }
}
