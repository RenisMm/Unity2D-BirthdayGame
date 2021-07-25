using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSoundStop : MonoBehaviour
{
    void Start()
    {
        BGSound.Instance.gameObject.GetComponent<AudioSource>().Pause();
    }
}
