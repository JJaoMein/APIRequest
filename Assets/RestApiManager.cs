using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using static System.Net.WebRequestMethods;
using UnityEngine.TextCore.Text;

public class RestApiManager : MonoBehaviour
{

    //[SerializeField] string rickAndMortyApi = "https://rickandmortyapi.com/api/character/";
    //[SerializeField]public string myApi = "https://my-json-server.typicode.com/JJaoMein/TempJSONServer/user";
    //[SerializeField] int characterId = 1;
    //[SerializeField] int userId = 1;
    [SerializeField] List<RawImage> yourRawImage;


    void Start()
    {
        
    }

    public void RequestOnCLick()
    {
        StartCoroutine(GetUsers());
    }

    IEnumerator GetUsers()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://my-json-server.typicode.com/JJaoMein/TempJSONServer/users/1");
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("ESTE ERROR" + www.error);
        }
        else
        {
            //Debug.Log(www.downloadHandler.text);

            if (www.responseCode == 200)
            {
                UserJsonData myUser = JsonUtility.FromJson<UserJsonData>(www.downloadHandler.text);
                Debug.Log("User ID: " + myUser.id);
                Debug.Log("User Name: " + myUser.name);

                //for (int i = 0; i < myUser.deck.Length; i++)
                //{
                //    StartCoroutine(GetCharacter(myUser.deck[i],i));
                    
                //}

            }
            else
            {
                string mensaje = www.responseCode.ToString();
                mensaje += "\ncontent-type" + www.GetRequestHeader("content-type");
                mensaje += "\nError!!!: " + www.error;
                Debug.Log(mensaje);
            }
        }
    }

    IEnumerator GetCharacter(int characterId, int imageNumber)
    {
        UnityWebRequest www = UnityWebRequest.Get("https://rickandmortyapi.com/api/character" + characterId.ToString());
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("ESTE ERROR" + www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);

            if (www.responseCode == 200)
            {
                Character card = JsonUtility.FromJson<Character>(www.downloadHandler.text);

                StartCoroutine(GetImage(card.image, imageNumber));
            }
            else
            {
                string mensaje = www.responseCode.ToString();
                mensaje += "\ncontent-type" + www.GetRequestHeader("content-type");
                mensaje += "\nError!!!: " + www.error;
                Debug.Log(mensaje);
            }
        }
    }

    IEnumerator GetImage(string MediaUrl, int imageNumber)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(request.error);
        }
        else 
        {
            yourRawImage[imageNumber].texture = ((DownloadHandlerTexture)request.downloadHandler).texture; 
        }
    }
}


public class UserJsonData
{
    public int id;
    public string name;
    public int[] deck;
}

[System.Serializable]
public class CharacterList
{
    public CharacterListInfo info;
    public List<Character> results;
}

[System.Serializable]
public class CharacterListInfo
{
    public int count;
    public int pages;
    public string next;
    public string prev;
}

[System.Serializable]
public class Character
{
    public int id;
    public string name;
    public string image;
}
