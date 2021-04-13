namespace foundation.config
{
    public static class RecordNumber
    {
        public static string Next(int changeType, int id)
        {
            return "1" + changeType.ToString("00") + id.ToString("0000000");
        }
    }
}