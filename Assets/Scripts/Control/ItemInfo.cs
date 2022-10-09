using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    [SerializeField] private Image _image;
    public Image Image
    {
        get => _image;
        set => _image = value;        
    }

    [SerializeField] private Text _name;
    public Text Name
    {
        get => _name;
        set => _name = value;
    }

    [SerializeField] private Text _description;
    public Text Description
    {
        get => _description;
        set => _description = value;
    }

    [SerializeField] private Text _stats;
    public Text Stats
    {
        get => _stats;
        set => _stats = value;
    }

    [SerializeField] private GameObject _basePanel;
    public GameObject BasePanel
    {
        get => _basePanel;
        set => _basePanel = value;
    }


    public void ShowItemInfo(Item item)
    {
        Image.sprite = item.image;
        Name.text = item.Name;
        Description.text = item.Description;
        Stats.text = item.StatsInfo;
        BasePanel.SetActive(true);
    }

    public void Hide()
    {
        BasePanel.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
