using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Aseguradora
    {
        public static  ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.UsuarioContext contex = new DL.UsuarioContext())
                {

                    var query =  contex.Aseguradoras.FromSqlRaw("AseguradoraGetAll").ToList();
                    if (query != null)
                    {
                        result.Objects = new List<object>();

                        foreach (var obj in query)
                        {
                            ML.Aseguradora aseguradora = new ML.Aseguradora();
                            aseguradora.IdAseguradora = obj.IdAseguradora;
                            aseguradora.Nombre = obj.Nombre;
                            aseguradora.FechaCreacion = obj.FechaCreacion.ToString();
                            aseguradora.FechaModificacion = obj.FechaModificacion.ToString();
                            aseguradora.Usuario = new ML.Usuario();
                            aseguradora.Usuario.IdUsuario = obj.IdUsuario;
                            aseguradora.Usuario.Nombre = obj.NombreUsuario;


                            result.Objects.Add(aseguradora);

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
        public static ML.Result GetById(int? idAseguradora)
        {
            ML.Result result = new ML.Result();

            try
            {

                using (DL.UsuarioContext context = new DL.UsuarioContext())
                {

                    var query = context.Aseguradoras.FromSqlRaw($"AseguradoraGetById {idAseguradora}").AsEnumerable().FirstOrDefault();
                   
                   // var query = context.Aseguradoras.FromSqlRaw("AseguradoraGetAll").ToList();
                    //var query = context.AseguradoraGetById(IdAseguradora).FirstOrDefault();

                    if (query != null)
                    {
                        ML.Aseguradora aseguradora = new ML.Aseguradora();

                        aseguradora.IdAseguradora = query.IdAseguradora;
                        aseguradora.Nombre = query.Nombre;
                        aseguradora.FechaCreacion = query.FechaCreacion.ToString();
                        aseguradora.FechaModificacion = query.FechaModificacion.ToString();
                        aseguradora.Usuario = new ML.Usuario();
                        aseguradora.Usuario.IdUsuario = query.IdUsuario;
                        aseguradora.Usuario.Nombre = query.NombreUsuario;

                        result.Object = aseguradora;

                        result.Correct = true;
                    }
                    else
                    {

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
        public static ML.Result Add(ML.Aseguradora aseguradora)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.UsuarioContext context = new DL.UsuarioContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"AseguradoraAdd '{aseguradora.Nombre}',{aseguradora.Usuario.IdUsuario}");
                   // int query = contex.AseguradoraAdd(aseguradora.Nombre, aseguradora.Usuario.IdUsuario);

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error en la inserción";
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
        public static ML.Result Update(ML.Aseguradora aseguradora)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.UsuarioContext context = new DL.UsuarioContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"AseguradoraUpdate {aseguradora.IdAseguradora},'{aseguradora.Nombre}', {aseguradora.Usuario.IdUsuario}");
                    //int query = context.AseguradoraUpdate(aseguradora.IdAseguradora, aseguradora.Nombre,aseguradora.Usuario.IdUsuario);

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio Un Error al Actualizar la Aseguradora";
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
        public static ML.Result Delete(int idAseguradora)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.UsuarioContext context = new DL.UsuarioContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"AseguradoraDelete {idAseguradora}");
                    //int query = contex.AseguradoraDelete(aseguradora.IdAseguradora);
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al eliminar el Usuario";
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
