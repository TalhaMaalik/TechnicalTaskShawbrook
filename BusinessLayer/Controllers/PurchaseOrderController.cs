using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Factory;
using BusinessLayer.Models.Base;
using BusinessLayer.Processor;
using BusinessLayer.Validator;
using DataAccessLayer.Data;
using Microsoft.AspNetCore.Mvc;

namespace BusinessLayer.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly IValidator _Validator;
        private readonly IItemRepository _ItemRepository;
        private readonly IMapper _Mapper;
        private readonly IItemFactory _ItemFactory;
        private readonly IPurchaseOrderProcessor _PurchaseOrderProcessor;

        public PurchaseOrderController(IValidator validator, IItemRepository itemRepository, IMapper mapper, IItemFactory itemFactory, IPurchaseOrderProcessor purchaseOrderProcessor) 
        {
            _Validator = validator;
            _ItemRepository = itemRepository;
            _Mapper = mapper;
            _ItemFactory = itemFactory;
            _PurchaseOrderProcessor = purchaseOrderProcessor;
        }

        [HttpPost]
        [Route("create")]
        public IActionResult CreatePurchaseOrder(PurchaseOrderCreateDTO order)
        {
            try
            {
                _Validator.Validate(order);
                var purchaseOrder = _Mapper.Map<PurchaseOrder>(order);
                foreach (var orderItem in order.Items!)
                    purchaseOrder.Items.Add(_ItemFactory.CreateItem(orderItem.ItemId));
                var shippingSlip = _PurchaseOrderProcessor.Process(purchaseOrder);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
