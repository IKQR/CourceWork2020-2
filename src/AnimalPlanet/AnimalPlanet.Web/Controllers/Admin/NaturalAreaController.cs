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
    [Route("admin/naturalArea")]
    public class NaturalAreaController : Controller
    {
        private readonly INaturalAreaService _naturalAreaService;
        private readonly INaturalAreaRepository _naturalAreaRepository;
        private readonly PaginationConfiguration _paginationConfiguration;

        public NaturalAreaController(
            INaturalAreaService naturalAreaService,
            INaturalAreaRepository naturalAreaRepository,
            PaginationConfiguration paginationConfiguration)
        {
            _naturalAreaService = naturalAreaService;
            _naturalAreaRepository = naturalAreaRepository;
            _paginationConfiguration = paginationConfiguration;

        }

        [Route("")]
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = _paginationConfiguration.PageSize;
            int width = _paginationConfiguration.Width;
            int count = await _naturalAreaRepository.GetCount();
            int currentPage = page ?? 1;

            DataResult<List<NaturalAreaModel>> result =
                await _naturalAreaService.GetPartOfNaturalAreas((currentPage - 1) * pageSize, pageSize);

            if (result.Success)
            {
                return View(new GenericPaginatedModel<NaturalAreaModel>
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

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(NaturalArea) });
        }

        [Route("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            DataResult<NaturalAreaModel> result = await _naturalAreaService.GetNaturalAreaById(id);

            if (result.Success)
            {
                return View("Details", result.Data);
            }

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(NaturalArea) });
        }

        [HttpGet]
        [Route("edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            DataResult<NaturalAreaModel> result = await _naturalAreaService.GetNaturalAreaById(id);

            if (result.Success)
            {
                return View("Edit", result.Data);
            }

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(NaturalArea) });
        }
        [HttpPost]
        [Route("edit/{id:int}")]
        public async Task<IActionResult> Edit(int id, NaturalAreaModel model)
        {

            Result result = await _naturalAreaService.UpdateNaturalArea(id, model);

            if (result.Success)
            {
                return RedirectToAction("Details", new { id });
            }

            if (result.ErrorCode == ErrorCode.UniquenessError)
            {
                ModelState[nameof(model.Denomination)].Errors.Add("Such a record already exists");
                return View("Edit", model);
            }

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(NaturalArea) });
        }

        [HttpGet]
        [Route("create")]
        public IActionResult Create(int id)
        {
            return View("Create", new NaturalAreaModel());
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(NaturalAreaModel model)
        {

            DataResult<NaturalAreaModel> result = await _naturalAreaService.CreateNaturalArea(model);

            if (result.Success)
            {
                return RedirectToAction("Details", new { result.Data.Id });
            }

            if (result.ErrorCode == ErrorCode.UniquenessError)
            {
                ModelState[nameof(model.Denomination)].Errors.Add("Such a record already exists");
                return View("Create", model);
            }

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(NaturalArea) });
        }

        [HttpGet]
        [Route("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            Result result = await _naturalAreaService.DeleteNaturalArea(id);

            if (result.Success)
            {
                return View("_DeleteModel");
            }

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(NaturalArea) });
        }
    }
}
