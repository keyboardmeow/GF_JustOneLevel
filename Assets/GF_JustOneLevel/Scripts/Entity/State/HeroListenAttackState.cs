using GameFramework.Event;
using GameFramework.Fsm;
using UnityEngine;

public class HeroListenAttackState : FsmState<HeroLogic> {
    private bool m_IsAtk = false;

    /// <summary>
    /// 有限状态机状态初始化时调用。
    /// </summary>
    /// <param name="fsm">有限状态机引用。</param>
    protected override void OnInit (IFsm<HeroLogic> fsm) { 
    }

    /// <summary>
    /// 有限状态机状态进入时调用。
    /// </summary>
    /// <param name="fsm">有限状态机引用。</param>
    protected override void OnEnter (IFsm<HeroLogic> fsm) {
        m_IsAtk = false;

        SubscribeEvent(ClickAttackButtonEventArgs.EventId, OnClickAttackButtonEvent);
    }

    /// <summary>
    /// 有限状态机状态轮询时调用。
    /// </summary>
    /// <param name="fsm">有限状态机引用。</param>
    /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
    /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
    protected override void OnUpdate (IFsm<HeroLogic> fsm, float elapseSeconds, float realElapseSeconds) {
        if (m_IsAtk) {
            ChangeState<HeroAtkState> (fsm);
        }
    }

    /// <summary>
    /// 有限状态机状态离开时调用。
    /// </summary>
    /// <param name="fsm">有限状态机引用。</param>
    /// <param name="isShutdown">是否是关闭有限状态机时触发。</param>
    protected override void OnLeave (IFsm<HeroLogic> fsm, bool isShutdown) {
        UnsubscribeEvent(ClickAttackButtonEventArgs.EventId, OnClickAttackButtonEvent);
    }

    /// <summary>
    /// 有限状态机状态销毁时调用。
    /// </summary>
    /// <param name="fsm">有限状态机引用。</param>
    protected override void OnDestroy (IFsm<HeroLogic> fsm) {
        base.OnDestroy (fsm);
        UnsubscribeEvent(ClickAttackButtonEventArgs.EventId, OnClickAttackButtonEvent);
    }

    private void OnClickAttackButtonEvent(IFsm<HeroLogic> fsm, object sender, object userData) {
        ChangeState<HeroAtkState> (fsm);
    }
}