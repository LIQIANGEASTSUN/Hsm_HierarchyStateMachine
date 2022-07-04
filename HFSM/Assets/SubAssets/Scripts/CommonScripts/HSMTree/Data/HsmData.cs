using System.Collections.Generic;
using GraphicTree;

public class HsmConfigTreeData
{
    public string fileName = string.Empty;

    public int defaultStateId;

    public List<HsmConfigNode> nodeList = new List<HsmConfigNode>();

    public List<NodeParameter> parameterList = new List<NodeParameter>();

    public string descript = string.Empty;

}

public class HsmConfigNode
{
    public int id;

    public int nodeType;

    public List<NodeParameter> parameterList = new List<NodeParameter>();

    public List<HsmConfigTransition> transitionList = new List<HsmConfigTransition>();

    public string nodeName = string.Empty;

    public string identification = string.Empty;

    public string descript = string.Empty;

    public RectT position;

    public List<int> childIdList = new List<int>();

    public int parentId;

}

public class HsmConfigTransition
{
    public int transitionId;

    public int toStateId;

    public List<NodeParameter> parameterList = new List<NodeParameter>();

    public List<ConditionGroup> groupList = new List<ConditionGroup>();

    public HsmConfigTransition Clone()
    {
        HsmConfigTransition configTransition = new HsmConfigTransition();
        configTransition.transitionId = this.transitionId;
        configTransition.toStateId = this.toStateId;

        for (int i = 0; i < parameterList.Count; ++i)
        {
            NodeParameter nodeParameter = parameterList[i];
            configTransition.parameterList.Add(nodeParameter.Clone());
        }

        for (int i = 0; i < groupList.Count; ++i)
        {
            ConditionGroup conditionGroup = groupList[i];
            configTransition.groupList.Add(conditionGroup.Clone());
        }

        return configTransition;
    }
}

