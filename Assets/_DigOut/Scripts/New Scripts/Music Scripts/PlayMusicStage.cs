using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicStage : MonoBehaviour
{
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        PlayMusic(index);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMusic(int indexMusic)
    {
        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.PlayMusic(MusicManager.Instance.musicClipList[indexMusic]);
        }
    }
}
