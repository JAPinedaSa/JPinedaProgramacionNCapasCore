using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Colonia
    {
        public static ML.Result GetByIdColonia(int idColonia)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.UsuarioContext context = new DL.UsuarioContext())
                {
                    
                    //var coloniaList = context.Colonia.FromSqlRaw($"ColoniaGetByIdMunicipio {idColonia}").AsEnumerable().ToList();
                    //var coloniaList = contex.ColoniaGetByIdMunicipio(idColonia).ToList();

                    var coloniaList = context.Colonia.FromSqlRaw($"ColoniaGetByIdMunicipio {idColonia}").AsEnumerable().ToList();
                    //var municipioList = contex.MunicipioGetByIdEstado(idMunicipio).ToList();

                    result.Objects = new List<object>();

                    foreach (var row in coloniaList)
                    {
                        ML.Colonia colonia = new ML.Colonia();

                        colonia.IdColonia = row.IdColonia;
                        colonia.Nombre = row.Nombre;

                        result.Objects.Add(colonia);

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
