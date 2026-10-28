namespace EduHome.Services.Interfaces
{
    public interface IOtpService
    {

        int GenerateOtp(string email);
        bool VerifyOtp(string email, int otp);
    }
}
