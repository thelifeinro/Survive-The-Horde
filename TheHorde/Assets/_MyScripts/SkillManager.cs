using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    [System.Serializable]
    public class Skill{
        public Button btn;
        public bool locked = true;
        public bool equipped = false;
        public EXPMilestone content;
    }

    public Skill[] skills;
    public GameObject detailsPanel;
    public GameObject detailsPrefab;
    public Text expCount;

    private Skill selectedSkill;
    public static SkillManager Instance;
    public CanvasGroup cnvs;

    void Awake()
    {
        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        cnvs.alpha = 0;
        cnvs.blocksRaycasts = false;
        // assign skill buttons the index they have
        for (int i = 0; i < skills.Length; i++)
        {
            int x = i;
            Skill sk = skills[i];
            sk.btn.onClick.AddListener(() => SelectSkill(x));
            //unlocking following updates
            if (sk.content.level == PlayerStats.instance.unlockedLevels[sk.content.type] + 1)
            {
                sk.locked = false;
                Debug.Log("Unlocked " + sk.content.name);
            }
            if (sk.locked) {
                GiveLockedLook(sk);
            }
            if (sk.equipped)
            {
                GiveBoughtLook(sk);
            }
        }

    }

    public void ShowCnvs() {
        cnvs.alpha = 1;
        cnvs.blocksRaycasts = true;
        //Time.timeScale = 0;
    }

    public void HideCnvs() {
        cnvs.alpha = 0;
        cnvs.blocksRaycasts = false;
        //Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        expCount.text = PlayerStats.EXP + " EXP";
    }

    public void GiveBoughtLook(Skill sk) {
        var color = sk.btn.GetComponent<Image>().color;
        Color baseCol = color;
        sk.btn.GetComponent<Image>().color = Color.white;
        sk.btn.transform.GetComponentInChildren<Text>().color = baseCol;
    }

    public void GiveLockedLook(Skill sk)
    {
        var color = sk.btn.GetComponent<Image>().color;
        color.a = 0.5f;
        sk.btn.GetComponent<Image>().color = color;
    }

    public void GiveUnlockedLook(Skill sk)
    {
        var color = sk.btn.GetComponent<Image>().color;
        color.a = 1.0f;
        sk.btn.GetComponent<Image>().color = color;
    }


    public void SelectSkill(int i)
    {
        //removing any previous card from the panel
        foreach (Transform child in detailsPanel.transform)
        {
            Destroy(child.gameObject);
        }

        Debug.Log("selected " + i);
        //instantiating detail cards
        GameObject detailsCard = MakeDetailCard();
        SkillCard sc = detailsCard.GetComponent<SkillCard>();

        sc.SetUpCard(skills[i]);
    }

    GameObject MakeDetailCard()
    {
        GameObject detailsCard = Instantiate(detailsPrefab, detailsPanel.transform);
        return detailsCard;
    }

    void UnlockSkills() {

    }

    public void UnlockSkill(Skill sk)
    {
        Debug.Log("unlocking");
        if (sk != null)
        {
            sk.locked = false;
            GiveUnlockedLook(sk);
        }
    }

    public static void BuySkill(Skill sk) {
        Debug.Log("buying");
        sk.equipped = true;


        PlayerStats.instance.unlockedLevels[sk.content.type] = sk.content.level;
        //looking for follow up skill to be unlocked for buying
        Skill followup = Instance.FindNextSkill(sk);
        Instance.UnlockSkill(followup);
        Instance.GiveBoughtLook(sk);
        //calling PlayerStats to unlock upgrade
        PlayerStats.instance.UnlockUpgrade(sk.content);
    }

    Skill FindNextSkill(Skill sk)
    {
        int indx = Array.IndexOf(skills, sk);
        if (indx != skills.Length-1 && skills[indx + 1].content.type == sk.content.type)
        {
            return skills[indx + 1];
        }
        else
            return null;
    }
}
