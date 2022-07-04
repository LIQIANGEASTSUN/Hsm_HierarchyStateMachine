using System.Collections.Generic;
using GraphicTree;

namespace HSMTree
{

    public class HSMParameterController
    {
        private HSMParameterModel _parameterModel;
        private HSMParameterView _parameterView;

        public HSMParameterController()
        {
            Init();
        }

        public void Init()
        {
            _parameterModel = new HSMParameterModel();
            _parameterView = new HSMParameterView();
        }

        public void OnDestroy()
        {

        }

        public void OnGUI()
        {
            List<NodeParameter> parameterList = _parameterModel.ParameterList;
            _parameterView.Draw(parameterList);
        }

    }
}
