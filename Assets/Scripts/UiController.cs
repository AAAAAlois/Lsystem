using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiController : MonoBehaviour
{
    // Start is called before the first frame update
    public Lsystem Ltree;
    public GameObject dropdownMenu;
    public GameObject leafDropdownMenu;
    public GameObject flowerDropdownMenu;
    public GameObject stoDropdownMenu;
    public GameObject colorDropdownMenu;

    public Slider iterateSlider;
    public TextMeshProUGUI iterateText;
    public Slider lengthSlider;
    public TextMeshProUGUI lengthText;
    public Slider widthSlider;
    public TextMeshProUGUI widthText;
    public Slider angleSlider;
    public TextMeshProUGUI angleText;
    public Slider rotationSlider;

    //public int dropdownIndex;
    public int GeneTime = 0;

    void Start()
    {
        SetDropdown();
        SetLeafDropDown();
        SetFlowerDropDown();
        SetStoDropDown();
        SetColorDropDown();

        SetIrratationSlider();
        SetAngleSlider();
        SetLengthSlider();
        SetWidthSlider();
        SetRotateSlider();

        iterateText.text = iterateSlider.value.ToString();
        lengthText.text = lengthSlider.value.ToString("0.0");
        widthText.text = widthSlider.value.ToString("0.0");
        angleText.text = angleSlider.value.ToString("0.0");
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetDropdown()
    {
        //Debug.Log("set");

        TMP_Dropdown dropdown = dropdownMenu.GetComponent<TMP_Dropdown>();

        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
    }

    void DropdownItemSelected(TMP_Dropdown dropdown)
    {
        Ltree.index = dropdown.value;

    }

    void SetLeafDropDown()
    {
        TMP_Dropdown dropdown = leafDropdownMenu.GetComponent<TMP_Dropdown>();

        dropdown.onValueChanged.AddListener(delegate { LeafDropdownItemSelected(dropdown); });
    }

    void LeafDropdownItemSelected(TMP_Dropdown dropdown)
    {
        int _leaf = dropdown.value;
        Ltree.hasLeaf = _leaf == 0 ? false : true;
    }

    void SetFlowerDropDown()
    {
        TMP_Dropdown dropdown = flowerDropdownMenu.GetComponent<TMP_Dropdown>();

        dropdown.onValueChanged.AddListener(delegate { FlowerDropdownItemSelected(dropdown); });
    }

    void FlowerDropdownItemSelected(TMP_Dropdown dropdown)
    {
        int _flower = dropdown.value;
        Ltree.hasFlower = _flower == 0 ? false : true;
        //Debug.Log("has flower");
    }

    void SetStoDropDown()
    {
        TMP_Dropdown dropdown = stoDropdownMenu.GetComponent<TMP_Dropdown>();

        dropdown.onValueChanged.AddListener(delegate { StoDropdownItemSelected(dropdown); });
    }

    void StoDropdownItemSelected(TMP_Dropdown dropdown)
    {
        int _sto = dropdown.value;
        Ltree.hasStochastic = _sto == 0 ? false : true;
    }

    void SetColorDropDown()
    {
        TMP_Dropdown dropdown = colorDropdownMenu.GetComponent<TMP_Dropdown>();
        dropdown.onValueChanged.AddListener(delegate { ColorDropdownItemSelected(dropdown); });
    }

    void ColorDropdownItemSelected(TMP_Dropdown dropdown)
    {
        Ltree.colorIndex = dropdown.value;
    }



    void SetIrratationSlider()
    {
        iterateSlider.onValueChanged.AddListener(Iterate) ;
    }

    void Iterate(float iterateTime)
    {
        iterateText.text = iterateSlider.value.ToString();
        Ltree.iteration = (int)iterateSlider.value;
 
    }

    void SetLengthSlider()
    {
        lengthSlider.onValueChanged.AddListener(Length);
    }

    void Length(float _length)
    {
        lengthText.text = lengthSlider.value.ToString("0.0");
        Ltree.length = lengthSlider.value;
    }

    void SetWidthSlider()
    {
        widthSlider.onValueChanged.AddListener(Width);
    }

    void Width(float _width)
    {
        widthText.text = widthSlider.value.ToString("0.0");
        Ltree.width = widthSlider.value;
    }

    void SetAngleSlider()
    {
        angleSlider.onValueChanged.AddListener(Angle);
    }

    void Angle(float _angle)
    {
        angleText.text = angleSlider.value.ToString("0.0");
        Ltree.angle = angleSlider.value;
    }

    void SetRotateSlider()
    {
        rotationSlider.onValueChanged.AddListener(RotateTree);
    }
    
    void RotateTree(float _rotate)
    {
        Ltree.tree.transform.rotation = Quaternion.Euler(0, _rotate, 0);
        Debug.Log("rotate");
    }
}
