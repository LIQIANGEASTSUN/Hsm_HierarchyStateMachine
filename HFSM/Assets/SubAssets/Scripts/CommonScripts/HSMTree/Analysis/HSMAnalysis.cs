using System.Collections.Generic;
using UnityEngine;
using LitJson;
using GraphicTree;

namespace HSMTree
{
    public class HSMAnalysis
    {
        public HSMAnalysis(){    }

        public void Analysis(HsmConfigTreeData data, IConditionCheck iConditionCheck, ref HSMStateMachine hsmStateMachine)
        {
            int entityId = NewEntityId;
            Analysis( entityId, data, iConditionCheck, ref hsmStateMachine);
        }

        private void Analysis(int entityId, HsmConfigTreeData data, IConditionCheck iConditionCheck, ref HSMStateMachine hsmStateMachine)
        {
            hsmStateMachine = new HSMStateMachine();
            hsmStateMachine.EntityId = entityId;

            if (null == data)
            {
                Debug.LogError("数据无效");
                return;
            }

            hsmStateMachine.SetDefaultStateId(data.defaultStateId);
            SetParameter(iConditionCheck, data);

            Dictionary<int, HSMStateBase> abstractNodeDic = AnalysisAllNode(entityId, data.nodeList, iConditionCheck);
            hsmStateMachine.AddAllNode(abstractNodeDic);
            foreach(var kv in abstractNodeDic)
            {
                HSMStateBase parentNode = kv.Value;
                if (parentNode.ParentId < 0)
                {
                    hsmStateMachine.AddChildNode(parentNode);
                }

                if (parentNode.ChildIdList.Count <= 0)
                {
                    continue;
                }

                for (int j = 0; j < parentNode.ChildIdList.Count; ++j)
                {
                    int childId = parentNode.ChildIdList[j];

                    HSMStateBase childNode = null;
                    if (!abstractNodeDic.TryGetValue(childId, out childNode))
                    {
                        continue;
                    }

                    childNode.SetParentSubMachine(parentNode);
                    parentNode.AddChildNode(childNode);
                }
            }
        }

        private Dictionary<int, HSMStateBase> AnalysisAllNode(int entityId, List<HsmConfigNode> nodeList , IConditionCheck iConditionCheck)
        {
            Dictionary<int, HSMStateBase> abstractNodeDic = new Dictionary<int, HSMStateBase>();

            nodeList.Sort((a, b) => {
                return b.childIdList.Count - a.childIdList.Count;
            });

            for (int i = 0; i < nodeList.Count; ++i)
            {
                HsmConfigNode nodeValue = nodeList[i];
                HSMStateBase abstractNode = AnalysisNode(nodeValue);
                abstractNode.EntityId = entityId;
                abstractNode.NodeId = nodeValue.id;
                abstractNode.ParentId = nodeValue.parentId;
                abstractNode.ChildIdList.AddRange(nodeValue.childIdList);
                abstractNode.SetNodeType((HSM_NODE_TYPE)nodeValue.nodeType);
                abstractNode.AddParameter(nodeValue.parameterList);
                abstractNode.AddTransition(nodeValue.transitionList);
                abstractNode.SetConditionCheck(iConditionCheck);

                abstractNodeDic[nodeValue.id] = abstractNode;
            }

            return abstractNodeDic;
        }

        private HSMStateBase AnalysisNode(HsmConfigNode nodeValue)
        {
            HSMStateBase node = HsmConfigBehaviorNode.Instance.GetNode(nodeValue.identification) as HSMStateBase;
            return node;
        }

        private void SetParameter(IConditionCheck iConditionCheck, HsmConfigTreeData data)
        {
            foreach(var parameter in data.parameterList)
            {
                iConditionCheck.AddParameter(parameter.Clone());
            }
        }

        private static int _entityId = 0;
        private int NewEntityId
        {
            get { return ++_entityId; }
        }
    }
}