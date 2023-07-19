using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.Data.SqlClient;

namespace BL
{
    public class Empleado
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.UsuarioContext context = new DL.UsuarioContext())
                {
                    var query = context.Empleados.FromSqlRaw("EmpleadoGetAll").ToList();
                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.Empleado empleado = new ML.Empleado();
                            empleado.NumeroEmpleado = obj.NumeroEmpleado;
                            empleado.Nombre = obj.Nombre;
                            empleado.ApellidoPaterno = obj.ApellidoPaterno;
                            empleado.ApellidoMaterno = obj.ApellidoMaterno;
                            empleado.RFC = obj.Rfc;
                            empleado.Correo = obj.Correo;
                            empleado.Telefono = obj.Telefono;
                            empleado.FechaNacimiento = obj.FechaNacimiento.ToString();
                            empleado.NSS = obj.Nss;
                            empleado.FechaRegistro = obj.FechaRegistro.ToString();
                            empleado.Foto = obj.Foto;
                            empleado.Empresa = new ML.Empresa();
                            empleado.Empresa.IdEmpresa = obj.IdEmpresa.Value;
                            empleado.Empresa.Nombre = obj.NombreEmpresa;
                            result.Objects.Add(empleado);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result GetById(string NumeroEmpleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.UsuarioContext context = new DL.UsuarioContext())
                {
                    var query = context.Empleados.FromSqlRaw($"EmpleadoGetById {NumeroEmpleado}").AsEnumerable().FirstOrDefault();

                    if (query != null)
                    {
                        result.Objects = new List<object>();

                        ML.Empleado empleado = new ML.Empleado();
                        empleado.NumeroEmpleado = query.NumeroEmpleado;
                        empleado.Nombre = query.Nombre;
                        empleado.ApellidoPaterno = query.ApellidoPaterno;
                        empleado.ApellidoMaterno = query.ApellidoMaterno;
                        empleado.RFC = query.Rfc;
                        empleado.Correo = query.Correo;
                        empleado.Telefono = query.Telefono;
                        empleado.FechaNacimiento = query.FechaNacimiento.ToString();
                        empleado.NSS = query.Nss;
                        empleado.FechaRegistro = query.FechaRegistro.ToString();
                        empleado.Foto = query.Foto;
                        empleado.Empresa = new ML.Empresa();
                        empleado.Empresa.IdEmpresa = query.IdEmpresa.Value;
                        empleado.Empresa.Nombre = query.NombreEmpresa;

                        result.Object = empleado;

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;

            }

            return result;


        }

        public static ML.Result Add(ML.Empleado empleado)
        {

            ML.Result result = new ML.Result();
            try
            {
                using (DL.UsuarioContext context = new DL.UsuarioContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"EmpleadoAdd '{empleado.NumeroEmpleado}','{empleado.RFC}'," +
                        $"'{empleado.Nombre}','{empleado.ApellidoPaterno}','{empleado.ApellidoMaterno}','{empleado.Correo}','{empleado.Telefono}'," +
                        $"'{empleado.FechaNacimiento}','{empleado.NSS}','{empleado.Foto}',{empleado.Empresa.IdEmpresa}");

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al ingresar el empleado";
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

        public static ML.Result Update(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.UsuarioContext context = new DL.UsuarioContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"EmpleadoUpdate '{empleado.NumeroEmpleado}','{empleado.RFC}'," +
                        $"'{empleado.Nombre}','{empleado.ApellidoPaterno}','{empleado.ApellidoMaterno}','{empleado.Correo}','{empleado.Telefono}'," +
                        $"'{empleado.FechaNacimiento}','{empleado.NSS}','{empleado.Foto}',{empleado.Empresa.IdEmpresa}");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al ingresar el empleado";
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

        public static ML.Result Delete(string NumeroEmpleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.UsuarioContext context = new DL.UsuarioContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"EmpleadoDelete '{NumeroEmpleado}'");
                    //int query = contex.UsuarioDelete(empleado.IdUsuario);
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al eliminar al Empleado";
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

        public static ML.Result ConvertExcelToDataTable(string connectionString)
        {
            ML.Result result = new ML.Result();
            

            try
            {
                using (OleDbConnection context = new OleDbConnection(connectionString))
                {
                    string query = "SELECT * FROM [CargaMasiva$]";
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                     
                        cmd.CommandText = query;
                        cmd.Connection = context;
                        
                        OleDbDataAdapter da = new OleDbDataAdapter();
                        da.SelectCommand = cmd;
                        
                        DataTable tableEmpleado = new DataTable();
                        da.Fill(tableEmpleado);

                        if (tableEmpleado.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();
                            
                            foreach (DataRow row in tableEmpleado.Rows)
                            {
                                
                                ML.Empleado empleado = new ML.Empleado();
                                empleado.NumeroEmpleado = row[0].ToString();
                                empleado.RFC = row[1].ToString();
                                empleado.Nombre = row[2].ToString();
                                empleado.ApellidoPaterno = row[3].ToString(); 
                                empleado.ApellidoMaterno = row[4].ToString(); 
                                empleado.Correo = row[5].ToString(); 
                                empleado.Telefono = row[6].ToString(); 
                                empleado.FechaNacimiento = row[7].ToString(); 
                                empleado.NSS = row[8].ToString(); 
                                empleado.Empresa = new ML.Empresa();
                                empleado.Empresa.IdEmpresa = int.Parse(row[9].ToString());
                                empleado.Foto = "";

                                result.Objects.Add(empleado);
                            }
                            result.Correct = true;
                        }
                        result.Object = tableEmpleado;

                        if (tableEmpleado.Rows.Count > 1)
                        {
                            result.Correct = true;

                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No existen Registros dentro del Archivo";
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

        public static ML.Result ValidarExcel(List<object> Object)
        {
            ML.Result result = new ML.Result();

            try
            {
                result.Objects = new List<object>();
                //DataTable  //Rows //Columns
                int i = 1;
                foreach (ML.Empleado empleado in result.Objects)
                {
                    ML.ErrorExcel error = new ML.ErrorExcel();
                    error.IdRegistro = i++;

                    empleado.NumeroEmpleado = (empleado.NumeroEmpleado == "") ? error.Mensaje += "Ingresar el NumeroEmpleado  " : empleado.NumeroEmpleado;
                    empleado.RFC = (empleado.RFC == "") ? error.Mensaje += "Ingresar el RFC  " : empleado.RFC;
                    empleado.Nombre = (empleado.Nombre == "") ? error.Mensaje += "Ingresar el Nombre  " : empleado.Nombre;
                    empleado.ApellidoPaterno = (empleado.ApellidoPaterno == "") ? error.Mensaje += "Ingresar el Apellido Paterno  " : empleado.ApellidoPaterno;
                    empleado.ApellidoMaterno = (empleado.ApellidoMaterno == "") ? error.Mensaje += "Ingresar el Apellido Materno " : empleado.ApellidoMaterno;
                    empleado.Correo = (empleado.Correo == "") ? error.Mensaje += "Ingresar el Correo  " : empleado.Correo;
                    empleado.Telefono = (empleado.Telefono == "") ? error.Mensaje += "Ingresar el Telefono  " : empleado.Telefono;
                    empleado.FechaNacimiento = (empleado.FechaNacimiento == "") ? error.Mensaje += "Ingresar el FechaNacimiento  " : empleado.FechaNacimiento;
                    empleado.NSS = (empleado.NSS == "") ? error.Mensaje += "Ingresar el NSS  " : empleado.NSS;
                    //empleado.Empresa.IdEmpresa = (empleado.Empresa.IdEmpresa < 0) ? error.Mensaje += "Ingresar el NSS  " : empleado.NSS;

                    if (empleado.Empresa.IdEmpresa < 0)
                    {
                        error.Mensaje += "Ingresa El Id de la Empresa";
                    }

                    if (error.Mensaje != null)
                    {
                        result.Objects.Add(error);
                    }


                }
                result.Correct = true;
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
