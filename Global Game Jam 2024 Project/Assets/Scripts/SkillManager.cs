using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public bool isPlayer1;
    public List<Skill> skillList; //= new List<Skill>();

    private void Start()
    {
        AddSkillToList("Troll Face");
        AddSkillToList("I like trains");
    }

    private void Update()
    {
        if (isPlayer1 == true)
        {
            if (Input.GetKeyDown("j"))
            {
                skillList[0].UseSkill();
            }
            else if (Input.GetKeyDown("k"))
            {
                skillList[1].UseSkill();
            }
            else if (Input.GetKeyDown("l"))
            {
                skillList[2].UseSkill();
            }
        }
        else
        {
            if (Input.GetKeyDown("[1]"))
            {
                skillList[0].UseSkill();
            }
            else if (Input.GetKeyDown("[2]"))
            {
                skillList[1].UseSkill();
            }
            else if (Input.GetKeyDown("[3]"))
            {
                skillList[2].UseSkill();
            }
        }

    }

    void AddSkillToList(string skillName)
    {
        skillList.Add(AddAndConvertStringToSkill(skillName));
    }

    Skill AddAndConvertStringToSkill(string skillName)
    {
        switch (skillName)
        {
            case "I like trains":
                return gameObject.AddComponent<TestSkill>();
            case "Mario 64 painting":
                return gameObject.AddComponent<TestSkill>();
            case "NOOT NOOT":
                return gameObject.AddComponent<TestSkill>();
            case "Troll Face":
                return gameObject.AddComponent<TestSkill>();
            case "Bing Chilling":
                return gameObject.AddComponent<TestSkill>();
            case "SIGMA":
                return gameObject.AddComponent<TestSkill>();
            case "GOAT":
                return gameObject.AddComponent<TestSkill>();
            case "PEPE PUNCHING":
                return gameObject.AddComponent<TestSkill>();
            default:
                return null;
        }
    }


}
