using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {
    public string name;
    public int firstprice = 0;
    public int upgradeworth = 0;
    public int sellworth = 0;
    public float range;
    public float rate = 1;
    public int maxMen;
    public int Level = 0;
    public int menCount = 1;
    public GameObject[] men;

    [System.Serializable]
    public class TurretLevel
    {
        public bool locked = false;
        public string name;
        public int price;
        public float rate;
        public float range;
        public int men;
    }

    public TurretLevel[] levels;
    public int nextLevel = 1;


    // Use this for initialization
    void Start () {
        rate = men[0].GetComponent<Archer>().fireRate;
    }
	
	// Update is called once per frame
	void Update () { 
	}

    public void UpgradeMen()
    {
        if(menCount <= men.Length)
        {
            men[menCount-1].SetActive(true);
        }
    }

    public void UpgradeRate(float newRate)
    {
        foreach (GameObject man in men)
        {
            Archer archer = man.GetComponent<Archer>();
            archer.fireRate = newRate;
        }
    }

    public void Upgrade()
    {
        if (nextLevel < levels.Length)
        {
            if (PlayerStats.Money >= levels[nextLevel].price)
            {
                PlayerStats.Money -= levels[nextLevel].price;
                TurretLevel nextLev = levels[nextLevel];
                range = nextLev.range;
                rate = nextLev.rate;
                menCount = nextLev.men;
                name = nextLev.name;

                //for calculating the sell worth
                upgradeworth = nextLev.price;
                sellworth = firstprice + (int)(0.75 * upgradeworth);


                UpgradeRate(rate);
                UpgradeMen();

                Level++;
                nextLevel++;
            }
            
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
