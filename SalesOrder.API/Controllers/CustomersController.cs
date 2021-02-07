using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesOrder.API.Core;
using SalesOrder.API.Core.Domain;
using SalesOrder.API.Dtos;

namespace SalesOrder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class CustomersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Customers
        [HttpGet]
        public IActionResult GetCustomers()
        {
            try
            {
                var customers = _unitOfWork.Customers.GetAll();

                var dtos = new List<CustomerDto>();

                foreach (var item in customers)
                {
                    CustomerDto dto = new CustomerDto();
                    dto.Id = item.Id;
                    dto.CustomerName = item.CustomerName;
                    dto.Address1 = item.Address1;
                    dto.Address2 = item.Address2;
                    dto.Address3 = item.Address3;
                    dto.Suburb = item.Suburb;
                    dto.State = item.State;
                    dto.PostCode = item.PostCode;

                    dtos.Add(dto);
                }
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public IActionResult GetCustomer(long id)
        {
            try
            {
                Customer customer = _unitOfWork.Customers.Get(id);
                if (customer == null)
                    return NotFound();

                var dto = new CustomerDto
                {
                    Id = customer.Id,
                    CustomerName = customer.CustomerName,
                    Address1 = customer.Address1,
                    Address2 = customer.Address2,
                    Address3 = customer.Address3,
                    Suburb = customer.Suburb,
                    State = customer.State,
                    PostCode = customer.PostCode
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
