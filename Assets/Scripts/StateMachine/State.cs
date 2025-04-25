using UnityEngine;

public abstract class State : MonoBehaviour
{
    public bool isEnded { get; protected set; } = false;
    private Animator animator;
    protected AnimationClip clip;
    protected Rigidbody2D rb;
    protected StateMachine machine;
    [SerializeField] protected GameObject player;


    
    public void InitState(Animator anim, Rigidbody2D rbN, GameObject pl, StateMachine mh)
    {
        animator = anim;
        rb = rbN;
        player = pl;
        machine = mh;
    }


    public virtual void StartState()
    {
        Debug.Log("Enter state");
        //animator.Play(clip.name);
    }

    public virtual void UpdateState()
    { }

    public virtual void EndState()
    { Debug.Log("Exit state"); }
}
