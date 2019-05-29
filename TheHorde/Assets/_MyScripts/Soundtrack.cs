using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundtrack : MonoBehaviour
{
    private Soundtrack instance;
    public AudioClip menuTrack;
    public AudioClip levelTrack;
    public AudioSource audioS;

    private void Awake()
    {
        //Check if there is already an instance of Soundtrack
        if (instance == null)
            instance = this;
        //if it already exists:
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayLevelTrack(bool on)
    {
        if(on)
            audioS.clip = levelTrack;
        else
            audioS.clip = menuTrack;
        audioS.Play();
    }

}
