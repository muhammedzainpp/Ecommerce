using Ecommerce.Web.Client.Services.Categories;
using Ecommerce.Web.Client.Services.Categories.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Ecommerce.Web.Client.Shared.Admin.Categories;

public partial class SaveSubCategory
{
    [Inject]
    public IConfiguration config { get; set; } = default!;
    private long maxFileSize = 1024 * 1024 * 3;
    private int maxAllowedFiles = 5;
    private List<string> errors = new();
    [Inject]
    public ICategoryService CategoryService { get; set; } = default!;

    [SupplyParameterFromForm]
    public CategoryDto SubCategory { get; set; } = new CategoryDto();

    protected override async Task OnInitializedAsync()
    {
        await CategoryService.GetCategories();
    }

    public async Task SaveSubCategoryAsync()
    {
        await CategoryService.SaveMainCategory(SubCategory);
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
