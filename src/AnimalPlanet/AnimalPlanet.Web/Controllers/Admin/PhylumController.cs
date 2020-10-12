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
    [Route("admin/phylum")]
    public class PhylumController : Controller
    {
        private readonly IPhylumService _phylumService;
        private readonly IPhylumRepository _phylumRepository;
        private readonly PaginationConfiguration _paginationConfiguration;

        public PhylumController(
            IPhylumService phylumService,
            IPhylumRepository phylumRepository,
            PaginationConfiguration paginationConfiguration)
        {
            _phylumService = phylumService;
            _phylumRepository = phylumRepository;
            _paginationConfiguration = paginationConfiguration;

        }

        [Route("")]
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = _paginationConfiguration.PageSize;
            int width = _paginationConfiguration.Width;
            int count = await _phylumRepository.GetCount();
            int currentPage = page ?? 1;

            DataResult<List<PhylumModel>> result =
                await _phylumService.GetPartOfPhylums((currentPage - 1) * pageSize, pageSize);

            if (result.Success)
            {
                return View(new GenericPaginatedModel<PhylumModel>
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

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(Phylum) });
        }

        [Route("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            DataResult<PhylumModel> result = await _phylumService.GetPhylumById(id);

            if (result.Success)
            {
                return View("Details", result.Data);
            }

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(Phylum) });
        }

        [HttpGet]
        [Route("edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            DataResult<PhylumModel> result = await _phylumService.GetPhylumById(id);

            if (result.Success)
            {
                return View("Edit", result.Data);
            }

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(Phylum) });
        }
        [HttpPost]
        [Route("edit/{id:int}")]
        public async Task<IActionResult> Edit(int id, PhylumModel model)
        {

            Result result = await _phylumService.UpdatePhylum(id, model);

            if (result.Success)
            {
                return RedirectToAction("Details", new { id });
            }

            if (result.ErrorCode == ErrorCode.UniquenessError)
            {
                ModelState[nameof(model.Denomination)].Errors.Add("Such a record already exists");
                return View("Edit", model);
            }

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(Phylum) });
        }

        [HttpGet]
        [Route("create")]
        public IActionResult Create(int id)
        {
            return View("Create", new PhylumModel());
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(PhylumModel model)
        {

            DataResult<PhylumModel> result = await _phylumService.CreatePhylum(model);

            if (result.Success)
            {
                return RedirectToAction("Details", new { result.Data.Id });
            }

            if (result.ErrorCode == ErrorCode.UniquenessError)
            {
                ModelState[nameof(model.Denomination)].Errors.Add("Such a record already exists");
                return View("Create", model);
            }

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(Phylum) });
        }

        [HttpGet]
        [Route("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            Result result = await _phylumService.DeletePhylum(id);

            if (result.Success)
            {
                return View("_DeleteModel");
            }

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(Phylum) });
        }
    }
}
