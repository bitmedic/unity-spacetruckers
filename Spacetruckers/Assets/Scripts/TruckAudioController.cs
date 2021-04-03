using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckAudioController : MonoBehaviour
{
    public AudioSource start;
    public AudioSource idle;
    public AudioSource booster;

    private bool isTruckSelected = false;
    private bool isTruckMoving = false;

    public void setTruckSelected(bool selected)
    {
        if (this.isTruckSelected == false && selected == true)
        {
            this.start.Play();
            this.idle.PlayDelayed(2.8f);
        }

        this.isTruckSelected = selected;

        if (this.isTruckSelected == false)
        {
            this.start.Stop();
            this.idle.Stop();
            this.booster.Stop();
        }
    }

    public void setTruckMoving(bool moving)
    {
        this.isTruckMoving = moving;

        if (this.isTruckMoving == true)
        {
            this.start.Stop();
            this.idle.Play();
            this.booster.Play();
        }
        else
        {
            this.booster.Stop();
            this.setTruckSelected(false);
        }
    }
}
