using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Usuario
    {
        public static ML.Result GetAll(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.UsuarioContext contex = new DL.UsuarioContext())
                {
                    var query = contex.Usuarios.FromSqlRaw($"UsuarioGetAll '{usuario.Nombre}','{usuario.ApellidoPaterno}','{usuario.ApellidoMaterno}'").ToList();
                    //var query = contex.UsuarioGetAll().ToList();
                    if (query != null)
                    {
                        result.Objects = new List<object>();

                        foreach (var obj in query)
                        {
                            usuario = new ML.Usuario();
                            usuario.IdUsuario = obj.IdUsuario;
                            usuario.Nombre = obj.Nombre;
                            usuario.ApellidoPaterno = obj.ApellidoPaterno;
                            usuario.ApellidoMaterno = obj.ApellidoMaterno;
                            usuario.Correo = obj.Correo;
                            usuario.Rol = new ML.Rol();
                            usuario.Rol.IdRol = obj.IdRol;
                            usuario.UserName = obj.UserName;
                            usuario.FechaNacimiento = obj.FechaNacimiento.ToString("dd-MM-yyyy");
                            usuario.Sexo = obj.Sexo;
                            usuario.Telefono = obj.Telefono;
                            usuario.Celular = obj.Celular;
                            usuario.CURP = obj.Curp;
                            usuario.Password = obj.Password;
                            usuario.Imagen = obj.Imagen;
                            usuario.Estatus = obj.Estatus.Value;
                            //usuario.Imagen = obj.Estatus;


                            usuario.Direccion = new ML.Direccion();
                            usuario.Direccion.Calle = obj.Calle;

                            usuario.Direccion.Colonia = new ML.Colonia();
                            usuario.Direccion.Colonia.IdColonia = obj.IdColonia;
                            usuario.Direccion.Colonia.Nombre = obj.Colonia;

                            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuario.Direccion.Colonia.Municipio.IdMunicipio = obj.IdMunicipio;
                            usuario.Direccion.Colonia.Municipio.Nombre = obj.Municipio;


                            result.Objects.Add(usuario);

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

        public static ML.Result GetById(int idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {

                using (DL.UsuarioContext context = new DL.UsuarioContext())
                {

                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetById {idUsuario}").AsEnumerable().FirstOrDefault();
                    //var query = context.UsuarioGetById(IdUsuario).FirstOrDefault();
                    result.Object = new List<object>();

                    if (query != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();

                        usuario.IdUsuario = query.IdUsuario;
                        usuario.Nombre = query.Nombre;
                        usuario.ApellidoPaterno = query.ApellidoPaterno;
                        usuario.ApellidoMaterno = query.ApellidoMaterno;
                        usuario.Correo = query.Correo;
                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = query.IdRol;
                        usuario.UserName = query.UserName;
                        usuario.FechaNacimiento = query.FechaNacimiento.ToString();
                        usuario.Sexo = query.Sexo;
                        usuario.Telefono = query.Telefono;
                        usuario.Celular = query.Celular;
                        usuario.CURP = query.Curp;
                        usuario.Password = query.Password;
                        usuario.Imagen = query.Imagen;
                        usuario.Estatus = query.Estatus.Value;

                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.Calle = query.Calle;
                        usuario.Direccion.NumeroInterior = query.NumeroInterior;
                        usuario.Direccion.NumeroExterior = query.NumeroExterior;

                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.Nombre = query.Colonia;
                        usuario.Direccion.Colonia.IdColonia = query.IdColonia;

                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.Nombre = query.Municipio;
                        usuario.Direccion.Colonia.Municipio.IdMunicipio = query.IdMunicipio;

                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        usuario.Direccion.Colonia.Municipio.Estado.IdEstado = query.IdEstado;
                        usuario.Direccion.Colonia.Municipio.Estado.Nombre = query.Estado;

                        usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = query.IdPais;
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = query.Pais;

                        result.Object = usuario;

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Algo paso :(";
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

        public static ML.Result GetByUserName(string UserName)
        {
            ML.Result result = new ML.Result();
            try
            {

                using (DL.UsuarioContext context = new DL.UsuarioContext())
                {

                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetByIdUserName'{UserName}'").AsEnumerable().FirstOrDefault();
                    //var query = context.UsuarioGetById(IdUsuario).FirstOrDefault();
                    result.Object = new List<object>();

                    if (query != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();

                        usuario.UserName = query.UserName;
                        usuario.Password = query.Password;

                        //usuario.IdUsuario = query.IdUsuario;
                        //usuario.Nombre = query.Nombre;
                        //usuario.ApellidoPaterno = query.ApellidoPaterno;
                        //usuario.ApellidoMaterno = query.ApellidoMaterno;
                        //usuario.Correo = query.Correo;
                        //usuario.Rol = new ML.Rol();
                        //usuario.Rol.IdRol = query.IdRol;

                        //usuario.FechaNacimiento = query.FechaNacimiento.ToString();
                        //usuario.Sexo = query.Sexo;
                        //usuario.Telefono = query.Telefono;
                        //usuario.Celular = query.Celular;
                        //usuario.CURP = query.Curp;

                        //usuario.Imagen = query.Imagen;
                        //usuario.Estatus = query.Estatus.Value;

                        //usuario.Direccion = new ML.Direccion();
                        //usuario.Direccion.Calle = query.Calle;
                        //usuario.Direccion.NumeroInterior = query.NumeroInterior;
                        //usuario.Direccion.NumeroExterior = query.NumeroExterior;

                        //usuario.Direccion.Colonia = new ML.Colonia();
                        //usuario.Direccion.Colonia.Nombre = query.Colonia;
                        //usuario.Direccion.Colonia.IdColonia = query.IdColonia;

                        //usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        //usuario.Direccion.Colonia.Municipio.Nombre = query.Municipio;
                        //usuario.Direccion.Colonia.Municipio.IdMunicipio = query.IdMunicipio;

                        //usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        //usuario.Direccion.Colonia.Municipio.Estado.IdEstado = query.IdEstado;
                        //usuario.Direccion.Colonia.Municipio.Estado.Nombre = query.Estado;

                        //usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                        //usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = query.IdPais;
                        //usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = query.Pais;

                        result.Object = usuario;

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Algo paso :(";
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

        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {

                using (DL.UsuarioContext context = new DL.UsuarioContext())
                {
                    int RowsAffected = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.Nombre}','{usuario.ApellidoPaterno}','{usuario.ApellidoMaterno}'" +
                        $",'{usuario.Correo}', {usuario.Rol.IdRol} ,'{usuario.UserName}','{usuario.FechaNacimiento}','{usuario.Sexo}','{usuario.Telefono}'" +
                        $",'{usuario.Celular}','{usuario.CURP}','{usuario.Password}','{usuario.Imagen}','{usuario.Direccion.Calle}','{usuario.Direccion.NumeroInterior}'" +
                        $",'{usuario.Direccion.NumeroExterior}',{usuario.Direccion.Colonia.IdColonia}");
                    //int rowsAffected = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.Nombre}','{usuario.ApellidoPaterno}','{usuario.ApellidoPaterno}'," +
                    //    $"");
                    //int RowsAffected = contex.UsuarioAdd(usuario.Nombre, usuario.ApellidoPaterno,
                    //    usuario.ApellidoMaterno, usuario.Correo, usuario.Rol.IdRol,
                    //    usuario.UserName, usuario.FechaNacimiento, usuario.Sexo, usuario.Telefono,
                    //    usuario.Celular, usuario.CURP, usuario.Password, usuario.Imagen, usuario.Direccion.Calle, usuario.Direccion.NumeroInterior,
                    //    usuario.Direccion.NumeroExterior, usuario.Direccion.Colonia.IdColonia);

                    if (RowsAffected >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al ingresar el usuario";
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

        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.UsuarioContext context = new DL.UsuarioContext())
                {
                    //int RowsAffected = contex.UsuarioUpdate(usuario.IdUsuario, usuario.Nombre, usuario.ApellidoPaterno,
                    //    usuario.ApellidoMaterno, usuario.Correo, usuario.Rol.IdRol,
                    //    usuario.UserName, usuario.FechaNacimiento, usuario.Sexo, usuario.Telefono,
                    //    usuario.Celular, usuario.CURP, usuario.Password, usuario.Imagen, usuario.Direccion.Calle, usuario.Direccion.NumeroInterior,
                    //    usuario.Direccion.NumeroExterior, usuario.Direccion.Colonia.IdColonia);
                    int RowsAffected = context.Database.ExecuteSqlRaw($"UsuarioUpdate {usuario.IdUsuario},'{usuario.Nombre}','{usuario.ApellidoPaterno}','{usuario.ApellidoMaterno}'" +
                        $",'{usuario.Correo}', {usuario.Rol.IdRol} ,'{usuario.UserName}','{usuario.FechaNacimiento}','{usuario.Sexo}','{usuario.Telefono}'" +
                        $",'{usuario.Celular}','{usuario.CURP}','{usuario.Password}','{usuario.Imagen}','{usuario.Direccion.Calle}','{usuario.Direccion.NumeroInterior}'" +
                        $",'{usuario.Direccion.NumeroExterior}',{usuario.Direccion.Colonia.IdColonia}");

                    if (RowsAffected >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al Actualizar el usuario";
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

        public static ML.Result Delete(int idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.UsuarioContext context = new DL.UsuarioContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"UsuarioDelete {idUsuario}");
                    //int query = contex.UsuarioDelete(usuario.IdUsuario);
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

        public static ML.Result ChangeStatus(int idUsuario, bool status)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.UsuarioContext context = new DL.UsuarioContext())
                {
                    //int RowsAffected = contex.UsuarioUpdate(usuario.IdUsuario, usuario.Nombre, usuario.ApellidoPaterno,
                    //    usuario.ApellidoMaterno, usuario.Correo, usuario.Rol.IdRol,
                    //    usuario.UserName, usuario.FechaNacimiento, usuario.Sexo, usuario.Telefono,
                    //    usuario.Celular, usuario.CURP, usuario.Password, usuario.Imagen, usuario.Direccion.Calle, usuario.Direccion.NumeroInterior,
                    //    usuario.Direccion.NumeroExterior, usuario.Direccion.Colonia.IdColonia);
                    int RowsAffected = context.Database.ExecuteSqlRaw($"ChangeStatus {idUsuario},{status}");

                    if (RowsAffected >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al Actualizar el Estatus ";
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
