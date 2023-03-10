using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UserCollection : MonoBehaviour
{
    [SerializeField] TMP_Text mMoneyText;
    [SerializeField] TMP_Text mCollectText;

    int collectionCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        SetMoneyText();
        SetCollectionText();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene("MainScreen");
        }
    }

    void SetMoneyText(){
        mMoneyText.text = UserInfo.instance.UserDataSet.Gold.ToString();
    }

    void SetCollectionText(){
        for(int i = 0; i < Constants.MaxCollectionNumber; i++){
            if(!UserInfo.instance.UserDataSet.Collection[i]){
                collectionCount++;
            }
        }
        mCollectText.text = "Collection : " + collectionCount.ToString() + " / " + Constants.MaxCollectionNumber;
    }
}
