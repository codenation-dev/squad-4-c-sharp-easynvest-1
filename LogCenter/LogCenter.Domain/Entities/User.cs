using LogCenter.Domain.Results;

namespace LogCenter.Domain.Entities
{
    public class User : EntityBase
    {
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }

        public override Result IsValid()
        {
            throw new System.NotImplementedException();
        }
    }
}
