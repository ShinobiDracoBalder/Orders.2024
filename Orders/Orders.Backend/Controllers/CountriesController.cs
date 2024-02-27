using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orders.Backend.Data;
using Orders.Shared.Entities;
using Orders.Shared.Responses;
using System.Net;

namespace Orders.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly DataContext _context;

        public CountriesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            JsonResponse jsonResponse = new JsonResponse();
            try
            {
                jsonResponse.code = (int)HttpStatusCode.OK;
                jsonResponse.ItsSuccessful = true;
                jsonResponse.ResultModel = await _context.Countries.ToListAsync();
                return Ok(jsonResponse);
            }
            catch (Exception ex)
            {
                jsonResponse.code = 500;
                jsonResponse.ItsSuccessful = false;
                jsonResponse.Message = ex.Message;
                return Ok(jsonResponse);
            }
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            JsonResponse jsonResponse = new JsonResponse();
            try
            {
                var country = await _context.Countries.FindAsync(id);
                if (country == null)
                {
                    jsonResponse.code = (int)HttpStatusCode.NotFound;
                    jsonResponse.ItsSuccessful = false;
                    jsonResponse.Message = "No hay datos!";
                    return Ok(jsonResponse);
                }
                jsonResponse.code = (int)HttpStatusCode.OK;
                jsonResponse.ItsSuccessful = true;
                jsonResponse.ResultModel = country;
                return Ok(jsonResponse);
            }
            catch (Exception ex )
            {
                jsonResponse.code = (int)HttpStatusCode.InternalServerError;
                jsonResponse.ItsSuccessful = false;
                jsonResponse.Message = ex.Message;
                return Ok(jsonResponse);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Country country)
        {
            JsonResponse jsonResponse = new JsonResponse();
            if (!ModelState.IsValid)
            {
                jsonResponse.code = (int)HttpStatusCode.NotFound;
                jsonResponse.ItsSuccessful = false;
                jsonResponse.Message = "No hay datos!";
                return Ok(jsonResponse);
            }
            try
            {
                _context.Add(country);
                await _context.SaveChangesAsync();
                jsonResponse.code = (int)HttpStatusCode.OK;
                jsonResponse.ItsSuccessful = true;
                jsonResponse.ResultModel = country;
                return Ok(jsonResponse);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    jsonResponse.code = (int)HttpStatusCode.BadRequest;
                    jsonResponse.ItsSuccessful = false;
                    jsonResponse.Message = "Ya existe una pais con el mismo nombre.";
                }
                else
                {
                    jsonResponse.code = (int)HttpStatusCode.BadRequest;
                    jsonResponse.ItsSuccessful = false;
                    jsonResponse.Message = dbUpdateException.Message;
                }
                return Ok(jsonResponse);
            }
            catch (Exception ex)
            {
                jsonResponse.code = (int)HttpStatusCode.InternalServerError;
                jsonResponse.ItsSuccessful = false;
                jsonResponse.Message = ex.Message;
                return Ok(jsonResponse);
            }
        }
        [HttpPut]
        public async Task<IActionResult> PutAsync(Country country)
        {
            JsonResponse jsonResponse = new JsonResponse();
            try
            {
                _context.Update(country);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                jsonResponse.code = (int)HttpStatusCode.InternalServerError;
                jsonResponse.ItsSuccessful = false;
                jsonResponse.Message = ex.Message;
                return Ok(jsonResponse);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            JsonResponse jsonResponse = new JsonResponse();
            try
            {
                var country = await _context.Countries.FindAsync(id);
                if (country == null)
                {
                    jsonResponse.code = (int)HttpStatusCode.NotFound;
                    jsonResponse.ItsSuccessful = false;
                    jsonResponse.Message = "No hay datos!";
                    return Ok(jsonResponse);
                }
                _context.Remove(country);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                jsonResponse.code = (int)HttpStatusCode.InternalServerError;
                jsonResponse.ItsSuccessful = false;
                jsonResponse.Message = ex.Message;
                return Ok(jsonResponse);
            }
           
        }
    }
}
