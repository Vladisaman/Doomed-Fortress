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

    private void OnEnable()
    {
        CreateSkills();
    }

    private void OnDisable()
    {

    }

    public void ExitPanel()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    private SkillButton[] GetRandomSkills()
    {
        List<SkillButton> randomSkills = new List<SkillButton>();

        while (randomSkills.Count < 3)
        {
            Debug.Log(skills.Count);
            SkillButton randomSkill = skills[Random.Range(0, skills.Count-1)];

            if (!randomSkills.Contains(randomSkill))
            {
                randomSkills.Add(randomSkill);
            }
        }

        return randomSkills.ToArray();
    }

    private void CreateSkills()
    {
        SkillButton[] skills = GetRandomSkills();

        for (int i = 0; i < skills.Length; i++)
        {
            var instance = Instantiate(skills[i], skillPlaces[i]);
            instance.button.onClick.AddListener(() => { Panel.SetActive(false); Time.timeScale = 1; });
        }
    }
}
