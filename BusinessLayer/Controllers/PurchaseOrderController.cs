using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Models.Base;
using BusinessLayer.Processor;
using BusinessLayer.Processors.Factory;
using BusinessLayer.Validator;
using Microsoft.AspNetCore.Mvc;

namespace BusinessLayer.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly IValidator _Validator;
        private readonly IMapper _Mapper;
        private readonly IItemFactory _ItemFactory;
        private readonly IPurchaseOrderProcessor _PurchaseOrderProcessor;

        public PurchaseOrderController(IValidator validator, IMapper mapper, IItemFactory itemFactory, IPurchaseOrderProcessor purchaseOrderProcessor) 
        {
            _Validator = validator;
            _Mapper = mapper;
            _ItemFactory = itemFactory;
            _PurchaseOrderProcessor = purchaseOrderProcessor;
        }

        [HttpPost]
        [Route("create")]
        public ActionResult<string> CreatePurchaseOrder(PurchaseOrderCreateDTO order)
        {
            try
            {
                _Validator.Validate(order);
                var purchaseOrder = _Mapper.Map<PurchaseOrder>(order);
                foreach (var orderItem in order.Items!)
                    purchaseOrder.Items.Add(_ItemFactory.Create(orderItem.ItemId));
                var shippingSlip = _PurchaseOrderProcessor.Process(purchaseOrder);
                return Ok(shippingSlip);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
