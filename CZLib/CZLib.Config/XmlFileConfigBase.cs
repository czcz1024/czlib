namespace CZLib.Config
{
    using System;
    using System.IO;
    using System.Linq.Expressions;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// 继续xml文件的自定义配置基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class XmlFileConfigBase<T> : FileTHZConfigBase<T> where T : THZConfigBase<T>
    {
        public override string Serialize(T obj)
        {
            var ser = new XmlSerializer(obj.GetType());
            var stream = new MemoryStream();
            ser.Serialize(stream, obj);
            return Encoding.UTF8.GetString(stream.ToArray());
        }

        public override T Deserialize(string str)
        {
            var ser = new XmlSerializer(typeof(T));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(str));

            return (T)ser.Deserialize(ms);
        }

        public override void ResetToDefault(Expression<Func<T,object>> field)
        {
            var paramName = field.Parameters[0].Name;
            var fieldName = field.Body.ToString().Replace("Convert(", "").Replace(")", "").Replace(paramName + ".", "");
            var xpath = fieldName.Replace(".", "/");

            var defaults = new XmlDocument();
            defaults.Load(this.DefaultFilePath());
            var root = defaults.DocumentElement;
            var defaultNodes = root.SelectNodes(xpath);

            var config = new XmlDocument();
            config.Load(this.FilePath());
            var root1 = config.DocumentElement;
            var configNodes = root1.SelectNodes(xpath);

            if (configNodes.Count == defaultNodes.Count)
            {
                for (var i = 0; i < defaultNodes.Count; i++)
                {
                    configNodes[i].RemoveAll();
                    configNodes[i].InnerXml = defaultNodes[i].InnerXml;
                    //foreach (XmlNode child in defaultNodes[i].ChildNodes)
                    //{
                    //    configNodes[i].AppendChild(child);
                    //}
                }

                config.Save(this.FilePath());
                THZConfigHelper<T>.Reload();
                
                return;
            }
            throw new Exception("默认配置与现有配置结构不一致");
        }
    }
}