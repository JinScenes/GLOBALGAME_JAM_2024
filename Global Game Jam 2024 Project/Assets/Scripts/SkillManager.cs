using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public bool isPlayer1;
    public List<Skill> skillList; //= new List<Skill>();

    private void Start()
    {
        string[] plrItems = isPlayer1 ? Inventory.PlayerItems.player1Items : Inventory.PlayerItems.player2Items;
       
        foreach (string itemName in plrItems)
        {
            if (string.IsNullOrEmpty(itemName)){
                continue;
            }

            print("Checking " + itemName);
            AddSkillToList(itemName);
        }

        //for (int i = 0; i < plrItems.Length; i++)
        //{
        //    AddAndConvertStringToSkill(plrItems[i]);
        //    print("CHECKING" + plrItems[i]);
        //}
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

    private void AddSkillToList(string skillName)
    {
        Skill newSkill = AddAndConvertStringToSkill(skillName);
        if (newSkill != null)
        {
            skillList.Add(newSkill);
            print("Converted " + skillName);
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }

    private Skill AddAndConvertStringToSkill(string skillName)
    {
        switch (skillName)
        {
            case "I like trains":
                return gameObject.AddComponent<Iliketrains>();
            case "Mario 64 painting":
                return gameObject.AddComponent<Mario64Painting>();
            case "NOOT NOOT":
                return gameObject.AddComponent<NOOTNOOT>();
            case "Troll Face":
                return gameObject.AddComponent<TrollFace>();
            case "Bing Chilling":
                return gameObject.AddComponent<BingChilling>();
            case "SIGMA":
                return gameObject.AddComponent<SIGMA>();
            case "GOAT":
                return gameObject.AddComponent<GOAT>();
            case "PEPE PUNCHING":
                return gameObject.AddComponent<PepePunching>();
            default:
                print(skillName + " Doesn't exist!");
                return null;
        }
    }


}
