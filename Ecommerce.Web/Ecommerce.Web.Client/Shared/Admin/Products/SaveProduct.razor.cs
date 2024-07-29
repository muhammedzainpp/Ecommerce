using Blazored.Toast.Services;
using Ecommerce.Web.Client.Services.AppSettings;
using Ecommerce.Web.Client.Services.AppSettings.Dtos;
using Ecommerce.Web.Client.Services.Categories;
using Ecommerce.Web.Client.Services.Categories.Dtos;
using Ecommerce.Web.Client.Services.Products;
using Ecommerce.Web.Client.Services.Products.Dtos;
using Ecommerce.Web.Shared.Reponses;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Ecommerce.Web.Client.Shared.Admin.Products;

public partial class SaveProduct
{
	private long maxFileSize = 1024 * 1024 * 3;
	private int maxAllowedFiles = 5;
	private List<string> errors = new();

	[Inject]
	public required IAppSettingsService AppSettingsService { get; set; }

	[Inject]
	public IConfiguration config { get; set; } = default!;
	[Inject]
	public ICategoryService CategoryService { get; set; } = default!;


	[Inject]
	public IProductService ProductService { get; set; } = default!;

	[Inject]
	public required IToastService ToastService { get; set; } = default!;

	[SupplyParameterFromForm]
	public ProductDto Product { get; set; } = new() { Description = string.Empty, Name = string.Empty, Cost = new() };
	public int CategoryId { get; set; }
	public required AppSettingsDto AppSetting { get; set; } = new AppSettingsDto();
	public List<CategoryDto> SubCategories { get; set; } = new();
	public required Response<IEnumerable<CategoryDto>> Categories { get; set; } = new Response<IEnumerable<CategoryDto>>();


	protected override async Task OnInitializedAsync()
	{
		var response = await CategoryService.GetCategories();
		if (!response.IsSuccess) { ToastService.ShowError(String.Join(",", response.Errors)); }
		else { Categories = response; }
		AppSetting = await AppSettingsService.GetAppSetings();
	}

	public async Task GetSubCategories()
	{
		SubCategories = (await CategoryService.GetSubCategories(CategoryId))
			.ToList();
		StateHasChanged();
	}
	public async void SaveProductAsync()
	{
		Product.Cost.Currency.Symbol = AppSetting.CurrencySymbol;
		Product.Cost.Currency.Name = AppSetting.CurrencyName;

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