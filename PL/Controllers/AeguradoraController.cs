
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class AseguradoraController : Controller
    {
        public ActionResult GetAll()
        {
            ML.Aseguradora aseguradora = new ML.Aseguradora();
            ML.Result result = BL.Aseguradora.GetAll();
            aseguradora.Aseguradoras = result.Objects;

            return View(aseguradora);

        }
        [HttpGet]
        public ActionResult Form(int? idAseguradora)
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result resultUsuario = BL.Usuario.GetAll(usuario);
            ML.Aseguradora aseguradoraUsuario = new ML.Aseguradora();
            aseguradoraUsuario.Usuario = new ML.Usuario();

            if (resultUsuario.Correct)
            {
                aseguradoraUsuario.Usuario.Usuarios = resultUsuario.Objects;
            }
            if (idAseguradora == null)
            {

                return View(aseguradoraUsuario);
            }
            else
            {
                ML.Result result = new ML.Result();
                
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5286/api/");
                    var responseTask = client.GetAsync("Aseguradora/GetById/" + idAseguradora);
                    responseTask.Wait();
                    var resultAPI = responseTask.Result;
                    if (resultAPI.IsSuccessStatusCode)
                    {
                        var readTask = resultAPI.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();
                        ML.Aseguradora resultItemList = new ML.Aseguradora();
                        resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Aseguradora>(readTask.Result.Object.ToString());
                        result.Object = resultItemList;
                       

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No existen registros en la tabla Departamento";
                    }

                }


                //ML.Result result = BL.Aseguradora.GetById(idAseguradora.Value);
                if (result.Correct)
                {
                    aseguradoraUsuario = (ML.Aseguradora)result.Object;
                    ML.Result aseguradoraResult = BL.Usuario.GetAll(usuario);

                    aseguradoraUsuario.Usuario.Usuarios = resultUsuario.Objects;
                    return View(aseguradoraUsuario);
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al hacer la consulta:" + result.ErrorMessage;
                    return View("Modal");
                }

            }


        }

        [HttpPost]
        public ActionResult Form(ML.Aseguradora aseguradora)
        {
            ML.Result result = new ML.Result();
            if (aseguradora.IdAseguradora == 0)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5286/api/");

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ML.Aseguradora>("Aseguradora/Add", aseguradora);
                    postTask.Wait();

                    var resultAseguradora = postTask.Result;
                    if (resultAseguradora.IsSuccessStatusCode)
                    {
                        
                        return RedirectToAction("GetAll");
                        
                    }
                    else
                    {
                        ViewBag.Message = "Ocurrio un error al insertar el registro" + " " + result.ErrorMessage;
                    }
                    ViewBag.Message = "El resigistro de Aseguradora a sido agrgado con exito";
                }

                return View("Modal");

                ////result = BL.Aseguradora.Add(aseguradora);
                //if (result.Correct)
                //{
                //    ViewBag.Message = "El resigistro de Aseguradora a sido agrgado con exito";
                //}
                //else
                //{
                //    ViewBag.Message = "Ocurrio un error al insertar el registro" + " " + result.ErrorMessage;
                //}
            }
            else
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5286/api/");

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ML.Aseguradora>("Aseguradora/Update/"+ aseguradora.IdAseguradora, aseguradora);
                    postTask.Wait();

                    var resultAseguradora = postTask.Result;
                    if (resultAseguradora.IsSuccessStatusCode)
                    {

                        return RedirectToAction("GetAll");

                    }
                    else
                    {
                        ViewBag.Message = "Ocurrio un error al insertar el registro" + " " + result.ErrorMessage;
                    }
                   
                }

                //result = BL.Aseguradora.Update(aseguradora);
                //if (result.Correct)
                //{
                //    ViewBag.Message = "El Registro de Aseguradora a sido Modificado con exito";
                //}
                //else
                //{
                //    ViewBag.Message = "Ocurrio un error al Modificar el registro" + " " + result.ErrorMessage;
                //}
            }
            return View("Modal");
        }

        [HttpGet]
        public ActionResult Delete(int idAseguradora)
        {
            ML.Result ResultAseguradoraDelete = new ML.Result();
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5286/api/");

                //HTTP POST
                var postTask = client.GetAsync("Aseguradora/Delete/" + idAseguradora);
                postTask.Wait();
                var result1 = postTask.Result;
                if (result1.IsSuccessStatusCode)
                {
                    ViewBag.Message = "El resigistro de Aseguradora a sido eliminado con exito";
                    //ResultAseguradoraDelete = BL.Aseguradora.GetAll();
                    //return RedirectToAction("GetAll");
                    return View("Modal");
                }
            }


            //ResultAseguradoraDelete = BL.SubCategoria.GetAll();
            ViewBag.Message = "Ocurrio un error al Eliminar el registro" + " " + ResultAseguradoraDelete.ErrorMessage;
            return View("Modal");




            //ML.Result result = BL.Aseguradora.Delete(idAseguradora);

            //if (result.Correct)
            //{
            //    ViewBag.Message = "El registro de Aseguradora a sido Eliminado con exito";
            //}
            //else
            //{
            //    ViewBag.Message = "Ocurrio un error al eliminar el registro: " + result.ErrorMessage;
            //}
            //return View("Modal");
        }



    }


}
