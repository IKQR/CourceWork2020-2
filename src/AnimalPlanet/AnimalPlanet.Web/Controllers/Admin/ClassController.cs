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
    [Route("admin/class")]
    public class ClassController : Controller
    {
        private readonly IClassService _classService;
        private readonly IClassRepository _classRepository;
        private readonly IPhylumRepository _phylumRepository;
        private readonly PaginationConfiguration _paginationConfiguration;

        public ClassController(
            IClassService classService,
            IClassRepository classRepository,
            IPhylumRepository phylumRepository,
            PaginationConfiguration paginationConfiguration)
        {
            _classService = classService;
            _classRepository = classRepository;
            _phylumRepository = phylumRepository;
            _paginationConfiguration = paginationConfiguration;
        }

        [Route("")]
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = _paginationConfiguration.PageSize;
            int width = _paginationConfiguration.Width;
            int count = await _classRepository.GetCount();
            int currentPage = page ?? 1;

            DataResult<List<ClassModel>> result =
                await _classService.GetPartOfClasses((currentPage - 1) * pageSize, pageSize);

            if (result.Success)
            {
                return View(new GenericPaginatedModel<ClassModel>
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

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(Class) });
        }

        [Route("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            DataResult<ClassModel> result = await _classService.GetClassById(id);

            if (result.Success)
            {
                return View("Details", result.Data);
            }

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(Class) });
        }

        [HttpGet]
        [Route("edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            DataResult<ClassModel> result = await _classService.GetClassById(id);

            ViewBag.Phylums = new SelectList(
                (await _phylumRepository.GetAll()).OrderBy(e => e.Denomination),
                nameof(Class.Id),
                nameof(Class.Denomination));

            if (result.Success)
            {
                return View("Edit", result.Data);
            }

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(Class) });
        }
        [HttpPost]
        [Route("edit/{id:int}")]
        public async Task<IActionResult> Edit(int id, ClassModel model)
        {

            Result result = await _classService.UpdateClass(id, model);

            if (result.Success)
            {
                return RedirectToAction("Details", new { id });
            }

            if (result.ErrorCode == ErrorCode.UniquenessError)
            {
                ViewBag.Phylums = new SelectList(
                    (await _phylumRepository.GetAll()).OrderBy(e => e.Denomination),
                    nameof(Class.Id),
                    nameof(Class.Denomination));

                ModelState[nameof(model.Denomination)].Errors.Add("Such a record already exists");
                return View("Edit", model);
            }

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(Class) });
        }

        [HttpGet]
        [Route("create")]
        public async Task<IActionResult> Create(int id)
        {
            ViewBag.Phylums = new SelectList(
                (await _phylumRepository.GetAll()).OrderBy(e => e.Denomination),
                nameof(Class.Id),
                nameof(Class.Denomination));

            return View("Create", new ClassModel());
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(ClassModel model)
        {
            DataResult<ClassModel> result = await _classService.CreateClass(model);

            if (result.Success)
            {
                return RedirectToAction("Details", new { result.Data.Id });
            }

            if (result.ErrorCode == ErrorCode.UniquenessError)
            {
                ViewBag.Phylums = new SelectList(
                    (await _phylumRepository.GetAll()).OrderBy(e => e.Denomination),
                    nameof(Class.Id),
                    nameof(Class.Denomination));

                ModelState[nameof(model.Denomination)].Errors.Add("Such a record already exists");
                return View("Create", model);
            }

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(Class) });
        }
        [HttpGet]
        [Route("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            Result result = await _classService.DeleteClass(id);

            if (result.Success)
            {
                return View("_DeleteModel");
            }

            return RedirectToAction("Error", "Error", new { result.ErrorCode, modelName = nameof(Phylum) });
        }
    }
}
