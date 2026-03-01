using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalStairsReveal : MonoBehaviour
{
    public GameObject knob_blue;
    public GameObject knob_black;
    public GameObject knob_red;
    public GameObject knob_white;

    public GameObject stairs;
    public GameObject stairs_floor;

    public AudioSource background_sound;
    public AudioSource newAudioSource;

    private bool hasPlayed = false;

    void Update()
    {
        if (knob_blue.GetComponent<BlackLeverManager>().isUp == true &&
            knob_black.GetComponent<BlackLeverManager>().isUp == false &&
            knob_red.GetComponent<BlackLeverManager>().isUp == true &&
            knob_white.GetComponent<BlackLeverManager>().isUp == false)
        {
            stairs.SetActive(true);
            stairs_floor.SetActive(false);
            
            if (!hasPlayed)
            {
                background_sound.Pause();
                newAudioSource.Play();
                hasPlayed = true;
            }
        }
        else
        {
            stairs.SetActive(false);
            stairs_floor.SetActive(true);

            if (hasPlayed)
            {
                background_sound.UnPause();
                newAudioSource.Stop();
                hasPlayed = false;
            }
        }
    }
}
