namespace ApiBase.Core.src.Infra.Extensions
{
    public static class Extension
    {
        public static T uExceptionSeNull<T>(this T obj, string message) where T : class
        {
            if (obj == null)
            {
                throw new Exception(message);
            }

            return obj;
        }

        public static string FlattenMessage(this Exception ex)
        {
            string message = ex.Message;
            for (Exception inner = ex.InnerException; inner != null; inner = inner.InnerException)
            {
                message += "\nMore details: " + inner.Message;
            }

            return message;
        }
    }
}
