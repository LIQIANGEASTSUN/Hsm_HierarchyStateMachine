using System.Collections.Generic;
using UnityEngine;
using GraphicTree;

namespace HSMTree
{
    /// <summary>
    /// 节点超类
    /// </summary>
    public abstract class HSMStateBase : AbstractNode
    {
        protected HSM_NODE_TYPE _nodeType = HSM_NODE_TYPE.STATE;
        protected int _parentId;
        protected List<NodeParameter> _parameterList = new List<NodeParameter>();
        protected List<HsmConfigTransition> _transitionList = new List<HsmConfigTransition>();
        protected IConditionCheck _iconditionCheck = null;
        protected Dictionary<int, ConditionParameter> _conditionParameterDic = new Dictionary<int, ConditionParameter>();
        protected List<int> _childIdList = new List<int>();
        protected HSMSubStateMachine _parentSubMachine = null;

        public HSMStateBase()
        {

        }

        /// <summary>
        /// 执行节点抽象方法
        /// </summary>
        /// <returns>返回执行结果</returns>
        public virtual void Execute()
        {
#if UNITY_EDITOR
            NodeNotify.NotifyExecute(EntityId, NodeId, 0, Time.realtimeSinceStartup);
#endif
        }

        public bool Transition(ref int toState)
        {
            bool result = false;
            for (int i = 0; i < _transitionList.Count; ++i)
            {
                HsmConfigTransition transition = _transitionList[i];
                ConditionParameter conditionParameter = null;
                if (_conditionParameterDic.TryGetValue(transition.transitionId, out conditionParameter)
                    && _iconditionCheck.Condition(conditionParameter))
                {
                    result = true;
                    toState = transition.toStateId;
                    break;
                }
            }

            return result;
        }

        public virtual void AddParameter(List<NodeParameter> parameterList)
        {
            _parameterList.AddRange(parameterList);
        }

        public virtual void AddParameter(NodeParameter parameter)
        {
            _parameterList.Add(parameter);
        }

        public virtual void AddTransition(List<HsmConfigTransition> transitionList)
        {
            for (int i = 0; i < transitionList.Count; ++i)
            {
                AddTransition(transitionList[i]);
            }
        }

        public virtual void AddTransition(HsmConfigTransition transition)
        {
            _transitionList.Add(transition);

            ConditionParameter conditionParametr = new ConditionParameter();
            conditionParametr.Init(transition.groupList, transition.parameterList);
            _conditionParameterDic[transition.transitionId] = conditionParametr;
        }

        public virtual void SetConditionCheck(IConditionCheck iConditionCheck)
        {
            _iconditionCheck = iConditionCheck;
        }

        public override int NodeType()
        {
            return (int)_nodeType;
        }

        public void SetNodeType(HSM_NODE_TYPE nodeType)
        {
            this._nodeType = nodeType;
        }

        public int ParentId
        {
            get { return _parentId; }
            set { _parentId = value; }
        }

        public List<int> ChildIdList
        {
            get { return _childIdList; }
            set { _childIdList = value; }
        }

        public virtual void AddChildNode(HSMStateBase node)
        {
        }

        public HSMSubStateMachine ParentSubMachine
        {
            get { return _parentSubMachine; }
        }

        public virtual void SetParentSubMachine(HSMStateBase node)
        {
            _parentSubMachine = (HSMSubStateMachine)node;
        }
    }
}