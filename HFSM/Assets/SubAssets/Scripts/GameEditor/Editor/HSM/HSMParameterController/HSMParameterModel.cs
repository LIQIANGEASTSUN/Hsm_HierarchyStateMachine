using System.Collections.Generic;
using GraphicTree;

namespace HSMTree
{
    public class HSMParameterModel
    {
        public HSMParameterModel()
        {
        }

        public List<NodeParameter> ParameterList
        {
            get
            {
                return HsmDataController.Instance.HSMTreeData.parameterList;
            }
        }
    }
}
