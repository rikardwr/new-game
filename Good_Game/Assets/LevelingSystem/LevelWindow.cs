/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class LevelWindow : MonoBehaviour {

    private Text levelText;
    private Image experienceBarImage;
    private LevelSystem levelSystem;
    private LevelSystemAnimated levelSystemAnimated;

    private void Awake() {
        levelText = transform.Find("levelText").GetComponent<Text>();
        experienceBarImage = transform.Find("experienceBar").Find("bar").GetComponent<Image>();
        
        transform.Find("experience5Btn").GetComponent<Button_UI>().ClickFunc = () => levelSystem.AddExperience(5);
        transform.Find("experience50Btn").GetComponent<Button_UI>().ClickFunc = () => levelSystem.AddExperience(50);
        transform.Find("experience500Btn").GetComponent<Button_UI>().ClickFunc = () => levelSystem.AddExperience(500);
        
    }

    private void SetExperienceBarSize(float experienceNormalized) {
        experienceBarImage.fillAmount = experienceNormalized;
    }

    private void SetLevelNumber(int levelNumber) {
        levelText.text = "LEVEL\n" + (levelNumber + 1);
    }

    public void SetLevelSystem(LevelSystem levelSystem) {
        this.levelSystem = levelSystem;
        SetLevelNumber(levelSystem.GetLevelNumber());

        SetExperienceBarSize(levelSystem.GetExperienceNormalized());
    }

    public void SetLevelSystemAnimated(LevelSystemAnimated levelSystemAnimated) {
        // Set the LevelSystemAnimated object
        this.levelSystemAnimated = levelSystemAnimated;

        // Update the starting values
        SetLevelNumber(levelSystemAnimated.GetLevelNumber());
        SetExperienceBarSize(levelSystemAnimated.GetExperienceNormalized());

        // Surbscribe to the changed events
        levelSystemAnimated.OnExperienceChanged += LevelSystem_OnExperienceChanged;
        levelSystemAnimated.OnLevelChanged += LevelSystem_OnLevelChanged;
    }

    private void LevelSystem_OnLevelChanged(object sender, System.EventArgs e) {
        // Level changed, update text
        SetLevelNumber(levelSystemAnimated.GetLevelNumber());
    }

    private void LevelSystem_OnExperienceChanged(object sender, System.EventArgs e) {
        // Experience changed, update bar size
        SetExperienceBarSize(levelSystemAnimated.GetExperienceNormalized());
    }
}
