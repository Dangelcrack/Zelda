using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Vector3 playerPosition; // Posición guardada
    public bool positionSaved; // Controla si se ha guardado una posición

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Asegura que el objeto no se destruya al cambiar de escena
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SavePlayerPosition(Vector3 newPosition)
    {
        playerPosition = newPosition;
        positionSaved = true;
    }
}
