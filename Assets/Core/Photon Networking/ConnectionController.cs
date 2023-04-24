using System.Collections;
using UnityEngine;
using Photon.Pun;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.Networking;

namespace Core
{
    public class ConnectionController : MonoBehaviourPunCallbacks
    {
        public string photonStatus;

        [SerializeField] public string roomName;

        //RoomManager RoomManager;
        //PlayerManager PlayerManager;
        public static string _addressUser;

        //private string _getUserAPI = "https://api-3d.maintest.net/user?address=";
        string rsAPI;
        JObject rsData;

        string rsUsername;

        // Start is called before the first frame update
        private void Start()
        {
            // RoomManager = new RoomManager();
            // PlayerManager = new PlayerManager();
            PhotonNetwork.ConnectUsingSettings();
            photonStatus = PhotonNetwork.NetworkClientState.ToString();
            PhotonNetwork.KeepAliveInBackground = 600000f;
        }



        public override void OnConnectedToMaster()
        {
            // if (_addressUser.Length > 18)
            // {
            //     _addressUser = _addressUser.Substring(0, 15) + "...";
            // }

            PhotonNetwork.LocalPlayer.NickName = "test";
            //GetData(_getUserAPI + _addressUser.ToLower());
            PhotonNetwork.JoinLobby();
        }

        public override void OnJoinedLobby()
        {
            PhotonNetwork.CreateRoom(roomName);
        }

        public override void OnCreatedRoom()
        {
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.Log(message);
            PhotonNetwork.JoinRoom(roomName);
        }

        public override void OnJoinedRoom()
        {
            PlayerSpawner.Instance.OnSpawnPlayer();
            
        }

        //public override void OnRoomListUpdate(List<RoomInfo> roomList)
        //{
        //    foreach (var val in roomList)
        //    {
        //        if (val.RemovedFromList) RoomManager.RemoveRoom(val);
        //        else RoomManager.AddRoom(val);
        //    }
        //}

        //Get data from api
        void GetData(string linkAPI) => StartCoroutine(GetData_Coroutine(linkAPI));

        IEnumerator GetData_Coroutine(string url)
        {
            using (UnityWebRequest request = UnityWebRequest.Get(url))
            {
                yield return request.SendWebRequest();

                if (request.isNetworkError || request.isHttpError) //Check network and http error
                    Debug.Log("Error get data from api");
                else
                {
                    rsAPI = request.downloadHandler.text;
                    rsData = JsonConvert.DeserializeObject<JObject>(rsAPI);
                    Debug.Log(rsData[@"data"]);
                    if (rsData[@"data"].ToString() != null && rsData[@"data"].ToString() != "")
                    {
                        rsUsername = rsData[@"data"]["userName"].ToString();
                        if (rsUsername != "" && rsUsername != null)
                        {
                            PhotonNetwork.LocalPlayer.NickName = rsUsername;
                        }
                    }
                    else
                    {
                        PhotonNetwork.LocalPlayer.NickName = "Username is null";
                    }
                }
            }
        }
    }
}