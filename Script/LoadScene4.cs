using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene4 : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadScene()
    {
        SceneManager.LoadSceneAsync("Scene4");
    }
}