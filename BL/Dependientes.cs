using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Dependientes
    {
        public static ML.Result DependenciaGetByIdEmpleado(string NumeroEmpleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.UsuarioContext context = new DL.UsuarioContext())
                {
                    var query = context.Dependientes.FromSqlRaw($"DepedienteGetByIdEmpleado '{NumeroEmpleado}'").ToList();
                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.Dependiente dependiente = new ML.Dependiente();
                            dependiente.IdDependiente = obj.IdDependiente;
                            dependiente.Nombre = obj.Nombre;
                            dependiente.ApellidoPaterno = obj.ApellidoPaterno;
                            dependiente.ApellidoMaterno = obj.ApellidoMaterno;
                            dependiente.FechaNacimiento = obj.FechaNacimiento.ToString();
                            dependiente.EstadoCivil = obj.EstadoCivil;
                            dependiente.Genero = obj.Genero;
                            dependiente.Telefono = obj.Telefono;
                            dependiente.RFC = obj.Rfc;
                            dependiente.DependienteTipo = new ML.DependienteTipo();
                            dependiente.DependienteTipo.IdDependienteTipo = obj.IdDependienteTipo;
                            dependiente.DependienteTipo.Nombre = obj.NombreDependienteTipo;
                            dependiente.Empleado = new ML.Empleado();
                            dependiente.Empleado.NumeroEmpleado = obj.IdEmpleado;
                            dependiente.Empleado.Nombre = obj.NombreEmpleado;

                            result.Objects.Add(dependiente);
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

        public static ML.Result DependientesGetById(int IdDependiente)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.UsuarioContext context = new DL.UsuarioContext())
                {
                    var query = context.Dependientes.FromSqlRaw($"DepedienteGetById {IdDependiente}").AsEnumerable().FirstOrDefault();

                    if (query != null)
                    {
                        result.Objects = new List<object>();

                        ML.Dependiente dependiente = new ML.Dependiente();

                        dependiente.IdDependiente = query.IdDependiente;
                        dependiente.Nombre = query.Nombre;
                        dependiente.ApellidoPaterno = query.ApellidoPaterno;
                        dependiente.ApellidoMaterno = query.ApellidoMaterno;
                        dependiente.FechaNacimiento = query.FechaNacimiento.ToString();
                        dependiente.EstadoCivil = query.EstadoCivil;
                        dependiente.Genero = query.Genero;
                        dependiente.Telefono = query.Telefono;
                        dependiente.RFC = query.Rfc;
                        dependiente.DependienteTipo = new ML.DependienteTipo();
                        dependiente.DependienteTipo.IdDependienteTipo = query.IdDependienteTipo;
                        dependiente.DependienteTipo.Nombre = query.NombreDependienteTipo;
                        dependiente.Empleado = new ML.Empleado();
                        dependiente.Empleado.NumeroEmpleado = query.IdEmpleado;
                        dependiente.Empleado.Nombre = query.NombreEmpleado;

                        result.Object = dependiente;

                        result.Correct = true;
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

        public static ML.Result Add(ML.Dependiente dependiente)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.UsuarioContext context = new DL.UsuarioContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"DependienteAdd '{dependiente.Empleado.NumeroEmpleado}','{dependiente.Nombre}'," +
                        $"'{dependiente.ApellidoPaterno}','{dependiente.ApellidoMaterno}','{dependiente.FechaNacimiento}'," +
                        $"");
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
