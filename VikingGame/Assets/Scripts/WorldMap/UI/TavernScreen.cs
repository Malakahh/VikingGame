using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TavernScreen : MonoBehaviour {

    public Slider Slider;
    public GameObject[] CharacterSlots = new GameObject[3];
    public List<Character> Characters = new List<Character>();
    
    int prevMiddleIndex = 1;
    float intervals;

	void Start () {
        Slider.onValueChanged.AddListener(delegate { Slider_OnValueChanged(); });

        intervals = Slider.maxValue / (CharacterSlots.Length - 1);
        Slider.value = prevMiddleIndex * intervals;

        this.gameObject.SetActive(false);
	}

    void Slider_OnValueChanged()
    {
        int middleIndex = System.Convert.ToInt32(Slider.value / intervals) + 1;

        if (middleIndex != prevMiddleIndex)
        {
            Characters[middleIndex - 1].gameObject.transform.position = CharacterSlots[0].transform.position + Vector3.back;
            Characters[middleIndex].gameObject.transform.position = CharacterSlots[1].transform.position + Vector3.back;
            Characters[middleIndex + 1].gameObject.transform.position = CharacterSlots[2].transform.position + Vector3.back;

            CharactersSetActive(prevMiddleIndex, false);
            CharactersSetActive(middleIndex, true);

            prevMiddleIndex = middleIndex;
        }
    }

    void CharactersSetActive(int middleIndex, bool active)
    {
        Characters[middleIndex - 1].gameObject.SetActive(active);
        Characters[middleIndex].gameObject.SetActive(active);
        Characters[middleIndex + 1].gameObject.SetActive(active);
    }
}
