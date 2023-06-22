using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiCore.Exceptions;
using WebApiCore.Models;
using WebApiCore.Services.CustomersServices;
using WebApiCore.Services.UnitOfWork;

namespace WebApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController : ControllerBase
    {

        private IUnitOfWork _unitOfWork;
        private readonly ILogger<CustomersController> _logger;
        public CustomersController(IUnitOfWork unitOfWork, ILogger<CustomersController> logger) {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into HomeController");
        }   

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customers>>> GetAllCustomers()
        {
            _logger.LogInformation("Hello, this is the GetAllCustomers!");
            IEnumerable<Customers> customer = await _unitOfWork.CustomerRepository.GetAll();
            if (customer == null)
            {
                return NotFound();
            }
            return customer.ToList(); 
        }


       
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Customers>>> GetSingleCustomer(int id)
        {

            var result = await _unitOfWork.CustomerRepository.GetById(id);
            if (result is null)
            {
                throw new NotFoundException("Customer not found");
            }
            return Ok(result);
        }


        [HttpPost]
        public async Task<ActionResult<List<Customers>>> AddCustomer(Customers customer)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _unitOfWork.CustomerRepository.Insert(customer);
            _unitOfWork.Save();
            return Ok(result);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<List<Customers>>> UpdateCustomer(int id, Customers customer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _unitOfWork.CustomerRepository.Update(id, customer);
                if (result is null)
                {
                    throw new NotFoundException("Customer not found");
                    //return NotFound("Customer not found");
                }
                _unitOfWork.Save();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Customers>>> DeleteCustomer(int id)
        {
            var result = await _unitOfWork.CustomerRepository.Delete(id);
            if(result is null)
            {
                return NotFound("Customer not found");
            }
            _unitOfWork.Save();
            return Ok(result);
           
        }
    }
}
