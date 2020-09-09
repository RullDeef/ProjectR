using UnityEngine;
using UnityEngine.UI;

/** Simple MessageBox by Mihail)00
    
    Вызвать метод Init, чтобы "инициализировать" тексты сообщений,
    Show - показать messageBox
    
    Определить onYes - действие на левую кнопку (Yes), так же дописать в конце
        messageBox.Exit(); для закрытия messageBox
    onNo - по умолчанию закрывает messageBox

    Предпологается размещение этого объекта на сцене единожды и в месте где нужен messageBox,
    перетащить скрипт messageBox в свой скрипт

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
            Exit();
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

    public void Exit()
    {
        gameObject.SetActive(false);
    }
}
