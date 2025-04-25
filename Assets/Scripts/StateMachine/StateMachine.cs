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


    public void RequestExitState()
    {
        currentState?.EndState();
        currentState = null;
    }


    void Update()
    {
        currentState?.UpdateState();
    }
}
