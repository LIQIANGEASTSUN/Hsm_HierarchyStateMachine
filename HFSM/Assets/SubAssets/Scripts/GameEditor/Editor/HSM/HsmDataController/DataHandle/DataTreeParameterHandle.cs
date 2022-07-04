using GraphicTree;

namespace HSMTree
{
    public class DataTreeParameterHandle
    {

        public void DataTreeAddParameter(NodeParameter parameter)
        {
            ParameterTool.AddParameter(HsmDataController.Instance.HSMTreeData.parameterList, parameter);
            HSMRunTime.Instance.Reset(HsmDataController.Instance.HSMTreeData);
        }

        public void DataTreeDelParameter(NodeParameter parameter)
        {
            ParameterTool.DelParameter(HsmDataController.Instance.HSMTreeData.parameterList, parameter);
            HSMRunTime.Instance.Reset(HsmDataController.Instance.HSMTreeData);
        }

    }

}
