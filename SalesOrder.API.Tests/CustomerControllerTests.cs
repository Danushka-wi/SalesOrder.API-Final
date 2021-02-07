using Microsoft.AspNetCore.Mvc;
using Moq;
using SalesOrder.API.Controllers;
using SalesOrder.API.Core;
using SalesOrder.API.Core.Domain;
using SalesOrder.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SalesOrder.API.Tests
{
    public class CustomerControllerTests
    {

        private CustomersController controller;
        private readonly Mock<IUnitOfWork> _unitOfWork = new Mock<IUnitOfWork>();


        public CustomerControllerTests()
        {
            controller = new CustomersController(_unitOfWork.Object);
        }

        [Fact]
        public void GetCustomer_ShouldReturnCustomer_WhenCustomerExists()
        {
            //Arrange
            var customerId = 1;
            var customerMock = new Mock<Customer>(customerId);

            _unitOfWork.Setup(x => x.Customers.Get(customerId)).Returns(customerMock.Object);

            //Act
            var result = controller.GetCustomer(customerId);
            var okResult = result as OkObjectResult;
            var cusDto = okResult.Value as CustomerDto;

            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(customerId, cusDto.Id);
        }
    }
}
