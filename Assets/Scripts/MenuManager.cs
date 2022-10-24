using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject menu;
    public static MenuManager instance;
    public AudioSource source;
    private bool gamePaused;

    //SFXs
    [System.Serializable]
    public class sfxClip
    {
        public string name;
        public AudioClip clip;
    }
    public sfxClip[] sfxClips;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void playSFX(string name)
    {
        foreach (sfxClip clip in sfxClips) {
            if (clip.name == name)
            {
                source.clip = clip.clip;
            }
        }
        source.loop = false;
        source.Play();
    }

    public void SetVolume(float val)
    {
        instance.source.volume = val;
    }

    public void Pause()
    {
        menu.SetActive(true);
        Time.timeScale = 0;
        gamePaused = true;
    }

    public void Resume()
    {
        menu.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }
}
