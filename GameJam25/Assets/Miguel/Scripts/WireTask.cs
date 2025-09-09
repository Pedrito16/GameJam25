using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class WireTask : MonoBehaviour
{
    [System.Serializable]
    public class WireConnection
    {
        public Wire wire;
        public WireSocket correctSocket;
        [HideInInspector] public bool isConnected = false;
    }

    [Header("Configuração dos Fios")]
    public List<WireConnection> wireConnections = new List<WireConnection>();
    public float connectionDistance = 0.5f;

    [Header("Cores dos Fios")]
    public Color[] wireColors = {
        Color.red, Color.blue, Color.yellow, Color.green,
        Color.magenta, Color.cyan, Color.white, Color.black
    };

    [Header("Eventos")]
    public UnityEvent onTaskStarted;
    public UnityEvent onWireConnected;
    public UnityEvent onTaskCompleted;

    [Header("Sons (Opcional)")]
    public AudioClip wireConnectSound;
    public AudioClip taskCompleteSound;
    public AudioSource audioSource;

    private bool isTaskActive = false;
    private Wire currentlyDraggingWire = null;
    private int connectedWiresCount = 0;

    void Start()
    {
        InitializeWires();
        SetupAudio();
    }

    void Update()
    {
        if (!isTaskActive) return;

        HandleWireDragging();
        CheckWireConnections();
    }

    private void SetupAudio()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
    }

    private void InitializeWires()
    {
        for (int i = 0; i < wireConnections.Count; i++)
        {
            if (i < wireColors.Length)
            {
                wireConnections[i].wire.SetWireColor(wireColors[i]);
                wireConnections[i].correctSocket.SetSocketColor(wireColors[i]);
            }
        }
    }

    public void StartTask()
    {
        isTaskActive = true;
        onTaskStarted?.Invoke();
        Debug.Log("Task de fios iniciada!");
    }

    private void HandleWireDragging()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryGrabWire();
        }

        if (Input.GetMouseButton(0) && currentlyDraggingWire != null)
        {
            DragWire();
        }

        if (Input.GetMouseButtonUp(0) && currentlyDraggingWire != null)
        {
            ReleaseWire();
        }
    }

    private void TryGrabWire()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            Wire wire = hit.collider.GetComponent<Wire>();
            if (wire != null && !IsWireConnected(wire))
            {
                currentlyDraggingWire = wire;
                currentlyDraggingWire.SetDragging(true);
            }
        }
    }

    private void DragWire()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        currentlyDraggingWire.UpdateEndPosition(mousePos);
    }

    private void ReleaseWire()
    {
        currentlyDraggingWire.SetDragging(false);

        WireConnection connection = FindWireConnection(currentlyDraggingWire);
        if (connection != null)
        {
            TryConnectWire(connection);
        }
        else
        {
            currentlyDraggingWire.ResetPosition();
        }

        currentlyDraggingWire = null;
    }

    private void TryConnectWire(WireConnection connection)
    {
        float distance = Vector3.Distance(
            connection.wire.GetEndPosition(),
            connection.correctSocket.transform.position
        );

        if (distance <= connectionDistance)
        {
            ConnectWire(connection);
        }
        else
        {
            connection.wire.ResetPosition();
        }
    }

    private void ConnectWire(WireConnection connection)
    {
        connection.wire.ConnectToSocket(connection.correctSocket.transform.position);
        connection.isConnected = true;
        connectedWiresCount++;

        onWireConnected?.Invoke();
        Debug.Log($"Fio conectado! {connectedWiresCount}/{wireConnections.Count}");

        PlaySound(wireConnectSound);
    }

    private void CheckWireConnections()
    {
        foreach (var connection in wireConnections)
        {
            if (connection.isConnected)
            {
                float distance = Vector3.Distance(
                    connection.wire.GetEndPosition(),
                    connection.correctSocket.transform.position
                );

                if (distance > connectionDistance * 1.5f)
                {
                    DisconnectWire(connection);
                }
            }
        }
    }

    private void DisconnectWire(WireConnection connection)
    {
        connection.isConnected = false;
        connectedWiresCount--;
        connection.wire.ResetPosition();
        Debug.Log("Fio desconectado!");
    }

    private void CompleteTask()
    {
        isTaskActive = false;
        onTaskCompleted?.Invoke();
        Debug.Log("Task de fios concluída!");

        PlaySound(taskCompleteSound);
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    private WireConnection FindWireConnection(Wire wire)
    {
        foreach (var connection in wireConnections)
        {
            if (connection.wire == wire)
            {
                return connection;
            }
        }
        return null;
    }

    private bool IsWireConnected(Wire wire)
    {
        foreach (var connection in wireConnections)
        {
            if (connection.wire == wire && connection.isConnected)
            {
                return true;
            }
        }
        return false;
    }

    public void ResetTask()
    {
        isTaskActive = false;
        connectedWiresCount = 0;
        currentlyDraggingWire = null;

        foreach (var connection in wireConnections)
        {
            connection.isConnected = false;
            connection.wire.ResetPosition();
        }

        Debug.Log("Task de fios resetada!");
    }

    void OnDrawGizmos()
    {
        if (wireConnections == null) return;

        Gizmos.color = Color.green;
        foreach (var connection in wireConnections)
        {
            if (connection.wire != null && connection.correctSocket != null)
            {
                Gizmos.DrawLine(
                    connection.wire.transform.position,
                    connection.correctSocket.transform.position
                );
            }
        }
    }
}