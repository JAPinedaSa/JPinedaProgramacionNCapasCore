using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Estado
    {
        public static ML.Result GetByIdEstado(int idPais)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.UsuarioContext context = new DL.UsuarioContext())
                {
                    var estadoList = context.Estados.FromSqlRaw($"EstadoGetByIdPais {idPais}" ).AsEnumerable().ToList();
                    //var estadoList = contex.EstadoGetByIdPais(idPais).ToList();

                    result.Objects = new List<object>();

                    foreach (var row in estadoList)
                    {
                        ML.Estado estado = new ML.Estado();

                        estado.IdEstado = row.IdEstado;
                        estado.Nombre = row.Nombre;

                        result.Objects.Add(estado);

                    }

                    result.Correct = true;
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = ex.Message;

            }
            return result;
        }
    }
}
