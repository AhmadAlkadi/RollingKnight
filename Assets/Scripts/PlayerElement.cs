using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.LightAnchor;

public class PlayerElement : MonoBehaviour
{
    public enum ELEMENT_TYPE { NORMAL, FIRE, ICE };
    public ELEMENT_TYPE currentElementType = ELEMENT_TYPE.NORMAL;
    public float elementTimeToVanishs = 10.0f;

    private bool isInvoked = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        switch (currentElementType)
        {
            case ELEMENT_TYPE.ICE:
            case ELEMENT_TYPE.FIRE:
                if(isInvoked == false)
                {
                    Invoke(nameof(setNormal), elementTimeToVanishs);
                    isInvoked = true;
                }             
                break;
        }
    }

    public ELEMENT_TYPE GetElementType_El()
    {
        return currentElementType;
    }

    public int GetIntELEMENT_TYPE()
    {
        int element = 0;
        switch (currentElementType)
        {
            case ELEMENT_TYPE.NORMAL:
                element = 0;
                break;
            case ELEMENT_TYPE.FIRE:
                element = 1;
                break;
            case ELEMENT_TYPE.ICE:
                element = 2;
                break;
        }
        return element;
    }

    public void setNormal()
    {
        currentElementType = ELEMENT_TYPE.NORMAL;
        gun currentGun = gameObject.GetComponentInChildren<gun>();
        NewPlayerMovement currentPlayer = gameObject.GetComponent<NewPlayerMovement>();
        currentGun.SetGun(gun.GUN_TYPE.NORMAL);
        currentPlayer.EnableNormalEffectOnPlayer();
        isInvoked = false;
    }

    public void SetElement(ELEMENT_TYPE element_type)
    {
        currentElementType = element_type;
    }
}
