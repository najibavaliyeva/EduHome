using EduHome.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace EduHome.Services.Implements
{
    public class OtpService : IOtpService
    {
        readonly IMemoryCache _cache;

        public OtpService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public int GenerateOtp(string email)
        {
            var random = new Random();
            var otp = random.Next(10000, 99999);
            _cache.Set(email, otp, DateTimeOffset.UtcNow.AddMinutes(5));
            return otp;
        }

        public bool VerifyOtp(string email, int otp)
        {
            var result = _cache.TryGetValue(email, out int otpOutcome);
            if (otp != otpOutcome) return false;
            return true;
        }
    }
}