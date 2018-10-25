using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameCore.SystemControls;
using UnityEngine.UI;
using GameCore.MemorySystem;
using GameCore.GameTime;


public abstract class MenuController : MonoBehaviour {

    [SerializeField]
    protected string gameScene;
    [SerializeField]
    protected string titleScene;
    [SerializeField]
    protected GameObject pauseMenu;
    [SerializeField]
    protected GameObject deadScreenGO;
    protected bool isPaused;

    protected int menuSize;
    protected int index;

    [SerializeField]
    protected Button[] menuOptions;
    protected bool returnedToCenter;

    protected bool canPause;

    public static bool deadScreen;
    private TimeManager tm;
    private bool initialDeadCount;
    private bool deadScreenCount;
    private bool finalDeadCount;

    [SerializeField]
    protected float initialDeadTime;
    [SerializeField]
    protected float deadScreenTime;
    [SerializeField]
    protected float finalDeadTime;

    private Image blackScreen;
    private Text redText;
    

    // Use this for initialization
    void Awake () {
        menuSize = menuOptions.Length;
        //index = 0;
        setPause();
        //RefreshMenu(index);
        //returnedToCenter = true;
        deadScreen = false;
        tm = new TimeManager(0f);
        initialDeadCount = false;
        finalDeadCount = false;
        deadScreenCount = false;
        blackScreen = deadScreenGO.GetComponent<Image>();
        redText = deadScreenGO.GetComponentInChildren<Text>();
    }

    private void Start()
    {
        if (!canPause)
        {
            for(int i = 0; i < menuOptions.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        menuOptions[i].onClick.AddListener(NewGame);
                        break;
                    case 1:
                        menuOptions[i].onClick.AddListener(LoadGame);
                        break;
                    case 2:
                        menuOptions[i].onClick.AddListener(ExitGame);
                        break;

                }
            }
        }
        else
        {
            for (int i = 0; i < menuOptions.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        menuOptions[i].onClick.AddListener(ResumeGame);
                        break;
                    case 1:
                        menuOptions[i].onClick.AddListener(ReturnToTitle);
                        break;
                }
            }
        }
    }

    // Update is called once per frame
    void Update () {
        if (Controllers.GetButton(1, "Start", 1) & canPause)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                isPaused = true;
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
                //RefreshMenu(index);
            }
        }
        /*
        if ((isPaused & canPause) || !canPause)
        {
            //print(returnedToCenter);
            //print(Controllers.GetJoystick(1, 1));
            //Controllers.GetJoystick(1, 1);
            if (returnedToCenter)
            {
                if (Controllers.GetJoystick(1, 1).y > 0 & index != 0)
                {
                    returnedToCenter = false;
                    index--;
                }
                else if (Controllers.GetJoystick(1, 1).y < 0 & index < menuSize - 1)
                {
                    returnedToCenter = false;
                    index++;
                }
                //RefreshMenu(index);
            }
            else
            {
                returnedToCenter = Controllers.JoystickReturnedCenter(1, 1);
            }
            

            if (Controllers.GetButton(1, "A", 2) & canPause)
            {
                switch (index)
                {
                    case 0:
                        ResumeGame();
                        break;
                    case 1:
                        ReturnToTitle();
                        break;
                }
            }
            else if (Controllers.GetButton(1, "A", 2) & !canPause)
            {
                switch (index)
                {
                    case 0:
                        NewGame();
                        break;
                    case 1:
                        LoadGame();
                        break;
                    case 2:
                        ExitGame();
                        break;
                }
            }
        }
        */
        /*
        if (Controllers.GetButton(1, "Y", 1))
        {
            deadScreen = true;
        }
        */


        if (deadScreen || initialDeadCount || finalDeadCount || deadScreenCount)
        {
            if (deadScreen)
            {
                deadScreen = false;
                initialDeadCount = true;
                tm.StartTime(initialDeadTime);
                deadScreenGO.SetActive(true);
                blackScreen.color = new Color(1f, 1f, 1f, 0f);
                redText.color = new Color(170f / 255f, 0f, 0f, 0f);
            }
            else if (initialDeadCount)
            {
                //advance deadScreen start
                FadeIn();

                if (tm.IsTimeOverNoUpdate())
                {
                    initialDeadCount = false;
                    deadScreenCount = true;
                    tm.StartTime(deadScreenTime);
                }
            }
            else if (deadScreenCount)
            {
                if (tm.IsTimeOverNoUpdate())
                {
                    deadScreenCount = false;
                    finalDeadCount = true;
                    tm.StartTime(finalDeadTime);
                }
            }
            else if (finalDeadCount)
            {
                // advance deadScreen end
                FadeOut();

                if (tm.IsTimeOverNoUpdate())
                {
                    finalDeadCount = false;
                    SceneManager.LoadScene(titleScene);
                }
            }

            tm.AdvanceTime();
        }

    }

    /*
    private void RefreshMenu(int index)
    {
        for (int i = 0; i < menuSize; i++)
        {
            menuOptions[i].color = i == index ? new Color(183f / 255f, 183f / 255f, 183f / 255f)
                : new Color(1, 1, 1);
            
        }
    }
    */
    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ReturnToTitle()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(titleScene);
    }

    public void NewGame()
    {
        print("NewGamer");
        SceneManager.LoadScene(gameScene);
        print("NewGamer2");
    }

    public void LoadGame()
    {
        print("LoadGamer");
        MemorySystem.loadGame = true;
        SceneManager.LoadScene(gameScene);
    }

    public void ExitGame()
    {
        print("_ExitGamer");
        Application.Quit();
    }

    protected virtual void setPause()
    {

    }

    private void FadeIn()
    {
        blackScreen.color = new Color(0f,0f,0f,
            blackScreen.color.a + (Time.deltaTime / initialDeadTime) * 100f/255f);
        redText.color = new Color(170f/255f, 0f,0f, 
            redText.color.a + (Time.deltaTime / initialDeadTime) );
    }

    private void FadeOut()
    {
        blackScreen.color = new Color(0f, 0f, 0f,
           blackScreen.color.a + (Time.deltaTime / initialDeadTime) * 155f/255f);
        redText.color = new Color(redText.color.r - (Time.deltaTime / initialDeadTime) * 170f/255f
            , 0f, 0f);
    }

}
