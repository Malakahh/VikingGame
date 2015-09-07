using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoWindow : MonoBehaviour {
    public static CharacterInfoWindow Instance;

    public Image CharacterImage;

    private Character _character;
    public Character Character
    {
        get { return _character; }
        set 
        { 
            _character = value;

            if (this.gameObject.activeInHierarchy)
            {
                Setup();
            }
        }
    }

	void Awake() {
        Instance = this;
        this.gameObject.SetActive(false);
	}
	
    void OnEnable()
    {
        Setup();
    }

	void Setup()
    {
        CharacterImage.sprite = Character.GetComponent<Image>().sprite;
    }

    public void Cancel()
    {
        this.gameObject.SetActive(false);
    }
}
