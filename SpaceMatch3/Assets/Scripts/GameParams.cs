using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameParams 
{
    public static int achievedLevel;
    public static int currentLevel;
    //0-eng, 1-Rus
    public static int language;

    private static string levelWord;
    private static string startWord;

    private static string endGameWord;
    private static string loadingWord;


    //index is level
    public static List<string> alienTextEng = new List<string> {
        "--Kneel before the troops of Emperor Seres\r\n--I am Lord Filsh and you will be destroyed by my vanguard.\r\n--Say goodbye to life\r\n",
        "--You were able to defeat a squad of weaklings\r\n--I will take revenge on you by destroying this planet\r\n--You will all burn\r\n",
        "--Well well, you were able to defeat the insignificant Filsh\r\n--I'm Lord Zerf and you can't compete with me\r\n--Marauders crush these ants and plunder this planet\r\n",
        "--Coming to my station you signed your own death warrant\r\n--Prepare for death\r\n--And then I will destroy this planet\r\n",
        "--I can't believe Filsh and my brother Zerf are destroyed\r\n--I have sent my elite squad to destroy your red planet and I will go to your main planet myself\r\n--Now you are definitely finished, Emperor Seres will be pleased\r\n",
        "--Is my squad destroyed?!\r\n--In the name of the Emperor, I will wipe you all into stardust\r\n--May the strongest win!\r\n",
        "--Nothingness!\r\n--How dare you stand against my troops\r\n--From the very beginning, our goal was your star, I plan to drain all its energy\r\n--And you will die like insects\r\n--Death to you!\r\n"

    };

    //index is level
    public static List<string> assistantTextEng = new List<string> {
        "--Greetings Captain of the Solar Federation\r\n--We have received a disturbing message from the borders of the Federation, we were attacked by hostile aliens\r\n" +
        "--You must fly to the boundary sectors and fight off the first wave of attack\r\n--Good luck Captain!\r\n",

        "--Assistant text\r\n--I will take revenge on you by destroying this planet\r\n--You will all burn\r\n",

        "--Assistant text\r\n--I'm Lord Zerf and you can't compete with me\r\n--Marauders crush these ants and plunder this planet\r\n",

        "--Assistant text\r\n--Prepare for death\r\n--And then I will destroy this planet\r\n",

        "--Assistant text\r\n--I have sent my elite squad to destroy your red planet and I will go to your main planet myself\r\n--Now you are definitely finished, Emperor Seres will be pleased\r\n",

        "--Assistant text\r\n--In the name of the Emperor, I will wipe you all into stardust\r\n--May the strongest win!\r\n",

        "--Assistant text\r\n--How dare you stand against my troops\r\n--From the very beginning, our goal was your star, I plan to drain all its energy\r\n--And you will die like insects\r\n--Death to you!\r\n",

        "--Congratulations captain, we repelled the alien attack\r\n--We have proven that the Federation is ready to develop beyond the boundaries of the solar system\r\n--Very soon new tasks will be set before you\r\n--We invite you to step into the headquarters of the command\r\n"
    };



    public static List<string> levelNameTextEng = new List<string> {
        "Battle on the border",
        "Protection of Neptune",
        "Marauders on Saturn",
        "Enemy station on Jupiter",
        "Attack on Mars",
        "The Battle for Earth",
        "Emperor at the Sun",
        "New challenges soon"
    };

    public static List<string> CharactersNameTextEng = new List<string> {
        "Assistant",
        "Lord Filsh",
        "Lord Zerf",
        "Admiral Zerbo",
        "Emperor Seres"
    };

    public static List <string> getAlienTextList() {
        if (language == 0) return alienTextEng;
        else return null;
    }
    public static List<string> getAssistantTextList()
    {
        if (language == 0) return assistantTextEng;
        else return null;
    }
    public static List<string> getLevelName()
    {
        if (language == 0) return levelNameTextEng;
        else return null;
    }

    public static string getLevelWord() {
        if (language == 0) levelWord = "Level";
        else levelWord = "Уровень";

        return levelWord;
    }

    public static string getStartWord () { 
        if (language == 0) startWord = "Start";
        else startWord = "Старт";

        return startWord;
    }

    public static string getDefeatWord() {
        if (language == 0) endGameWord = "Defeat";
        else endGameWord = "Поражение";

        return endGameWord;
    }
    public static string getVictoryWord()
    {
        if (language == 0) endGameWord = "Victory";
        else endGameWord = "Победа";

        return endGameWord;
    }

    public static string getLoadingWord()
    {
        if (language == 0) loadingWord = "Loading";
        else loadingWord = "Загрузка";

        return loadingWord;
    }

    public static List<string> getCharacterName()
    {
        if (language == 0) return CharactersNameTextEng;
        else return null;
    }
}
