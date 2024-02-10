using System.Collections.Generic;
using UnityEngine;

public class PourPancakeParticles : MonoBehaviour
{
    [SerializeField] private GameObject particle;
    [SerializeField] private float spawnDistance;
    [SerializeField] private float spawnCooldown;
    private float currentSpawnCooldown;
    private GameObject pancakeCanvas;
    private Pancake pancake;
    private List<GameObject> storedPancakes;
    
    [SerializeField] private Color rawColor;
    [SerializeField] private Color burntColor;

    public void Start()
    {
        createEmptyPancakeCanvas();
    }

    public void Update()
    {
        if (currentSpawnCooldown > 0)
        {
            currentSpawnCooldown -= Time.deltaTime;
        }

        if (Input.GetMouseButton(0))
        {
            if (currentSpawnCooldown <= 0)
            {
                Ray rayCast = Camera.main.ScreenPointToRay(Input.mousePosition);
                GameObject spawnedParticle = Instantiate(particle, rayCast.GetPoint(spawnDistance), Quaternion.identity);
                pancake.AddToParticles(spawnedParticle);
                currentSpawnCooldown = spawnCooldown;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            pancake.ToggleCooking();
        }

        if (pancake.IsCooking())
        {
            pancake.UpdateSideColors(rawColor, burntColor);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            pancake.Flip();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Instantiate(pancakeCanvas, new Vector3(10, 0, 0), Quaternion.identity);
            Destroy(pancakeCanvas);
            createEmptyPancakeCanvas();
        }
    }

    private void createEmptyPancakeCanvas()
    {
        pancakeCanvas = new GameObject();
        pancake = pancakeCanvas.AddComponent<Pancake>();
    }
}