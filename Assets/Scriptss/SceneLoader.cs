using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class SceneLoader : MonoBehaviour
{
    public void LoadSceneAsync(string sceneID)
    {
        GameMaster.instance.LoadSceneAsyncByString(sceneID);
    }
}
