using Microsoft.AspNetCore.Mvc;

namespace SLWebApi.Controllers
{
    public class AseguradoraController : Controller
    {
        [HttpGet]
        [Route("api/Aseguradora/GetAll")]
        public IActionResult GetAll()
        {
            
            ML.Result result = BL.Aseguradora.GetAll();

            if (result.Correct)
            {
                return Ok(result.Objects);
            }
            else
            {
                return NotFound(result.ErrorMessage);
            }
        }

        [HttpPost]
        [Route("api/Aseguradora/Add")]
        public IActionResult Add([FromBody] ML.Aseguradora aseguradora)
        {
            ML.Result result = BL.Aseguradora.Add(aseguradora);
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
        [Route("api/Aseguradora/Delete/{idAseguradora}")]
        public IActionResult Delete(int idAseguradora)
        {
            ML.Result result = BL.Aseguradora.Delete(idAseguradora);
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
        [Route("api/Aseguradora/GetbyId/{idAseguradora}")]

        public IActionResult GetById(int idAseguradora)
        {
            
            ML.Result result = BL.Aseguradora.GetById(idAseguradora);
            
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
        [Route("api/Aseguradora/Update/{id}")]
        public IActionResult Update(int id, [FromBody] ML.Aseguradora aseguradora)
        {
            aseguradora.IdAseguradora = id;

            ML.Result result = BL.Aseguradora.Update(aseguradora);

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
