using System.Collections.Generic;
using GraphicTree;

namespace HSMTree
{
    public class HsmConfigBehaviorNode : AbstractConfigNode
    {
        public readonly static HsmConfigBehaviorNode Instance = new HsmConfigBehaviorNode();
        public HsmConfigBehaviorNode() : base()
        {
            Init();
        }

        public override void Init()
        {
            base.Init();

            #region Human
            Config<StateEat>("Human/吃饭");
            Config<StateHomeWork>("Human/写作业");
            Config<StateNetWork>("Human/网上听课");
            Config<StatePlayBasketBall>("Human/打篮球");
            Config<StatePlayGame>("Human/打游戏");
            Config<StateRead>("Human/读书");
            Config<StateTakeReset>("Human/休息");
            Config<StateWatchTV>("Human/看电视");
            #endregion

            Config<HSMSubStateMachine>("SubMachine");
            Config<HSMStateEntry>("Entry");
            Config<HSMStateExit>("Exit");
        }

        protected override void PrimaryNode()
        {

        }
    }
}
