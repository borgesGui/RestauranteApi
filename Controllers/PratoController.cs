using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RestauranteApi.Models;
using RestauranteApi.Context;
using RestauranteApi.Bll;
using RestauranteApi.Response;
using RestauranteApi.Response.Enums;
using RestauranteApi.Exceptions;
using Microsoft.AspNetCore.Cors;

namespace RestauranteApi.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    public class PratoController : Controller
    {
        private readonly RestauranteDbContext _context;

        public PratoController(RestauranteDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            ResponseDto<List<Prato>> response = new ResponseDto<List<Prato>>();
            try
            {
                PratoBll bll = new PratoBll(_context);
                List<Prato> pratos = bll.GetAll();

                response.Result = pratos;
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
                response.Message = "Erro ao consultar todos os pratos: " + ex.Message;
            }

            return new ObjectResult(response);
        }

        [HttpGet("{id}", Name = "GetPrato")]
        public IActionResult GetById(long id)
        {
            ResponseDto<Prato> response = new ResponseDto<Prato>();
            try
            {
                PratoBll bll = new PratoBll(_context);
                Prato prato = bll.GetById(id);

                response.Result = prato;
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
                response.Message = "Erro ao consultar prato: " + ex.Message;
            }

            return new ObjectResult(response);
        }

        [HttpPost]
        public IActionResult Save([FromBody] Prato item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            ResponseDto<Prato> response = new ResponseDto<Prato>();
            try
            {
                PratoBll bll = new PratoBll(_context);
                Prato prato = bll.SaveWithRestauranteValidation(item);

                response.Result = bll.GetById(prato.Id, false);
                response.Status = StatusResponse.SUCCESS.Value;
            }
            catch (Exception ex)
            {
                response.Status = StatusResponse.ERROR.Value;
                response.Message = "Erro ao salvar o prato: " + ex.Message;
            }

            return new ObjectResult(response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            ResponseDto<bool> response = new ResponseDto<bool>();
            try
            {
                PratoBll bll = new PratoBll(_context);
                bll.Delete(id);
                response.Status = StatusResponse.SUCCESS.Value;
            }
            catch (Exception ex)
            {
                response.Status = StatusResponse.ERROR.Value;
                response.Message = "Erro ao excluir prato: " + ex.Message;
            }

            return new ObjectResult(response);
        }
    }
}