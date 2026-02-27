using System;
using System.Collections;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

class AnnoucementsUI : MonoBehaviour
{
    [Required] [SerializeField] TextMeshProUGUI announcementText;
    
    [SerializeField] float announcementShowTime = 5f;
    [SerializeField] float timePerLetter = 0.05f; 

    private Coroutine typingCoroutine;
    
    void Start()
    {
        announcementText.gameObject.SetActive(false);
        GameManager.i.facilityAnnouncements.onAnnouncement.AddListener(ShowAnnouncement);
    }

    void ShowAnnouncement(FacilityAnnouncement announcement)
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        announcementText.gameObject.SetActive(true);
        typingCoroutine = StartCoroutine(TypeMessage(announcement.message));
    }

    IEnumerator TypeMessage(string message)
    {
        announcementText.text = "";
        
        foreach (char letter in message)
        {
            announcementText.text += letter;
            
            yield return new WaitForSeconds(timePerLetter);
        }

        yield return new WaitForSeconds(announcementShowTime);
        
        announcementText.gameObject.SetActive(false);
    }
}