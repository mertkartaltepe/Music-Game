using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.SceneManagement;

public class gameControl : MonoBehaviour
{
    public AudioSource[] Notes;
    List<int> recordedNotes = new List<int>();
    // stores note lengths
    List<float> noteTimes = new List<float>();
    bool recordFlag = false;
    float timeClickNote = 0;

    // ****** button shine
    UnityEngine.UI.Button buttonDoZero;
    UnityEngine.UI.Button buttonRe;
    UnityEngine.UI.Button buttonMi;
    UnityEngine.UI.Button buttonFa;
    UnityEngine.UI.Button buttonSol;
    UnityEngine.UI.Button buttonLa;
    UnityEngine.UI.Button buttonSi;
    UnityEngine.UI.Button buttonDoOne;
    // play limit control
    UnityEngine.UI.Button buttonPlay;
    // record note length
    UnityEngine.UI.Button buttonRec;

    void Start()
    {
        
        Notes = GetComponents<AudioSource>();
        //PlayerPrefs.DeleteAll();
        buttonDoZero = GameObject.Find("0_do").GetComponent<UnityEngine.UI.Button>();
        buttonRe = GameObject.Find("1_re").GetComponent<UnityEngine.UI.Button>();
        buttonMi = GameObject.Find("2_mi").GetComponent<UnityEngine.UI.Button>();
        buttonFa = GameObject.Find("3_fa").GetComponent<UnityEngine.UI.Button>();
        buttonSol = GameObject.Find("4_sol").GetComponent<UnityEngine.UI.Button>();
        buttonLa = GameObject.Find("5_la").GetComponent<UnityEngine.UI.Button>();
        buttonSi = GameObject.Find("6_si").GetComponent<UnityEngine.UI.Button>();
        buttonDoOne = GameObject.Find("7_do").GetComponent<UnityEngine.UI.Button>();

        buttonPlay = GameObject.Find("playLastRecord").GetComponent<UnityEngine.UI.Button>();
        buttonRec = GameObject.Find("recordButton").GetComponent<UnityEngine.UI.Button>();

        // Set the add listener to detect the clicks
        buttonDoZero.onClick.AddListener(calculateNoteTime);
        buttonRe.onClick.AddListener(calculateNoteTime);
        buttonMi.onClick.AddListener(calculateNoteTime);
        buttonFa.onClick.AddListener(calculateNoteTime);
        buttonSol.onClick.AddListener(calculateNoteTime);
        buttonLa.onClick.AddListener(calculateNoteTime);
        buttonSi.onClick.AddListener(calculateNoteTime);
        buttonDoOne.onClick.AddListener(calculateNoteTime);

        buttonRec.onClick.AddListener(calculateNoteTime);

    }

    void calculateNoteTime(){
        if (recordFlag)
        {
            float clickTime = timeClickNote;
            noteTimes.Add(clickTime);
            Debug.Log("click time: " + clickTime);   
            timeClickNote = 0;
                     
        }
    }

    void Update()
    {
        if (recordFlag)
        {
            timeClickNote += Time.deltaTime;
        }
    }

    public void playLastRecord(){
        
        // take stored data from playerPrefs
        int recordSize = PlayerPrefs.GetInt("recordedNotes_count");
        int temp;
        float tempTime;
        Debug.Log("Rec size: " + recordSize);
        // clear the recordedNotesList before taking the stored data
        recordedNotes.Clear();
        noteTimes.Clear();
 
        for (int i = 0; i < recordSize; i++)
        {
            temp = PlayerPrefs.GetInt("recordedNotes_" + i);
            tempTime = PlayerPrefs.GetFloat("noteTimes_" + i);
            recordedNotes.Insert(i, temp);
            noteTimes.Insert(i, tempTime);
            //Debug.Log("temp: " + temp);
        }
        noteTimes.Insert(recordSize, PlayerPrefs.GetFloat("noteTimes_" + recordSize));
        // call playRecord method for playing last record that is stored
        playRecord();
        
    }

    public void playRecord(){
        
        StartCoroutine(playRecordedNotes());
        
    }

