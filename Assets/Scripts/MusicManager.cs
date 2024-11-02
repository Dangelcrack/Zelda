using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    private static MusicManager Instance;
    private AudioSource audioSource;
    [SerializeField] private AudioClip backgroundMusic; // Música de fondo predeterminada
    private Slider currentSlider; // Slider actualmente asignado para controlar el volumen

    private const string VolumeKey = "Volume"; // Clave para almacenar el volumen en PlayerPrefs

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
            PlayBackgroundMusic(backgroundMusic);
            LoadVolume(); // Cargar el volumen guardado al inicio
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Método para asignar un slider nuevo desde el controlador de escena
    public void SetVolumeSlider(Slider slider)
    {
        if (currentSlider != null)
        {
            // Elimina el listener anterior si ya había un slider asignado
            currentSlider.onValueChanged.RemoveListener(SetVolume);
        }

        // Asigna el nuevo slider y agrega el listener
        currentSlider = slider;
        if (currentSlider != null)
        {
            currentSlider.onValueChanged.AddListener(SetVolume);
            SetVolume(currentSlider.value); // Ajusta el volumen inicial al valor del nuevo slider
        }
    }

    public void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
            PlayerPrefs.SetFloat(VolumeKey, volume); // Guarda el volumen en PlayerPrefs
            PlayerPrefs.Save(); // Asegúrate de guardar los cambios
        }
    }

    public void PlayBackgroundMusic(AudioClip musicClip)
    {
        if (musicClip != null && audioSource.clip != musicClip)
        {
            audioSource.clip = musicClip;
            audioSource.Play();
        }
    }

    private void LoadVolume()
    {
        // Cargar el volumen guardado; si no existe, establecer el volumen predeterminado (por ejemplo, 1.0)
        float volume = PlayerPrefs.GetFloat(VolumeKey, 1.0f);
        audioSource.volume = volume;

        // Si hay un slider actual, también lo actualizamos
        if (currentSlider != null)
        {
            currentSlider.value = volume; // Ajusta el slider al valor guardado
        }
    }
}
