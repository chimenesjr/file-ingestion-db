using System;

namespace file_ingest_db.Extensions
{
    public static class IngestException
    {
        public static string GetExceptionMessages(this Exception e, string msgs = "")
        {
            if (e == null) return string.Empty;
            if (msgs == "") msgs = e.Message;
            if (e.InnerException != null)
                msgs += " - InnerException: " + GetExceptionMessages(e.InnerException);
            return msgs;
        }
    }
}
