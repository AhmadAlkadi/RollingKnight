using UnityEngine;
using UnityEngine.UI;

public class VolumeChange : MonoBehaviour
{
    public MusicManager manageVolume;
    public MainMenuMusic mainMenuVolume;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Slider volumeSlider = GetComponent<Slider>();
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (manageVolume.volume < 1.0f)
            {
                manageVolume.SetVolume(manageVolume.volume + 0.1f);
                mainMenuVolume.SetVolume(manageVolume.volume + 0.1f);
                volumeSlider.value += 0.1f;
            }

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (manageVolume.volume > 0.0f)
            {
                manageVolume.SetVolume(manageVolume.volume - 0.1f);
                mainMenuVolume.SetVolume(manageVolume.volume + 0.1f);
                volumeSlider.value -= 0.1f;
            }

        }
    }

}
