using UnityEngine;

public class InterEquipNewSkill : EnvSignInteractionAction
{
    //Zmienne przechowujace dane o skillu w tym itemku
    public int skillID;

    public override void Play()
    {
        //po kliknieciu [e] wskakuje na wybrany slot
        Debug.Log("newItem");
    }
}
