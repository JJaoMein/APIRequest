using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class RestApiManager : MonoBehaviour
{

    [SerializeField] string rickAndMortyApi = "https://rickandmortyapi.com/api/character/";
    [SerializeField] string myApi;
    [SerializeField] int characterId = 1;
    [SerializeField] RawImage yourRawImage;

    void Start()
    {
        
    }

    public void ActionOnCLick()
    {
        StartCoroutine(GetText());
    }

    //IEnumerator GetUsers()
    //{
    //    UnityWebRequest www = UnityWebRequest.Get(myApi);
    //    yield return www.SendWebRequest();

    //    if (www.result == UnityWebRequest.Result.ConnectionError)
    //    {
    //        Debug.Log(www.error);
    //    }
    //    else
    //    {
    //        // Show results as text
    //        Debug.Log(www.downloadHandler.text);

    //        if (www.responseCode == 200)
    //        {
    //            User users = JsonUtility.FromJson<User>(www.downloadHandler.text);

    //            foreach (int id in users.id)
    //            {
    //                Debug.LogWarning("Name: " + users.name);

    //            }
    //        }
    //        else
    //        {
    //            string mensaje = www.responseCode.ToString();
    //            mensaje += "\ncontent-type" + www.GetRequestHeader("content-type");
    //            mensaje += "\nError: " + www.error;
    //            Debug.Log(mensaje);
    //        }
    //    }
    //}

    IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get(rickAndMortyApi);
        yield return www.SendWebRequest();

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
                CharacterList characters = JsonUtility.FromJson<CharacterList>(www.downloadHandler.text);

                foreach (Character character in characters.results)
                {
                    Debug.LogWarning("Name: " + character.name);
                    Debug.Log("Image: " + character.image);
                    StartCoroutine(GetImage(character.image));
                    break;
                }
            }
            else
            {
                string mensaje = www.responseCode.ToString();
                mensaje += "\ncontent-type" + www.GetRequestHeader("content-type");
                mensaje += "\nError: " + www.error;
                Debug.Log(mensaje);
            }
        }
    }

    IEnumerator GetImage(string MediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(request.error);
        }
        else 
        {
            yourRawImage.texture = ((DownloadHandlerTexture)request.downloadHandler).texture; 
        }
    }
}


public class User
{
    public int id;
    public string name;
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
