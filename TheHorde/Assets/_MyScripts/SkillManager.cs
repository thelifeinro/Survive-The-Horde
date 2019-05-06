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
        public EXPMilestone content;
    }

    public Skill[] skills;
    public GameObject detailsPanel;
    public GameObject detailsPrefab;

    public delegate void UnlockEventHandler(EXPMilestone ms);
    public static event UnlockEventHandler OnUpgradeUnlock;

    private Skill selectedSkill;
    

    // Start is called before the first frame update
    void Start()
    {
        // assign skill buttons the index they have
        for(int i = 0; i < skills.Length; i++)
        {
            int x = i;
            Skill sk = skills[i];
            sk.btn.onClick.AddListener(() => SelectSkill(x));
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SelectSkill(int i)
    {
        Debug.Log("selected " + i);
    }

    void ShowSkillDetails()
    {
        GameObject detailsCard = Instantiate(detailsPrefab, detailsPanel.transform);
    }

    void UnlockSkills() {

    }

    void UnlockSkill(Skill sk) {
        if (OnUpgradeUnlock != null)
        {
            //letting subscribers know an upgrade has been unlocked
            OnUpgradeUnlock(sk.content);
        }
    }
}
