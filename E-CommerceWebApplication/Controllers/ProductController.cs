using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using E_CommerceWebApplication.Models;
using E_CommerceWebApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using E_CommerceWebApplication.Repository;

namespace E_CommerceWebApplication.Controllers;
public class ProductController : Controller 
{
    private readonly IHostingEnvironment _hostingEnvironment;
    private readonly IProductRepository _productServices;
    private readonly ICategoryRepository _categoryServices;
    public static readonly string ProductPhotoRootPath = "/Images/Product";

    public ProductController(
        IProductRepository productServices,
        ICategoryRepository categoryServices,
        IHostingEnvironment hostingEnvironment)

    {
        _productServices = productServices;
        _categoryServices = categoryServices;
        _hostingEnvironment = hostingEnvironment;
    }

    public async Task<IActionResult> Index() =>
        View("ViewAllProducts",await _productServices.GetAll());
    public async Task<IActionResult> Search(string searchKey)
    {
        ViewData["Search"] = searchKey;
        return View("ViewAllProducts",await _productServices.Search(searchKey));
    }
    public async Task<IActionResult> Details(int id)
    {

        Product product = await _productServices.Get(id);
        if (product == null)
            return RedirectToAction(nameof(Index));
        ProductDetails productDetails = new()
        {
            ID = product.ID,
            Name = product.Name,
            Description = product.Description,
            ImageURL = ProductPhotoRootPath +'/'+ product.ImageURL,
            Price = product.Price
        };

        Category cat = await _categoryServices.Get(product.CategoryID);
        productDetails.CategoryName = cat.Name;
        return View(productDetails);
    }

    [Authorize(Roles ="Admin")]
    public async Task<IActionResult> Edit(int id)
    {

        Product cat = await _productServices.Get(id);
        if (cat == null)
            return RedirectToAction(nameof(Index));

        EditProductViewModel viewModel = new()
        {
            ID = cat.ID,
            Name = cat.Name,
            Description = cat.Description,
            Price = cat.Price,
            ExistsPhotoPath = ProductPhotoRootPath +'/'+ cat.ImageURL,
        };
        viewModel.Categories = await _categoryServices.GetAll();
        return View(viewModel);
    }
    [HttpPost]
    [Authorize(Roles ="Admin")]
    public async Task<IActionResult> Edit(EditProductViewModel viewModel)
    {

        if (viewModel == null)
        {
            viewModel.Categories = await _categoryServices.GetAll();
            return RedirectToAction(nameof(Index));
        }


        Product product = await _productServices.Get(viewModel.ID);
        if (viewModel.Image != null)
        {
            if (!Helper.ImageValidation.IsSizeValid(viewModel.Image))
            {
                ModelState.AddModelError("Image", "Max Allowed Poster Size Is 2MB");
                return View(viewModel);
            }
            if (!Helper.ImageValidation.IsValidExtensions(viewModel.Image))
            {
                ModelState.AddModelError("Image", "Only .JPG & .PNG Extensions Allowed");
                return View(viewModel);
            }

            if (product.ImageURL != null)
            {
                string path = Path.Combine(_hostingEnvironment.WebRootPath, "Images", "Product", product.ImageURL);
                System.IO.File.Delete(path);
            }


            product.ImageURL = ProcessUploadedFile(viewModel);
        }
        product.Description = viewModel.Description;
        product.Name = viewModel.Name;
        product.Price = viewModel.Price;
        _productServices.Update(product);
        return RedirectToAction(nameof(Index));
    }
    [Authorize(Roles ="Admin")]
    public async Task<IActionResult> Create()
    {
        CreateProductViewModel viewModel = new();
        viewModel.Categories = await _categoryServices.GetAll();
        return View(viewModel);
    }
    [HttpPost]
    [Authorize(Roles ="Admin")]
    public async Task<IActionResult> Create(CreateProductViewModel viewModel)
    {

        var files = Request.Form.Files;
        if (!files.Any())
        {
            ModelState.AddModelError("Image", "Image is required");
            viewModel.Categories = await _categoryServices.GetAll();
            return View(viewModel);
        }

        if (!Helper.ImageValidation.IsSizeValid(viewModel.Image))
        {
            ModelState.AddModelError("Image", "Max Allowed Poster Size Is 2MB");
            viewModel.Categories = await _categoryServices.GetAll();
            return View(viewModel);
        }
        if (!Helper.ImageValidation.IsValidExtensions(viewModel.Image))
        {
            viewModel.Categories = await _categoryServices.GetAll();
            ModelState.AddModelError("Image", "Only JPG & PNG Extensions Allowed");
            return View(viewModel);
        }

        Product product = new()
        {
            Name = viewModel.Name,
            Description = viewModel.Description,
            CategoryID = viewModel.CategoryId,
            Price = viewModel.Price

        };
        product.ImageURL = ProcessUploadedFile(viewModel);

        _productServices.Create(product);
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles ="Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        Product product = await _productServices.Get(id);

        if (product == null)
            return RedirectToAction(nameof(Index));

        product.ImageURL = Path.Combine(_hostingEnvironment.WebRootPath, "Images", "Product", product.ImageURL);
        System.IO.File.Delete(product.ImageURL);
        _productServices.Delete(id);

        return RedirectToAction(nameof(Index));

    }

    private string ProcessUploadedFile(CreateProductViewModel model)
    {
        string uniqueFileName = string.Empty;
        if (model.Image != null)
        {

            string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Images", "Product");
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
