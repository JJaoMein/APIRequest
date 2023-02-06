using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class RestApiManager : MonoBehaviour
{

    [SerializeField] string rickAndMortyApi = "https://rickandmortyapi.com/api";
    [SerializeField] string myApi;
    [SerializeField] int characterID = 1;

    void Start()
    {
        
    }

    IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get(rickAndMortyApi + "/character/" + characterID);
        yield return www.Send();

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);

            if (www.responseCode == 200)
            {

            }
            else
            {

            }
        }
    }
}
