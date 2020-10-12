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
    [Route("admin/family")]
    public class FamilyController : Controller
    {
        private readonly IFamilyService _familyService;
        private readonly IFamilyRepository _familyRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly PaginationConfiguration _paginationConfiguration;

        public FamilyController(
            IFamilyService familyService,
            IFamilyRepository familyRepository,
            IOrderRepository orderRepository,
            PaginationConfiguration paginationConfiguration)
        {
            _familyService = familyService;
            _familyRepository = familyRepository;
            _orderRepository = orderRepository;
            _paginationConfiguration = paginationConfiguration;
        }

        [Route("")]
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = _paginationConfiguration.PageSize;
            int width = _paginationConfiguration.Width;
            int count = await _familyRepository.GetCount();
            int currentPage = page ?? 1;

            DataResult<List<FamilyModel>> result =
                await _familyService.GetPartOfFamilies((currentPage - 1) * pageSize, pageSize);

            if (result.Success)
            {
                return View(new GenericPaginatedModel<FamilyModel>
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

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(Family) });
        }

        [Route("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            DataResult<FamilyModel> result = await _familyService.GetFamilyById(id);

            if (result.Success)
            {
                return View("Details", result.Data);
            }

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(Family) });
        }

        [HttpGet]
        [Route("edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            DataResult<FamilyModel> result = await _familyService.GetFamilyById(id);

            ViewBag.Orders = new SelectList(
                (await _orderRepository.GetAll()).OrderBy(e => e.Denomination),
                nameof(Family.Id),
                nameof(Family.Denomination));

            if (result.Success)
            {
                return View("Edit", result.Data);
            }

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(Family) });
        }
        [HttpPost]
        [Route("edit/{id:int}")]
        public async Task<IActionResult> Edit(int id, FamilyModel model)
        {

            Result result = await _familyService.UpdateFamily(id, model);

            if (result.Success)
            {
                return RedirectToAction("Details", new { id });
            }

            if (result.ErrorCode == ErrorCode.UniquenessError)
            {
                ViewBag.Orders = new SelectList(
                    (await _orderRepository.GetAll()).OrderBy(e => e.Denomination),
                    nameof(Family.Id),
                    nameof(Family.Denomination));

                ModelState[nameof(model.Denomination)].Errors.Add("Such a record already exists");
                return View("Edit", model);
            }

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(Family) });
        }

        [HttpGet]
        [Route("create")]
        public async Task<IActionResult> Create(int id)
        {
            ViewBag.Orders = new SelectList(
                (await _orderRepository.GetAll()).OrderBy(e => e.Denomination),
                nameof(Family.Id),
                nameof(Family.Denomination));

            return View("Create", new FamilyModel());
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(FamilyModel model)
        {
            DataResult<FamilyModel> result = await _familyService.CreateFamily(model);

            if (result.Success)
            {
                return RedirectToAction("Details", new { result.Data.Id });
            }

            if (result.ErrorCode == ErrorCode.UniquenessError)
            {
                ViewBag.Orders = new SelectList(
                    (await _orderRepository.GetAll()).OrderBy(e => e.Denomination),
                    nameof(Family.Id),
                    nameof(Family.Denomination));

                ModelState[nameof(model.Denomination)].Errors.Add("Such a record already exists");
                return View("Create", model);
            }

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(Family) });
        }
        [HttpGet]
        [Route("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            Result result = await _familyService.DeleteFamily(id);

            if (result.Success)
            {
                return View("_DeleteModel");
            }

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(Order) });
        }
    }
}
