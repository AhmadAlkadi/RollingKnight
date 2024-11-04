using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SceneAttachment : MonoBehaviour
{
    enum FACE_ATTACHMENTS : int {LEFT_FACE=0, RIGHT_FACE=1, FRONT_FACE = 2, BACK_FACE = 3, TOP_FACE = 4 };
    private GameObject frontFaceAttachmentPoint = null;
    private GameObject backFaceAttachmentPoint = null;
    private GameObject leftFaceAttachmentPoint = null;
    private GameObject rightFaceAttachmentPoint = null;
    private GameObject topFaceAttachmentPoint = null;
    private GameObject sceneRootObject = null;

    private string currentActiveScene = "Test Scene 1";

    public Camera cubeCamera;

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
        backFaceAttachmentPoint = transform.GetChild((int)FACE_ATTACHMENTS.BACK_FACE).gameObject;
        leftFaceAttachmentPoint = transform.GetChild((int)FACE_ATTACHMENTS.LEFT_FACE).gameObject;
        rightFaceAttachmentPoint = transform.GetChild((int)FACE_ATTACHMENTS.RIGHT_FACE).gameObject;
        topFaceAttachmentPoint = transform.GetChild((int)FACE_ATTACHMENTS.TOP_FACE).gameObject;
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
        print("scene " + scene.name + " finished loading!");

        if (scene.name == "Test Scene 1")
        {
            AttachToOrientedFace(scene, mode, FACE_ATTACHMENTS.FRONT_FACE);
        }
        else if (scene.name == "Test Scene 2")
        {
            AttachToOrientedFace(scene, mode, FACE_ATTACHMENTS.RIGHT_FACE);
        }
    }

    private void AttachToOrientedFace(Scene scene, LoadSceneMode mode, FACE_ATTACHMENTS face_attachment)
    {
        if (scene.name == currentActiveScene)
        {
            print("active scene " + scene.name);
            SceneManager.SetActiveScene(scene);
        }

        float horizontal_rotation = 0.0f;
        float vertical_rotation = 0.0f;
        float cube_scale_x = 0.0f;
        float cube_scale_y = 0.0f;
        float cube_scale_z = 0.0f;
        float margin_x = 0.0f;
        float margin_y = 0.0f;
        float margin_z = 0.0f;

        GameObject face_attachment_point;

        switch (face_attachment)
        {
            case FACE_ATTACHMENTS.BACK_FACE:
                horizontal_rotation = 180.0f;
                cube_scale_z = transform.localScale.z;
                face_attachment_point = backFaceAttachmentPoint;
                break;

            case FACE_ATTACHMENTS.LEFT_FACE:
                horizontal_rotation = 90.0f;
                cube_scale_x = -transform.localScale.x;
                face_attachment_point = leftFaceAttachmentPoint;
                break;

            case FACE_ATTACHMENTS.RIGHT_FACE:
                horizontal_rotation = -90.0f;
                cube_scale_x = transform.localScale.x;
                face_attachment_point = rightFaceAttachmentPoint;
                break;

            case FACE_ATTACHMENTS.TOP_FACE:
                vertical_rotation = 90.0f;
                cube_scale_y = transform.localScale.y;
                face_attachment_point = topFaceAttachmentPoint;
                break;

            default:
                horizontal_rotation = 0.0f;
                vertical_rotation = 0.0f;
                cube_scale_z = -transform.localScale.z;
                margin_z = -10.0f;
                face_attachment_point = frontFaceAttachmentPoint;
                break;
        }

        sceneRootObject = scene.GetRootGameObjects()[0].gameObject;
        sceneRootObject.transform.Rotate(0.0f, horizontal_rotation, vertical_rotation);

        var cameraRootObject = scene.GetRootGameObjects()[1].gameObject;
        cameraRootObject.SetActive(false);

        sceneRootObject.transform.position = new Vector3((cube_scale_x / 2.0f), (cube_scale_y / 1.0f), (cube_scale_z / 2.0f));
        cameraRootObject.transform.position = new Vector3((cube_scale_x / 2.0f) + margin_x, (cube_scale_y / 1.0f) + margin_y, (cube_scale_z / 2.0f) + margin_z);

        sceneRootObject.transform.SetParent(face_attachment_point.transform);
    }
}
