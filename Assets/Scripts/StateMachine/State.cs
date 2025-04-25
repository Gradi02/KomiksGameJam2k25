using UnityEngine;

public abstract class State : MonoBehaviour
{
    public bool isEnded { get; protected set; } = false;
    private Animator animator;
    [SerializeField] protected AnimationClip clip;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected GameObject player;


    
    public void InitState(Animator anim, Rigidbody2D rbN, GameObject pl)
    {
        animator = anim;
        rb = rbN;
        player = pl;
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
