using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Pais
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.UsuarioContext context = new DL.UsuarioContext ())
                {
                    var query = context.Pais.FromSqlRaw("PaisGetAll").ToList();
                   // var query = context.PaisGetAll().ToList();
                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var paisList in query)
                        {
                            ML.Pais pais = new ML.Pais();
                            pais.IdPais = paisList.IdPais;
                            pais.Nombre = paisList.Nombre;

                            result.Objects.Add(pais);
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
