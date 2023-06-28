using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private SpriteAtlas charactersAtlas;
    [SerializeField]
    private Image _characterRenderer;

    [SerializeField]
    private TextMeshProUGUI characterText;

    [SerializeField]
    private TextMeshProUGUI levelWord;
    [SerializeField]
    private TextMeshProUGUI levelTitle;
    [SerializeField]
    private TextMeshProUGUI levelCount;

    [SerializeField]
    private AudioSource alienVoice;

    [SerializeField]
    private TextMeshProUGUI StartWord;
    [SerializeField]
    private TextMeshProUGUI alienName;

    [SerializeField]
    private Button nextButton;
    [SerializeField]
    private Button prevButton;


    // Start is called before the first frame update
    void Start()
    {
        GameParams.achievedLevel = 6;
        GameParams.currentLevel = GameParams.achievedLevel;

        StartCoroutine(TypeText());

        levelWord.text = GameParams.getLevelWord();
        StartWord.text = GameParams.getStartWord();

        setLevelparameters();
    }

    private IEnumerator TypeText()
    {
        alienVoice.Play();
        characterText.text = "";
        foreach (char letter in GameParams.getAlienTextList()[GameParams.currentLevel])
        {
            characterText.text += letter;
            yield return null;
        }

        alienVoice.Stop();
    }

    private void setLevelparameters()
    {
        if (GameParams.currentLevel < 2)
        {
            alienName.text = GameParams.AlienNameTextEng[0];
            _characterRenderer.sprite = charactersAtlas.GetSprite("0");
        }
        else if (GameParams.currentLevel < 4)
        {
            alienName.text = GameParams.AlienNameTextEng[1];
            _characterRenderer.sprite = charactersAtlas.GetSprite("1");
        }
        else if (GameParams.currentLevel < 6)
        {
            alienName.text = GameParams.AlienNameTextEng[2];
            _characterRenderer.sprite = charactersAtlas.GetSprite("2");
        }
        else
        {
            alienName.text = GameParams.AlienNameTextEng[3];
            _characterRenderer.sprite = charactersAtlas.GetSprite("3");
        }
        levelTitle.text = GameParams.getLevelName()[GameParams.currentLevel];
        levelCount.text = (GameParams.currentLevel+1).ToString();

        if (GameParams.currentLevel == GameParams.achievedLevel) nextButton.interactable = false;
        else if (!nextButton.interactable) nextButton.interactable = true;
        if (GameParams.currentLevel == 0) prevButton.interactable = false;
        else if (!prevButton.interactable) prevButton.interactable = true;
    }

    public void switchTheLevel(bool next) {
        if (next)
        {
            if (GameParams.currentLevel < GameParams.achievedLevel)
            {
                GameParams.currentLevel++;
                setLevelparameters();

                StopAllCoroutines();
                alienVoice.Stop();
                StartCoroutine(TypeText());
            }
        }
        else {
            if (GameParams.currentLevel >0)
            {
                GameParams.currentLevel--;
                setLevelparameters();
                StopAllCoroutines();
                alienVoice.Stop();
                StartCoroutine(TypeText());
            }
        }
    }

    public void GoToBattle() {
        SceneSwitchManager.LoadBattleScene();
    }

}
