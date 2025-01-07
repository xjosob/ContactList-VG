namespace Business.Helpers
{
    public static class GuidHelper
    {
        public static string GenerateGuid()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
