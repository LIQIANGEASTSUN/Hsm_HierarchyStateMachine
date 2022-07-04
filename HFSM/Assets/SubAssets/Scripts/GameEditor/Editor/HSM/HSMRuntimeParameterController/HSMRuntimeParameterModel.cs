using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GraphicTree;

namespace HSMTree
{
    public class HSMRuntimeParameterModel
    {
        private List<NodeParameter> _parameterList = new List<NodeParameter>();

        public HSMRuntimeParameterModel()
        {
        }

        public void AddParameter(List<NodeParameter> parameterList)
        {
            _parameterList = parameterList;
        }

        public List<NodeParameter> ParameterList
        {
            get
            {
                return _parameterList;
            }
        }

    }
}
