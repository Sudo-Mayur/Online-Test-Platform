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
                return default(T);
            }
            else
            {
                return JsonSerializer.Deserialize<T>(data);
            }
        }
    }
}


//var id = HttpContext.Session.GetString("LoginID");
//HttpContext.Session.SetString("LoginID", get.Id);
