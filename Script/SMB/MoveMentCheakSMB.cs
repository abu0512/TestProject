using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMentCheakSMB : StateMachineBehaviour {

    private bool transIn = false;
    private bool exited = false;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        transIn = animator.IsInTransition(layerIndex);
        exited = false;
        // 플레이어 이동 & 예외처리상태 OFF
        animator.GetComponent<CPlayerAniEvent>().MoveTypes(1);
        animator.GetComponent<CPlayerManager>().m_isRotationAttack = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!exited)
        {
            if (animator.IsInTransition(layerIndex))
            {
                if (!transIn)
                {
                    // 플레이어 이동 & 예외처리상태 ON
                    animator.GetComponent<CPlayerAniEvent>().MoveTypes(2);
                    animator.GetComponent<CPlayerManager>().m_isRotationAttack = true;
                    exited = true;
                }
            }
            else if (transIn)
            {
                transIn = false;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