    IEnumerator playRecordedNotes(){
        // play lastRecord once before it ends
        buttonPlay.interactable = false;
        yield return new WaitForSeconds(noteTimes[0]);
        //Debug.Log("Note List:"); 
        for (int i = 0; i < recordedNotes.Count; i++)
        {
            
            Notes[recordedNotes[i]].Play();
            // play click button shines
            if (recordedNotes[i] == 0)
            {
                buttonDoZero.GetComponent<ButtonShine>().buttonClicked();
            }
            else if (recordedNotes[i] == 1)
            {
                buttonRe.GetComponent<ButtonShine>().buttonClicked();
            }
            else if (recordedNotes[i] == 2)
            {
                buttonMi.GetComponent<ButtonShine>().buttonClicked();
            }
            else if (recordedNotes[i] == 3)
            {
                buttonFa.GetComponent<ButtonShine>().buttonClicked();
            }
            else if (recordedNotes[i] == 4)
            {
                buttonSol.GetComponent<ButtonShine>().buttonClicked();
            }
            else if (recordedNotes[i] == 5)
            {
                buttonLa.GetComponent<ButtonShine>().buttonClicked();
            }
            else if (recordedNotes[i] == 6)
            {
                buttonSi.GetComponent<ButtonShine>().buttonClicked();
            }
            else if (recordedNotes[i] == 7)
            {
                buttonDoOne.GetComponent<ButtonShine>().buttonClicked();
            }
            yield return new WaitForSeconds(noteTimes[i+1]);
            //Debug.Log("Note: " + recordedNotes[i]);           
        }
        buttonPlay.interactable = true;
    }

    public void deleteLastRecord(){
        PlayerPrefs.DeleteAll();
        recordedNotes.Clear();
        noteTimes.Clear();
    }


    // record button on/off
    public void recordNotes(){
        // record off
        if (recordFlag)
        {
            calculateNoteTime();
            recordFlag = false;
            // when record is done, store it last record
            PlayerPrefs.SetInt("recordedNotes_count", recordedNotes.Count);
            for (int i = 0; i < recordedNotes.Count; i++)
            {
                PlayerPrefs.SetInt("recordedNotes_" + i, recordedNotes[i]);
                //Debug.Log("recordedNotes_" + i + ": " + PlayerPrefs.GetInt("recordedNotes_" + i));
                PlayerPrefs.SetFloat("noteTimes_" + i, noteTimes[i+1]);
            }
            // noteTimes list is +1 greater than recordedNotes list
            PlayerPrefs.SetFloat("noteTimes_" + recordedNotes.Count, noteTimes[recordedNotes.Count + 1]);
        }
        // record on
        else
        {
            recordFlag = true;
            // delete the elements of the list before new record
            recordedNotes.Clear();
            noteTimes.Clear();
        }

    }

    // play notes for recording
    public void recDoZeroNote(){
        if (recordFlag)
        {
            recordedNotes.Add(0);
        }    
    }
    public void recReNote(){
        if (recordFlag)
        {
            recordedNotes.Add(1);
        }
    }
    public void recMiNote(){
        if (recordFlag)
        {
            recordedNotes.Add(2);
        }
    }
    public void recFaNote(){
        if (recordFlag)
        {
            recordedNotes.Add(3);
        }
    }
    public void recSolNote(){
        if (recordFlag)
        {
            recordedNotes.Add(4);
        }
    }
    public void recLaNote(){
        if (recordFlag)
        {
            recordedNotes.Add(5);
        }
    }
    public void recSiNote(){
        if (recordFlag)
        {
            recordedNotes.Add(6);
        }
    }
    public void recDoOneNote(){
        if (recordFlag)
        {
            recordedNotes.Add(7);
        }
    }

    // play notes in real-time
    public void playDoZeroNote(){
        Notes[0].Play();
    }
    public void playReNote(){
        Notes[1].Play();
    }
    public void playMiNote(){
        Notes[2].Play();
    }
    public void playFaNote(){
        Notes[3].Play();
    }
    public void playSolNote(){
        Notes[4].Play();
    }
    public void playLaNote(){
        Notes[5].Play();
    }
    public void playSiNote(){
        Notes[6].Play();
    }
    public void playDoOneNote(){
        Notes[7].Play();
    }

}
