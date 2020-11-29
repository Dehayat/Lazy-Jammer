using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour
{
    [System.Serializable]
    public struct SoundData
    {
        public string name;
        public GameObject prefab;
        public KeyCode key;
        public bool isMouse;
        public float baseScore;
        public float coolDown;
    }

    [System.Serializable]
    public struct LevelData
    {
        public float scoreLostPerSecond;
        public float maxScore;
        public AnimationCurve distanceMultiplier;
        public AnimationCurve repeatMultiplier;
        public int lookBackRepeat;
    }
    public event Action<int> OnLevelEnded;
    public event Action<int> OnLevelStarted;

    public Text scoreText;
    public Slider scoreSlider;
    public GameObject[] SleepEffects;
    public GameObject[] WinEffects;

    public int maxComboDepth = 10;
    public float comboWaitTime = 0.3f;

    public bool isPlaying = true;

    public SoundData[] Sounds;
    public LevelData[] Levels;
    public Combo[] combos;

    private float score = 0;
    private Vector3 lastSoundPosition = new Vector3(-100, -100);
    private LinkedList<string> last5Sounds = new LinkedList<string>();
    private float[] lastTimeSoundPlayed;
    public int currentLevel = 0;

    private float lastButtonPressedAt = 0;
    private LinkedList<string> comboSounds = new LinkedList<string>();
    private Dictionary<string, char> soundHash = new Dictionary<string, char>();

    private void Start()
    {
        lastTimeSoundPlayed = new float[Sounds.Length];
        for (int i = 0; i < lastTimeSoundPlayed.Length; i++)
        {
            soundHash[Sounds[i].name] = (char)i;
            lastTimeSoundPlayed[i] = -100f;
        }
        foreach (var combo in combos)
        {
            string hash = "";
            for (int i = 0; i < combo.sequence.Length; i++)
            {
                hash += soundHash[combo.sequence[i]];
            }
            combo.comboHash = hash;
        }
        OnLevelEnded += MusicPlayer_OnLevelEnded;
        OnLevelStarted += MusicPlayer_OnLevelStarted;
    }

    private void MusicPlayer_OnLevelStarted(int level)
    {
        foreach (var effect in SleepEffects)
        {
            effect.SetActive(true);
        }
    }

    private void MusicPlayer_OnLevelEnded(int level)
    {
        foreach (var effect in SleepEffects)
        {
            effect.SetActive(false);
        }

        foreach (var effect in WinEffects)
        {
            effect.SetActive(true);
        }
    }

    private void Update()
    {
        if (!isPlaying && currentLevel < Levels.Length) return;
        int i = 0;

        float finalMuliplier = 1f;
        float baseScore = 0;
        foreach (var sound in Sounds)
        {
            if ((Input.GetKeyDown(sound.key) || Input.GetMouseButtonDown(0) && sound.isMouse) && Time.time > lastTimeSoundPlayed[i] + sound.coolDown)
            {
                Vector3 soundPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                soundPosition.z = 0;
                Instantiate(sound.prefab, soundPosition, Quaternion.identity);
                float distanceMulti = Levels[currentLevel].distanceMultiplier.Evaluate(Vector3.Distance(lastSoundPosition, soundPosition) / 3);
                float repeatsMulti = Levels[currentLevel].repeatMultiplier.Evaluate(GetRepeats(sound.name));
                Debug.Log(distanceMulti + " / " + repeatsMulti);
                baseScore = sound.baseScore;
                finalMuliplier *= distanceMulti * repeatsMulti;
                lastTimeSoundPlayed[i] = Time.time;
                lastSoundPosition = soundPosition;
                comboSounds.AddLast(sound.name);
                lastButtonPressedAt = Time.time;
                break;
            }
            i++;
        }

        score += baseScore * finalMuliplier;
        score += CalculateCombos() * Mathf.Clamp(finalMuliplier, 0.5f, 1);

        score = Mathf.Max(0, score);
        score = Mathf.Min(score, Levels[currentLevel].maxScore);
        scoreText.text = score.ToString("0.0");
        scoreSlider.value = score / Levels[currentLevel].maxScore;
        if (score == Levels[currentLevel].maxScore)
        {
            isPlaying = false;
            OnLevelEnded?.Invoke(currentLevel);
        }
        score -= Levels[currentLevel].scoreLostPerSecond * Time.deltaTime;
    }

    private float CalculateCombos()
    {
        if (lastButtonPressedAt + comboWaitTime < Time.time)
        {
            comboSounds.Clear();
            return 0f;
        }
        if (comboSounds.Count > maxComboDepth)
        {
            comboSounds.RemoveFirst();
        }
        string sequence = "";
        foreach (var item in comboSounds)
        {
            sequence += soundHash[item];
        }
        foreach (var combo in combos)
        {
            if (sequence.Contains(combo.comboHash))
            {
                Debug.Log("Found");
                comboSounds.Clear();
                return combo.score;
            }
        }
        return 0f;
    }

    private int GetRepeats(string name)
    {
        int repeats = 0;
        int depth = 0;
        foreach (var sound in last5Sounds)
        {
            if (depth >= Levels[currentLevel].lookBackRepeat) break;
            if (sound == name) repeats++;
            depth++;
        }
        last5Sounds.AddFirst(name);
        if (last5Sounds.Count > 5)
        {
            last5Sounds.RemoveLast();
        }
        return repeats;
    }

    public void NextLevel()
    {
        currentLevel++;
        score = 0;
        lastSoundPosition = new Vector3(-100, -100);
        isPlaying = true;
        OnLevelStarted?.Invoke(currentLevel);
    }
    public void StartGame()
    {
        isPlaying = true;
        OnLevelStarted?.Invoke(currentLevel);
    }
}
