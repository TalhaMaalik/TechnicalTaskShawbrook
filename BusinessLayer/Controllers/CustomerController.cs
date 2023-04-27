using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Validator;
using DataAccessLayer.Data;
using DataAccessLayer.DataModel;
using Microsoft.AspNetCore.Mvc;

namespace BusinessLayer.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _CustomerRepository;
        private readonly IValidator _Validator;
        private readonly IMapper _Mapper;

        public CustomerController(ICustomerRepository customerRepository, IValidator validator, IMapper mapper)
        {
            _CustomerRepository = customerRepository;
            _Validator = validator;
            _Mapper = mapper;
        }

        [HttpPost]
        [Route("create")]
        public ActionResult<CustomerReadDTO> Create(CustomerCreateDTO customer)
        {
            try
            {
                _Validator.Validate(customer);
                var customerModel = _Mapper.Map<CustomerModel>(customer);
                customerModel.Id = Guid.NewGuid();
                _CustomerRepository.CreateCustomer(customerModel);
                if(_CustomerRepository.SaveChanges())
                    return Created(HttpContext.Request.Path, _Mapper.Map<CustomerReadDTO>(customerModel));
                else
                    return Problem();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
