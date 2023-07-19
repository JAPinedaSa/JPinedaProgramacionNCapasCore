using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        //[HttpGet]
        //public ActionResult GetAll()
        //{
        //    ML.Usuario usuario = new ML.Usuario();
        //    ML.Result result = BL.Usuario.GetAll(usuario);
        //    usuario.Usuarios = result.Objects;

        //    return View(usuario);

        //}
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result resultWebApi = new ML.Result();
            resultWebApi.Objects = new List<object>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5286/api/");

                var responseTask = client.GetAsync("Usuario/GetAll");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Usuario resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                        resultWebApi.Objects.Add(resultItemList);
                    }
                    ML.Usuario usuario = new ML.Usuario();
                    usuario.Usuarios = resultWebApi.Objects;
                    return View(usuario);
                }
            }
            return View(resultWebApi);
        }

        [HttpPost]
        public ActionResult GetAll(ML.Usuario usuario)
        {

            ML.Result result = BL.Usuario.GetAll(usuario);
            usuario.Usuarios = result.Objects;

            return View(usuario);

        }

        [HttpGet]
        public ActionResult Form(int? idUsuario)
        {
            ML.Result resultRol = BL.Rol.GetAll();
            ML.Result resultPais = BL.Pais.GetAll();
            ML.Usuario usuario = new ML.Usuario();

            usuario.Rol = new ML.Rol();

            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

            if (resultRol.Correct)
            {
                usuario.Rol.Roles = resultRol.Objects;
                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;

            }

            if (idUsuario == null)
            {

                return View(usuario);
            }
            else
            {
                //ML.Result result = BL.Usuario.GetById(idUsuario.Value);
                ML.Result result = new ML.Result();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5286/api/");
                    var responseTask = client.GetAsync("Usuario/GetById/" + idUsuario);
                    responseTask.Wait();
                    var resultAPI = responseTask.Result;
                    if (resultAPI.IsSuccessStatusCode)
                    {
                        var readTask = resultAPI.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();
                        ML.Usuario resultItemList = new ML.Usuario();
                        resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(readTask.Result.Object.ToString());
                        result.Object = resultItemList;


                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No existen registros en la tabla Departamento";
                    }

                }
                if (result.Correct)
                {

                    usuario = (ML.Usuario)result.Object;

                    ML.Result resultColonia = BL.Colonia.GetByIdColonia(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                    ML.Result resultMunicipio = BL.Municipio.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                    ML.Result resultEstado = BL.Estado.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                    ML.Result resultPaises = BL.Pais.GetAll();

                    usuario.Rol.Roles = resultRol.Objects;
                    usuario.Direccion.Colonia.Colonias = resultColonia.Objects;
                    usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                    usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                    usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPaises.Objects;



                    return View(usuario);
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al hacer la consulta:" + result.ErrorMessage;
                    return View("Modal");
                }

            }


        }

        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            IFormFile file = Request.Form.Files["inpImagen"];
            if (ModelState.IsValid) //validar si se cumplieron todas las data annotations
            {

                //HttpPostedFileBase file = Request.Files["inpImagen"];


                if (file != null)
                {
                    usuario.Imagen = Convert.ToBase64String(ConvertToBytes(file));


                }

                ML.Result result = new ML.Result();
                if (usuario.IdUsuario == 0)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:5286/api/");

                        //HTTP POST
                        var postTask = client.PostAsJsonAsync<ML.Usuario>("Usuario/Add", usuario);
                        postTask.Wait();

                        var resultUsuario = postTask.Result;
                        if (resultUsuario.IsSuccessStatusCode)
                        {

                            return RedirectToAction("GetAll");

                        }
                        else
                        {
                            ViewBag.Message = "Ocurrio un error al insertar el registro" + " " + result.ErrorMessage;
                        }
                        ViewBag.Message = "El resigistro de Usuario a sido agrgado con exito";
                    }

                    return View("Modal");
                    //result = BL.Usuario.Add(usuario);
                    //if (result.Correct)
                    //{
                    //    ViewBag.Message = "El usuario a sido agrgado con exito";
                    //}
                    //else
                    //{
                    //    ViewBag.Message = "Ocurrio un error en la inserción:" + " " + result.ErrorMessage;
                    //}
                }
                else
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:5286/api/");

                        //HTTP POST
                        var postTask = client.PostAsJsonAsync<ML.Usuario>("Usuario/Update/" + usuario.IdUsuario, usuario);
                        postTask.Wait();

                        var resultAseguradora = postTask.Result;
                        if (resultAseguradora.IsSuccessStatusCode)
                        {

                            return RedirectToAction("GetAll");

                        }
                        else
                        {
                            ViewBag.Message = "Ocurrio un error al Actualizar el registro" + " " + result.ErrorMessage;
                        }

                    }
                    //result = BL.Usuario.Update(usuario);
                    //if (result.Correct)
                    //{
                    //    ViewBag.Message = "El usuario a sido Actualizado con exito";
                    //}
                    //else
                    //{
                    //    ViewBag.Message = "Ocurrio un error en la Actualizacion del usuario: " + result.ErrorMessage;
                    //}
                }
                return View("Modal");

            }
            else
            {
                ML.Result resultRol = BL.Rol.GetAll();
                ML.Result resultPais = BL.Pais.GetAll();


                usuario.Rol = new ML.Rol();

                usuario.Direccion = new ML.Direccion();
                usuario.Direccion.Colonia = new ML.Colonia();
                usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                usuario.Rol.Roles = resultRol.Objects;
                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;
                return View(usuario);
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            

            return View();

        }

        [HttpPost]
        public ActionResult Login(string UserName, string Password)
        {
            //ML.Result result = BL.Usuario.GetByUserName(UserName);
            ML.Result resultLogin = new ML.Result();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5286/api/");
                var responseTask = client.GetAsync("Usuario/GetByUserName/" + UserName+"/"+Password);
                responseTask.Wait();
                var resultAPI = responseTask.Result;
                if (resultAPI.IsSuccessStatusCode)
                {
                    var readTask = resultAPI.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();
                    ML.Usuario resultItemList = new ML.Usuario();
                    resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(readTask.Result.Object.ToString());
                    resultLogin.Object = resultItemList;


                    resultLogin.Correct = true;
                }
                else
                {
                    resultLogin.Correct = false;
                    resultLogin.ErrorMessage = "No existen registros en la tabla Departamento";
                }

            }

            if (resultLogin.Correct)
            {
                ML.Usuario usuario = (ML.Usuario)resultLogin.Object;
                if (usuario.Password == Password)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = "La contraseña no coincide";
                    return PartialView("ModalLogin");
                }
            }
            else
            {
                ViewBag.Message = "El usuario no existe o esta mal escrito";
                return PartialView("ModalLogin");
            }
        }


        public static byte[] ConvertToBytes(IFormFile imagen)
        {
            using var Imagen = imagen.OpenReadStream();
            byte[] bytes = new byte[Imagen.Length];
            Imagen.Read(bytes, 0, (int)Imagen.Length);

            return bytes;
        }


        [HttpGet]
        public ActionResult Delete(int idUsuario)
        {
            ML.Result ResultUsuarioDelete = new ML.Result();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5286/api/");

                //HTTP POST
                var postTask = client.GetAsync("Usuario/Delete/" + idUsuario);
                postTask.Wait();
                var result1 = postTask.Result;
                if (result1.IsSuccessStatusCode)
                {
                    ViewBag.Message = "El resigistro de Usuario a sido eliminado con exito";
                    //resultLogin = BL.Aseguradora.GetAll();
                    //return RedirectToAction("GetAll");
                    return View("Modal");
                }
            }


            //resultLogin = BL.SubCategoria.GetAll();
            ViewBag.Message = "Ocurrio un error al Eliminar el registro" + " " + ResultUsuarioDelete.ErrorMessage;
            return View("Modal");
            //ML.Usuario usuario = new ML.Usuario();
            //usuario.IdUsuario = IdUsuario;
            //ML.Result result = BL.Usuario.Delete(IdUsuario);

            //if (result.Correct)
            //{
            //    ViewBag.Message = "El usuario a sido Eliminado con exito";
            //}
            //else
            //{
            //    ViewBag.Message = "Ocurrio un error al eliminar el usuario: " + result.ErrorMessage;
            //}
            //return View("Modal");
        }

        public JsonResult EstadoGetByIdPais(int idPais)
        {
            var result = BL.Estado.GetByIdEstado(idPais);

            //var result = BL.Grupo.GetByIdPlantel(idPlantel);

            return Json(result.Objects);
        }

        public JsonResult MunicipioGetByIdEstado(int idEstado)
        {
            var result = BL.Municipio.GetByIdMunicipio(idEstado);

            //var result = BL.Grupo.GetByIdPlantel(idPlantel);

            return Json(result.Objects);
        }

        public JsonResult ColoniaGetByIdMunicipio(int idMunicipio)
        {
            var result = BL.Colonia.GetByIdColonia(idMunicipio);

            //var result = BL.Grupo.GetByIdPlantel(idPlantel);

            return Json(result.Objects);
        }

        [HttpPost]
        public JsonResult ChangeStatus(int idUsuario, bool status)
        {
            ML.Result result = BL.Usuario.ChangeStatus(idUsuario, status);

            return Json(result);
        }

    }
}
