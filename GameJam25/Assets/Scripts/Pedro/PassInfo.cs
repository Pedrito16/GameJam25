using UnityEngine;

public class PassInfo : MonoBehaviour
{
    public Vector2 lastSavedPos;
    public static PassInfo instance;
    public int totalScore;
    public int[] scoreTypes; //tem que fazer eles passar pro final e aparecer nos textos
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    public void LoadPlayerPos()
    {
        Invoke("Load", 0.1f);
    }
    void Load()
    {
        print("loading");
        Player.instance.LoadPosition(lastSavedPos);
    }
}
