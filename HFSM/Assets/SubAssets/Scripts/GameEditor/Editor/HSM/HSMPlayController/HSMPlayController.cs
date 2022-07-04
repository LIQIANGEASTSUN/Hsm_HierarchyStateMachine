
namespace HSMTree
{
    public enum HSMPlayType
    {
        INVALID = -1,
        PLAY    = 0,
        PAUSE   = 1,
        STOP    = 2,
    }

    public class HSMPlayController
    {
        private HSMPlayView _playView;

        public HSMPlayController()
        {
            Init();
        }

        private void Init()
        {
            _playView = new HSMPlayView();
        }

        public void OnDestroy()
        {

        }

        public void OnGUI()
        {
            _playView.Draw();
        }
        
    }

}