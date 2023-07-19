using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace BL
{
    public class Rol
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.UsuarioContext context = new DL.UsuarioContext())
                {
                    var query = context.Rols.FromSqlRaw("RolGetAll").ToList();
                    //var query = context.RolGetAll();
                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var rollist in query)
                        {
                            ML.Rol rol = new ML.Rol();
                            rol.IdRol = rollist.IdRol;
                            rol.Nombre = rollist.Nombre;

                            result.Objects.Add(rol);
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
            }
            return result;
        }
    }
}
