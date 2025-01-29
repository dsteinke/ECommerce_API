namespace ECommerce_API.Application
{
    public interface IUserService
    {
        Task RegisterUser(UserAddDTO userAddDTO);
        Task<UserResponseDTO> GetUserById(Guid userId);
    }
}
