using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        plate = transform.Find("Plate").gameObject;
        smudgesSpawnZone = transform.Find("Smudges").gameObject;

        plateAnimator = plate.GetComponent<Animator>();

        smudgeCoordinateGenerator = coordinateGenerator as SmudgeCoordinateGenerator;

        soundBytePlayer.SetSounds("EarlyDish", "HiHat", "LateDish");
        soundBytePlayer.SetPlayMode(SoundBytePlayer.PlayMode.THREE_SOUNDS);

        intitialScale = plate.transform.localScale;
        initialPos = plate.transform.position;
        SongInfo.Instance.onMeasure.AddListener(NewMeasure);
    }

    public void NewMeasure()
    {
        smudgeCoordinateGenerator.NewPlate();
        lineManager.DrawLine();
        PlaySwapAnimation();                
        ReservoirManager.GetPlates().Add(smudgeCoordinateGenerator.CreateReservoirPlate());
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