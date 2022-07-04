using System.Collections.Generic;

namespace HSMTree
{
    public class HsmNodeDrawModel
    {
        public HsmNodeDrawModel()
        {
        }

        public HsmConfigNode GetCurrentSelectNode()
        {
            return HsmDataController.Instance.CurrentNode;
        }

        public List<HsmConfigNode> GetNodeList()
        {
            return HsmDataController.Instance.NodeList;
        }

        public List<HsmConfigNode> GetBaseNode()
        {
            List<HsmConfigNode> allNodeList = GetNodeList();
            List<HsmConfigNode> drawList = new List<HsmConfigNode>();
            foreach(var node in allNodeList)
            {
                if (node.parentId <= 0)
                {
                    drawList.Add(node);
                }
            }
            return drawList;
        }

        public string[] GetOptionArr(ref int selectIndex, ref List<int> nodeList)
        {
            nodeList = new List<int>();
            List<string> optionList = new List<string>();
            nodeList.Add(-1);
            optionList.Add("Base");
            selectIndex = nodeList.Count - 1;

            int nodeId = HsmDataController.Instance.CurrentOpenSubMachineId;
            HsmConfigNode nodeValue = HsmDataController.Instance.GetNode(nodeId);
            while (null != nodeValue && nodeValue.nodeType == (int)HSM_NODE_TYPE.SUB_STATE_MACHINE)
            {
                nodeList.Insert(1, nodeValue.id);
                selectIndex = nodeList.Count - 1;

                string name = GetNodeName(nodeValue);
                optionList.Insert(1, name);
                nodeValue = HsmDataController.Instance.GetNode(nodeValue.parentId);
            }

            return optionList.ToArray();
        }

        private string GetNodeName(HsmConfigNode nodeValue)
        {
            int nodeIndex = EnumNames.GetEnumIndex<HSM_NODE_TYPE>((HSM_NODE_TYPE)nodeValue.nodeType);
            string name = EnumNames.GetEnumName<HSM_NODE_TYPE>(nodeIndex);
            return string.Format("{0}_{1}", name, nodeValue.id);
        }
    }
}