using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvaManager : MonoBehaviour {

    public static MainCanvaManager Instance;

    [SerializeField]
    private LobbyCanvas _lobbyCanvas;
    public LobbyCanvas LobbyCanvas
    {
        get { return _lobbyCanvas; }
    }

    private void Awake()
    {
        Instance = this;
    }
}
