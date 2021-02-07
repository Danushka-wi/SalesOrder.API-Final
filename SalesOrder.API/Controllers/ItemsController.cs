using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesOrder.API.Core;
using SalesOrder.API.Core.Domain;
using SalesOrder.API.Data;
using SalesOrder.API.Dtos;

namespace SalesOrder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class ItemsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ItemsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Items
        [HttpGet]
        public IActionResult GetItems()
        {
            try
            {
                var items = _unitOfWork.Items.GetAll();

                var dtos = new List<ItemDto>();

                foreach (var item in items)
                {
                    ItemDto dto = new ItemDto();
                    dto.Id = item.Id;
                    dto.Desription = item.Desription;
                    dto.Price = item.Price;

                    dtos.Add(dto);
                }
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: api/Items/5
        [HttpGet("{id}")]
        public IActionResult GetItem(long id)
        {
            try
            {
                Item item = _unitOfWork.Items.Get(id);
                if (item == null)
                    return NotFound();

                var dto = new ItemDto
                {
                    Id = item.Id,
                    Desription = item.Desription,
                    Price = item.Price
                };

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }        
    }
}
