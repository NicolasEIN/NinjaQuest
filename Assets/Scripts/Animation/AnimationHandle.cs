using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class AnimationHandle : MonoBehaviour
{

    private static Animator animator;
    public enum PlayerState
    {
        Idle,
        Move,
        Attack,
        Die,
    }
    //private Animator animator;
   private PlayerState currentState;

    //public readonly string horizontal = "Horizontal";
    //public readonly string vertical = "Vertical";
    //public readonly string lastHorizontal = "LastHorizontal";
    //public readonly string lastVertical = "LastVertical";
    //public string currentState;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public PlayerState CurrentState
    {
        set
        {
            currentState = value;

            switch (currentState)
            {
                case PlayerState.Idle:
                    animator.Play("Idle");
                    break;
                case PlayerState.Move:
                    animator.Play("Movement");
                    break;
                case PlayerState.Attack:
                    animator.Play("Attack");
                    break;
            }
        }
    }

    //public void ChangeAnimationState(string newState)
    //{
    //    if (newState == currentState)
    //    {
    //        return;
    //    }

    //    animator.Play(newState);
    //    currentState = newState;
    //}
    //public bool isAnimationPlaying(Animator animator, string stateName)
    //{
    //    if (animator.GetCurrentAnimatorStateInfo(0).IsName(stateName) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}
}