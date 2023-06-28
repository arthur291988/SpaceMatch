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



    //index is level
    public static List<string> alienTextEng = new List<string> {
        "--Kneel before the troops of Emperor Seres\r\n--I am Lord Filsh and you will be destroyed by my vanguard.\r\n--Say goodbye to life\r\n",
        "--You were able to defeat a squad of weaklings\r\n--I will take revenge on you by destroying this planet\r\n--You will all burn\r\n",
        "--Well well, you were able to defeat the insignificant Filsh\r\n--I'm Lord Zerf and you can't compete with me\r\n--Marauders crush these ants and plunder this planet\r\n",
        "--Coming to my station you signed your own death warrant\r\n--Prepare for death\r\n--And then I will destroy this planet\r\n",
        "--I can't believe Filsh and my brother Zerf are destroyed.\r\n--I have sent my elite squad to destroy your red planet and I will go to your main planet myself\r\n--Now you are definitely finished, Emperor Seres will be pleased\r\n",
        "--Is my squad destroyed?!\r\n--In the name of the Emperor, I will wipe you all into stardust\r\n--May the strongest win!\r\n",
        "--Nothingness!\r\n--How dare you stand against my troops\r\n--From the very beginning, our goal was your star, I plan to drain all its energy\r\n--And you will die like insects\r\n--Death to you!\r\n"

    };

    public static List<string> levelNameTextEng = new List<string> {
        "Battle on the border",
        "Protection of Neptune",
        "Marauders on Saturn",
        "Enemy station on Jupiter",
        "Attack on Mars",
        "The Battle for Earth",
        "Emperor at the Sun"
    };

    public static List<string> AlienNameTextEng = new List<string> {
        "Lord Filsh",
        "Lord Zerf",
        "Admiral Zerbo",
        "Emperor Seres"
    };

    public static List <string> getAlienTextList() {
        if (language == 0) return alienTextEng;
        else return null;
    }
    public static List<string> getLevelName()
    {
        if (language == 0) return levelNameTextEng;
        else return null;
    }

    public static string getLevelWord() {
        if (language == 0) levelWord = "Level";
        else levelWord = "�������";

        return levelWord;
    }

    public static string getStartWord () { 
        if (language == 0) startWord = "Start";
        else startWord = "�����";

        return startWord;
    }
    public static List<string> getAlienName()
    {
        if (language == 0) return AlienNameTextEng;
        else return null;
    }
}