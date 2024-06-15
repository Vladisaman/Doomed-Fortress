using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : MonoBehaviour
{
    [SerializeField] private Button exitButton;
    [SerializeField] public List<SkillButton> skills;
    [SerializeField] private List<Transform> skillPlaces;
    [SerializeField] private GameObject Panel;
    [SerializeField] public Mortar mortar;
    [SerializeField] public Crossbow crossbow;
    [SerializeField] private int rerollAmount;

    private int currentSkillNumber;

    private void Awake()
    {
        currentSkillNumber = 0;
    }

    public void CreateTutorialSkills()
    {
        SkillButton[] skills = this.skills.ToArray();

        for (int i = 0; i < skills.Length; i++)
        {
            var instance = Instantiate(skills[i], skillPlaces[i]);
            instance.button.onClick.AddListener(() => { Panel.SetActive(false); Time.timeScale = 1; });
        }
    }

    public void ExitPanel()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void CreateSkills()
    {
        SkillButton[] skills = GetRandomSkills();

        for (int i = 0; i < skills.Length; i++)
        {
            var instance = Instantiate(skills[i], skillPlaces[i]);
            instance.button.onClick.AddListener(() => { Panel.SetActive(false); Time.timeScale = 1; });
        }
    }

    public void RerollSkills()
    {
        if(rerollAmount >= 1)
        {
            CreateSkills();
            rerollAmount--;
        }
    }

    private SkillButton[] GetRandomSkills()
    {
        List<SkillButton> randomSkills = new List<SkillButton>();

        while (randomSkills.Count < 3)
        {
            SkillButton randomSkill = skills[Random.Range(0, skills.Count-1)];

            if (!randomSkills.Contains(randomSkill))
            {
                randomSkills.Add(randomSkill);
            }
        }

        return randomSkills.ToArray();
    }

    public void GiveSkills(int currentSkill)
    {
        if (currentSkill <= Spawner.playerData.skillAmount - 1)
        {
            Debug.Log("GOVNO");
            CreateGiveSkills();
        } 
        else
        {
            Debug.Log("JOPA");
            Panel.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void CreateGiveSkills()
    {
        SkillButton[] skills = GetRandomSkills();

        for (int i = 0; i < skills.Length; i++)
        {
            var instance = Instantiate(skills[i], skillPlaces[i]);
            instance.button.onClick.AddListener(() => { currentSkillNumber++; GiveSkills(currentSkillNumber); });
        }
    }
}
