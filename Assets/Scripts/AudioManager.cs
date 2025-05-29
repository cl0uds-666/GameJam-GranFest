using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("AudioSources")]
    public AudioSource musicSource;
    public AudioSource CarSource1;
    public AudioSource CarSource2;
    public AudioSource CarSource3;
    public AudioSource CarSource4;
    public AudioSource SFXSource;

    [Header("AudioClips")]
    public AudioClip BGM;
    public AudioClip EngineRev;
    public AudioClip Bump;
    public AudioClip Screech;
    public AudioClip Explode;
    public AudioClip OilSlip;
    public AudioClip CrowdCheer;
    public AudioClip CrowdCheer2;
    public AudioClip CrowdCheer3;
    public AudioClip IdleStartNoise;

    public void Update()
    {
        CarSource1.pitch = GetComponent<CarControllerRB>().forwardSpeed;
        CarSource2.pitch = GetComponent<CarControllerRB>().forwardSpeed;
        CarSource3.pitch = GetComponent<CarControllerRB>().forwardSpeed;
        CarSource4.pitch = GetComponent<CarControllerRB>().forwardSpeed;
    }
}



