using UnityEngine;

public abstract class State : MonoBehaviour
{
    public bool isEnded { get; protected set; } = false;
    private Animator animator;
    [SerializeField] protected AnimationClip clip;
    
    public void InitState(Animator anim)
    {
        animator = anim;
    }


    public virtual void StartState()
    {
        animator.Play(clip.name);
    }

    public virtual void UpdateState()
    { }

    public virtual void EndState()
    { }
}
