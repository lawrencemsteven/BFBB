using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DishStation : Station
{
    [SerializeField] private GameObject sponge;
    [SerializeField] private float scale = 1;

    public randomPlateSprite randPlateSprite;
    public GameObject ready;

    private GameObject plate;
    private GameObject smudgesSpawnZone;
    private Vector3 intitialScale, initialPos;
    private Animator plateAnimator;
    private SmudgeCoordinateGenerator smudgeCoordinateGenerator;
    private ScoreAndStreakManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name != "MainScene")
        {
            return;
        }

        plate = transform.Find("Plate").gameObject;
        smudgesSpawnZone = transform.Find("Smudges").gameObject;

        plateAnimator = plate.GetComponent<Animator>();

        smudgeCoordinateGenerator = coordinateGenerator as SmudgeCoordinateGenerator;

        soundBytePlayer.SetSounds(GameInfoManager.Instance.Dish.earlySound, GameInfoManager.Instance.Dish.onTimeSound, GameInfoManager.Instance.Dish.lateSound);
        soundBytePlayer.SetPlayMode(SoundBytePlayer.PlayMode.THREE_SOUNDS);

        intitialScale = plate.transform.localScale;
        initialPos = plate.transform.position;
        Composer.Instance.onMeasure.AddListener(NewMeasure);
        scoreManager = GetComponent<ScoreAndStreakManager>();
    }

    public void NewMeasure()
    {
        if (!running)
        {
            return;
        }

        ReservoirManager.GetPlates().Add(smudgeCoordinateGenerator.CreateReservoirPlate());
        smudgeCoordinateGenerator.NewPlate();
        lineManager.DrawLine();
        PlaySwapAnimation();
    }

    protected override void pointCollision(int index)
    {
        if (lineManager.GetCurrentBeat() == index)
        {
            smudgeCoordinateGenerator.HandleCollision(index, lineManager.IsEarly());
        }
        else if (lineManager.GetCurrentBeat() > index)
        {
            smudgeCoordinateGenerator.SetSmudgeAsScrape(index);
            if (smudgeCoordinateGenerator.IsSmudgeCollided(index)) {
                scoreManager.resetStreak();
            }
        }
    }

    public void PlaySwapAnimation()
    {
        plateAnimator.Play("MovePlateOffscreen");
    }

    public void RefreshPlate()
    {
        randPlateSprite.RandomizeSprite();
        scalePlate();
        plateAnimator.Play("MovePlateOnscreen");
    }

    public void EndPlateMovement()
    { }

    private void scalePlate()
    {
        Vector3 newScale = new Vector3(intitialScale.x * scale, intitialScale.y, intitialScale.z * scale);

        plate.transform.localScale = newScale;
        plate.transform.position = new Vector3(initialPos.x, initialPos.y, initialPos.z);
    }

    public GameObject GetPlate() { return plate; }
    public GameObject GetSponge() { return sponge; }
    public GameObject GetSmudgesSpawnZone() { return smudgesSpawnZone; }
    public float GetScale() { return scale; }
    public void SetScale(float scale) { this.scale = scale; }
}