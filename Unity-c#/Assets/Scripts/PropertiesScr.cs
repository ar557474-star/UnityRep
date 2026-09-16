using UnityEngine;

public class PropertiesScr : MonoBehaviour
{
    // Backing field to hold the actual value
    private static string _playerName;
    //private static string PlayerName { get; set; }
    private static string playerName {
        get {
            if (_playerName == null)
            {
                return "Unkown";
            }
            return _playerName;
        }
        set { 
            _playerName = value;
        } 
    }


    void Start()
    {
        Debug.Log(playerName);
        playerName = "\nCodeAhm";
        Debug.Log(playerName);
    }


}
