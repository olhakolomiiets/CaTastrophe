using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerDisplay : MonoBehaviour
{
  [SerializeField]
  private Image icoCat;
  [SerializeField]
  private Text valueText;
  [SerializeField]
  private Text nameText;
  [SerializeField]
  private Slider slider;
  [SerializeField]
  private FloatSO powerCat;

  private void Awake() {
      SetText(powerCat.Value);
      SetSlider(powerCat.Value);
      nameText.text = powerCat.nameCat.ToString().ToUpper();
      slider.maxValue = 10;
  }

  private void OnEnable() {
      powerCat.ValueChangeBy +=SetText;
      powerCat.ValueSetToNewAmount +=SetText;
       powerCat.ValueChangeBy +=SetSlider;
      powerCat.ValueSetToNewAmount +=SetSlider;
  }
private void OnValidate() {
    if(powerCat == null) return;
    icoCat.sprite = powerCat.icon;
    gameObject.name = powerCat.nameCat + "Display";
    SetText(powerCat.Value);
    SetSlider(powerCat.Value);
}

private void SetText (float newAmount)
{
valueText.text = powerCat.Value.ToString("F0");
}
private void SetSlider (float newAmount)
{
slider.value = powerCat.Value;
}
private void OnDisable() {
     powerCat.ValueChangeBy -=SetText;
      powerCat.ValueSetToNewAmount -=SetText;
       powerCat.ValueChangeBy -=SetSlider;
      powerCat.ValueSetToNewAmount -=SetSlider;
}


}
