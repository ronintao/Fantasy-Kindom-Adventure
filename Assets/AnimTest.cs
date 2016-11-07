using UnityEngine;
using System.Collections;

public class AnimTest : StateMachineBehaviour {

    public int Value;

    private int mNameHash;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        mNameHash = stateInfo.fullPathHash;
        Debug.Log(string.Format("Time : {3}, Fuc : {0}, Value : {1}, hash : {2}", "Enter", Value, stateInfo.fullPathHash, Time.time));
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Debug.Log(string.Format("Time : {3}, Fuc : {0}, Value : {1}, hash : {2}", "", Value, stateInfo.fullPathHash, Time.time));
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Debug.Log(string.Format("Time : {3}, Fuc : {0}, Value : {1}, hash : {2}", "Exit", Value, stateInfo.fullPathHash, Time.time));
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
