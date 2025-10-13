using UnityEngine;

public class StatePersistence : MonoBehaviour
{
    [System.Serializable]
    public class State
    {
        public Vector3 playerPosition;
        public Quaternion playerRotation;
        // TODO : add other quest/data/other things ass needed ( yes it's not a mistake )
    }

    public void SaveState(Vector3 pos, Quaternion rot, Object quests)
    {
        State state = new State { playerPosition = pos, playerRotation = rot };
        string json = JsonUtility.ToJson(state);
        // TODO : In the future change to use Scriptable Object instead of player prefs if needed
        // I know its not necessary but I just want to use scriptable object so i can see the data.
        // thats all.
        PlayerPrefs.SetString("ExplorationState", json);
    }
    
    public State LoadState()
    {
        string json = PlayerPrefs.GetString("ExplorationState", "{}");      // this also
        return JsonUtility.FromJson<State>(json);
    }
}