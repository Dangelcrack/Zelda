using UnityEngine;
using UnityEngine.UI;

public class SceneMusicController : MonoBehaviour
{
    [SerializeField] private AudioClip sceneMusic; // Música específica para la escena
    [SerializeField] private Slider sceneSlider;   // Slider específico de la escena

    private void Start()
    {
        // Buscar el `MusicManager` y asignar la música y el slider de la escena actual
        MusicManager musicManager = FindObjectOfType<MusicManager>();
        if (musicManager != null)
        {
            // Asignar la música de fondo específica de la escena
            musicManager.PlayBackgroundMusic(sceneMusic);

            // Asignar el slider de la escena para controlar el volumen
            musicManager.SetVolumeSlider(sceneSlider);
        }
    }
}
