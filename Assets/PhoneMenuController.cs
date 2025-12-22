using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PhoneMenuController : MonoBehaviour
{
    public GameObject PhoneMenu;
    public GameObject message;
    public GameObject note;
    public GameObject noteapptext;
    public GameObject backButton;
    public GameObject nextButton;
    public GameObject noteappname;
    public GameObject exitButton;
    public GameObject exitButton2;
    public GameObject MessengerBox1;
    public GameObject MessengerBox2;
    public GameObject MessengerBox3;
    public GameObject PlayerMessageBox;
    public GameObject NextLineButton;
    public MessengerMenu messengerMenu;
    public GameObject phonecallicon;
    public GameObject phonecallpage;
    public GameObject gcname;
    [SerializeField] GameTime gameTime;
    
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (PhoneMenu.activeInHierarchy == false)
            {
                ShowMenu();
                
            }
            else
            {
                CloseMenu();
            }
        }
    }

    public void ShowMenu()
    {
        
        gameTime.PauseTime();
        gcname.SetActive(false);
        PhoneMenu.SetActive(true);
        noteapptext.SetActive(false);
        noteappname.SetActive(false);
        //backgroundImage.raycastTarget = true; // Enable raycast interaction
        message.SetActive(true);
        note.SetActive(true);
        exitButton.SetActive(false);
        exitButton2.SetActive(false);
        nextButton.SetActive(false);
        backButton.SetActive(false);
        MessengerBox1.SetActive(false);
        MessengerBox2.SetActive(false);
        MessengerBox3.SetActive(false);
        NextLineButton.SetActive(false);
        PlayerMessageBox.SetActive(false);
        phonecallicon.SetActive(true);
        phonecallpage.SetActive(false);
    }
    public void CloseMenu()
    {
        phonecallicon.SetActive(false);
        backButton.SetActive(false);
        gameTime.ResumeTime();
        PhoneMenu.SetActive(false);
        noteapptext.SetActive(false);
        noteappname.SetActive(false);
        //backgroundImage.raycastTarget = true; // Enable raycast interaction
        message.SetActive(false);
        note.SetActive(false);
        
        exitButton.SetActive(false);
        nextButton.SetActive(false);
    }
}
