using SecuDevCore.Models;

namespace SecuDevCore.Helper
{
    public class TreeHelper
    {
        public static List<object> JsTreeFormat(List<Tree> nodes, int? parentId = null)
        {
            var jsTreeData = new List<object>();

            foreach (var node in nodes.Where(n => n.ParentLocationID == parentId))
            {
                jsTreeData.Add(new
                {
                    id = node.LocationID.ToString(),
                    parent = parentId == null ? "#" : node.ParentLocationID.ToString(),
                    text = node.text,
                    state = new
                    {
                        opened = true,    // 기본적으로 열림
                        selected = false // 기본적으로 선택되지 않음
                    }
                });

                if (node.nodes != null && node.nodes.Any())
                {
                    jsTreeData.AddRange(JsTreeFormat(node.nodes, node.LocationID));
                }
            }

            return jsTreeData;
        }

    }
}
