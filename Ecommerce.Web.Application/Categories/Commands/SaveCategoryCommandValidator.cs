using Ecommerce.Web.Application.Interfaces;
using FluentValidation;
using System.Security.Cryptography.X509Certificates;

namespace Ecommerce.Web.Application.Categories.Commands;
public class SaveCategoryCommandValidator : AbstractValidator<SaveCategoryCommand>
{
    private IAppDbContext _context;
    public SaveCategoryCommandValidator(IAppDbContext context)
    {
        _context = context;
        RuleFor(x => x.Name).NotEmpty().Must(CategoryExist).WithMessage("Category Alredy Exists");
        
    }

    private bool CategoryExist(string name)
    {
        var isExist = _context.Categories.Any(x => x.Name == name) ? false : true;
        return isExist;
    }
}
