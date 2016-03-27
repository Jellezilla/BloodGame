using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIManagerScript : MonoBehaviour {

    public GameObject HpbarSingle;
    public GameObject HpbarMain;
    public Sprite HpSpriteFrontface;
    public Sprite HpSpriteBackface;
    public Text HpbarIndicator;

    private List<CanvasRenderer> _healthbars;
    private int _amountOfHealthbars = 15;

    // Use this for initialization
    void Start () {

        SetupUI();
        //ChangeHealth(GameController.Instance.ChasisCurrentDurability, GameController.Instance.ChasisMaxDurability);
	}

    // Update is called once per frame
    void Update() {
        ChangeHealth(GameController.Instance.ChasisCurrentDurability, GameController.Instance.ChasisMaxDurability);
    }

    void SetupUI() {
        SetupHealthbar();
    }

    void SetupHealthbar() {
        int hspace = 9;
        int hoffset = -5;

        _healthbars = new List<CanvasRenderer>();



        for (int i = 0; i < 2; i++){
            for (int j = 0; j < _amountOfHealthbars; j++){
                GameObject go = (GameObject)Instantiate(HpbarSingle);
                RectTransform rt = (RectTransform)go.transform;
                go.transform.localPosition = new Vector2(hoffset + j * (rt.rect.width + hspace), rt.rect.height);
                go.transform.SetParent(HpbarMain.transform, false);
                if (i == 0){
                    go.GetComponent<Image>().sprite = HpSpriteBackface;
                }
                else{
                    go.GetComponent<Image>().sprite = HpSpriteFrontface;
                    CanvasRenderer goR = go.GetComponent<CanvasRenderer>();
                    goR.SetAlpha(0);
                    _healthbars.Add(goR);
                }
            }
        }
    }

    public void ChangeHealth(double currentHp, double maxHp) {

        float singlebarHp = (float)(maxHp/_amountOfHealthbars);
        float hpCount = (float)currentHp;
        if (hpCount < 1) 
        {
            HpbarIndicator.text = "0";
        }
        else
        {
            HpbarIndicator.text = Mathf.Round(hpCount).ToString();
        }


        for (int i = 0; i < _amountOfHealthbars; i++) {
           
            if (hpCount <= 0){
               _healthbars[i].SetAlpha(0f);
               break;
            }
            else{
                if (hpCount>singlebarHp){
                    _healthbars[i].SetAlpha(1);
                }
                else {
                    float a = hpCount/singlebarHp;
                    _healthbars[i].SetAlpha(a); //do crossfade here perhaps;
                }
                hpCount -= singlebarHp;
            }
        }
    }
}
