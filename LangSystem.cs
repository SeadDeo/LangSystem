using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;
using UnityEngine.UI;

public class LangSystem : MonoBehaviour
{
    private string lang;
    private string json;
    public Text[] gameMenu = new Text[4];
    public Text[] gameOptions = new Text[3];
    public Toggle toggleENG;
    public Toggle toggleRUS;
    
    public static lang lng = new lang();
    int activLang;
    
    void Start()
    {
       if (!PlayerPrefs.HasKey("Languages"))//проверка на файл с уже выбранным языком
        {
            if (Application.systemLanguage == SystemLanguage.Russian)//проверка на язык в системе 
            {                
                PlayerPrefs.SetString("Languages", "ru_RU");
                LangLoadRus();
            }
            else
            {
                PlayerPrefs.SetString("Languages", "eng_ENG");
                LangLoadEng();
            }         
        }
        LangLoad();//загрузка языка
    }
    private void LateUpdate()//МЕТОД ОТВЕЧАЮЩИЙ ЗА ЗАГРУСКУ ИЗМЕНЁНОГО ЯЗЫКА (КОСТЫЛЬ, НУЖНО СДЕЛАТЬ ЧЕРЕЗ КЛЮЧ)
    {
        ActivLang();
    }
    void LangLoad()//МЕТОД ДЛЯ ПРИСВАЕНИЯ ФАЙЛА (JSON) В ПЕРЕМЕНУЮ 
    {
        json = File.ReadAllText(Application.streamingAssetsPath + "/Languages/" + PlayerPrefs.GetString("Languages") + ".json");
        lng = JsonUtility.FromJson<lang>(json);
        Debug.Log(lng.menu[0]);
    }
    public void LangLoadEng()
    {
        PlayerPrefs.SetString("Languages", "eng_ENG");
        LangLoad();
        for (int i = 0; i < gameMenu.Length; i++)       gameMenu[i].text = lng.menu[i]; 
        for (int i = 0; i < gameOptions.Length; i++)    gameOptions[i].text = lng.options[i];
    }
    public void LangLoadRus()
    {
        PlayerPrefs.SetString("Languages", "ru_RU");
        LangLoad();
        for (int i = 0; i < gameMenu.Length; i++)     gameMenu[i].text = lng.menu[i];
        for (int i = 0; i < gameOptions.Length; i++)  gameOptions[i].text = lng.options[i];
    }
    public int ActivLang()//МЕТОД ИЗМЕННИЯ ЯЗЫКА ПО ВЫБОРУ
    {
        if (toggleENG.isOn) 
        {
            activLang = 1; 
            LangLoadEng(); 
            return activLang;
        }         
        else
        { 
            activLang = 0;
            LangLoadRus();
            return activLang; 
        } 
    }
   public void SetToggleEng(int activLang)
    {
        PlayerPrefs.SetString("toggleENG", (activLang == 1).ToString());
    }
    public void SetToggleRus(int activLang)
    {
        PlayerPrefs.SetString("toggleRUS", (activLang == 0).ToString());
    }

}
public class lang//class ОТВЕЧАЮЩИЙ ЗА ФАЙЛЫ С ПЕРЕВОДОМ
{
    public string[] menu = new string[4]; //массив с тестом для менюшки
    public string[] options = new string[3];//массив с тестом для опций(ещё нету)
}