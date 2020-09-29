using UnityEngine;
using UnityEngine.UI;

/** Simple MessageBox by Mihail)00
    
    Вызвать метод Init, чтобы "инициализировать" тексты сообщений,
    Show - показать messageBox
    
    Определить onYes - действие на левую кнопку (Yes), так же дописать в конце
        messageBox.Close(); для закрытия messageBox
    onNo - по умолчанию закрывает messageBox

**/
public class MessageBox : MonoBehaviour
{
    public delegate void OnYes();
    public delegate void OnNo();
    public OnYes onYes;
    public OnNo onNo;

    public Text _text, _yes, _no;

    public void Init(string text, string yes, string no)
    {
        _text.text = text;
        _yes.text = yes;
        _no.text = no;

        onNo = () =>
        {
            Close();
        };
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Yes()
    {
        if (onYes != null)
            onYes();
    }

    public void No()
    {
        if (onNo != null)
            onNo();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
