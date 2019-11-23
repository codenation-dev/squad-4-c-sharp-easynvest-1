using LogCenter.Domain.Entities;

namespace LogCenter.Domain.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Token { get; set; }

        public UserDTO()
        {
        }

        public UserDTO(User user)
        {
            Id = user.Id;
            Email = user.Email;
            Nome = user.Nome;
            Token = user.Token;
        }
    }
}
