namespace CZLib.Config.Mvc4
{
    using System.IO;
    using System.Text;

    public partial class ConfigCshtmlCreator
    {
        public static void CreateCshtml<T>(string path, string postAction) where T : XmlFileConfigBase<T>
        {
            var creator = new ConfigCshtmlCreator();
            creator.Session = new Microsoft.VisualStudio.TextTemplating.TextTemplatingSession();
            creator.Session["configType"] = typeof(T);
            creator.Session["postAction"] = postAction;
            // Add other parameter values to t.Session here.
            creator.Initialize(); // Must call this to transfer values.
            var html = creator.TransformText();
            var finfo = new FileInfo(path);
            if (!finfo.Directory.Exists)
            {
                finfo.Directory.Create();
            }
            File.WriteAllText(path, html, Encoding.UTF8);
        }
    }
}