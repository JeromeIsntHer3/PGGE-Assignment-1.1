using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [Header ("Audio Clips")]
    [SerializeField]
    private AudioClip[] dirtClips;
    [SerializeField]
    private AudioClip[] grassClips;
    [SerializeField]
    private AudioClip[] sandClips;

    [Header("Pitch and Volume Randomizer Multiplier")]
    [Range (0.1f,0.5f)]
    public float volumeMultiplier = 0.2f;
    [Range(0.1f, 0.5f)]
    public float pitchMultiplier = 0.2f;

    private AudioSource audioSource;
    private TerrainDetector terrainDetector;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        terrainDetector = new TerrainDetector();
    }

    private void Step()
    {
        audioSource.clip = GetRandomClip();
        audioSource.volume = Random.Range(1 - volumeMultiplier, 1);
        audioSource.pitch = Random.Range(1 - pitchMultiplier, 1 + pitchMultiplier);
        audioSource.PlayOneShot(audioSource.clip);
    }

    private AudioClip GetRandomClip()
    {
        int terrainTextureIndex = terrainDetector.GetActiveTerrainTextureIdx(transform.position);

        switch (terrainTextureIndex)
        {
            case 0:
                return dirtClips[UnityEngine.Random.Range(0, dirtClips.Length)];
            case 1:
                return grassClips[UnityEngine.Random.Range(0, grassClips.Length)];
            case 2:
            default:
                return sandClips[UnityEngine.Random.Range(0, sandClips.Length)];
        }
    }
}