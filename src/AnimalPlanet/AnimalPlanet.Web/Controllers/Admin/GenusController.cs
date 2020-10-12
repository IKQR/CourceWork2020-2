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
    [Route("admin/genus")]
    public class GenusController : Controller
    {
        private readonly IGenusService _genusService;
        private readonly IGenusRepository _genusRepository;
        private readonly IFamilyRepository _familyRepository;
        private readonly PaginationConfiguration _paginationConfiguration;

        public GenusController(
            IGenusService genusService,
            IGenusRepository genusRepository,
            IFamilyRepository familyRepository,
            PaginationConfiguration paginationConfiguration)
        {
            _genusService = genusService;
            _genusRepository = genusRepository;
            _familyRepository = familyRepository;
            _paginationConfiguration = paginationConfiguration;
        }

        [Route("")]
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = _paginationConfiguration.PageSize;
            int width = _paginationConfiguration.Width;
            int count = await _genusRepository.GetCount();
            int currentPage = page ?? 1;

            DataResult<List<GenusModel>> result =
                await _genusService.GetPartOfGenuses((currentPage - 1) * pageSize, pageSize);

            if (result.Success)
            {
                return View(new GenericPaginatedModel<GenusModel>
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

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(Genus) });
        }

        [Route("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            DataResult<GenusModel> result = await _genusService.GetGenusById(id);

            if (result.Success)
            {
                return View("Details", result.Data);
            }

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(Genus) });
        }

        [HttpGet]
        [Route("edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            DataResult<GenusModel> result = await _genusService.GetGenusById(id);

            ViewBag.Families = new SelectList(
                (await _familyRepository.GetAll()).OrderBy(e => e.Denomination),
                nameof(Genus.Id),
                nameof(Genus.Denomination));

            if (result.Success)
            {
                return View("Edit", result.Data);
            }

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(Genus) });
        }
        [HttpPost]
        [Route("edit/{id:int}")]
        public async Task<IActionResult> Edit(int id, GenusModel model)
        {

            Result result = await _genusService.UpdateGenus(id, model);

            if (result.Success)
            {
                return RedirectToAction("Details", new { id });
            }

            if (result.ErrorCode == ErrorCode.UniquenessError)
            {
                ViewBag.Families = new SelectList(
                    (await _familyRepository.GetAll()).OrderBy(e => e.Denomination),
                    nameof(Genus.Id),
                    nameof(Genus.Denomination));

                ModelState[nameof(model.Denomination)].Errors.Add("Such a record already exists");
                return View("Edit", model);
            }

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(Genus) });
        }

        [HttpGet]
        [Route("create")]
        public async Task<IActionResult> Create(int id)
        {
            ViewBag.Families = new SelectList(
                (await _familyRepository.GetAll()).OrderBy(e => e.Denomination),
                nameof(Genus.Id),
                nameof(Genus.Denomination));

            return View("Create", new GenusModel());
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(GenusModel model)
        {
            DataResult<GenusModel> result = await _genusService.CreateGenus(model);

            if (result.Success)
            {
                return RedirectToAction("Details", new { result.Data.Id });
            }

            if (result.ErrorCode == ErrorCode.UniquenessError)
            {
                ViewBag.Families = new SelectList(
                    (await _familyRepository.GetAll()).OrderBy(e => e.Denomination),
                    nameof(Genus.Id),
                    nameof(Genus.Denomination));

                ModelState[nameof(model.Denomination)].Errors.Add("Such a record already exists");
                return View("Create", model);
            }

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(Genus) });
        }
        [HttpGet]
        [Route("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            Result result = await _genusService.DeleteGenus(id);

            if (result.Success)
            {
                return View("_DeleteModel");
            }

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(Family) });
        }
    }
}
