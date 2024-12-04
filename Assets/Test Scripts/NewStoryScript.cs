using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewStoryScript : MonoBehaviour
{
    [SerializeField]
    private Text BodyText;
    [SerializeField]
    private Text NameText;
    string[] storyLine = {"Flashes and images scatter about the mind. Fragments of memories from hours ago blink and dissapate.",
    "Over there! A blasted magic wielder, prepare yourselves!",
    "A blurry image of what appears to be a crow comes into view. The bird throws out his magenta solution and it shatters against the ground.",
    "Purple gasses fill the lungs and it all returns to darkness.",
    "...",   
    "A mysterious warmth envelops the mouse’s mind. It was as though a warm blanket wrapped around him on a cold winter night.", 
    "Wake up.", 
    "The mouse struggles to move nor awaken.", 
    "Your powers are still awakening. Get up and go find the cheese they have taken away from your King. I will tell you what to do next.", 
    "The warmth starts to intensify.",
    "Winsworth, there are many of them that hate us.",  
    "Please I beg of you, a great reward I will bestow upon you. As an initial payment to what I can give you, is this.", 
    "Winsworth awakens and bolts out of his restraints."};
    string[] nameLine = {null, "Cronen", null, null, null, null, "???", null, "???", null, "???", "???", null};
    int i = 1;
    [SerializeField]
    //public AudioClip soundClip;
    private AudioSource audioSource;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // start of story with first element
        BodyText.text = storyLine[0];
    }

    // Update is called once per frame
    void Update()
    {
        // when user presses space or clicks iterate text array
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) 
        {
            if(i < storyLine.Length){
                BodyText.text = storyLine[i];
                NameText.text = nameLine[i];
                i++;
                audioSource.Play();
            }
            // else play first level
            else{
                SceneManager.LoadScene("Level_Intro");
            }
        }
    }
}
