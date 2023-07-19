using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace PL.Controllers
{
    public class EmpleadoCargaMasivaController : Controller
    {
        private IHostingEnvironment Environment;
        private IConfiguration Configuration;
        public EmpleadoCargaMasivaController(IHostingEnvironment _environment, IConfiguration _configuration)
        {
            Environment = _environment;
            Configuration = _configuration;
        }
        [HttpGet]
        public ActionResult CargaMasiva()
        {

            ML.Result result = new ML.Result();

            return View(result);
        }


        [HttpPost]
        public ActionResult CargaMasiva(ML.Empleado empleado)
        {
            IFormFile file = Request.Form.Files["inpExcel"];
            if (HttpContext.Session.GetString("PathArchivo") == null)
            {
                if (file != null)
                {

                    string fileName = Path.GetFileName(file.FileName);
                    string folderPath = Configuration["PathFolder"];
                    string extensionArchivo = Path.GetExtension(file.FileName).ToLower();
                    string extensionAppSetting = Configuration["TipoExcel"];
                    string extensionAppSettingTXTX = Configuration["TipoTXT"];

                    if (extensionArchivo == extensionAppSetting || extensionArchivo == extensionAppSettingTXTX)
                    {
                        string filePath = Path.Combine(Environment.ContentRootPath, folderPath, Path.GetFileNameWithoutExtension(fileName)) + '-' + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                        if (!System.IO.File.Exists(filePath))
                        {
                            using (FileStream stream = new FileStream(filePath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }

                            string connString = Configuration["ExcelConnection"] + filePath;


                            //CREAR EL METODO EN EL BL.EMPLEADO
                            ML.Result resultExcel = BL.Empleado.ConvertExcelToDataTable(connString);

                            if (resultExcel.Correct)
                            {
                                ML.Result resultValidacion = BL.Empleado.ValidarExcel(resultExcel.Objects);
                                if (resultValidacion.Objects.Count == 0)
                                {
                                    resultValidacion.Correct = true;
                                    HttpContext.Session.SetString("PathArchivo", filePath);
                                }
                                return View(resultValidacion);
                            }
                            else
                            {
                                ViewBag.Message = "El excel no contiene ningun Registro";
                            }
                        }
                    }
                }
                else
                {
                    ViewBag.Message = "El archivo que intenta procesar no es un archivo excel";
                }
                }
                else
                {
                    string rutaArchivoExcel = HttpContext.Session.GetString("PathArchivo");
                    string connectionString = Configuration["ExcelConnection"] + rutaArchivoExcel;


                    ML.Result resultTableConvert = BL.Empleado.ConvertExcelToDataTable(connectionString);

                    if (resultTableConvert.Correct)
                    {
                        ML.Result resultErrors = new ML.Result();
                        resultErrors.Objects = new List<object>();

                        foreach (ML.Empleado empleadoItem in resultTableConvert.Objects)
                        {
                            ML.Result resultAdd = BL.Empleado.Add(empleadoItem);
                            if (!resultAdd.Correct)
                            {
                                resultErrors.Objects.Add("No se insertó el Semestre con nombre: " + empleadoItem.Nombre + " Error: " + resultAdd.ErrorMessage);
                            }
                        }
                        if (resultErrors.Objects.Count > 0)
                        {
                            //string fileError = Path.Combine(Environment.WebRootPath, @"C:\Users\digis\OneDrive\Documents\Jose_Alejandro_Pineda_Sanchez\Documentos");
                            string fileError = Path.Combine(Environment.WebRootPath, @"");
                            using (StreamWriter writer = new StreamWriter(fileError))
                            {
                                foreach (string ln in resultErrors.Objects)
                                {
                                    writer.WriteLine(ln);
                                }
                            }
                            ViewBag.Message = "Los Empleados No han sido registrados correctamente";
                        }
                        else
                        {
                            //borrar session

                            ViewBag.Message = "Las Alumnos han sido registrados correctamente";
                        }
                    }
                }

                return PartialView("Modal");
            }
        }
    }


