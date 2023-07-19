using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Empresa
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.UsuarioContext context = new DL.UsuarioContext())
                {
                    var query = context.Empresas.FromSqlRaw("EmpresaGetAll").ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();

                        foreach (var obj in query)
                        {
                            ML.Empresa empresa = new ML.Empresa();
                            empresa.IdEmpresa = obj.IdEmpresa;
                            empresa.Nombre = obj.Nombre;
                            empresa.Telefono = obj.Telefono;
                            empresa.Correo = obj.Correo;
                            empresa.DireccionWeb = obj.DireccionWeb;
                            empresa.Logo = obj.Logo;

                            result.Objects.Add(empresa);
                        }
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch
            {

            }


            return result;
        }

        public static ML.Result GetAllLinq()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.UsuarioContext context = new DL.UsuarioContext())
                {
                    var query = (

                        from empresa in context.Empresas
                        select new
                        {
                            IdEmpresa = empresa.IdEmpresa,
                            Nombre = empresa.Nombre,
                            Telefono = empresa.Telefono,
                            Correo = empresa.Correo,
                            DireccionWeb = empresa.DireccionWeb,
                            Logo = empresa.Logo
                        }
                    );
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Empresa empresa = new ML.Empresa();
                            empresa.IdEmpresa = obj.IdEmpresa;
                            empresa.Nombre = obj.Nombre;
                            empresa.Telefono = obj.Telefono;
                            empresa.Correo = obj.Correo;
                            empresa.DireccionWeb = obj.DireccionWeb;
                            empresa.Logo = obj.Logo;

                            result.Objects.Add(empresa);
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
        public static ML.Result Add(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.UsuarioContext context = new DL.UsuarioContext())
                {
                    DL.Empresa empresaLinq = new DL.Empresa();
                    empresaLinq.IdEmpresa = empresa.IdEmpresa;
                    empresaLinq.Nombre = empresa.Nombre;
                    empresaLinq.Telefono = empresa.Telefono;
                    empresaLinq.Correo = empresa.Correo;
                    empresaLinq.DireccionWeb = empresa.DireccionWeb;
                    empresaLinq.Logo = empresa.Logo;

                    context.Empresas.Add(empresaLinq);
                    var rowAffected = context.SaveChanges();

                    if(rowAffected > 0)
                    {
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

        public static ML.Result GetById(int idEmpresa)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.UsuarioContext context = new DL.UsuarioContext())
                {
                    var query = (

                        from empresa in context.Empresas
                        where empresa.IdEmpresa  == idEmpresa
                        select new
                        {
                            IdEmpresa = empresa.IdEmpresa,
                            Nombre = empresa.Nombre,
                            Telefono = empresa.Telefono,
                            Correo = empresa.Correo,
                            DireccionWeb = empresa.DireccionWeb,
                            Logo = empresa.Logo
                        }
                    ).FirstOrDefault();

                    if (query != null)
                    {
                        
                            ML.Empresa empresa = new ML.Empresa();
                            empresa.IdEmpresa = query.IdEmpresa;
                            empresa.Nombre = query.Nombre;
                            empresa.Telefono = query.Telefono;
                            empresa.Correo = query.Correo;
                            empresa.DireccionWeb = query.DireccionWeb;
                            empresa.Logo = query.Logo;

                        result.Object = empresa;
                       
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static ML.Result Update(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.UsuarioContext context = new DL.UsuarioContext())
                {
                    var query = (from empresaUpdate in context.Empresas
                                 where empresaUpdate.IdEmpresa == empresa.IdEmpresa
                                 select empresaUpdate).SingleOrDefault();

                    if(query != null)
                    {
                        query.IdEmpresa = empresa.IdEmpresa;
                        query.Nombre = empresa.Nombre;
                        query.Telefono = empresa.Telefono;
                        query.Correo = empresa.Correo;
                        query.DireccionWeb = empresa.DireccionWeb;
                        query.Logo = empresa.Logo;

                        context.SaveChanges();
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


    }
}
