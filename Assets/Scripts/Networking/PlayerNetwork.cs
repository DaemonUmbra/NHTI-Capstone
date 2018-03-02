using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerNetwork : Photon.PunBehaviour
{
    public static PlayerNetwork Instance;
    public string PlayerName { get; private set; }
    private PhotonView PhotonView;
    private int PlayersInGame = 0;
    private ExitGames.Client.Photon.Hashtable m_playerCustomProperties = new ExitGames.Client.Photon.Hashtable();
    private Coroutine m_pingCoroutine;
    private GameObject defaultSpawnPoint;
    public static GameObject localPlayer;


    // Use this for initialization
    private void Awake()
    {
        Instance = this;
        PhotonView = GetComponent<PhotonView>();

        PhotonNetwork.sendRate = 60;
        PhotonNetwork.sendRateOnSerialize = 30;

        //SceneManager.sceneLoaded += OnSceneFinishedLoading;
        PhotonNetwork.automaticallySyncScene = true;
    }

    //private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    //{
    //    if (scene.name == "Sandbox")
    //    {
    //        if (PhotonNetwork.isMasterClient)
    //            MasterLoadedGame();
    //        else
    //            NonMasterLoadedGame();
    //    }
    //}

    //private void MasterLoadedGame()
    //{
    //    PhotonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient);
    //    PhotonView.RPC("RPC_LoadGameOthers", PhotonTargets.Others);
    //}
    //private void NonMasterLoadedGame()
    //{
    //    PhotonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient);

    //}

    //[PunRPC]
    //private void RPC_LoadGameOthers()
    //{
    //    PhotonNetwork.LoadLevel(1);
    //}

    //[PunRPC]
    //private void RPC_LoadedGameScene()
    //{
    //    PlayersInGame++;
    //    if (PlayersInGame == PhotonNetwork.playerList.Length)
    //    {
    //        print("All players are in the game scene.");
    //        //PhotonView.RPC("RPC_CreatePlayer", PhotonTargets.All); not working yet
    //    }
    //    PhotonView.RPC("RPC_CreatePlayer", PhotonTargets.All);
    //}

    //[PunRPC]
    //private void RPC_CreatePlayer()
    //{
    //    var spawnPoint = GetRandomSpawnPoint();
    //    localPlayer = PhotonNetwork.Instantiate("BasicPlayer", spawnPoint.position, spawnPoint.rotation, 0);
    //}


    //public static List<GameObject> GetAllObjectsOfTypeInScene<T>()
    //{
    //    List<GameObject> objectsInScene = new List<GameObject>();

    //    foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject))
    //             as GameObject[])
    //    {
    //        if (go.hideFlags == HideFlags.NotEditable ||
    //            go.hideFlags == HideFlags.HideAndDontSave)
    //            continue;

    //        if (go.GetComponent<T>() != null)
    //            objectsInScene.Add(go);
    //    }

    //    return objectsInScene;
    //}

    //public Transform GetRandomSpawnPoint()
    //{
    //    var spawnPoints = GetAllObjectsOfTypeInScene<SpawnPoint>();
    //    if (spawnPoints.Count == 0)
    //    {
    //        return defaultSpawnPoint.transform;
    //    }
    //    else
    //    {
    //        return spawnPoints[Random.Range(0, spawnPoints.Count)].transform;
    //    }
    //}

 

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

    public override void OnConnectedToMaster()
    {
        if (m_pingCoroutine != null) StopCoroutine(m_pingCoroutine);
        m_pingCoroutine = StartCoroutine(C_SetPing());
    }
}