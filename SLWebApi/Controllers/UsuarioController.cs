using Microsoft.AspNetCore.Mvc;

namespace SLWebApi.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        [Route("api/Usuario/GetAll")]
        public IActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = BL.Usuario.GetAll(usuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result.ErrorMessage);
            }
        }

        [HttpPost]
        [Route("api/Usuario/Add")]
        public IActionResult Add([FromBody] ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.Add(usuario);
            if (result.Correct)
            {
                return Ok(result.Objects);
            }
            else
            {
                return NotFound(result.ErrorMessage);
            }
        }

        [HttpGet]
        [Route("api/Usuario/Delete/{idUsuario}")]
        public IActionResult Delete(int idUsuario)
        {
            ML.Result result = BL.Usuario.Delete(idUsuario);
            if (result.Correct)
            {
                return Ok(result.Objects);
            }
            else
            {
                return NotFound(result.ErrorMessage);
            }
        }

        [HttpGet]
        [Route("api/Usuario/GetbyId/{idUsuario}")]
        public IActionResult GetById(int idUsuario)
        {
            ML.Result result = BL.Usuario.GetById(idUsuario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result.ErrorMessage);
            }

        }

        [HttpGet]
        [Route("api/Usuario/GetbyUserName/{UserName}/{Password}")]
        public IActionResult GetbyUserName(string UserName, string Password)
        {
            ML.Result result = BL.Usuario.GetByUserName(UserName);
            if (result.Correct)
            {
                ML.Usuario usuario = (ML.Usuario)result.Object;

                if (usuario.Password == Password)
                {
                    return Ok(result);
                }
                else
                {
                    return Unauthorized(result.ErrorMessage = "La Contraseña es incorrecta");
                }
                
            }
            else
            {
                return Unauthorized(result.ErrorMessage = "El usuario no exxiste o esta mal escrito");
            }

        }

        [HttpPost]
        [Route("api/Usuario/Update/{idUsuario}")]
        public IActionResult Update(int idUsuario, [FromBody] ML.Usuario usuario)
        {
            usuario.IdUsuario = idUsuario;
            ML.Result result = BL.Usuario.Update(usuario);
            if (result.Correct)
            {
                return Ok(result.Objects);
            }
            else
            {
                return NotFound(result.ErrorMessage);
            }
            
        }
    }
}
