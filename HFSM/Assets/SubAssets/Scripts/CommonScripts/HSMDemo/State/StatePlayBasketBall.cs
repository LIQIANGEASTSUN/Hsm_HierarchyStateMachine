using UnityEngine;
using HSMTree;

/// <summary>
/// 打篮球
/// </summary>
public class StatePlayBasketBall : HSMStateBase, IHuman
{
    private Human human;
    public StatePlayBasketBall() : base()
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.LogError("开始打篮球");
    }

    public override void OnExit()
    {
        Debug.LogError("结束打篮球");
    }

    public void SetHuman(Human human)
    {
        this.human = human;
    }

    /// <summary>
    /// 执行节点抽象方法
    /// </summary>
    /// <returns>返回执行结果</returns>
    public override void Execute()
    {
        if (!human.MoveTo(PlaceEnum.Basketball))
        {
            return;
        }

        human.Read(Random.Range(0.2f, 0.5F));
        human.Homework(Random.Range(0.2f, 0.5F));
        human.NetWrok(Random.Range(0.2f, 0.5F));
        human.PlayGame(0);
        human.WatchTV(0);
        human.Basketball(-2);
        human.Hunger(0.5f);
        human.Enery(-0.5f);
    }
}
