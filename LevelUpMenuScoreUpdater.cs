using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class LevelUpMenuScoreUpdater : MonoBehaviour
{
    //Script is attached to  Game > RestCanvas > LevelUpMenu

    //Displaying Levels
    public TMP_Text LevelUpMenuScore_Text;
    public TMP_Text LevelUpMenuCost_Text;
    public TMP_Text LevelUpMenuDisplayLvl_Text;
    public TMP_Text LevelUpMenuDisplayHealthLvl_Text;
    public TMP_Text LevelUpMenuDisplayStrengthLvl_Text;
    public TMP_Text LevelUpMenuDisplayDexterityLvl_Text;
    public TMP_Text LevelUpMenuDisplayDefenceLvl_Text;
    public TMP_Text LevelUpMenuDisplayManaLvl_Text;
    public TMP_Text LevelUpMenuDisplayAgilityLvl_Text;

    //Plus Level Buttons
    public Button HealthPlusButton;
    public Button StrengthPlusButton;
    public Button DexterityPlusButton;
    public Button DefencePlusButton;
    public Button ManaPlusButton;
    public Button AgilityPlusButton;
    
    //Minus Level Buttons
    public Button HealthMinusButton;
    public Button StrengthMinusButton;
    public Button DexterityMinusButton;
    public Button DefenceMinusButton;
    public Button ManaMinusButton;
    public Button AgilityMinusButton;

    //Confirm Button
    public Button ConfirmButton;

    //Exp Cost amount
    public int ExpToNextLvl;

    //Variables that store the amount of times each plus button has been pressed
    public static int HealthPressCount;
    public static int StrengthPressCount;
    public static int DexterityPressCount;
    public static int DefencePressCount;
    public static int ManaPressCount;
    public static int AgilityPressCount;
    public static int TotalPressCount;

    //This activates when the level up menu is made active
    private void OnEnable() 
    {
        PlayerStats.PlayerLvl = PlayerStats.HealthLvl + PlayerStats.StrengthLvl + PlayerStats.DexterityLvl + PlayerStats.DefenceLvl + PlayerStats.ManaLvl + PlayerStats.AgilityLvl; //recalcs player level
        LevelUpMenuDisplayLvl_Text.text = PlayerStats.PlayerLvl.ToString();         //Update display of player level
        ExpToNextLvl = (int)Mathf.Pow((float)PlayerStats.PlayerLvl, 2f);

        LevelUpMenuScore_Text.text = PlayerStats.currentExp.ToString();

        LevelUpMenuCost_Text.text = ExpToNextLvl.ToString();

        LevelUpMenuDisplayLvl_Text.text = PlayerStats.PlayerLvl.ToString();
        LevelUpMenuDisplayHealthLvl_Text.text = PlayerStats.HealthLvl.ToString();
        LevelUpMenuDisplayStrengthLvl_Text.text = PlayerStats.StrengthLvl.ToString();
        LevelUpMenuDisplayDexterityLvl_Text.text = PlayerStats.DexterityLvl.ToString();
        LevelUpMenuDisplayDefenceLvl_Text.text = PlayerStats.DefenceLvl.ToString();
        LevelUpMenuDisplayManaLvl_Text.text = PlayerStats.ManaLvl.ToString();
        LevelUpMenuDisplayAgilityLvl_Text.text = PlayerStats.AgilityLvl.ToString();



        if (ExpToNextLvl <= PlayerStats.currentExp)
        {
            HealthPlusButton.interactable = true;
            StrengthPlusButton.interactable = true;
            DexterityPlusButton.interactable = true;
            DefencePlusButton.interactable = true;
            ManaPlusButton.interactable = true;
            AgilityPlusButton.interactable = true;
        }

        else
        {
            HealthPlusButton.interactable = false;
            StrengthPlusButton.interactable = false;
            DexterityPlusButton.interactable = false;
            DefencePlusButton.interactable = false;
            ManaPlusButton.interactable = false;
            AgilityPlusButton.interactable = false;
        }

        if (HealthPlusButton.interactable == false)
        {
            ConfirmButton.Select();
        }
    }

    public void HealthUpButton()
    {
        HealthPressCount++;
        TotalPressCount++;
        HealthMinusButton.interactable = true;
        PlayerStats.HealthLvl++;
        PlayerStats.currentExp -= ExpToNextLvl;                                     //spend spirit orbs on level
        LevelUpMenuDisplayHealthLvl_Text.text = PlayerStats.HealthLvl.ToString();   //refreshes health lvl displayed
        PlayerStats.PlayerLvl = PlayerStats.HealthLvl + PlayerStats.StrengthLvl + PlayerStats.DexterityLvl + PlayerStats.DefenceLvl + PlayerStats.ManaLvl + PlayerStats.AgilityLvl; //recalcs player level
        LevelUpMenuDisplayLvl_Text.text = PlayerStats.PlayerLvl.ToString();         //displays player level
        ExpToNextLvl = (int)Mathf.Pow((float)PlayerStats.PlayerLvl, 2f);                      //Recalculates cost of a level up based on PlayerLvl
        LevelUpMenuCost_Text.text = ExpToNextLvl.ToString();                        //Display new cost of level following level up
        LevelUpMenuScore_Text.text = PlayerStats.currentExp.ToString();             //Update display of spirit orbs held

        if (ExpToNextLvl <= PlayerStats.currentExp)
        {
            HealthPlusButton.interactable = true;
            StrengthPlusButton.interactable = true;
            DexterityPlusButton.interactable = true;
            DefencePlusButton.interactable = true;
            ManaPlusButton.interactable = true;
            AgilityPlusButton.interactable = true;
        }

        else
        {
            HealthPlusButton.interactable = false;
            StrengthPlusButton.interactable = false;
            DexterityPlusButton.interactable = false;
            DefencePlusButton.interactable = false;
            ManaPlusButton.interactable = false;
            AgilityPlusButton.interactable = false;

            HealthMinusButton.Select();
        }
    }
    public void HealthDownButton()
    {
        HealthPressCount--;
        TotalPressCount--;
        PlayerStats.HealthLvl--;
        LevelUpMenuDisplayHealthLvl_Text.text = PlayerStats.HealthLvl.ToString();   //refreshes health lvl displayed
        PlayerStats.PlayerLvl = PlayerStats.HealthLvl + PlayerStats.StrengthLvl + PlayerStats.DexterityLvl + PlayerStats.DefenceLvl + PlayerStats.ManaLvl + PlayerStats.AgilityLvl; //recalcs player level
        LevelUpMenuDisplayLvl_Text.text = PlayerStats.PlayerLvl.ToString();         //Update display of player level
        ExpToNextLvl = (int)Mathf.Pow((float)PlayerStats.PlayerLvl, 2f);                       //Recalculates cost of a level up based on PlayerLvl
        PlayerStats.currentExp += ExpToNextLvl;                                     //refunds spirit orbs
        LevelUpMenuScore_Text.text = PlayerStats.currentExp.ToString();             //Update display of spirit orbs held
        LevelUpMenuCost_Text.text = ExpToNextLvl.ToString();                        //Update display of level cost

        if (HealthPressCount <= 0)
        {
            HealthMinusButton.interactable = false;
            HealthPlusButton.Select();
        }

        if (ExpToNextLvl <= PlayerStats.currentExp)
        {
            HealthPlusButton.interactable = true;
            StrengthPlusButton.interactable = true;
            DexterityPlusButton.interactable = true;
            DefencePlusButton.interactable = true;
            ManaPlusButton.interactable = true;
            AgilityPlusButton.interactable = true;
        }

        else
        {
            HealthPlusButton.interactable = false;
            StrengthPlusButton.interactable = false;
            DexterityPlusButton.interactable = false;
            DefencePlusButton.interactable = false;
            ManaPlusButton.interactable = false;
            AgilityPlusButton.interactable = false;
        }
    }
    public void StrengthUpButton()
    {
        StrengthPressCount++;
        TotalPressCount++;
        StrengthMinusButton.interactable = true;
        PlayerStats.StrengthLvl++;
        PlayerStats.currentExp -= ExpToNextLvl;                                     //spend spirit orbs on level
        LevelUpMenuDisplayStrengthLvl_Text.text = PlayerStats.StrengthLvl.ToString();   //refreshes Strength lvl displayed
        PlayerStats.PlayerLvl = PlayerStats.HealthLvl + PlayerStats.StrengthLvl + PlayerStats.DexterityLvl + PlayerStats.DefenceLvl + PlayerStats.ManaLvl + PlayerStats.AgilityLvl; //recalcs player level
        LevelUpMenuDisplayLvl_Text.text = PlayerStats.PlayerLvl.ToString();         //displays player level
        ExpToNextLvl = (int)Mathf.Pow((float)PlayerStats.PlayerLvl, 2f);                        //Recalculates cost of a level up based on PlayerLvl
        LevelUpMenuCost_Text.text = ExpToNextLvl.ToString();                        //Display new cost of level following level up
        LevelUpMenuScore_Text.text = PlayerStats.currentExp.ToString();             //Update display of spirit orbs held

        if (ExpToNextLvl <= PlayerStats.currentExp)
        {
            HealthPlusButton.interactable = true;
            StrengthPlusButton.interactable = true;
            DexterityPlusButton.interactable = true;
            DefencePlusButton.interactable = true;
            ManaPlusButton.interactable = true;
            AgilityPlusButton.interactable = true;
        }

        else
        {
            HealthPlusButton.interactable = false;
            StrengthPlusButton.interactable = false;
            DexterityPlusButton.interactable = false;
            DefencePlusButton.interactable = false;
            ManaPlusButton.interactable = false;
            AgilityPlusButton.interactable = false;
            StrengthMinusButton.Select();
        }
    }
    public void StrengthDownButton()
    {
        StrengthPressCount--;
        TotalPressCount--;
        PlayerStats.StrengthLvl--;
        LevelUpMenuDisplayStrengthLvl_Text.text = PlayerStats.StrengthLvl.ToString();   //refreshes Strength lvl displayed
        PlayerStats.PlayerLvl = PlayerStats.HealthLvl + PlayerStats.StrengthLvl + PlayerStats.DexterityLvl + PlayerStats.DefenceLvl + PlayerStats.ManaLvl + PlayerStats.AgilityLvl; //recalcs player level
        LevelUpMenuDisplayLvl_Text.text = PlayerStats.PlayerLvl.ToString();         //Update display of player level
        ExpToNextLvl = (int)Mathf.Pow((float)PlayerStats.PlayerLvl, 2f);                        //Recalculates cost of a level up based on PlayerLvl
        PlayerStats.currentExp += ExpToNextLvl;                                     //refunds spirit orbs
        LevelUpMenuScore_Text.text = PlayerStats.currentExp.ToString();             //Update display of spirit orbs held
        LevelUpMenuCost_Text.text = ExpToNextLvl.ToString();                        //Update display of level cost

        if (StrengthPressCount <= 0)
        {
            StrengthMinusButton.interactable = false;
            StrengthPlusButton.Select();
        }

        if (ExpToNextLvl <= PlayerStats.currentExp)
        {
            HealthPlusButton.interactable = true;
            StrengthPlusButton.interactable = true;
            DexterityPlusButton.interactable = true;
            DefencePlusButton.interactable = true;
            ManaPlusButton.interactable = true;
            AgilityPlusButton.interactable = true;
        }

        else
        {
            HealthPlusButton.interactable = false;
            StrengthPlusButton.interactable = false;
            DexterityPlusButton.interactable = false;
            DefencePlusButton.interactable = false;
            ManaPlusButton.interactable = false;
            AgilityPlusButton.interactable = false;
        }
    }
    public void DexterityUpButton()
    {
        DexterityPressCount++;
        TotalPressCount++;
        DexterityMinusButton.interactable = true;
        PlayerStats.DexterityLvl++;
        PlayerStats.currentExp -= ExpToNextLvl;                                     //spend spirit orbs on level
        LevelUpMenuDisplayDexterityLvl_Text.text = PlayerStats.DexterityLvl.ToString();   //refreshes Dexterity lvl displayed
        PlayerStats.PlayerLvl = PlayerStats.HealthLvl + PlayerStats.StrengthLvl + PlayerStats.DexterityLvl + PlayerStats.DefenceLvl + PlayerStats.ManaLvl + PlayerStats.AgilityLvl; //recalcs player level
        LevelUpMenuDisplayLvl_Text.text = PlayerStats.PlayerLvl.ToString();         //displays player level
        ExpToNextLvl = (int)Mathf.Pow((float)PlayerStats.PlayerLvl, 2f);                        //Recalculates cost of a level up based on PlayerLvl
        LevelUpMenuCost_Text.text = ExpToNextLvl.ToString();                        //Display new cost of level following level up
        LevelUpMenuScore_Text.text = PlayerStats.currentExp.ToString();             //Update display of spirit orbs held

        if (ExpToNextLvl <= PlayerStats.currentExp)
        {
            HealthPlusButton.interactable = true;
            StrengthPlusButton.interactable = true;
            DexterityPlusButton.interactable = true;
            DefencePlusButton.interactable = true;
            ManaPlusButton.interactable = true;
            AgilityPlusButton.interactable = true;
        }

        else
        {
            HealthPlusButton.interactable = false;
            StrengthPlusButton.interactable = false;
            DexterityPlusButton.interactable = false;
            DefencePlusButton.interactable = false;
            ManaPlusButton.interactable = false;
            AgilityPlusButton.interactable = false;
            DexterityMinusButton.Select();
        }
    }
    public void DexterityDownButton()
    {
        DexterityPressCount--;
        TotalPressCount--;
        PlayerStats.DexterityLvl--;
        LevelUpMenuDisplayDexterityLvl_Text.text = PlayerStats.DexterityLvl.ToString();   //refreshes Dexterity lvl displayed
        PlayerStats.PlayerLvl = PlayerStats.HealthLvl + PlayerStats.StrengthLvl + PlayerStats.DexterityLvl + PlayerStats.DefenceLvl + PlayerStats.ManaLvl + PlayerStats.AgilityLvl; //recalcs player level
        LevelUpMenuDisplayLvl_Text.text = PlayerStats.PlayerLvl.ToString();         //Update display of player level
        ExpToNextLvl = (int)Mathf.Pow((float)PlayerStats.PlayerLvl, 2f);                       //Recalculates cost of a level up based on PlayerLvl
        PlayerStats.currentExp += ExpToNextLvl;                                     //refunds spirit orbs
        LevelUpMenuScore_Text.text = PlayerStats.currentExp.ToString();             //Update display of spirit orbs held
        LevelUpMenuCost_Text.text = ExpToNextLvl.ToString();                        //Update display of level cost

        if (DexterityPressCount <= 0)
        {
            DexterityMinusButton.interactable = false;
            DexterityPlusButton.Select();
        }

        if (ExpToNextLvl <= PlayerStats.currentExp)
        {
            HealthPlusButton.interactable = true;
            StrengthPlusButton.interactable = true;
            DexterityPlusButton.interactable = true;
            DefencePlusButton.interactable = true;
            ManaPlusButton.interactable = true;
            AgilityPlusButton.interactable = true;
        }

        else
        {
            HealthPlusButton.interactable = false;
            StrengthPlusButton.interactable = false;
            DexterityPlusButton.interactable = false;
            DefencePlusButton.interactable = false;
            ManaPlusButton.interactable = false;
            AgilityPlusButton.interactable = false;
        }
    }

    public void DefenceUpButton()
    {
        DefencePressCount++;
        TotalPressCount++;
        DefenceMinusButton.interactable = true;
        PlayerStats.DefenceLvl++;
        PlayerStats.currentExp -= ExpToNextLvl;                                     //spend spirit orbs on level
        LevelUpMenuDisplayDefenceLvl_Text.text = PlayerStats.DefenceLvl.ToString();   //refreshes Defence lvl displayed
        PlayerStats.PlayerLvl = PlayerStats.HealthLvl + PlayerStats.StrengthLvl + PlayerStats.DexterityLvl + PlayerStats.DefenceLvl + PlayerStats.ManaLvl + PlayerStats.AgilityLvl; //recalcs player level
        LevelUpMenuDisplayLvl_Text.text = PlayerStats.PlayerLvl.ToString();         //displays player level
        ExpToNextLvl = (int)Mathf.Pow((float)PlayerStats.PlayerLvl, 2f);                       //Recalculates cost of a level up based on PlayerLvl
        LevelUpMenuCost_Text.text = ExpToNextLvl.ToString();                        //Display new cost of level following level up
        LevelUpMenuScore_Text.text = PlayerStats.currentExp.ToString();             //Update display of spirit orbs held

        if (ExpToNextLvl <= PlayerStats.currentExp)
        {
            HealthPlusButton.interactable = true;
            StrengthPlusButton.interactable = true;
            DexterityPlusButton.interactable = true;
            DefencePlusButton.interactable = true;
            ManaPlusButton.interactable = true;
            AgilityPlusButton.interactable = true;
        }

        else
        {
            HealthPlusButton.interactable = false;
            StrengthPlusButton.interactable = false;
            DexterityPlusButton.interactable = false;
            DefencePlusButton.interactable = false;
            ManaPlusButton.interactable = false;
            AgilityPlusButton.interactable = false;
            DefenceMinusButton.Select();
        }
    }

    public void DefenceDownButton()
    {
        DefencePressCount--;
        TotalPressCount--;
        PlayerStats.DefenceLvl--;
        LevelUpMenuDisplayDefenceLvl_Text.text = PlayerStats.DefenceLvl.ToString();   //refreshes Defence lvl displayed
        PlayerStats.PlayerLvl = PlayerStats.HealthLvl + PlayerStats.StrengthLvl + PlayerStats.DexterityLvl + PlayerStats.DefenceLvl + PlayerStats.ManaLvl + PlayerStats.AgilityLvl; //recalcs player level
        LevelUpMenuDisplayLvl_Text.text = PlayerStats.PlayerLvl.ToString();         //Update display of player level
        ExpToNextLvl = (int)Mathf.Pow((float)PlayerStats.PlayerLvl, 2f);                        //Recalculates cost of a level up based on PlayerLvl
        PlayerStats.currentExp += ExpToNextLvl;                                     //refunds spirit orbs
        LevelUpMenuScore_Text.text = PlayerStats.currentExp.ToString();             //Update display of spirit orbs held
        LevelUpMenuCost_Text.text = ExpToNextLvl.ToString();                        //Update display of level cost

        if (DefencePressCount <= 0)
        {
            DefenceMinusButton.interactable = false;
            DefencePlusButton.Select();
        }

        if (ExpToNextLvl <= PlayerStats.currentExp)
        {
            HealthPlusButton.interactable = true;
            StrengthPlusButton.interactable = true;
            DexterityPlusButton.interactable = true;
            DefencePlusButton.interactable = true;
            ManaPlusButton.interactable = true;
            AgilityPlusButton.interactable = true;
        }

        else
        {
            HealthPlusButton.interactable = false;
            StrengthPlusButton.interactable = false;
            DexterityPlusButton.interactable = false;
            DefencePlusButton.interactable = false;
            ManaPlusButton.interactable = false;
            AgilityPlusButton.interactable = false;
        }
    }
    public void ManaUpButton()
    {
        ManaPressCount++;
        TotalPressCount++;
        ManaMinusButton.interactable = true;
        PlayerStats.ManaLvl++;
        PlayerStats.currentExp -= ExpToNextLvl;                                     //spend spirit orbs on level
        LevelUpMenuDisplayManaLvl_Text.text = PlayerStats.ManaLvl.ToString();   //refreshes Mana lvl displayed
        PlayerStats.PlayerLvl = PlayerStats.HealthLvl + PlayerStats.StrengthLvl + PlayerStats.DexterityLvl + PlayerStats.DefenceLvl + PlayerStats.ManaLvl + PlayerStats.AgilityLvl; //recalcs player level
        LevelUpMenuDisplayLvl_Text.text = PlayerStats.PlayerLvl.ToString();         //displays player level
        ExpToNextLvl = (int)Mathf.Pow((float)PlayerStats.PlayerLvl, 2f);                       //Recalculates cost of a level up based on PlayerLvl
        LevelUpMenuCost_Text.text = ExpToNextLvl.ToString();                        //Display new cost of level following level up
        LevelUpMenuScore_Text.text = PlayerStats.currentExp.ToString();             //Update display of spirit orbs held

        if (ExpToNextLvl <= PlayerStats.currentExp)
        {
            HealthPlusButton.interactable = true;
            StrengthPlusButton.interactable = true;
            DexterityPlusButton.interactable = true;
            DefencePlusButton.interactable = true;
            ManaPlusButton.interactable = true;
            AgilityPlusButton.interactable = true;
        }

        else
        {
            HealthPlusButton.interactable = false;
            StrengthPlusButton.interactable = false;
            DexterityPlusButton.interactable = false;
            DefencePlusButton.interactable = false;
            ManaPlusButton.interactable = false;
            AgilityPlusButton.interactable = false;
            ManaMinusButton.Select();
        }
    }
    public void ManaDownButton()
    {
        ManaPressCount--;
        TotalPressCount--;
        PlayerStats.ManaLvl--;
        LevelUpMenuDisplayManaLvl_Text.text = PlayerStats.ManaLvl.ToString();   //refreshes Mana lvl displayed
        PlayerStats.PlayerLvl = PlayerStats.HealthLvl + PlayerStats.StrengthLvl + PlayerStats.DexterityLvl + PlayerStats.DefenceLvl + PlayerStats.ManaLvl + PlayerStats.AgilityLvl; //recalcs player level
        LevelUpMenuDisplayLvl_Text.text = PlayerStats.PlayerLvl.ToString();         //Update display of player level
        ExpToNextLvl = (int)Mathf.Pow((float)PlayerStats.PlayerLvl, 2f);                       //Recalculates cost of a level up based on PlayerLvl
        PlayerStats.currentExp += ExpToNextLvl;                                     //refunds spirit orbs
        LevelUpMenuScore_Text.text = PlayerStats.currentExp.ToString();             //Update display of spirit orbs held
        LevelUpMenuCost_Text.text = ExpToNextLvl.ToString();                        //Update display of level cost

        if (ManaPressCount <= 0)
        {
            ManaMinusButton.interactable = false;
            ManaPlusButton.Select();
        }

        if (ExpToNextLvl <= PlayerStats.currentExp)
        {
            HealthPlusButton.interactable = true;
            StrengthPlusButton.interactable = true;
            DexterityPlusButton.interactable = true;
            DefencePlusButton.interactable = true;
            ManaPlusButton.interactable = true;
            AgilityPlusButton.interactable = true;
        }

        else
        {
            HealthPlusButton.interactable = false;
            StrengthPlusButton.interactable = false;
            DexterityPlusButton.interactable = false;
            DefencePlusButton.interactable = false;
            ManaPlusButton.interactable = false;
            AgilityPlusButton.interactable = false;
        }
    }

    public void AgilityUpButton()
    {
        AgilityPressCount++;
        TotalPressCount++;
        AgilityMinusButton.interactable = true;
        PlayerStats.AgilityLvl++;
        PlayerStats.currentExp -= ExpToNextLvl;                                     //spend spirit orbs on level
        LevelUpMenuDisplayAgilityLvl_Text.text = PlayerStats.AgilityLvl.ToString();   //refreshes Agility lvl displayed
        PlayerStats.PlayerLvl = PlayerStats.HealthLvl + PlayerStats.StrengthLvl + PlayerStats.DexterityLvl + PlayerStats.DefenceLvl + PlayerStats.ManaLvl + PlayerStats.AgilityLvl; //recalcs player level
        LevelUpMenuDisplayLvl_Text.text = PlayerStats.PlayerLvl.ToString();         //displays player level
        ExpToNextLvl = (int)Mathf.Pow((float)PlayerStats.PlayerLvl, 2f);                   //Recalculates cost of a level up based on PlayerLvl
        LevelUpMenuCost_Text.text = ExpToNextLvl.ToString();                        //Display new cost of level following level up
        LevelUpMenuScore_Text.text = PlayerStats.currentExp.ToString();             //Update display of spirit orbs held

        if (ExpToNextLvl <= PlayerStats.currentExp)
        {
            HealthPlusButton.interactable = true;
            StrengthPlusButton.interactable = true;
            DexterityPlusButton.interactable = true;
            DefencePlusButton.interactable = true;
            ManaPlusButton.interactable = true;
            AgilityPlusButton.interactable = true;
        }

        else
        {
            HealthPlusButton.interactable = false;
            StrengthPlusButton.interactable = false;
            DexterityPlusButton.interactable = false;
            DefencePlusButton.interactable = false;
            ManaPlusButton.interactable = false;
            AgilityPlusButton.interactable = false;
            AgilityMinusButton.Select();
        }
    }

    public void AgilityDownButton()
    {
        AgilityPressCount--;
        TotalPressCount--;
        PlayerStats.AgilityLvl--;
        LevelUpMenuDisplayAgilityLvl_Text.text = PlayerStats.AgilityLvl.ToString();   //refreshes Agility lvl displayed
        PlayerStats.PlayerLvl = PlayerStats.HealthLvl + PlayerStats.StrengthLvl + PlayerStats.DexterityLvl + PlayerStats.DefenceLvl + PlayerStats.ManaLvl + PlayerStats.AgilityLvl; //recalcs player level
        LevelUpMenuDisplayLvl_Text.text = PlayerStats.PlayerLvl.ToString();         //Update display of player level
        ExpToNextLvl = (int)Mathf.Pow((float)PlayerStats.PlayerLvl, 2f);                       //Recalculates cost of a level up based on PlayerLvl
        PlayerStats.currentExp += ExpToNextLvl;                                     //refunds spirit orbs
        LevelUpMenuScore_Text.text = PlayerStats.currentExp.ToString();             //Update display of spirit orbs held
        LevelUpMenuCost_Text.text = ExpToNextLvl.ToString();                        //Update display of level cost

        if (AgilityPressCount <= 0)
        {
            AgilityMinusButton.interactable = false;
            AgilityPlusButton.Select();
        }

        if (ExpToNextLvl <= PlayerStats.currentExp)
        {
            HealthPlusButton.interactable = true;
            StrengthPlusButton.interactable = true;
            DexterityPlusButton.interactable = true;
            DefencePlusButton.interactable = true;
            ManaPlusButton.interactable = true;
            AgilityPlusButton.interactable = true;
        }

        else
        {
            HealthPlusButton.interactable = false;
            StrengthPlusButton.interactable = false;
            DexterityPlusButton.interactable = false;
            DefencePlusButton.interactable = false;
            ManaPlusButton.interactable = false;
            AgilityPlusButton.interactable = false;
        }
    }
    public void ConfirmLevelUp()
    {
        //The following code removes the ability to minus levels if player was to go back into level up menu
        HealthMinusButton.interactable = false;
        StrengthMinusButton.interactable = false;
        DexterityMinusButton.interactable = false;
        DefenceMinusButton.interactable = false;
        ManaMinusButton.interactable = false;
        AgilityMinusButton.interactable = false;

        //Remove all press counts including total presses
        HealthPressCount = 0;
        StrengthPressCount = 0;
        DexterityPressCount = 0;
        DefencePressCount = 0;
        ManaPressCount = 0;
        AgilityPressCount = 0;
        TotalPressCount = 0;
    }
    public void CancelLevelUp()
    {
        //Remove all levels
        PlayerStats.HealthLvl -= HealthPressCount;
        PlayerStats.StrengthLvl -= StrengthPressCount;
        PlayerStats.DexterityLvl -= DexterityPressCount;
        PlayerStats.DefenceLvl -= DefencePressCount;
        PlayerStats.ManaLvl -= ManaPressCount;
        PlayerStats.AgilityLvl -= AgilityPressCount;

        for (int i = 0; i < TotalPressCount; i++)
        {
            PlayerStats.PlayerLvl--;
            ExpToNextLvl = (int)Mathf.Pow((float)PlayerStats.PlayerLvl, 2f); //Recalculates cost of a level up based on PlayerLvl
            PlayerStats.currentExp += ExpToNextLvl; //refunds spirit orbs
        }

        //The following code removes the ability to minus levels if player was to go back into level up menu
        HealthMinusButton.interactable = false;
        StrengthMinusButton.interactable = false;
        DexterityMinusButton.interactable = false;
        DefenceMinusButton.interactable = false;
        ManaMinusButton.interactable = false;
        AgilityMinusButton.interactable = false;

        //Remove all press counts
        HealthPressCount = 0;
        StrengthPressCount = 0;
        DexterityPressCount = 0;
        DefencePressCount = 0;
        ManaPressCount = 0;
        AgilityPressCount = 0;
        TotalPressCount = 0;
    }
}