using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesOrder.API.Core;
using SalesOrder.API.Core.Common;
using SalesOrder.API.Core.Domain;
using SalesOrder.API.Dtos;

namespace SalesOrder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrdersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Order
        // GET: api/Orders
        [HttpGet]
        public IActionResult GetAllOrders()
        {
            try
            {
                var orders = _unitOfWork.Orders.GetAll();

                var dtos = new List<OrderDto>();

                foreach (var order in orders)
                {
                    OrderDto dto = new OrderDto();
                    dto.Id = order.Id;
                    dto.InvoiceNo = order.InvoiceNo;
                    dto.InvoiceDate = order.InvoiceDate;
                    dto.ReferenceNo = order.ReferenceNo;
                    dto.Note = order.Note;
                    dto.CustomerId = order.Customer.Id;

                    dtos.Add(dto);
                }
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public IActionResult GetOrderById(long id)
        {
            try
            {
                Order order = _unitOfWork.Orders.Get(id);
                if (order == null)
                    return NotFound("Order Not Found");

                var dto = new OrderDto
                {
                    Id = order.Id,
                    InvoiceNo = order.InvoiceNo,
                    InvoiceDate = order.InvoiceDate,
                    ReferenceNo = order.ReferenceNo,
                    Note = order.Note,
                    CustomerId = order.Customer.Id
                };

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult AddOrder([FromBody] OrderDto orderDto)
        {
            try
            {
                Customer customer = _unitOfWork.Customers.Get(orderDto.CustomerId);

                if (customer == null)
                {
                    return NotFound("Customer Not Found");
                }

                Result<InvoiceNo> invoiceNoOrError = InvoiceNo.Create(orderDto.InvoiceNo);
                Result<InvoiceDate> invoiceDateOrError = InvoiceDate.Create(orderDto.InvoiceDate);
                Result<ReferenceNo> referenceNoOrError = ReferenceNo.Create(orderDto.ReferenceNo);

                Result result = Result.Combine(invoiceNoOrError, invoiceDateOrError, referenceNoOrError);
                if (result.IsFailure)
                {
                    return BadRequest(result.Error);
                }

                var order = new Order(invoiceNoOrError.Value, invoiceDateOrError.Value, referenceNoOrError.Value, orderDto.Note, customer);

                _unitOfWork.Orders.Add(order);
                _unitOfWork.Complete();

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public IActionResult PutOrder(long id, OrderDto orderDto)
        {
            try
            {
                if (id != orderDto.Id)
                {
                    return BadRequest();
                }

                Order order = _unitOfWork.Orders.Get(id);
                if (order == null)
                    return NotFound("Order Not Found");

                Customer customer = _unitOfWork.Customers.Get(orderDto.CustomerId);

                if (customer == null)
                {
                    return NotFound("Customer Not Found");
                }

                Result<InvoiceDate> invoiceDateOrError = InvoiceDate.Create(orderDto.InvoiceDate);
                Result<ReferenceNo> referenceNoOrError = ReferenceNo.Create(orderDto.ReferenceNo);

                Result result = Result.Combine(invoiceDateOrError, referenceNoOrError);
                if (result.IsFailure)
                {
                    return BadRequest(result.Error);
                }

                order.UpdateOrder(invoiceDateOrError.Value, referenceNoOrError.Value, orderDto.Note, customer);

                _unitOfWork.Complete();

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(long id)
        {
            try
            {
                Order order = _unitOfWork.Orders.Get(id);

                if (order == null)
                    return NotFound("Order Not Exist.");

                _unitOfWork.Orders.Remove(order);

                _unitOfWork.Complete();

                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }


        #endregion

        #region Order Line
        [HttpGet("/api/Orders/{orderId}/OrderLine")]
        public IActionResult GetOrderLinesForOrder(long orderId)
        {
            try
            {
                var order = _unitOfWork.Orders.GetOrderWithOrderLines(orderId);

                if(order == null)
                {
                    return NotFound("Order Not Exist.");
                }

                var dtos = new List<OrderLineDto>();

                foreach (var orderLine in order.OrderLines)
                {
                    OrderLineDto dto = new OrderLineDto();
                    dto.Id = orderLine.Id;
                    dto.Note = orderLine.Note;
                    dto.Quantity = orderLine.Quantity;
                    dto.Tax = orderLine.Tax;
                    dto.ItemId = orderLine.Item.Id;
                    dto.ExclAmount = orderLine.ExclAmount;
                    dto.TaxAmount = orderLine.TaxAmount;
                    dto.InclAmount = orderLine.InclAmount;

                    dtos.Add(dto);
                }
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("/api/Orders/{orderId}/OrderLine")]
        public IActionResult AddOrderLineToOrder(long orderId, [FromBody] CreateOrderLineDto createOrderLineDto)
        {
            try
            {
                Order order = _unitOfWork.Orders.Get(orderId);

                if (order == null)
                {
                    return BadRequest("Order Not Found");
                }

                Item item = _unitOfWork.Items.Get(createOrderLineDto.ItemId);

                if (item == null)
                {
                    return BadRequest("Item Not Found");
                }

                Result<Quantity> quantityOrError = Quantity.Create(createOrderLineDto.Quantity);
                Result<Tax> taxOrError = Tax.Create(createOrderLineDto.Tax);

                Result result = Result.Combine(quantityOrError, taxOrError);
                if (result.IsFailure)
                {
                    return BadRequest(result.Error);
                }

                order.AddOrderLine(createOrderLineDto.Note, quantityOrError.Value, taxOrError.Value, item, order);

                _unitOfWork.Orders.Add(order);
                _unitOfWork.Complete();

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        [HttpPut("/api/Orders/{orderId}/OrderLine")]
        public IActionResult UpdateOrderLineInOrder(long orderId, [FromBody] CreateOrderLineDto createOrderLineDto)
        {
            try
            {
                var order = _unitOfWork.Orders.GetOrderWithOrderLines(orderId);

                if (order == null)
                {
                    return BadRequest("Order Not Found");
                }


                Result<Quantity> quantityOrError = Quantity.Create(createOrderLineDto.Quantity);
                Result<Tax> taxOrError = Tax.Create(createOrderLineDto.Tax);

                Result result = Result.Combine(quantityOrError, taxOrError);
                if (result.IsFailure)
                {
                    return BadRequest(result.Error);
                }

                order.UpdateOrderLine(createOrderLineDto.Id, createOrderLineDto.Note, quantityOrError.Value, taxOrError.Value);

                _unitOfWork.Complete();

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        [HttpDelete("/api/Orders/{orderId}/OrderLine/{OrderLineId}")]
        public IActionResult RemoveOrderLineFromOrder(long orderId, long OrderLineId)
        {
            try
            {
                var order = _unitOfWork.Orders.GetOrderWithOrderLines(orderId);

                if (order == null)
                {
                    return BadRequest("Order Not Found");
                }

                order.RemoveOrderLine(OrderLineId);

                _unitOfWork.Complete();

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }
        #endregion
    }
}
