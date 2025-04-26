using UnityEngine;

public class InterExit : EnvSignInteractionAction
{
    public override void Play(EqManager eq)
    {
        Debug.Log("exit");
        Application.Quit();
    }
}
