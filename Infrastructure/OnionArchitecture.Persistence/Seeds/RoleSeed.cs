using Microsoft.Extensions.DependencyInjection;
using OnionArchitecture.Application.Abstractions.Repositories;

namespace OnionArchitecture.Persistence.Seeds;

public class RoleSeed
{
    public static async Task Seed(IServiceProvider service)
    {
        var unitOfWork = service.GetRequiredService<IUnitOfWork>();
        await SeedRoleItem(unitOfWork, "User", true);
        await SeedRoleItem(unitOfWork, "Admin", false);
        await SeedRoleItem(unitOfWork, "Root", false);

        await unitOfWork.CompleteAsync();
    }

    private static async Task SeedRoleItem(IUnitOfWork unitOfWork, string title, bool isDefault)
    {
        var role = await unitOfWork.RoleReadRepository.GetByTitleAsync(title);
        if (role == null)
        {
            await unitOfWork.RoleWriteRepository.AddAsync(new(title, isDefault));
        }
    }
}