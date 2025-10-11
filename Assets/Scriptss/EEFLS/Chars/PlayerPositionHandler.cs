using UnityEngine;

public class PlayerPositionHandler : MonoBehaviour
{
    void Start()
    {
        if (GameMaster.instance.hasSavedPosition)
        {
            gameObject.GetComponent<CharacterController>().enabled = false;
            transform.position = GameMaster.instance.GetPlayerPosition();
            gameObject.GetComponent<CharacterController>().enabled = true;
        }
    }

    void OnDestroy()
    {
        GameMaster.instance.SavePlayerPosition(transform.position);
    }
}
