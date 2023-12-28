using E_CommerceWebApplication.Models;
using E_CommerceWebApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace E_CommerceWebApplication.Controllers;
public class CategoryController : Controller 
{
    private readonly IHostingEnvironment _hostingEnvironment;
    private readonly ICategoryRepository _categoryServices;
    public static readonly string CategoryPhotoRootPath = "/Images/Category/";

    public CategoryController(ICategoryRepository categoryServices, IHostingEnvironment hostingEnvironment)
    {
        _categoryServices = categoryServices;
        _hostingEnvironment = hostingEnvironment;
    }
    public async Task< IActionResult> Index()=>
        View(await _categoryServices.GetAll());

    public async Task<IActionResult> Details(int id)
    {

        Category cat = await _categoryServices.Get(id);
        if (cat == null)
            return RedirectToAction(nameof(Index));

        //cat.ImageURL = CategoryPhotoRootPath + cat.ImageURL;
        return View(cat);
    }
    [Authorize(Roles ="Admin")]
    public async Task<IActionResult> Edit(int id)
    {

        Category cat = await _categoryServices.Get(id);
        if (cat == null)
            return RedirectToAction(nameof(Index));

        EditCategoryViewModel viewModel = new()
        {
            Id = cat.Id,
            Name = cat.Name,
            Description = cat.Description,
            ExistsPhotoPath = CategoryPhotoRootPath + cat.ImageURL,
        };
        return View(viewModel);
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Edit(EditCategoryViewModel viewModel)
    {

        if (viewModel == null)
            return RedirectToAction(nameof(Index));

        Category category = await _categoryServices.Get(viewModel.Id);
        if (viewModel.Image != null)
        {
            if (!Helper.ImageValidation.IsSizeValid(viewModel.Image))
            {
                ModelState.AddModelError("Image", "Max Allowed Poster Size Is 2MB");
                return View(viewModel);
            }
            if (!Helper.ImageValidation.IsValidExtensions(viewModel.Image))
            {
                ModelState.AddModelError("Image", "Only JPG & PNG Extensions Allowed");
                return View(viewModel);
            }

            if (viewModel.ExistsPhotoPath != null)
            {
                string path = Path.Combine(_hostingEnvironment.WebRootPath, "Images", "Category", category.ImageURL);
                System.IO.File.Delete(path);
            }


            category.ImageURL = ProcessUploadedFile(viewModel);
        }
        category.Description = viewModel.Description;
        category.Name = viewModel.Name;
        _categoryServices.Update(category);
        return RedirectToAction(nameof(Index));
    }
    [Authorize(Roles ="Admin")]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [Authorize(Roles ="Admin")]
    public IActionResult Create(CategoryViewModel viewModel)
    {
        if(!ModelState.IsValid) 
            return View(viewModel);

        var files = Request.Form.Files;
        if(!files.Any())
        {
            ModelState.AddModelError("Image", "Image is required");
            return View(viewModel);
        }

        if(!Helper.ImageValidation.IsSizeValid(viewModel.Image))
        {
            ModelState.AddModelError("Image", "Max Allowed Poster Size Is 2MB");
            return View(viewModel);
        }
        if(!Helper.ImageValidation.IsValidExtensions(viewModel.Image))
        {
            ModelState.AddModelError("Image", "Only JPG & PNG Extensions Allowed");
            return View(viewModel);
        }

        Category cat = new()
        {
            Name = viewModel.Name,
            Description = viewModel.Description
        };
        cat.ImageURL = ProcessUploadedFile(viewModel);
        //cat.ImageURL = Helper.ImageSave.pro

        _categoryServices.Create(cat);
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles ="Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        Category category = await _categoryServices.Get(id);

        if(category == null)
          return RedirectToAction(nameof(Index));

        category.ImageURL = Path.Combine(_hostingEnvironment.WebRootPath, "Images","Category",category.ImageURL);
        _categoryServices.Delete(id);

        return RedirectToAction(nameof(Index));

    }

    public async Task<IActionResult> ViewAllProducts(int id)
    {
        Category category = await _categoryServices.Get(id);
        ///ViewData["Products"] = category.Name;
        return View(await _categoryServices.GetProducts(id));
    }


    private string ProcessUploadedFile(CategoryViewModel model)
    {
        string uniqueFileName = "";
        if (model.Image != null)
        {

            string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Images","Category");
            uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                model.Image.CopyTo(fileStream);
            }

        }

        return uniqueFileName;
    }


}
