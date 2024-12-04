using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public bool checker = true;
    public string loadSceneName = "";
    public float time = 5.0f;
    private bool runInvoke = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!runInvoke)
        {
            Invoke(nameof(allowExit), time);
            runInvoke = true;
        }

        if (Input.GetKeyDown(KeyCode.Return) && checker)
        {
            SceneManager.LoadScene(loadSceneName);
        }
    }

    public void allowExit()
    {
        checker = true;
    }
}
