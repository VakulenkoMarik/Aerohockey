public static class PathUtils
{
    public static string Get(string path)
    {
        string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", ".."));
                
        return Path.Combine(projectRoot, path);
    }
}