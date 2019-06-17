namespace projectTest.Session
{
    public static class Globals
    {
        public static string GetCurrentLocation()
        {
            return (string)System.Web.HttpContext.Current.Session["CurrentLocation"];
        }
        public static void SetCurrentLocation(string value)
        {
            System.Web.HttpContext.Current.Session["CurrentLocation"] = value;
        }
    }
}