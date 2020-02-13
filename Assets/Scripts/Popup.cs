using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{

    public GameObject dataPanel;
    public GameObject messagePanel;
    public GameObject emailMessagePanel;
    public GameObject buttonSend;


    public void Show()
    {
        dataPanel.SetActive(true);
        ClearField("InputFieldName");
        ClearField("InputFieldSurname");
        ClearField("InputFieldEmail");
        ClearField("InputFieldPhone");
        ClearField("InputFieldDescription");
        dataPanel.transform.SetAsLastSibling();
    }


    private string RetrieveField(string field)
    {
        GameObject inputFieldGo = GameObject.Find(field);
        InputField inputFieldCo = inputFieldGo.GetComponent<InputField>();
        return inputFieldCo.text;
    }


    private void ClearField(string field)
    {
        GameObject inputFieldGo = GameObject.Find(field);
        InputField inputFieldCo = inputFieldGo.GetComponent<InputField>();
        inputFieldCo.text = string.Empty;
    }


    public void Submit()
    {
        Person.Instance = new Person(RetrieveField("InputFieldName"),
            RetrieveField("InputFieldSurname"),
            RetrieveField("InputFieldEmail"),
            RetrieveField("InputFieldPhone"),
            RetrieveField("InputFieldDescription"));
        dataPanel.SetActive(false);
        messagePanel.SetActive(false);
        buttonSend.SetActive(true);
    }


    private void SendEmail()
    {
        messagePanel.SetActive(true);

        //MailMessage mail = new MailMessage();

        //mail.From = new MailAddress("ivan22meleshko@gmail.com");
        //mail.To.Add("ivan22meleshko@gmail.com");
        //mail.Subject = "Test Mail";
        //mail.Body = "This is for testing SMTP mail from GMAIL";

        //SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        //smtpServer.Port = 587;
        //smtpServer.Credentials = new System.Net.NetworkCredential("ivan22meleshko@gmail.com", "") as ICredentialsByHost;
        //smtpServer.EnableSsl = true;
        ////ServicePointManager.ServerCertificateValidationCallback =
        ////    delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        ////    { return true; };
        //smtpServer.Send(mail);
        //Debug.Log("success");
    }


    public void ShowKeyboard()
    {
        //TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, true);
    }

}
