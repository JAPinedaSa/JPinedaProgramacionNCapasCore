using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLConsole
{
    public class Insert
    {
        public static void ReadFile()
        {
            int count;
            count = 0;
            string file = @"C:\Users\digis\OneDrive\Escritorio\Informcion.txt";

            if (File.Exists(file))
            {
               
                try
                {
                    StreamReader Textfile = new StreamReader(file);
                string line;
                
                line = Textfile.ReadLine();
                    

                
                    while ((line = Textfile.ReadLine()) != null)
                    {
                        count = count + 1;
                        string[] lines = line.Split(",");
                        ML.Empleado empleado = new ML.Empleado();

                        empleado.NumeroEmpleado = lines[0];
                        empleado.RFC = lines[1];
                        empleado.Nombre = lines[2];
                        empleado.ApellidoPaterno = lines[3];
                        empleado.ApellidoMaterno = lines[4];
                        empleado.Correo = lines[5];
                        empleado.Telefono = lines[6];
                        empleado.FechaNacimiento = lines[7];
                        empleado.NSS = lines[8];
                        empleado.Empresa = new ML.Empresa();
                        empleado.Empresa.IdEmpresa = int.Parse(lines[9]);

                        empleado.Foto = " ";

                        ML.Result result = BL.Empleado.Add(empleado);

                        

                        if (result.Correct)
                        {

                            Console.WriteLine("Se inserto el registro");
                            Console.ReadKey();
                        }
                        else
                        {
                            //string fileError = @"C:\Users\digis\OneDrive\Escritorio\Errores.txt";

                            ////CREAR UN TXT DE ERRORES
                            //using (StreamWriter TextError = new StreamWriter(fileError, true))
                            //{
                            //    TextError.WriteLine("HUBO UN ERROR EN EL REGISTRO");
                            //    TextError.Close();
                            //}
                            Console.WriteLine("Hubo un Error");

                        }

                    }
                }
                catch (Exception ex)
                {
                    string fileError = @"C:\Users\digis\OneDrive\Escritorio\Errores.txt";

                    //CREAR UN TXT DE ERRORES
                    using (StreamWriter TextError = new StreamWriter(fileError, true))
                    {
                        TextError.WriteLine("ERROR " + " " + ex.Message + " " + " Al Intentar Insertar el Registro:" + " " + count );
                        TextError.Close();
                    }
                }

            }
            else
            {
                Console.WriteLine("EL ARCHIVO NO EXISTE O LA RUTA ES INCORRECTA");
         
            }
            //
        }

   
    
    //
    }
}

