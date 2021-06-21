using System;

namespace WeddingRsvp.Api.Utilities
{
    public static class InviteCodeGenerator
    {
        // https://stackoverflow.com/questions/39261767/is-it-possible-to-reduce-the-length-of-datetime-now-ticks-tostringx-and-stil
        public static string GenerateCode()
        {
            return Convert.ToBase64String(BitConverter.GetBytes((DateTime.Now.Ticks - new DateTime(1993, 06, 10).Ticks) / 10000L)).Substring(0,7).Replace("+","");
        }
    }
}
