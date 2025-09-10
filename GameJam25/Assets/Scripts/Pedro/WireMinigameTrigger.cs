using UnityEngine;
using Unity.Cinemachine;
using TMPro;
using System.Collections;

public class WireMinigameTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] CinemachineCamera CinemachineCamNew;
    [SerializeField] GameObject particle;
    [SerializeField] AudioSource shockSound;
    public static WireMinigameTrigger instance;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        text.gameObject.SetActive(false);

        int minigames = PlayerPrefs.GetInt("MinigamesCompleted", 0);
        if (minigames > 0)
        {
            MinigameDone();
            return;
        }
        StartCoroutine(shockSoundLoop());
    }
    void MinigameDone()
    {
        shockSound.gameObject.SetActive(false);
        particle.SetActive(false);
        Destroy(this);
    }
    public void EnterRadius()
    {
        text.gameObject.SetActive(true);
        text.text = "<Color=yellow>E</color> - Consertar";
    }

    public void Interact()
    {
        print("Locking camera");
        CinemachineCamNew.Priority = 2;
        CountdownTimer.instance.PauseTimer(true);
        Player.instance.canMove = false;
    }
    public void Deactivate()
    {
        CinemachineCamNew.Priority = 0;

        CountdownTimer.instance.PauseTimer(false);
        Player.instance.canMove = true;
        

        MinigamesController.instance.CheckIfAllCompleted();
        StopAllCoroutines();
        shockSound.gameObject.SetActive(false);
        particle.SetActive(false);
        text.gameObject.SetActive(false);
        Destroy(this);
    }
    IEnumerator shockSoundLoop()
    {
        while (true)
        {
            shockSound.Play();
            yield return new WaitForSeconds(5f);
            shockSound.Stop();
        }
    }
    public void LeaveRadius()
    {
        text.gameObject.SetActive(false);
    }
}
