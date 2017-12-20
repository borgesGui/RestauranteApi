using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RestauranteApi.Models;
using RestauranteApi.Context;
using RestauranteApi.Bll;
using RestauranteApi.Response;
using RestauranteApi.Response.Enums;
using RestauranteApi.Exceptions;
using System;
using Microsoft.AspNetCore.Cors;

namespace RestauranteApi.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    public class RestauranteController : Controller
    {
        private readonly RestauranteDbContext _context;

        public RestauranteController(RestauranteDbContext context)
        {
            _context = context;
        }

        [HttpPost(Name = "Save")]
        public IActionResult Save([FromBody] Restaurante item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            ResponseDto<Restaurante> response = new ResponseDto<Restaurante>();
            try
            {
                RestauranteBll bll = new RestauranteBll(_context);
                Restaurante restaurante = bll.Save(item);

                return GetById(restaurante.Id);
            }
            catch (Exception ex)
            {
                response.Status = StatusResponse.ERROR.Value;
                response.Message = "Erro ao salvar o restaurante: " + ex.Message;
            }

            return new ObjectResult(response);
        }

        [HttpGet(Name = "GetAllRestaurantes")]
        public IActionResult GetAll()
        {
            ResponseDto<List<Restaurante>> response = new ResponseDto<List<Restaurante>>();
            try
            {
                RestauranteBll bll = new RestauranteBll(_context);
                List<Restaurante> restaurantes = bll.GetAll();

                response.Result = restaurantes;
                response.Status = StatusResponse.SUCCESS.Value;
            }
            catch (ListNoContentException ex)
            {
                response.Status = StatusResponse.ERROR.Value;
                response.Message = ex.Message;
            }
            catch (Exception ex)
            {
                response.Status = StatusResponse.ERROR.Value;
                response.Message = "Erro ao consultar todos os restaurantes: " + ex.Message;
            }

            return new ObjectResult(response);
        }

        [HttpGet("{id}", Name = "GetRestaurante")]
        public IActionResult GetById(long id)
        {
            ResponseDto<Restaurante> response = new ResponseDto<Restaurante>();
            try
            {
                RestauranteBll bll = new RestauranteBll(_context);
                Restaurante restaurante = bll.GetById(id);

                response.Result = restaurante;
                response.Status = StatusResponse.SUCCESS.Value;
            }
            catch (EntityNotFoundException ex)
            {
                response.Status = StatusResponse.ERROR.Value;
                response.Message = ex.Message;
            }
            catch (Exception ex)
            {
                response.Status = StatusResponse.ERROR.Value;
                response.Message = "Erro ao consultar restaurante: " + ex.Message;
            }

            return new ObjectResult(response);
        }

        [HttpGet("{nome}/pesquisar", Name = "GetRestauranteByName")]
        public IActionResult GetByNome(string nome)
        {
            ResponseDto<List<Restaurante>> response = new ResponseDto<List<Restaurante>>();
            try
            {
                RestauranteBll bll = new RestauranteBll(_context);
                List<Restaurante> restaurantes = bll.GetByNome(nome);

                response.Result = restaurantes;
                response.Status = StatusResponse.SUCCESS.Value;
            }
            catch (EntityNotFoundException ex)
            {
                response.Status = StatusResponse.ERROR.Value;
                response.Message = ex.Message;
            }
            catch (Exception ex)
            {
                response.Status = StatusResponse.ERROR.Value;
                response.Message = "Erro ao consultar restaurante: " + ex.Message;
            }

            return new ObjectResult(response);
        }

        [HttpDelete("{id}", Name = "Delete")]
        public IActionResult Delete(int id)
        {
            ResponseDto<bool> response = new ResponseDto<bool>();
            try
            {
                RestauranteBll bll = new RestauranteBll(_context);
                bll.Delete(id);
                response.Status = StatusResponse.SUCCESS.Value;
            }
            catch (Exception ex)
            {
                response.Status = StatusResponse.ERROR.Value;
                response.Message = "Erro ao excluir restaurante: " + ex.Message;
            }

            return new ObjectResult(response);
        }
    }
}