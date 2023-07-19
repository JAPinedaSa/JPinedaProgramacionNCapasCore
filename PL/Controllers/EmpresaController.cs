using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpresaController : Controller
    {
        public ActionResult GetAll()
        {
            ML.Empresa empresa = new ML.Empresa();
            ML.Result result = BL.Empresa.GetAllLinq();
            empresa.Empresas = result.Objects;

            return View(empresa);

        }
        

        [HttpGet]
        public ActionResult Form(int? idEmpresa)
        {
            ML.Empresa empresa = new ML.Empresa();
            ML.Result result = new ML.Result();
            if(idEmpresa == null)
            {
                return View(empresa);
            }
            else
            {
                result = BL.Empresa.GetById(idEmpresa.Value);
                if (result.Correct)
                {
                    empresa =  (ML.Empresa)result.Object;
                    return View(empresa);
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al hacer la consulta:" + result.ErrorMessage;
                    return View("Modal");
                }
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Empresa empresa)
        {

            IFormFile file = Request.Form.Files["inpImagen"];
            if (file != null)
            {
                empresa.Logo = Convert.ToBase64String(ConvertToBytes(file));


            }
            ML.Result result = new ML.Result();

            if (empresa.IdEmpresa ==0)
            {
                result = BL.Empresa.Add(empresa);
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
                result = BL.Empresa.Update(empresa);
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

        public static byte[] ConvertToBytes(IFormFile imagen)
        {
            using var Imagen = imagen.OpenReadStream();
            byte[] bytes = new byte[Imagen.Length];
            Imagen.Read(bytes, 0, (int)Imagen.Length);

            return bytes;
        }
    }
}
