using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GraphicTree;

namespace HSMTree
{
    public class NodeTransitionParaemeterHandle
    {

        private HsmConfigTransition GetTransition(int stateId, int transitionId)
        {
            HsmConfigTransition transition = null;
            HsmConfigNode nodeValue = HsmDataController.Instance.GetNode(stateId);
            if (null == nodeValue)
            {
                return transition;
            }

            for (int i = 0; i < nodeValue.transitionList.Count; ++i)
            {
                if (nodeValue.transitionList[i].transitionId == transitionId)
                {
                    transition = nodeValue.transitionList[i];
                    break;
                }
            }

            return transition;
        }

        public void TransitionAddParemeter(int stateId, int transitionId)
        {
            HsmConfigTransition transition = GetTransition(stateId, transitionId);
            if (null == transition)
            {
                return;
            }

            NodeParameter addParameter = null;
            foreach(var parameter in HsmDataController.Instance.HSMTreeData.parameterList)
            {
                NodeParameter temp = transition.parameterList.Find(a => a.parameterName.CompareTo(parameter.parameterName) == 0);
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

            List<NodeParameter> parameterList = transition.parameterList;
            ParameterTool.AddParameter(parameterList, addParameter);
        }

        public void TransitionDelParameter(int stateId, int transitionId, NodeParameter parameter)
        {
            HsmConfigTransition transition = GetTransition(stateId, transitionId);
            if (null != transition)
            {
                List<NodeParameter> parameterList = transition.parameterList;
                ParameterTool.DelParameter(parameterList, parameter);
            }
        }

        public void TransitionChangeParameter(int stateId, int transitionId, string newParameter)
        {
            HsmConfigTransition transition = GetTransition(stateId, transitionId);
            if (null == transition)
            {
                return;
            }

            NodeParameter parameter = HsmDataController.Instance.GetDataTreeParameter(newParameter);
            if (null == parameter)
            {
                return;
            }

            List<NodeParameter> parameterList = transition.parameterList;
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
