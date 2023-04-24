using Photon.Pun;
using UnityEngine;
using Zenject;

namespace Core
{
    public class PlayerSpawner : MonoBehaviour
    {
        public static PlayerSpawner Instance;
        public Transform transPlayerSpawn;
        public GameObject playerPrefabs;
        
        
        [SerializeField]
        private DiContainer _container;

        [Inject]
        private void Construct(DiContainer _container)
        {
            Debug.Log($"{_container}");
            this._container = _container;
        }
        //[Inject] private PlayerController.Factory _factory;
        public void Start()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public void OnSpawnPlayer()
        {
            var player = PhotonNetwork.Instantiate(playerPrefabs.name, transPlayerSpawn.position, Quaternion.identity);
            _container.InjectGameObject(player);
        }
    }

}