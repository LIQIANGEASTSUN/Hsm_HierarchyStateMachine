
namespace HSMTree
{
    public class HSMDescriptController
    {
        private HSMDescriptModel _descriptModel;
        private HSMDescriptView _descriptView;

        public HSMDescriptController()
        {
            Init();
        }

        public void Init()
        {
            _descriptModel = new HSMDescriptModel();
            _descriptView = new HSMDescriptView();
        }

        public void OnDestroy()
        {

        }

        public void OnGUI()
        {
            HsmConfigTreeData data = _descriptModel.GetData();
            _descriptView.Draw(data);
        }
    }
}