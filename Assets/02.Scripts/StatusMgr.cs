using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusMgr : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //저장되어있는 포만, 위생, 친밀을 불러온다.
        SceneMgr.hungry_bar.GetComponent<Slider>().value = PlayerPrefs.GetFloat("Hungry");
        SceneMgr.health_bar.GetComponent<Slider>().value = PlayerPrefs.GetFloat("Health");
        SceneMgr.realationship_bar.GetComponent<Slider>().value = PlayerPrefs.GetFloat("Realation");
    }

}
