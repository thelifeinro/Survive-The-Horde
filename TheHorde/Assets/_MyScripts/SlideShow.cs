using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideShow : MonoBehaviour
{
    public Sprite[] slides;
    public Image img;
    public Animator anim;
    int i = 0;
    public bool paused = false;
    public GameObject nextBtn;
    public Text skipText;

    // Start is called before the first frame update
    void Start()
    {
        
        i = 0;
        img.sprite = slides[i];
        if (slides.Length == 1)
            ReachedEnd();
    }

    private void Awake()
    {
        Debug.Log("pausing");
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0;
        }
    }

    public void Next()
    {
        if (i < slides.Length-1)
        {
            i++;
            img.sprite = slides[i];
            anim.Play("SLideLeft");
        }
        if (i == slides.Length - 1)
            ReachedEnd();
    }

    public void Close()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
    }

    public void ReachedEnd()
    {
        nextBtn.SetActive(false);
        skipText.text = "BEGIN LEVEL";
    }
}
