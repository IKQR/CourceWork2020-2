using System.Collections.Generic;
using System.Threading.Tasks;

using AnimalPlanet.Bl.Abstract.IServices;
using AnimalPlanet.Configuration;
using AnimalPlanet.DAL.Abstract.IRepositories;
using AnimalPlanet.DAL.Entities.Tables;
using AnimalPlanet.Models;
using AnimalPlanet.Models.Models;
using AnimalPlanet.Models.Pagination;

using Microsoft.AspNetCore.Mvc;

namespace AnimalPlanet.Web.Controllers.Admin
{
    [Route("admin/reserve")]
    public class ReserveController : Controller
    {
        private readonly IReserveService _reserveService;
        private readonly IReserveRepository _reserveRepository;
        private readonly PaginationConfiguration _paginationConfiguration;

        public ReserveController(
            IReserveService reserveService,
            IReserveRepository reserveRepository,
            PaginationConfiguration paginationConfiguration)
        {
            _reserveService = reserveService;
            _reserveRepository = reserveRepository;
            _paginationConfiguration = paginationConfiguration;

        }

        [Route("")]
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = _paginationConfiguration.PageSize;
            int width = _paginationConfiguration.Width;
            int count = await _reserveRepository.GetCount();
            int currentPage = page ?? 1;

            DataResult<List<ReserveModel>> result =
                await _reserveService.GetPartOfReserves((currentPage - 1) * pageSize, pageSize);

            if (result.Success)
            {
                return View(new GenericPaginatedModel<ReserveModel>
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

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(Reserve) });
        }

        [Route("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            DataResult<ReserveModel> result = await _reserveService.GetReserveById(id);

            if (result.Success)
            {
                return View("Details", result.Data);
            }

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(Reserve) });
        }

        [HttpGet]
        [Route("edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            DataResult<ReserveModel> result = await _reserveService.GetReserveById(id);

            if (result.Success)
            {
                return View("Edit", result.Data);
            }

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(Reserve) });
        }
        [HttpPost]
        [Route("edit/{id:int}")]
        public async Task<IActionResult> Edit(int id, ReserveModel model)
        {

            Result result = await _reserveService.UpdateReserve(id, model);

            if (result.Success)
            {
                return RedirectToAction("Details", new { id });
            }

            if (result.ErrorCode == ErrorCode.UniquenessError)
            {
                ModelState[nameof(model.Name)].Errors.Add("Such a record already exists");
                return View("Edit", model);
            }

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(Reserve) });
        }

        [HttpGet]
        [Route("create")]
        public IActionResult Create(int id)
        {
            return View("Create", new ReserveModel());
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(ReserveModel model)
        {

            DataResult<ReserveModel> result = await _reserveService.CreateReserve(model);

            if (result.Success)
            {
                return RedirectToAction("Details", new { result.Data.Id });
            }

            if (result.ErrorCode == ErrorCode.UniquenessError)
            {
                ModelState[nameof(model.Name)].Errors.Add("Such a record already exists");
                return View("Create", model);
            }

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(Reserve) });
        }

        [HttpGet]
        [Route("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            Result result = await _reserveService.DeleteReserve(id);

            if (result.Success)
            {
                return View("_DeleteModel");
            }

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(Reserve) });
        }
    }
}
