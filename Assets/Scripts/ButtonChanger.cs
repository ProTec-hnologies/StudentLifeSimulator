using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace LearnNamespace
{
    public class ButtonChanger : MonoBehaviour
    {
        public GameObject CheckToDoList;
        public GameObject PanelForCases;
        public GameObject EarlyEndButton;
        public GameObject BackToPreviousSceneButton;
        public GameObject PlayerResultPanel;
        public static bool IfEarlyEndButtonPressed;

        public void OnPressedStartButton()
        {
            CheckToDoList.SetActive(false);
            BackToPreviousSceneButton.SetActive(false);
            PlayerResultPanel.SetActive(false);
            PanelForCases.SetActive(true);
            EarlyEndButton.SetActive(true);
        }

        public void OnPressedEndButton() =>
            PlayerResultPanel.SetActive(false);

        void Start() =>
            PanelForCases.SetActive(false);
    }
}
