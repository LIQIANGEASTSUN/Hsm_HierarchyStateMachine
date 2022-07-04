using UnityEngine;
using HSMTree;

/// <summary>
/// 吃饭
/// </summary>
public class StateEat : HSMStateBase, IHuman
{
    private Human human;
    public StateEat() : base()
    {  }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.LogError("开始吃饭");
    }

    public override void OnExit()
    {
        Debug.LogError("结束吃饭");
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
        // 吃饭状态执行的功能
        if (!human.MoveTo(PlaceEnum.Eat))
        {
            return;
        }

        human.Read(Random.Range(0.2f, 1));
        human.Homework(Random.Range(0.2f, 1));
        human.NetWrok(Random.Range(0.2f, 1));
        human.PlayGame(Random.Range(0.2f, 1));
        human.WatchTV(Random.Range(0.2f, 1));
        human.Basketball(Random.Range(0.2f, 1));
        human.Hunger(-2);
        human.Enery(0);
    }
}