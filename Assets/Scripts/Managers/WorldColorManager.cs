using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class WorldColorManager : MonoBehaviour
{
    public List<VolumeProfile> allWorldVolumeProfiles = new List<VolumeProfile>();
    public int vpIndex = 0;

    public VolumeProfile vpAllGray;
    public VolumeProfile vpOnlyGreen;
    public VolumeProfile vpGreenBlue;
    public VolumeProfile vpGreenBlueRed;
    //public VolumeProfile vpAllColor;

    Volume globalVolume;

    public VolumeProfile currentProfile;

    public static WorldColorManager Instance { get { return instance; } }
    static WorldColorManager instance;

    private void Awake()
    {
        instance = this;

        allWorldVolumeProfiles?.Add(vpAllGray);
        allWorldVolumeProfiles?.Add(vpOnlyGreen);
        allWorldVolumeProfiles?.Add(vpGreenBlue);
        allWorldVolumeProfiles?.Add(vpGreenBlueRed);
        //allWorldVolumeProfiles?.Add(vpAllColor);

        currentProfile = allWorldVolumeProfiles[0];
    }

    void Start()
    {
        globalVolume = GameObject.Find("Global Volume").GetComponent<Volume>();
        globalVolume.profile = currentProfile;
    }

    public void OnChangeWorldProfile()
    {
        if ((vpIndex + 1) < allWorldVolumeProfiles.Count)
        {
            currentProfile = allWorldVolumeProfiles[++vpIndex];
        }
        else
        {
            Debug.Log("Changing to VolumeProfile that doesnt exist");
        }

        globalVolume.profile = currentProfile;
    }
}
