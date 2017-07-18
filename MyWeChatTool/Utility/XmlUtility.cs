using System.Xml.Linq;
using Newtonsoft.Json;

namespace MyWeChatTool.Utility
{
    class XmlUtility
    {
        /// <summary>
        /// json格式转换为xml格式
        /// </summary>
        /// <param name="json"></param>
        /// <param name="rootName"></param>
        /// <returns></returns>
        public static XDocument ParseJson(string json, string rootName)
        {
            return JsonConvert.DeserializeXNode(json, rootName);
        }
    }
}
