using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerEntryExitBackside : MonoBehaviour
{
    public Camera mainCamera;
    public Camera secondaryCamera;
    public NewPlayerMovement mainCharacterMovement;

    public GameObject TowerOutsideBackside;
    public GameObject TowerOutsideBackside2;


    public GameObject TowerOutside;
    public GameObject TowerOutsideDoorArchway;
    public GameObject TowerOutsideDoorArchway2;
    public GameObject TowerInsideBack;
    public GameObject TowerInsideFront;
    public GameObject TowerInsidePlatforms;
    public GameObject TowerOutsidePlatforms;
    public GameObject TowerMasking;
    public float alphaVisibilityOfArchway = 0.5f;
    public float alphaVisibilityOfMasking = 0.5f;

    [SerializeField]
    private bool withinTowerEntryWay = false; // should be set to private when tested to work

    [SerializeField]
    private bool withinTowerEntryWayKeyPressed = false; // should be set to private when tested to work

    [SerializeField]
    private bool isInsideTower = true; // should be set to private when tested to work

    [SerializeField]
    private BoxCollider2D[] towerInsideFrontColliders; // should be set to private when tested to work

    [SerializeField]
    private BoxCollider2D[] towerInsideFrontPlatforms; // should be set to private when tested to work

    [SerializeField]
    private BoxCollider2D[] towerOutsideBackPlatforms; // should be set to private when tested to work

    [SerializeField]
    private BoxCollider2D[] towerInsideBackColliders; // should be set to private when tested to work

    [SerializeField]
    private Material matTowerOutsideDoorArchway;
    
    [SerializeField]
    private Material matTowerOutsideDoorArchway2;

    [SerializeField]
    private Material matTowerMasking;

    // Check if Player enters the Entry Way of the Tower
    private void OnTriggerEnter2D(Collider2D collision)
    {
        withinTowerEntryWay = true;
    }

    // Check if Player leaves the Entry Way of the Tower
    private void OnTriggerExit2D(Collider2D collision)
    {
        withinTowerEntryWay = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*
        towerInsideFrontColliders = TowerInsideFront.GetComponents<BoxCollider2D>();
        towerInsideFrontPlatforms = TowerInsidePlatforms.GetComponentsInChildren<BoxCollider2D>();
        towerInsideBackColliders = TowerInsideBack.GetComponents<BoxCollider2D>();
        matTowerOutsideDoorArchway = TowerOutsideDoorArchway.GetComponent<TilemapRenderer>().material;
        matTowerOutsideDoorArchway2 = TowerOutsideDoorArchway2.GetComponent<TilemapRenderer>().material;
        matTowerMasking = TowerMasking.GetComponent<TilemapRenderer>().material;
        */

        towerInsideFrontPlatforms = TowerInsidePlatforms.GetComponentsInChildren<BoxCollider2D>();
        towerOutsideBackPlatforms = TowerOutsidePlatforms.GetComponentsInChildren<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        EnterAndExitTower();
    }

    public void EnterAndExitTower()
    {
        // If the player hits the up-direction then the player should enter the building
        // this is done by making the outside tower grid inactive when inside and active when outside

        float up_key_press = Input.GetAxisRaw("Vertical");

        if ((up_key_press > 0.0f) && withinTowerEntryWay)
        {
            if (!withinTowerEntryWayKeyPressed)
            {
                isInsideTower ^= true;

                if (mainCamera.gameObject.activeSelf && !isInsideTower)
                {
                    secondaryCamera.transform.position = mainCamera.transform.position;

                    mainCamera.gameObject.SetActive(false);
                    secondaryCamera.gameObject.SetActive(true);

                    mainCharacterMovement.flipMovement = true;
                }
                else if (secondaryCamera.gameObject.activeSelf && isInsideTower)
                {
                    mainCamera.transform.position = secondaryCamera.transform.position;

                    secondaryCamera.gameObject.SetActive(false);
                    mainCamera.gameObject.SetActive(true);
                    mainCharacterMovement.flipMovement = false;
                }

                
                foreach (var collider in towerInsideFrontColliders)
                {
                    //collider.enabled = isInsideTower;
                }
                
                foreach (var collider in towerInsideFrontPlatforms)
                {
                    collider.enabled = isInsideTower;
                }

                foreach (var collider in towerOutsideBackPlatforms)
                {
                    collider.enabled = !isInsideTower;
                }

                foreach (var collider in towerInsideBackColliders)
                {
                    //collider.enabled = isInsideTower;
                }
                

                withinTowerEntryWayKeyPressed = true;
            }
        }
        else
        {
            withinTowerEntryWayKeyPressed = false;
        }

        TowerOutsideBackside.SetActive(!isInsideTower);
        TowerOutsideBackside2.SetActive(!isInsideTower);
        TowerInsideBack.SetActive(isInsideTower);
        TowerInsideFront.SetActive(isInsideTower);
        //TowerOutside.SetActive(!isInsideTower);
        //TowerInsideFront.SetActive(isInsideTower);

        // show slightly transparent color of the archway/entry to the tower and completely opaque when outside
        // the tower
        /*
        if (isInsideTower && matTowerOutsideDoorArchway)
        {
            Color c = matTowerOutsideDoorArchway.color;
            c.a = alphaVisibilityOfArchway;
            matTowerOutsideDoorArchway.color = c;
            matTowerOutsideDoorArchway2.color = c;

            Color c_masking = matTowerMasking.color;
            c_masking.a = alphaVisibilityOfMasking;
            matTowerMasking.color = c_masking;
        }
        else if (matTowerOutsideDoorArchway)
        {
            Color c = matTowerOutsideDoorArchway.color;
            c.a = 1.0f;
            matTowerOutsideDoorArchway.color = c;
            matTowerOutsideDoorArchway2.color = c;

            Color c_masking = matTowerMasking.color;
            c_masking.a = 0.0f;
            matTowerMasking.color = c_masking;
        }
        */
    }
}
