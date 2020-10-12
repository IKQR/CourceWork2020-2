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
    [Route("admin/specie")]
    public class SpecieController : Controller
    {
        private readonly ISpecieService _specieService;
        private readonly ISpecieRepository _specieRepository;
        private readonly PaginationConfiguration _paginationConfiguration;
        private readonly IGenusRepository _genusRepository;
        private readonly IReserveRepository _reserveRepository;
        private readonly INaturalAreaRepository _naturalAreaRepository;

        public SpecieController(
            ISpecieService specieService,
            ISpecieRepository specieRepository,
            IGenusRepository genusRepository,
            IReserveRepository reserveRepository,
            INaturalAreaRepository naturalAreaRepository,
            PaginationConfiguration paginationConfiguration)
        {
            _specieService = specieService;
            _specieRepository = specieRepository;
            _paginationConfiguration = paginationConfiguration;
            _genusRepository = genusRepository;
            _reserveRepository = reserveRepository;
            _naturalAreaRepository = naturalAreaRepository;
        }

        [Route("")]
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = _paginationConfiguration.PageSize;
            int width = _paginationConfiguration.Width;
            int count = await _specieRepository.GetCount();
            int currentPage = page ?? 1;

            DataResult<List<SpecieViewModel>> result =
                await _specieService.GetPartOfSpecieViews((currentPage - 1) * pageSize, pageSize);

            if (result.Success)
            {
                return View(new GenericPaginatedModel<SpecieViewModel>
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

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(Specie) });
        }

        [Route("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            DataResult<SpecieViewModel> result = await _specieService.GetSpecieViewById(id);

            if (result.Success)
            {
                return View("Details", result.Data);
            }

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(Specie) });
        }

        [HttpGet]
        [Route("edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            DataResult<SpecieCreateModel> result = await _specieService.GetSpecieById(id);

            await InstallViewBags();

            if (result.Success)
            {
                return View("Edit", result.Data);
            }

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(Specie) });
        }
        [HttpPost]
        [Route("edit/{id:int}")]
        public async Task<IActionResult> Edit(int id, SpecieCreateModel model)
        {
            Result result = await _specieService.UpdateSpecie(id, model);

            if (result.Success)
            {
                return RedirectToAction("Details", new { id });
            }

            if (result.ErrorCode == ErrorCode.UniquenessError)
            {
                await InstallViewBags();
                ModelState[nameof(model.Denomination)].Errors.Add("Such a record already exists");
                return View("Edit", model);
            }

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(Specie) });
        }

        [HttpGet]
        [Route("create")]
        public async Task<IActionResult> Create(int id)
        {
            await InstallViewBags();
            return View("Create", new SpecieCreateModel());
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(SpecieCreateModel model)
        {

            DataResult<Specie> result = await _specieService.CreateSpecie(model);

            if (result.Success)
            {
                return RedirectToAction("Details", new { result.Data.Id });
            }

            if (result.ErrorCode == ErrorCode.UniquenessError)
            {
                await InstallViewBags();
                ModelState[nameof(model.Denomination)].Errors.Add("Such a record already exists");
                return View("Create", model);
            }

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(Specie) });
        }

        [HttpGet]
        [Route("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            Result result = await _specieService.DeleteSpecie(id);

            if (result.Success)
            {
                return View("_DeleteModel");
            }

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(Specie) });
        }

        private async Task InstallViewBags()
        {
            ViewBag.Genuses = new SelectList(
                (await _genusRepository.GetAll()).OrderBy(e => e.Denomination),
                nameof(Genus.Id),
                nameof(Genus.Denomination));

            ViewBag.NaturalAreas = new SelectList(
                (await _naturalAreaRepository.GetAll()).OrderBy(e => e.Denomination),
                nameof(NaturalArea.Id),
                nameof(NaturalArea.Denomination));

            ViewBag.Reserves = new SelectList(
                (await _reserveRepository.GetAll()).OrderBy(e => e.Name),
                nameof(Reserve.Id),
                nameof(Reserve.Name));
        }
    }
}
