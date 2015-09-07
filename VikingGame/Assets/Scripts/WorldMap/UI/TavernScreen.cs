using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TavernScreen : MonoBehaviour {

    public Slider Slider;
    public Button[] CharacterSlots = new Button[3];
    public List<Character> Characters = new List<Character>();
    
    int middleIndex = 1;
    float intervals;

	void Start () {
        Slider.onValueChanged.AddListener(delegate { Slider_OnValueChanged(); });

        intervals = Slider.maxValue / (CharacterSlots.Length - 1);
        Slider.value = middleIndex * intervals;

        CharacterSlots[0].onClick.AddListener(delegate
        { 
            OnCharacterSlotClick(Characters[middleIndex - 1]); //Leftmost character
        });
        CharacterSlots[1].onClick.AddListener(delegate
        {
            OnCharacterSlotClick(Characters[middleIndex]); //Center character
        });
        CharacterSlots[2].onClick.AddListener(delegate
        {
            OnCharacterSlotClick(Characters[middleIndex + 1]); //Rightmost character
        });

        this.gameObject.SetActive(false);
	}

    void Slider_OnValueChanged()
    {
        int newMiddleIndex = System.Convert.ToInt32(Slider.value / intervals) + 1;

        if (newMiddleIndex != middleIndex)
        {
            SetCharacters(newMiddleIndex);

            middleIndex = newMiddleIndex;
        }
    }

    void SetCharacters(int newMiddleIndex)
    {
        Character left = Characters[newMiddleIndex - 1],
                center = Characters[newMiddleIndex],
                right = Characters[newMiddleIndex + 1];

        left.gameObject.transform.position = new Vector3(
            CharacterSlots[0].transform.position.x,
            CharacterSlots[0].transform.position.y,
            -1);
        center.gameObject.transform.position = new Vector3(
            CharacterSlots[1].transform.position.x,
            CharacterSlots[1].transform.position.y,
            -1);
        right.gameObject.transform.position = new Vector3(
            CharacterSlots[2].transform.position.x,
            CharacterSlots[2].transform.position.y,
            -1);

        CharacterSlots[0].image.sprite = left.GetComponent<Image>().sprite;
        CharacterSlots[1].image.sprite = center.GetComponent<Image>().sprite;
        CharacterSlots[2].image.sprite = right.GetComponent<Image>().sprite;
    }

    void OnCharacterSlotClick(Character c)
    {
        CharacterInfoWindow.Instance.Character = c;
        CharacterInfoWindow.Instance.gameObject.SetActive(true);
    }

    public void Cancel()
    {
        this.gameObject.SetActive(false);
    }
}
