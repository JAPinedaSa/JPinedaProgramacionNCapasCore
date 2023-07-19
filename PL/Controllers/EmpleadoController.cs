using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpleadoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Empleado empleado = new ML.Empleado();
            ML.Result result = BL.Empleado.GetAll();
            empleado.Empleados = result.Objects;

            return View(empleado);

        }
        [HttpGet]
        public ActionResult GetAllDependienteEmpleado()
        {
            ML.Empleado empleado = new ML.Empleado();
            ML.Result result = BL.Empleado.GetAll();
            empleado.Empleados = result.Objects;

            return View(empleado);

            
        }

        [HttpGet]
        public ActionResult DependientesGetByIdEmpleado(string NumeroEmpleado)
        {

            
            ML.Result resultDependientes = BL.Dependientes.DependenciaGetByIdEmpleado(NumeroEmpleado);
            ML.Dependiente dependiente = new ML.Dependiente();
            if (resultDependientes.Correct)
            {
                ML.Result resultTipoDependiente = BL.DependienteTipo.GetAll();
                dependiente.DependienteTipo = new ML.DependienteTipo();
                dependiente.DependienteTipo.TiposDependientes = resultTipoDependiente.Objects;
                dependiente.Dependientes = resultDependientes.Objects;
                dependiente.Empleado = new ML.Empleado();
                ML.Result resultEmpleado = BL.Empleado.GetById(NumeroEmpleado);
                dependiente.Empleado = (ML.Empleado)resultEmpleado.Object;
              

                return View(dependiente);
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al hacer la consulta:" + resultDependientes.ErrorMessage;
                return View("Modal");
            }
        }


        [HttpGet]
        public ActionResult Form(string? NumeroEmpleado)
        {
            ML.Result resulEmpresas = BL.Empresa.GetAll();
            ML.Empleado empleado = new ML.Empleado();
            empleado.Empresa = new ML.Empresa();

            if (resulEmpresas.Correct)
            {
                empleado.Empresa.Empresas = resulEmpresas.Objects;
            }
            if (NumeroEmpleado == null)
            {
                empleado.Action = "Add";
                return View(empleado);
            }
            else
            {
                empleado.Action = "Update";
                ML.Result result = BL.Empleado.GetById(NumeroEmpleado);
                if (result.Correct)
                {
                    empleado = (ML.Empleado)result.Object;
                    empleado.Empresa.Empresas = resulEmpresas.Objects;

                    return View(empleado);
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al hacer la consulta:" + result.ErrorMessage;
                    return View("Modal");
                }
            }



        }

        [HttpPost]
        public ActionResult Form(ML.Empleado empleado)
        {
            IFormFile file = Request.Form.Files["inpImagen"];
            if (ModelState.IsValid) //validar si se cumplieron todas las data annotations
            {

                //HttpPostedFileBase file = Request.Files["inpImagen"];


                if (file != null)
                {
                    empleado.Foto = Convert.ToBase64String(ConvertToBytes(file));


                }

                ML.Result result = new ML.Result();

                if (empleado.Action == "Add")
                {
                    result = BL.Empleado.Add(empleado);
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
                    result = BL.Empleado.Update(empleado);
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
            else
            {

                ML.Result resultEmpresas = BL.Empresa.GetAll();


                empleado.Empresa = new ML.Empresa();

                empleado.Empresa.Empresas = resultEmpresas.Objects;

                return View(empleado);
            }
        }

        public ActionResult Delete(string NumeroEmpleado)
        {
            ML.Empleado empleado = new ML.Empleado();
            empleado.NumeroEmpleado = NumeroEmpleado;
            ML.Result result = BL.Empleado.Delete(NumeroEmpleado);

            if (result.Correct)
            {
                ViewBag.Message = "El usuario a sido Eliminado con exito";
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al eliminar el usuario: " + result.ErrorMessage;
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


        [HttpGet]
        public ActionResult FormDependiente(int? idDependiente)
        {
            ML.Result resultTipoDependiente = BL.DependienteTipo.GetAll();
            ML.Dependiente dependiente = new ML.Dependiente();
            dependiente.DependienteTipo = new ML.DependienteTipo();

            if (idDependiente == null)
            {
                dependiente.DependienteTipo.TiposDependientes = resultTipoDependiente.Objects;
                dependiente.Action = "Add";
                return View(dependiente);
            }
            else
            {
                dependiente.Action = "Update";
                ML.Result result = BL.Dependientes.DependientesGetById(idDependiente.Value);
                if (result.Correct)
                {
                    dependiente.DependienteTipo.TiposDependientes = resultTipoDependiente.Objects;
                        dependiente = (ML.Dependiente)result.Object;
                   

                    return View(dependiente);
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al hacer la consulta:" + result.ErrorMessage;
                    return View("Modal");
                }
            }



        }

        //[HttpPost]
        //public ActionResult FormDependiente(ML.Dependiente dependiente)
        //{
        //    IFormFile file = Request.Form.Files["inpImagen"];
        //    if (ModelState.IsValid) //validar si se cumplieron todas las data annotations
        //    {

        //        //HttpPostedFileBase file = Request.Files["inpImagen"];


        //        if (file != null)
        //        {
        //            dependiente.Foto = Convert.ToBase64String(ConvertToBytes(file));


        //        }

        //        ML.Result result = new ML.Result();

        //        if (dependiente.Action == "Add")
        //        {
        //            result = BL.Empleado.Add(dependiente);
        //            if (result.Correct)
        //            {
        //                ViewBag.Message = "El usuario a sido agrgado con exito";
        //            }
        //            else
        //            {
        //                ViewBag.Message = "Ocurrio un error en la inserción:" + " " + result.ErrorMessage;
        //            }
        //        }
        //        else
        //        {
        //            result = BL.Empleado.Update(dependiente);
        //            if (result.Correct)
        //            {
        //                ViewBag.Message = "El usuario a sido Actualizado con exito";
        //            }
        //            else
        //            {
        //                ViewBag.Message = "Ocurrio un error en la Actualizacion del usuario: " + result.ErrorMessage;
        //            }
        //        }
        //        return View("Modal");

        //    }
        //    else
        //    {

        //        ML.Result resultEmpresas = BL.Empresa.GetAll();


        //        dependiente.Empresa = new ML.Empresa();

        //        dependiente.Empresa.Empresas = resultEmpresas.Objects;

        //        return View(dependiente);
        //    }
        //}

    }

}
