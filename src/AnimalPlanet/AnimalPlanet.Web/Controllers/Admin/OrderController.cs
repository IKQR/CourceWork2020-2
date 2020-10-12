using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AnimalPlanet.Bl.Abstract.IServices;
using AnimalPlanet.Configuration;
using AnimalPlanet.DAL.Abstract.IRepositories;
using AnimalPlanet.DAL.Entities.Tables;
using AnimalPlanet.Models;
using AnimalPlanet.Models.Models;
using AnimalPlanet.Models.Pagination;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AnimalPlanet.Web.Controllers.Admin
{
    [Route("admin/order")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IOrderRepository _orderRepository;
        private readonly IClassRepository _classRepository;
        private readonly PaginationConfiguration _paginationConfiguration;

        public OrderController(
            IOrderService orderService,
            IOrderRepository orderRepository,
            IClassRepository classRepository,
            PaginationConfiguration paginationConfiguration)
        {
            _orderService = orderService;
            _orderRepository = orderRepository;
            _classRepository = classRepository;
            _paginationConfiguration = paginationConfiguration;
        }

        [Route("")]
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = _paginationConfiguration.PageSize;
            int width = _paginationConfiguration.Width;
            int count = await _orderRepository.GetCount();
            int currentPage = page ?? 1;

            DataResult<List<OrderModel>> result =
                await _orderService.GetPartOfOrders((currentPage - 1) * pageSize, pageSize);

            if (result.Success)
            {
                return View(new GenericPaginatedModel<OrderModel>
                {
                    Models = result.Data,
                    Pagination = new PaginationModel(
                        count,
                        currentPage,
                        pageSize,
                        "Index"
                    ),
                });
            }

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(Order) });
        }

        [Route("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            DataResult<OrderModel> result = await _orderService.GetOrderById(id);

            if (result.Success)
            {
                return View("Details", result.Data);
            }

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(Order) });
        }

        [HttpGet]
        [Route("edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            DataResult<OrderModel> result = await _orderService.GetOrderById(id);

            ViewBag.Classs = new SelectList(
                (await _classRepository.GetAll()).OrderBy(e => e.Denomination),
                nameof(Order.Id),
                nameof(Order.Denomination));

            if (result.Success)
            {
                return View("Edit", result.Data);
            }

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(Order) });
        }
        [HttpPost]
        [Route("edit/{id:int}")]
        public async Task<IActionResult> Edit(int id, OrderModel model)
        {

            Result result = await _orderService.UpdateOrder(id, model);

            if (result.Success)
            {
                return RedirectToAction("Details", new { id });
            }

            if (result.ErrorCode == ErrorCode.UniquenessError)
            {

                ViewBag.Classs = new SelectList(
                    (await _classRepository.GetAll()).OrderBy(e => e.Denomination),
                    nameof(Order.Id),
                    nameof(Order.Denomination));

                ModelState[nameof(model.Denomination)].Errors.Add("Such a record already exists");
                return View("Edit", model);
            }

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(Order) });
        }

        [HttpGet]
        [Route("create")]
        public async Task<IActionResult> Create(int id)
        {
            ViewBag.Classs = new SelectList(
                (await _classRepository.GetAll()).OrderBy(e => e.Denomination),
                nameof(Order.Id),
                nameof(Order.Denomination));

            return View("Create", new OrderModel());
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(OrderModel model)
        {
            DataResult<OrderModel> result = await _orderService.CreateOrder(model);

            if (result.Success)
            {
                return RedirectToAction("Details", new { result.Data.Id });
            }

            if (result.ErrorCode == ErrorCode.UniquenessError)
            {
                ViewBag.Classs = new SelectList(
                    (await _classRepository.GetAll()).OrderBy(e => e.Denomination),
                    nameof(Order.Id),
                    nameof(Order.Denomination));

                ModelState[nameof(model.Denomination)].Errors.Add("Such a record already exists");
                return View("Create", model);
            }

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(Order) });
        }
        [HttpGet]
        [Route("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            Result result = await _orderService.DeleteOrder(id);

            if (result.Success)
            {
                return View("_DeleteModel");
            }

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(Class) });
        }
    }
}
