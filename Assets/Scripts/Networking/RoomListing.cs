using UnityEngine;
using UnityEngine.UI;

public class RoomListing : MonoBehaviour {

    [SerializeField]
    private Text _roomNameText;
    private Text RoomNameText
    {
        get { return _roomNameText; }
    }

    public bool Updated { get; set; }


	// Use this for initialization
	private void Start () {
		
	}
}
