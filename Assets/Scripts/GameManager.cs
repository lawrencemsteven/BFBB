/*
 * Copyright (c) 2017 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * Notwithstanding the foregoing, you may not use, copy, modify, merge, publish, 
 * distribute, sublicense, create a derivative work, and/or sell copies of the 
 * Software in any work that is designed, intended, or marketed for pedagogical or 
 * instructional purposes related to programming, coding, application development, 
 * or information technology.  Permission for such use, copying, modification,
 * merger, publication, distribution, sublicensing, creation of derivative works, 
 * or sale is expressly withheld.
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

[System.Serializable]
public class Arena
{
    public string arenaFilePath;
    public int levelNumber;
    public string levelName;
    public float startX;
    public float startZ;
    public float targetX;
    public float targetZ;
}

public class GameManager : MonoBehaviour
{

    [Header("Arena")]
    public List<Arena> arenaList = new List<Arena>();
    public Texture2D arenaTexture;

    [Header("Arena Prefabs")]
    public GameObject floorPrefab;
    public GameObject weakFloorPrefab;
    public GameObject wallPrefab;
    public GameObject weakWallPrefab;
    public GameObject mineTilePrefab;

    [Header("Arena Objects")]
    public GameObject defaultArena;
    public GameObject arenaTiles;
    public GameObject target;
    [Space]

    private GameObject playerTank;
    public AudioSource musicPlayer;

    [Header("Game UI")]
    public GameObject loadingScreen;
    public GameObject pauseMenuCamera;
    public GameObject pauseScreen;
    public GameObject gameOverScreen;
    public GameObject winScreen;
    public string arenaName = "Starter Level";
    public Image playerAvatar;
    public Text playerName;
    public GameObject PlayerUI;

    [Header("Shape Customisation")]
    public Texture2D iconTexture;
    public Texture2D iconTreads;
    public Renderer iconRenderer;
    private Texture2D newiconTexture;
    private Vector3 defaulticonPrimary = new Vector3(580, 722, 467);
    private Vector3 defaulticonSecondary = new Vector3(718, 149, 0);
    public List<Texture2D> iconshapes;
    public List<string> iconShapeNames;
    public List<string> shapeData;
    public GameObject shapeContainer;
    public GameObject shapeObject;
    private bool shapeMenuAssembled = false;

    [Header("Tank Customisation")]
    public Texture2D tankTexture;
    public Texture2D tankTreads;
    public Renderer tankRenderer;
    private Texture2D newTankTexture;
    private Vector3 defaultTankPrimary = new Vector3(580, 722, 467);
    private Vector3 defaultTankSecondary = new Vector3(718, 149, 0);
    public List<Texture2D> tankSkins;
    public List<string> tankSkinNames;
    public GameObject skinContainer;
    public GameObject skinObject;
    private bool skinMenuAssembled = false;

    [Space]
    public int currentLevel = 1;
    private bool isPaused = false;

    public Text timerText;
    private float timer;
    public string formattedTime;

    public void UpdateTimerUI()
    {
        timer += Time.deltaTime;
        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        formattedTime = string.Format("{0:0}:{1:00}", minutes, seconds);
        timerText.text = arenaList[currentLevel - 1].levelName + " " + formattedTime;

    }

    void Start()
    {
        loadingScreen.SetActive(true);
        Time.timeScale = 0.0f;
        playerTank = GameObject.FindGameObjectWithTag("Player");
        DirectoryInfo directoryInfo = new DirectoryInfo(Application.streamingAssetsPath);
        print("Streaming Assets Path: " + Application.streamingAssetsPath);
        FileInfo[] allFiles = directoryInfo.GetFiles("*.*");
        foreach (FileInfo file in allFiles)
        {
            if (file.Name.Contains("shape"))
            {
                StartCoroutine("LoadShapeData", file);
            }
            if (file.Name.Contains("icon"))
            {
                StartCoroutine("LoadShapeImage", file);
            }
            /*
             *
            if (file.Name.Contains("player1"))
                        {
                            StartCoroutine("LoadPlayerUI", file);
                        }
                        else if (file.Name.Contains("soundtrack"))
                        {
                            StartCoroutine("LoadBackgroundMusic", file);
                        }
                        else if (file.Name.Contains("playercolor"))
                        {
                            StartCoroutine("LoadPlayerColor", file);
                        }
                        else if (file.Name.Contains("skin"))
                        {
                            StartCoroutine("LoadSkin", file);
                        }
                        else if (file.Name.Contains("Arena"))
                        {
                            StartCoroutine("LoadArena", file);
                        }
            */
           }
        /*
        if (arenaList.Count != 0)
        {
            Destroy(defaultArena);
            StartCoroutine("LoadLevel", arenaList[0]);
        }
 */
        StartCoroutine("RemoveLoadingScreen");
   }

    IEnumerator LoadShapeData(FileInfo shapeFile)
    {
        if (shapeFile.Name.Contains("meta"))
        {
            yield break;
        }
        else
        {
            string shapeFileWithoutExtension = Path.GetFileNameWithoutExtension(shapeFile.ToString());
            string[] shapeData = shapeFileWithoutExtension.Split(" "[0]);
            string shapeName = shapeData[0];
            string wwwshapePath = "file://" + shapeFile.FullName.ToString();
            string wwwshapePath = "file://" + shapeFile.FullName.ToString();

            //texture 
            WWW www = new WWW(wwwshapePath);
            yield return www;
            String newShapeData = www.text;
            shapedata.Add(newShapeData);
            iconShapeNames.Add(shapeName);
        }
    }

    IEnumerator LoadShapeImage(FileInfo shapeFile)
    {
        if (shapeFile.Name.Contains("meta"))
        {
            yield break;
        }
        else
        {
            string shapeFileWithoutExtension = Path.GetFileNameWithoutExtension(shapeFile.ToString());
            string[] shapeData = shapeFileWithoutExtension.Split(" "[0]);
            string shapeName = shapeData[0];
            string wwwshapePath = "file://" + shapeFile.FullName.ToString();

            //texture 
            WWW www = new WWW(wwwshapePath);
            yield return www;
            Texture2D newIconshape = www.texture;
            iconshapes.Add(newIconshape);
            //iconshapeNames.Add(shapeName);
        }
    }


    /*
    IEnumerator LoadPlayerUI(FileInfo playerFile)
    {
        if (playerFile.Name.Contains("meta"))
        {
            yield break;
        }
        else
        {
            string playerFileWithoutExtension = Path.GetFileNameWithoutExtension(playerFile.ToString());
            string[] playerNameData = playerFileWithoutExtension.Split(" "[0]);
            string tempPlayerName = "";
            int i = 0;
            foreach (string stringFromFileName in playerNameData)
            {
                if (i != 0)
                {
                    tempPlayerName = tempPlayerName + stringFromFileName + " ";
                }
                i++;
            }
            string wwwPlayerFilePath = "file://" + playerFile.FullName.ToString();
            WWW www = new WWW(wwwPlayerFilePath);
            yield return www;
            playerAvatar.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0.5f, 0.5f));
            playerName.text = tempPlayerName;
        }
    }

    IEnumerator LoadBackgroundMusic(FileInfo musicFile)
    {
        if (musicFile.Name.Contains("meta"))
        {
            yield break;
        }
        else
        {
            string musicFilePath = musicFile.FullName.ToString();
            string url = string.Format("file://{0}", musicFilePath);
            WWW www = new WWW(url);
            yield return www;
            musicPlayer.clip = www.GetAudioClip(false, false);
            musicPlayer.Play();
        }
    }

    IEnumerator LoadPlayerColor(FileInfo colorFile)
    {
        if (colorFile.Name.Contains("meta"))
        {
            yield break;
        }
        else
        {
            string wwwColorPath = "file://" + colorFile.FullName.ToString();
            WWW www = new WWW(wwwColorPath);
            yield return www;
            Texture2D playerColorTexture = www.texture;
            Color primaryColor = playerColorTexture.GetPixel(5, 5);
            Color secondaryColor = playerColorTexture.GetPixel(15, 5);
            Color[] currentPixelColors = tankTexture.GetPixels();
            Color[] newPixelColors = new Color[currentPixelColors.Length];
            float percentageDifferenceAllowed = 0.05f;
            int i = 0;
            foreach (Color color in currentPixelColors)
            {
                Vector3 colorToTest = new Vector3((Mathf.RoundToInt(color.r * 1000)), (Mathf.RoundToInt(color.g * 1000)), (Mathf.RoundToInt(color.b * 1000)));
                if ((colorToTest - defaultTankPrimary).sqrMagnitude <= (colorToTest * percentageDifferenceAllowed).sqrMagnitude)
                {
                    newPixelColors.SetValue(primaryColor, i);
                }
                else if ((colorToTest - defaultTankSecondary).sqrMagnitude <= (colorToTest * percentageDifferenceAllowed).sqrMagnitude)
                {
                    newPixelColors.SetValue(secondaryColor, i);
                }
                else
                {
                    newPixelColors.SetValue(color, i);
                }
                i++;
            }
            newTankTexture = new Texture2D(tankTexture.width, tankTexture.height);
            newTankTexture.SetPixels(newPixelColors);
            newTankTexture.Apply();
            ApplyTextureToTank(tankRenderer, newTankTexture);
        }
    }
    public void ApplyTextureToTank(Renderer tankRenderer, Texture2D textureToApply)
    {
        Renderer[] childRenderers = tankRenderer.gameObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in childRenderers)
        {
            renderer.material.mainTexture = textureToApply;
        }
        tankRenderer.materials[1].mainTexture = textureToApply;
        tankRenderer.materials[0].mainTexture = tankTreads;
    }*/
    /*
    IEnumerator LoadSkin(FileInfo skinFile)
    {
        if (skinFile.Name.Contains("meta"))
        {
            yield break;
        }
        else
        {
            string skinFileWithoutExtension = Path.GetFileNameWithoutExtension(skinFile.ToString());
            string[] skinData = skinFileWithoutExtension.Split(" "[0]);
            string skinName = skinData[0];            
            string wwwSkinPath = "file://" + skinFile.FullName.ToString();
            WWW www = new WWW(wwwSkinPath);
            yield return www;
            Texture2D newTankSkin = www.texture;
            tankSkins.Add(newTankSkin);
            tankSkinNames.Add(skinName);
        }
    }

    IEnumerator LoadLevel(Arena arenaToLoad)
    {
        arenaName = arenaToLoad.levelName;
        loadingScreen.SetActive(true);
        gameOverScreen.SetActive(false);
        winScreen.SetActive(false);
        foreach (Transform child in arenaTiles.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        WWW www = new WWW(arenaToLoad.arenaFilePath);
        yield return www;
        arenaTexture = www.texture;
        Color[] arenaData = arenaTexture.GetPixels();
        int x = 0;
        foreach (Color color in arenaData)
        {
            int xPosition = ((x + 1) % 100);
            if (xPosition == 0)
            {
                xPosition = 100;
            }
            int zPosition = (x / 100) + 1;
            if (color.a < 0.1f)
            {
                GameObject.Instantiate(floorPrefab, new Vector3(xPosition / 1.0f, 0.0f, zPosition / 1.0f), Quaternion.Euler(90, 0, 0), arenaTiles.transform);
            }
            else
            {
                if (color.r > 0.9f && color.g > 0.9f && color.b < 0.1f)
                {
                }
                else if (color.r > 0.9f && color.g < 0.1f && color.b < 0.1f)
                {
                    GameObject.Instantiate(mineTilePrefab, new Vector3(xPosition / 1.0f, 0.0f, zPosition / 1.0f), Quaternion.identity, arenaTiles.transform);
                }
                else if (color.r < 0.1f && color.g > 0.9f && color.b < 0.1f)
                {
                    GameObject.Instantiate(weakWallPrefab, new Vector3(xPosition / 1.0f, 0.0f, zPosition / 1.0f), Quaternion.identity, arenaTiles.transform);
                }
                else if (color.r < 0.1f && color.g < 0.1f && color.b > 0.9f)
                {
                    GameObject.Instantiate(weakFloorPrefab, new Vector3(xPosition / 1.0f, 0.0f, zPosition / 1.0f), Quaternion.identity, arenaTiles.transform);
                }
                else
                {
                    GameObject.Instantiate(wallPrefab, new Vector3(xPosition / 1.0f, 0.0f, zPosition / 1.0f), Quaternion.identity, arenaTiles.transform);
                }
            }
            x++;
        }
        StartCoroutine("RemoveLoadingScreen");
        Time.timeScale = 1.0f;
        playerTank.transform.position = new Vector3(arenaToLoad.startX / 1.0f, 1.0f, (100 - arenaToLoad.startZ) / 1.0f);
        playerTank.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        target.transform.position = new Vector3(arenaToLoad.targetX / 1.0f, 0.6f, (100 - arenaToLoad.targetZ) / 1.0f);
    }
    */

    IEnumerator RemoveLoadingScreen()
    {
        yield return new WaitForSecondsRealtime(1);
        loadingScreen.SetActive(false);
        timer = 0.0f;
        Time.timeScale = 1.0f;
        if (!skinMenuAssembled)
        {
            StartCoroutine("AssembleSkinMenu");
        }
    }

    IEnumerator AssembleshapeMenu()
    {
        shapeMenuAssembled = true;
        int i = 0;
        foreach (Texture2D shapeTexture in iconshapes)
        {
            GameObject currentshapeObject = Instantiate(shapeObject, new Vector3(0, 0, 0), Quaternion.identity, shapeContainer.transform);
            currentshapeObject.transform.localPosition = new Vector3(100 + (200 * i), -80, 0);
            shapeManager currentshapeManager = currentshapeObject.GetComponent<shapeManager>();
            currentshapeManager.Configureshape(iconshapeNames[i], i);
            ApplyTextureToicon(currentshapeManager.iconRenderer, iconshapes[i]);
            i++;
        }
        yield return null;
    }

