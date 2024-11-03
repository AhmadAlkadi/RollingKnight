using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SceneAttachment : MonoBehaviour
{
    enum FACE_ATTACHMENTS : int {LEFT_FACE=0, RIGHT_FACE=1, FRONT_FACE = 2, BACK_FACE = 3, TOP_FACE = 4 };
    private GameObject frontFaceAttachmentPoint;
    private GameObject rightFaceAttachmentPoint;
    private GameObject sceneRootObject = null;

    private string currentActiveScene = "Test Scene 1";

    private void OnEnable()
    {
        SceneManager.sceneLoaded += onSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= onSceneLoaded;
    }

    private void Awake()
    {
        frontFaceAttachmentPoint = transform.GetChild((int)FACE_ATTACHMENTS.FRONT_FACE).gameObject;
        rightFaceAttachmentPoint = transform.GetChild((int)FACE_ATTACHMENTS.RIGHT_FACE).gameObject;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var op2 = SceneManager.LoadSceneAsync("Test Scenes/Test Scene 2", LoadSceneMode.Additive);
        op2.allowSceneActivation = true;
        print("waiting for scene Test Scene 2 to load...");

        var op1 = SceneManager.LoadSceneAsync("Test Scenes/Test Scene 1", LoadSceneMode.Additive);
        op1.allowSceneActivation = true;
        print("waiting for scene Test Scene 1 to load...");
    }

    // Update is called once per frame
    void Update()
    {
        
    
    }

    void onSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Test Scene 1")
        {
            print("scene " + scene.name + " finished loading!");

            if (scene.name == currentActiveScene)
            {
                print("active scene " + scene.name);
                SceneManager.SetActiveScene(scene);
            }

            sceneRootObject = scene.GetRootGameObjects()[0].gameObject;
            var cameraRootObject = scene.GetRootGameObjects()[1].gameObject;

            sceneRootObject.transform.position = new Vector3(0.0f, 0.0f, -(transform.localScale.z / 2.0f));
            cameraRootObject.transform.position = new Vector3(0.0f, 0.0f, -(transform.localScale.z / 2.0f) - 10.0f);

            sceneRootObject.transform.SetParent(frontFaceAttachmentPoint.transform);
        }
        else if (scene.name == "Test Scene 2")
        {
            print("scene " + scene.name + " finished loading!");

            if (scene.name == currentActiveScene)
            {
                print("active scene " + scene.name);
                SceneManager.SetActiveScene(scene);
            }

            sceneRootObject = scene.GetRootGameObjects()[0].gameObject;
            sceneRootObject.transform.Rotate(0.0f, 90.0f, 0.0f);

            sceneRootObject.transform.position = new Vector3((transform.localScale.x / 2.0f), 0.0f, 0.0f);

            sceneRootObject.transform.SetParent(rightFaceAttachmentPoint.transform);
        }
    }
}
