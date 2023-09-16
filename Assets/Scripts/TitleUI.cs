using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TitleUI : MonoBehaviour
{
    [SerializeField] GameObject manualPopup;
    [SerializeField] GameObject playerInfoPopup;
    [SerializeField] Button realStartButton;
    [SerializeField] TMP_InputField inputName;
    [SerializeField] TMP_InputField inputNumber;
    [SerializeField] GameObject rankPannel;
    [SerializeField] GameObject noPlayerDataPopup;
    [SerializeField] GameObject questionKindPopup;
    [SerializeField] TextMeshProUGUI questionKindText;

    private string playerName = null;
    private string playerNumber = null;

    void Start()
    {
        realStartButton.interactable = false;
        inputName.onEndEdit.AddListener(delegate { LoadNameInput(inputName); });
        inputNumber.onEndEdit.AddListener(delegate { LoadNumberInput(inputNumber); });
    }

    private void LoadNameInput(TMP_InputField input)
    {
        if (input.text.Length > 0)
        {
            playerName = input.text;
        }
        else if (input.text.Length == 0)
        {
            Debug.Log("Name Input Empty");
        }

        ActiveStartButton();
    }

    private void LoadNumberInput(TMP_InputField input)
    {
        if (input.text.Length > 0)
        {
            playerNumber = input.text;
        }
        else if (input.text.Length == 0)
        {
            Debug.Log("Number Input Empty");
        }

        ActiveStartButton();
    }

    private void ActiveStartButton()
    {
        if (playerName != null && playerNumber != null)
            realStartButton.interactable = true;
        else realStartButton.interactable = false;
    }

    public void InteractQuestionKindPopup(bool open)
    {
        if (open)
        {
            playerInfoPopup.SetActive(false);
            if (!CanOpenPopup()) { Debug.Log("Hello"); return; }
        }

        questionKindPopup.SetActive(open);
    }

    public void SendPlayerDataToGameManager()
    {
        GameManager.Instance.CreateNewPlayer(playerName, playerNumber);
        GameManager.Instance.ChangeScene("PlayScene");
        playerName = "";
        playerNumber = "";
    }

    public void InteractManualPopup(bool open)
    {
        if(open) if (!CanOpenPopup()) return;
        manualPopup.SetActive(open);
    }

    public void InteractPlayerInfoPopup(bool open)
    {
        if (open)
        {
            if (!CanOpenPopup()) return;
            inputName.text = "";
            inputNumber.text = "";
            playerName = null;
            playerNumber = null;
        }

        playerInfoPopup.SetActive(open);

        ActiveStartButton();
    }

    public void InteractRankPannel(bool open)
    {
        if (open) if (!CanOpenPopup()) return;

        rankPannel.SetActive(open);
    }

    public void ChangeQuestionKindText()
    {
        QuestionKind kind = GameManager.Instance.QuestionKind;
        switch (kind)
        {
            case QuestionKind.Math:
                { questionKindText.text = "수학"; break; }
            case QuestionKind.History:
                { questionKindText.text = "역사"; break; }
            case QuestionKind.CommonSense:
                { questionKindText.text = "상식"; break; }
            default: break;
        }
    }

    private bool CanOpenPopup()
    {
        if (manualPopup.activeInHierarchy == false && noPlayerDataPopup.activeInHierarchy == false &&
            playerInfoPopup.activeInHierarchy == false && rankPannel.activeInHierarchy == false)
            return true;
        else return false;
    }
}
