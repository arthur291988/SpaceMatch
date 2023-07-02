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
    private TextMeshProUGUI StartWord;
    [SerializeField]
    private TextMeshProUGUI CharacterName;

    [SerializeField]
    private Button nextButton;
    [SerializeField]
    private Button prevButton;

    [SerializeField]
    private GameObject messageButton;
    [SerializeField]
    private GameObject startButton;
    [SerializeField]
    private GameObject discordButton;

    private bool assistantTextFinished;


    // Start is called before the first frame update
    void Start()
    {
        GameParams.achievedLevel = 7;
        GameParams.currentLevel = GameParams.achievedLevel;

        //StartCoroutine(TypeText());

        levelWord.text = GameParams.getLevelWord();
        StartWord.text = GameParams.getStartWord();

        setLevelparameters();

    }

    private IEnumerator TypeAssistantText()
    {
        AudioManager.Instance.assistantVoiceFunc(true);
        characterText.text = "";

        assistantTextFinished = true;
        foreach (char letter in GameParams.getAssistantTextList()[GameParams.currentLevel])
        {
            characterText.text += letter;
            yield return null;
        }

        //last level and last message from assistant TO DO FOR DEVELOP
        if (GameParams.achievedLevel == 7 && GameParams.achievedLevel == GameParams.currentLevel)
        {
            messageButton.SetActive(false);
            AudioManager.Instance.messageVoiceFunc(false);
            discordButton.SetActive(true);
        }
        else
        {
            messageButton.SetActive(true);
            AudioManager.Instance.messageVoiceFunc(true);
        }
        AudioManager.Instance.assistantVoiceFunc(false);
    }
    private IEnumerator TypeAlienText()
    {
        AudioManager.Instance.alienVoiceFunc(true);
        characterText.text = "";
        foreach (char letter in GameParams.getAlienTextList()[GameParams.currentLevel])
        {
            characterText.text += letter;
            yield return null;
        }

        AudioManager.Instance.alienVoiceFunc(false);
    }

    private void setLevelparameters()
    {
        CharacterName.text = GameParams.CharactersNameTextEng[0];
        _characterRenderer.sprite = charactersAtlas.GetSprite("0");

        levelTitle.text = GameParams.getLevelName()[GameParams.currentLevel];
        levelCount.text = (GameParams.currentLevel+1).ToString();

        if (GameParams.currentLevel == GameParams.achievedLevel) nextButton.interactable = false;
        else if (!nextButton.interactable) nextButton.interactable = true;
        if (GameParams.currentLevel == 0) prevButton.interactable = false;
        else if (!prevButton.interactable) prevButton.interactable = true;

        AudioManager.Instance.alienVoiceFunc(false);
        AudioManager.Instance.assistantVoiceFunc(false);

        StopAllCoroutines();
        characterText.text = "";
        messageButton.SetActive(true);
        AudioManager.Instance.messageVoiceFunc(true);

        startButton.SetActive(false);
        discordButton.SetActive(false);
        assistantTextFinished = false;
    }

    public void nextMessage() {
        if (assistantTextFinished)
        {
            messageButton.SetActive(false);
            startButton.SetActive(true);
            if (GameParams.currentLevel < 2)
            {
                CharacterName.text = GameParams.CharactersNameTextEng[1];
                _characterRenderer.sprite = charactersAtlas.GetSprite("1");
            }
            else if (GameParams.currentLevel < 4)
            {
                CharacterName.text = GameParams.CharactersNameTextEng[2];
                _characterRenderer.sprite = charactersAtlas.GetSprite("2");
            }
            else if (GameParams.currentLevel < 6)
            {
                CharacterName.text = GameParams.CharactersNameTextEng[3];
                _characterRenderer.sprite = charactersAtlas.GetSprite("3");
            }
            else
            {
                CharacterName.text = GameParams.CharactersNameTextEng[4];
                _characterRenderer.sprite = charactersAtlas.GetSprite("4");
            }

            StopAllCoroutines();
            AudioManager.Instance.assistantVoiceFunc(false);
            StartCoroutine(TypeAlienText());
        }
        else
        {
            messageButton.SetActive(false);
            StopAllCoroutines();
            AudioManager.Instance.assistantVoiceFunc(false);
            StartCoroutine(TypeAssistantText());
        }

        AudioManager.Instance.connectionVoice();
        AudioManager.Instance.messageVoiceFunc(false);
    } 

    public void switchTheLevel(bool next) {
        if (next)
        {
            if (GameParams.currentLevel < GameParams.achievedLevel)
            {
                GameParams.currentLevel++;
                setLevelparameters();
            }
        }
        else {
            if (GameParams.currentLevel >0)
            {
                GameParams.currentLevel--;
                setLevelparameters();
            }
        }
    }

    

    public void GoToBattle() {
        SceneSwitchManager.LoadBattleScene();
    }

}
