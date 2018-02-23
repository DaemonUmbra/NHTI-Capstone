using System.Collections;
using UnityEngine;

public class PlayerNetwork : MonoBehaviour
{
    public static PlayerNetwork Instance;
    public string PlayerName { get; private set; }
    private PhotonView PhotonView;
    private int PlayersInGame = 0;
    private ExitGames.Client.Photon.Hashtable m_playerCustomProperties = new ExitGames.Client.Photon.Hashtable();
    private Coroutine m_pingCoroutine;
    

    // Use this for initialization
    private void Awake()
    {
        Instance = this;
        PhotonView = GetComponent<PhotonView>();

        PhotonNetwork.sendRate = 60;
        PhotonNetwork.sendRateOnSerialize = 30;

        //SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    private IEnumerator C_SetPing()
    {
        while (PhotonNetwork.connected)
        {
            m_playerCustomProperties["Ping"] = PhotonNetwork.GetPing();
            PhotonNetwork.player.SetCustomProperties(m_playerCustomProperties);

            yield return new WaitForSeconds(5f);
        }

        yield break;
    }

    private IEnumerator C_ShowPing()
    {
        while (PhotonNetwork.connected)
        {
            int ping = (int)PhotonNetwork.player.CustomProperties["Ping"];

            yield return new WaitForSeconds(5f);
        }

        yield break;
    }

    private void OnConnectedToMaster()
    {
        if (m_pingCoroutine != null) StopCoroutine(m_pingCoroutine);
        m_pingCoroutine = StartCoroutine(C_SetPing());
    }
}