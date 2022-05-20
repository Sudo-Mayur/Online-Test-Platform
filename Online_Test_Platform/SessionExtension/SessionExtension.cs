using System.Text.Json;

namespace Online_Test_Platform.SessionExtension
{
    public static class SessionExtensions
    {
        public static void SetSessionData<T>(this ISession session, string sessionKey, T sessionValue)
        {
            session.SetString(sessionKey, JsonSerializer.Serialize(sessionValue));
        }

        public static T GetSessionData<T>(this ISession session, string sessionKey)
        {
            var data = session.GetString(sessionKey);
            if (data == null)
            {
#pragma warning disable CS8603 // Possible null reference return.
                return default(T);
#pragma warning restore CS8603 // Possible null reference return.
            }
            else
            {
#pragma warning disable CS8603 // Possible null reference return.
                return JsonSerializer.Deserialize<T>(data);
#pragma warning restore CS8603 // Possible null reference return.
            }
        }
    }
}



