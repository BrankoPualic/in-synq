namespace InSynq.Core.Dtos.Auth;

public class SigninDto : BaseDto<SigninDto>
{
    public SigninDto() : base(new SigninDtoValidator())
    {
    }

    public string Email { get; set; }

    public string Password { get; set; }
}