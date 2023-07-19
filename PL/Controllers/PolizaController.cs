using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class PolizaController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Poliza poliza = new ML.Poliza();
            ML.Result result = BL.Poliza.GetAll();
            poliza.Polizas = result.Objects;

            return View(poliza);

        }


        [HttpGet]
        public ActionResult Form(int? idPoliza)
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result resultUsuario = BL.Usuario.GetAll(usuario);
            ML.Poliza poliza = new ML.Poliza();
            poliza.Usuario = new ML.Usuario();
          
            if (resultUsuario.Correct)
            {
                poliza.Usuario.Usuarios = resultUsuario.Objects;
            }
            if (idPoliza == null)
            {
                return View(poliza);
            }
            else
            {
                ML.Result result = BL.Poliza.GetById(idPoliza.Value);
                if (result.Correct)
                {
                    
                    poliza = (ML.Poliza)result.Object;
                    poliza.Usuario.Usuarios = resultUsuario.Objects;
                    return View(poliza);
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al hacer la consulta:" + result.ErrorMessage;
                    return View("Modal");
                }
            }
        }

        
        [HttpPost]
        public ActionResult Form(ML.Poliza poliza)
        {
            
            ML.Result result = new ML.Result();

            if (poliza.IdPoliza == 0)
            {
                result = BL.Poliza.Add(poliza);
                if (result.Correct)
                {
                    ViewBag.Message = "El usuario a sido agrgado con exito";
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error en la inserción:" + " " + result.ErrorMessage;
                }
            }
            else
            {
                result = BL.Poliza.Update(poliza);
                if (result.Correct)
                {
                    ViewBag.Message = "El usuario a sido Actualizado con exito";
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error en la Actualizacion del usuario: " + result.ErrorMessage;
                }

            }
            return View("Modal");
        }

    }
}
