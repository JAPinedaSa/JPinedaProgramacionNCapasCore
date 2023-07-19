using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class DependienteTipo
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.UsuarioContext context = new DL.UsuarioContext())
                {
                    var query = context.DependienteTipos.FromSqlRaw("DependienteTipoGetAll").ToList();
                    //var query = context.RolGetAll();
                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var dependientesTipoList in query)
                        {
                            ML.DependienteTipo dependienteTip = new ML.DependienteTipo();
                            dependienteTip.IdDependienteTipo = dependientesTipoList.IdDependienteTipo;
                            dependienteTip.Nombre = dependientesTipoList.Nombre;

                            result.Objects.Add(dependienteTip);
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
