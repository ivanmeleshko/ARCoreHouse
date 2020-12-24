using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenMenuAnim : MonoBehaviour
{

    public Image paintImage;
    private Sprite paintIcon, closeIcon;


    private void Awake()
    {
        paintIcon = Resources.Load<Sprite>("change-color-icon-7");
        closeIcon = Resources.Load<Sprite>("close");
    }


    public void RaiseMenu()
    {
        if (gameObject != null)
        {
            Animator animator = gameObject.GetComponent<Animator>();

            if (animator != null)
            {
                bool open = animator.GetBool("open");
                animator.SetBool("open", !open);

                paintImage.sprite = open ? paintIcon : closeIcon;
            }
        }
    }

}
