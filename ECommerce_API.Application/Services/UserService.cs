using AutoMapper;
using ECommerce_API.Core;

namespace ECommerce_API.Application
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public UserService
            (IUserRepository userRepository, IAuthService authService, IMapper mapper)
        {
            _userRepository = userRepository;
            _authService = authService;
            _mapper = mapper;
        }

        public async Task RegisterUser(UserAddDTO userAddDTO)
        {
            //Check if user already exists
            if (await _userRepository.UserExists(userAddDTO.Username, userAddDTO.Email) == false)
                throw new InvalidOperationException("User with the same username or email already exists");

            var user = userAddDTO.ToUser();

            //create cart for user
            var cart = new Cart
            {
                CartId = Guid.NewGuid(),
                UserId = user.UserId,
                User = user,
                CartItems = new List<CartItem>()
            };

            user.PasswordHash = _authService.HashPassword(user, userAddDTO.PasswordHash);
            user.Cart = cart;

            await _userRepository.RegisterUser(user);
        }

        public async Task<UserResponseDTO> GetUserById(Guid userId)
        {
            var user = await _userRepository.GetUserById(userId);

            if (user == null)
                throw new KeyNotFoundException($"No user found with userId: {userId}");

            var result = _mapper.Map<UserResponseDTO>(user);

            return result;
        }
    }
}
