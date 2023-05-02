namespace XBase.Utils;

public class PathHelper
{
    public static string GetDirectoryLastFolderName(string fullDir)
    {
        var dirInfo = new DirectoryInfo(fullDir);
        return dirInfo.Name;
    }
}