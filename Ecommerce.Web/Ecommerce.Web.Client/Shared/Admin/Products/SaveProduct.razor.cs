using Ecommerce.Web.Client.Services.Categories;
using Ecommerce.Web.Client.Services.Categories.Dtos;
using Ecommerce.Web.Client.Services.Products;
using Ecommerce.Web.Client.Services.Products.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ecommerce.Web.Client.Shared.Admin.Products;

public partial class SaveProduct
{
    private long maxFileSize = 1024 * 1024 * 3;
    private int maxAllowedFiles = 5;
    private List<string> errors = new();


    [Inject]
    public IConfiguration config { get; set; } = default!;
    public int CategoryId { get; set; }

    [Inject]
    public ICategoryService CategoryService { get; set; } = default!;
    [Inject]
    public IProductService ProductService { get; set; } = default!;

    [SupplyParameterFromForm]
    public ProductDto Product { get; set; } = new();
    public List<CategoryDto> SubCategories { get; set; } = new();
    protected override async Task OnInitializedAsync()
    {
        await CategoryService.GetCategories();
    }
    public async Task GetSubCategories()
    {

        SubCategories = (await CategoryService.GetSubCategories(CategoryId))
            .ToList();
        StateHasChanged();
    }
    public async void SaveProductAsync()
    {       
        await ProductService.SaveProduct(Product);
    }

    private async Task LoadFiles(InputFileChangeEventArgs e)
    {
        errors.Clear();

        if (e.FileCount > maxAllowedFiles)
        {
            errors.Add(
                $"Error: Attempting to upload {e.FileCount} files, but only {maxAllowedFiles} files are allowed"
            );
            return;
        }

        foreach (var file in e.GetMultipleFiles(maxAllowedFiles))
        {
            try
            {



                string randomFileName = Path.GetRandomFileName();
                string originalFileExtension = Path.GetExtension(file.Name);
                string newFileName = Path.ChangeExtension(randomFileName, originalFileExtension);

                //Combines three parts into a complete file path, string path = Path.Combine(Root directory, "sub-directory", newFileName);
                string path = Path.Combine(
                    config.GetValue<string>("FileStorage")!,
                    "tcorey",
                    newFileName);

                Directory.CreateDirectory(Path.Combine(config.GetValue<string>("FileStorage")!, "tcorey"));

                await using FileStream fs = new(path, FileMode.Create);
                await file.OpenReadStream(maxFileSize).CopyToAsync(fs);
            }
            catch (Exception ex)
            {
                errors.Add($"File: {file.Name} Error: {ex.Message}");
            }
        }
    }
}