/*
    IEnumerator AssembleSkinMenu()
    {
        skinMenuAssembled = true;
        int i = 0;    
        foreach (Texture2D skinTexture in tankSkins)
        {
            GameObject currentSkinObject = Instantiate(skinObject, new Vector3(0, 0, 0), Quaternion.identity, skinContainer.transform);        
            currentSkinObject.transform.localPosition = new Vector3(100 + (200 * i), -80, 0);
            SkinManager currentSkinManager = currentSkinObject.GetComponent<SkinManager>();
            currentSkinManager.ConfigureSkin(tankSkinNames[i], i);
            ApplyTextureToTank(currentSkinManager.tankRenderer, tankSkins[i]);
            i++;
        }
        yield return null;
    }

    IEnumerator LoadArena(FileInfo arenaFile)
    {
        if (arenaFile.Name.Contains(".meta"))
        {
            yield break;
        }
        else
        {
            Arena arenaInstance = new Arena();

            string arenaFileWithoutExtension = Path.GetFileNameWithoutExtension(arenaFile.ToString());
            string[] arenaDataArray = arenaFileWithoutExtension.Split(" "[0]);
            arenaInstance.startX = int.Parse(arenaDataArray[1]);
            arenaInstance.startZ = int.Parse(arenaDataArray[2]);
            arenaInstance.targetX = int.Parse(arenaDataArray[3]);
            arenaInstance.targetZ = int.Parse(arenaDataArray[4]);
            string levelName = "";
            if (arenaDataArray.Length <= 5)
            {
                if (arenaList.Count != 0)
                {
                    levelName = "Level " + (arenaList.Count + 1);
                }
                else
                {
                    levelName = "Level 1";
                }
            }
            else
            {
                int i = 0;
                foreach (string stringFromDataArray in arenaDataArray)
                {
                    if (i > 4)
                    {
                        levelName = levelName + stringFromDataArray + " ";
                    }
                    i++;
                }
            }
            arenaInstance.levelName = levelName;
            arenaInstance.arenaFilePath = "file://" + arenaFile.FullName.ToString();
            arenaList.Add(arenaInstance);
        }
    }
*/


    void Update()
    {
        UpdateTimerUI();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                PlayerUI.SetActive(true);
                pauseScreen.SetActive(false);
                pauseMenuCamera.SetActive(false);
                isPaused = false;
                Time.timeScale = 1.0f;
            }
            else
            {
                PlayerUI.SetActive(false);
                pauseScreen.SetActive(true);
                pauseMenuCamera.SetActive(true);
                isPaused = true;
                Time.timeScale = 0.0f;
            }
        }
    }

    public void RestartLevel()
    {
        if (arenaList.Count != 0)
        {
            StartCoroutine("LoadLevel", arenaList[currentLevel - 1]);
        }
        else
        {
            SceneManager.LoadScene("MainScene");
        }

        Time.timeScale = 1.0f;
        Rigidbody playerRB = playerTank.GetComponent<Rigidbody>();
        playerRB.isKinematic = true;
        playerRB.isKinematic = false;
    }


    public void StartNextLevel()
    {
        if (arenaList.Count >= currentLevel + 1)
        {
            currentLevel++;
            StartCoroutine("LoadLevel", arenaList[currentLevel - 1]);
        }
        else
        {
            SceneManager.LoadScene("MainScene");
        }
        Rigidbody playerRB = playerTank.GetComponent<Rigidbody>();
        playerRB.isKinematic = true;
        playerRB.isKinematic = false;

    }

    public void ApplySkin(int indexOfSkin)
    {
        ApplyTextureToTank(tankRenderer, tankSkins[indexOfSkin]);
        PlayerUI.SetActive(true);
        pauseMenuCamera.SetActive(false);
        isPaused = false;
        Time.timeScale = 1.0f;

    }

}
