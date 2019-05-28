using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sniper : SpecialAttack {

    public GameObject SniperOverlay;
    public GameObject SniperPlayer;
    public Camera MainCam;
    public Text eta;
    public float duration;
    public bool running = false;
    public Camera sniperCam;
    public GameObject UI;
    public Transform EffectDropzone;
    public GameObject effectPrefab;
    public AudioSource audioSource;

    private int enemiesHit;
    private int expAwarded;
    float shake_decay;
    float shake_intensity;
    Vector3 originPosition;
    Quaternion originRotation;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (running)
        {
            //be able to shoot
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("pressed");
                Shoot();
                Shake();
            }

            //tryiong some camera shake
            if (shake_intensity > 0)
            {
                sniperCam.transform.position = originPosition + Random.insideUnitSphere * shake_intensity;
                sniperCam.transform.rotation = new Quaternion(
                                originRotation.x + Random.Range(-shake_intensity, shake_intensity) * 0.2f,
                                originRotation.y + Random.Range(-shake_intensity, shake_intensity) * 0.2f,
                                originRotation.z + Random.Range(-shake_intensity, shake_intensity) * 0.2f,
                                originRotation.w + Random.Range(-shake_intensity, shake_intensity) * 0.2f);
                shake_intensity -= shake_decay;
            }
        }
	}

    void Shoot()
    {
        

        if (audioSource != null)
        {
            audioSource.Play();
        }
        RaycastHit hit;
        if(Physics.Raycast(sniperCam.transform.position, sniperCam.transform.forward, out hit))
        {
            Debug.Log("Hit " + hit.transform.name);
            if (hit.transform.tag == "Enemy")
            {
                hit.transform.GetComponent<Enemy>().Die();
                enemiesHit++;
                //Debug.Log("HitEnemy");
                if(enemiesHit == 7)
                {
                    DoubleEXP();
                    return;
                }
                AwardEXP();
            }
        }
    }

    public void Shake()
    {
        originPosition = sniperCam.transform.position;
        originRotation = sniperCam.transform.rotation;
        shake_intensity = 0.06f;
        shake_decay = 0.01f;
    }

    void AwardEXP()
    {
        expAwarded++;
        SpawnEffect("+1 EXP");
    }

    void DoubleEXP()
    {
        Debug.Log("Doubling EXP");
        expAwarded *= 2;
        SpawnEffect("X 2");
    }

    void SpawnEffect(string content)
    {
        GameObject effect  = Instantiate(effectPrefab, EffectDropzone);
        effect.GetComponent<Text>().text = content;
        Destroy(effect, 2);
    }

    public override void StartAttack()
    {
        GameObject.FindGameObjectWithTag("EnemyBook").GetComponent<EnemyBook>().specialAttack = true;

        enemiesHit = 0;
        expAwarded = 0;
        hq.PauseMission();
        UIhiddener.HideUI();
        GlobalVariables.objectsAreInteractible = false;
        Cursor.lockState = CursorLockMode.Locked;
        UI.GetComponent<UIManager>().Hide();
        running = true;
        SniperOverlay.SetActive(true);

        SniperPlayer.SetActive(true);
        MainCam.enabled = false;
        StartCoroutine(Countdown());
        //start timer or sth

    }

    private IEnumerator Countdown()
    {
        float normalizedTime = 0;
        while (normalizedTime <= duration)
        {
            //countdownImage.fillAmount = normalizedTime;
            normalizedTime += Time.deltaTime;
            float remaining = duration - normalizedTime;
            eta.text = System.String.Format("{0:0.00}", remaining);
            //asign to text
            yield return null;
        }
        StopAttack();
    }

    void StopAttack()
    {

        UIhiddener.ShowUI();
        running = false;
        SniperOverlay.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        MainCam.enabled = true;
        SniperPlayer.SetActive(false);
        UI.GetComponent<UIManager>().Show();
        GlobalVariables.objectsAreInteractible = true;
        hq.ResumeMission();
        PlayerStats.instance.AddEXP(expAwarded);
        GameObject.FindGameObjectWithTag("EnemyBook").GetComponent<EnemyBook>().specialAttack = false ;
    }
}
