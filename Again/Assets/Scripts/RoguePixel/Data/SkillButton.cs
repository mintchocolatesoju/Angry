using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public SkillData skill;
    public  Image cooldownImage;
    private Image skillImage;
    [SerializeField] private Button button;
    
    // Start is called before the first frame update
    void Start()
    {
        button= GetComponent<Button>();
        skillImage = GetComponent<Image>();
        skillImage.sprite = skill.skillIcon;
        cooldownImage.fillAmount = 0;
    }

    public void OnClick()
    {
        if(cooldownImage.fillAmount >0) 
            return;
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        float tick = 1f/skill.cooldown;
        float t = 0;
        cooldownImage.fillAmount = 1;
        while (cooldownImage.fillAmount > 0)
        {
            cooldownImage.fillAmount = Mathf.Lerp(1,0,t);
            t += Time.deltaTime;
            yield return null;
        }
    }
    
}
