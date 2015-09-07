using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TavernScreen : MonoBehaviour {

    public Slider Slider;
    public Button[] CharacterSlots = new Button[3];
    
    int middleIndex = 1;
    float intervals;

	void Start () {
        Slider.onValueChanged.AddListener(delegate { Slider_OnValueChanged(); });

        intervals = Slider.maxValue / (CharacterSlots.Length - 1);
        Slider.value = middleIndex * intervals;

        CharacterSlots[0].onClick.AddListener(delegate
        { 
            OnCharacterSlotClick(DataCarrier.PersistentData.AllCharacters[middleIndex - 1]); //Leftmost character
        });
        CharacterSlots[1].onClick.AddListener(delegate
        {
            OnCharacterSlotClick(DataCarrier.PersistentData.AllCharacters[middleIndex]); //Center character
        });
        CharacterSlots[2].onClick.AddListener(delegate
        {
            OnCharacterSlotClick(DataCarrier.PersistentData.AllCharacters[middleIndex + 1]); //Rightmost character
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
        Character left = DataCarrier.PersistentData.AllCharacters[newMiddleIndex - 1],
                center = DataCarrier.PersistentData.AllCharacters[newMiddleIndex],
                right = DataCarrier.PersistentData.AllCharacters[newMiddleIndex + 1];

        CharacterSlots[0].image.sprite = left.Model;
        CharacterSlots[1].image.sprite = center.Model;
        CharacterSlots[2].image.sprite = right.Model;
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
