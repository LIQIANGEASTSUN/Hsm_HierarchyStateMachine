using System.Collections.Generic;
using GraphicTree;

namespace HSMTree
{
    public class HSMRuntimeParameterController
    {
        private HSMRuntimeParameterModel _runtimeParameterModel;
        private HSMRuntimeParameterView _runtimeParameterView;

        public HSMRuntimeParameterController()
        {
            Init();
        }

        public void Init()
        {
            _runtimeParameterModel = new HSMRuntimeParameterModel();
            _runtimeParameterView = new HSMRuntimeParameterView();
            HsmDataController.hSMRuntimePlay += RuntimePlay;
        }

        public void OnDestroy()
        {
            HsmDataController.hSMRuntimePlay -= RuntimePlay;
        }

        public void OnGUI()
        {
            _runtimeParameterView.Draw(_runtimeParameterModel.ParameterList);
        }

        private void RuntimePlay(HSMPlayType state)
        {
            if (state == HSMPlayType.PLAY && null != HSMRunTime.Instance.ConditionCheck)
            {
                List<NodeParameter> parameterList = HSMRunTime.Instance.ConditionCheck.GetAllParameter();
                _runtimeParameterModel.AddParameter(parameterList);
            }
        }
    }
}