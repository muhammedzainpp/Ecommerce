using Blazored.Toast.Services;
using Ecommerce.Web.Client.Services.Categories;
using Ecommerce.Web.Client.Services.Categories.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace Ecommerce.Web.Client.Shared.Admin.Categories;

public partial class SaveCategory
{

    [Inject]
    public IConfiguration config { get; set; } = default!;
    private long maxFileSize = 1024 * 1024 * 3;
    private int maxAllowedFiles = 5;
    private List<string> errors = new();
    [Inject]
    public required IToastService ToastService { get; set; } 

    [Inject] 
    public IJSRuntime JS { get; set; } = default!;

    [Inject]
    public ICategoryService CategoryService { get; set; } = default!;

    [SupplyParameterFromForm]
    public  CategoryDto Category { get; set; } = new CategoryDto();
    public async Task SaveMainCategory() 
    {
         var response = await CategoryService.SaveMainCategory(Category);
         if (!response.IsSuccess)
            ToastService.ShowError(String.Join(",", response.Errors));
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
