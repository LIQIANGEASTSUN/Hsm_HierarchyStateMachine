using System.Collections.Generic;
using GraphicTree;

namespace HSMTree
{
    public class NodeParameterHandle
    {

        public void NodeAddParameter(int stateId)
        {
            HsmConfigNode nodeValue = HsmDataController.Instance.GetNode(stateId);
            if (null == nodeValue)
            {
                return;
            }
            NodeParameter addParameter = null;
            foreach (var parameter in HsmDataController.Instance.HSMTreeData.parameterList)
            {
                NodeParameter temp = nodeValue.parameterList.Find(a => a.parameterName.CompareTo(parameter.parameterName) == 0);
                if (null == temp)
                {
                    addParameter = parameter.Clone();
                    break;
                }
            }

            if (null == addParameter || HsmDataController.Instance.HSMTreeData.parameterList.Count <= 0)
            {
                string msg = "没有参数可添加，请先添加参数";
                if (HSMNodeWindow.window != null)
                {
                    HSMNodeWindow.window.ShowNotification(msg);
                }
                return;
            }

            List<NodeParameter> parameterList = nodeValue.parameterList;
            ParameterTool.AddParameter(parameterList, addParameter);
        }

        public void NodeDelParameter(int stateId, NodeParameter parameter)
        {
            HsmConfigNode nodeValue = HsmDataController.Instance.GetNode(stateId);
            if (null == nodeValue)
            {
                return;
            }
            List<NodeParameter> parameterList = nodeValue.parameterList;
            ParameterTool.DelParameter(parameterList, parameter);
        }

        public void NodeChangeParameter(int stateId, string newParameter)
        {
            HsmConfigNode nodeValue = HsmDataController.Instance.GetNode(stateId);
            if (null == nodeValue)
            {
                return;
            }

            NodeParameter parameter = HsmDataController.Instance.GetDataTreeParameter(newParameter);
            if (null == parameter)
            {
                return;
            }

            List<NodeParameter> parameterList = nodeValue.parameterList;
            NodeParameter temp = parameterList.Find((a) => {
                return a.parameterName.CompareTo(parameter.parameterName) == 0;
            });

            if (null != temp)
            {
                temp.CloneFrom(parameter);
            }
        }
    }
}
