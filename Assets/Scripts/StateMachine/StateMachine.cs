using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State currentState { get; private set; } = null;




    public void RequestChangeState(State newS)
    {
        if(currentState == null || (currentState.isEnded && currentState != newS))
        {
            currentState?.EndState();
            currentState = newS;
            currentState.StartState();
        }
    }

    void Update()
    {
        currentState?.UpdateState();
    }
}
