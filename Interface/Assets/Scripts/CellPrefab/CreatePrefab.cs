using UnityEngine;

public class CreatePrefab : MonoBehaviour
{
    #region private Members
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _spawnZone;
    [SerializeField] private float _radiusOfSpawnZone;
    private InterfaceManager _interfaceManager;
    #endregion

    #region private Methods
    private void Start()
    {
        _interfaceManager = GameObject.FindObjectOfType<InterfaceManager>();
    }

    private void Update()
    {
        UpdateInput();
    }

    private void UpdateInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject obj = Instantiate(_prefab, _spawnZone.position + new Vector3(Random.Range(-_radiusOfSpawnZone, _radiusOfSpawnZone), 5f,
                Random.Range(-_radiusOfSpawnZone, _radiusOfSpawnZone)), Quaternion.identity);

            Renderer rend = obj.GetComponent<Renderer>();
            Color color = new Color((float)Random.Range(0, 255) / 255, (float)Random.Range(0, 255) / 255, (float)Random.Range(0, 255) / 255);
            rend.material.color = color;
            _interfaceManager.AddNewObjectToList(obj, color);
        }
    }
    #endregion
}
