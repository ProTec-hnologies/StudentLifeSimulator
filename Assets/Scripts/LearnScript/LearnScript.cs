using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LearnNamespace
{
    public class LearnScript : MonoBehaviour
    {
        public GameObject EndGamePanel;
        public GameObject PanelForButtons;
        public GameObject TimeCounterPanel;
        public GameObject EnergyCounterPanel;
        public GameObject PanelForCases;
        public GameObject EarlyEndButton;
        public GameObject ShaurmaButton;
        public static bool IfFoodButtonPressed;
        public static bool IfEarlyEndButtonPressed;

        public static double CoefficientFatigue = 0.0;
        public static double ElapsedTime;
        public static GameObject currentObject;
        public static double Energy = 100.0;
        public static double OptimalTime = 581.4d;
        public static double MaxTime = 755.175d;

        /// <summary>
        /// активация/деактивация всех нужных нам панелей
        /// </summary>
        private void Awake()
        {
            EndGamePanel.SetActive(false);
            PanelForButtons.SetActive(true);
            TimeCounterPanel.SetActive(true);
            EnergyCounterPanel.SetActive(true);
            PanelForCases.SetActive(true);
            EarlyEndButton.SetActive(false);
        }

        /// <summary>
        /// Проверяет, нажата ли кнопка шаурмы
        /// </summary>
        public void PressedFoodButton() 
        {
            ElapsedTime += 30;
            ShaurmaButton.SetActive(false);

            if ((double)PerCent.OverMax - Energy <= 10)
                Energy = 100;

            if (Energy < (double)PerCent.OverMax && Energy > (double)PerCent.Max)
                Energy += 10;

            if (Energy < (double)PerCent.Max && Energy > (double)PerCent.Middle)
                Energy += 20;

            if (Energy < (double)PerCent.Middle && Energy > (double)PerCent.Min)
                Energy += 30;

            if (Energy < (double)PerCent.Min && Energy > (double)PerCent.LessThanMin)
                Energy += 40;

            else Energy += 0;
        }

        /// <summary>
        /// Проверяет, была ли нажата кнопка "Закончить"
        /// </summary>
        public static void PressedEarlyExitButton() => IfEarlyEndButtonPressed = true;

        /// <summary>
        /// Вызывается при клике на кнопку + указываем, какая кнопка нажата
        /// </summary>
        /// <param name="inputObject"></param>
        public void PressedCaseButton(GameObject inputObject)
        {
            currentObject = inputObject;

            var button = PanelForCasesElements.GetAllButtons(PanelForCases);

            UpdateData(button[TypeCase.BigCase], TimeToComplete.BigCase, FatigueFactor.BigCase);

            UpdateData(button[TypeCase.MiddleCase], TimeToComplete.MiddleCase, FatigueFactor.MiddleCase);

            UpdateData(button[TypeCase.SimpleCase], TimeToComplete.SimpleCase, FatigueFactor.SimpleCase);

            Debug.Log($"new coeff: {CoefficientFatigue}. " +
                $"\nElapsed Time: {ElapsedTime}. " +
                $"\nEnergy: {Energy}.");

            if (Energy <= 0) Debug.Log($"END of the game! :( ");
        }

        /// <summary>
        /// Функция рассчета затраченного времени
        /// </summary>
        private static double TimeFunction(double time) => time + (time * CoefficientFatigue);
        private static double EnergyFunction(double fatigu) => fatigu * 100;

        private double GetPercentValue(double less, double more)
            => Mathf.Ceil((float)(100 - ((less / more) * 100f)));

        /// <summary>
        /// Обновляет данные затраченного времени, энергии
        /// </summary>
        /// <param name="typeButtons">Лист кнопок определённого типа</param>
        /// <param name="time">Время, необходимое для выполнения данной задачи</param>
        /// <param name="fatigu">Значение усталости, необходимое для выполнения данной задачи</param>
        private static void UpdateData(List<GameObject> typeButtons, int time, double fatigu)
        {
            for (int j = 0; j < typeButtons.Count; j++)
            {
                if (currentObject == typeButtons[j])
                {
                    ElapsedTime += TimeFunction(time);
                    CoefficientFatigue += fatigu;
                    currentObject.SetActive(false);
                    Energy -= EnergyFunction(fatigu);
                }
            }
        }

        /// <summary>
        /// конвертирует значение double-минут в строку типа "[X] ч. [Y] мин."
        /// </summary>
        private static string TimeToString(double elTime)
        {
            int hours = (int)elTime / 60;
            int minutes = (int)elTime % 60;

            return hours.ToString() + " ч. " + minutes.ToString() + " мин.";
        }

        private void LateUpdate()
        {
            var gO = GameObject.FindWithTag("Energy_Counter");
            if (gO != null)
            {
                Text textComponent = gO.GetComponent<Text>();
                textComponent.text = $"Энергия: {Mathf.Round((float)Energy).ToString()}%";
            }

            var timeText = GameObject.FindWithTag("Time_Counter");
            if (timeText != null)
            {
                Text textComponent = timeText.GetComponent<Text>();
                textComponent.fontStyle = FontStyle.Bold;
                textComponent.text = $"Затраченное время: {TimeToString(ElapsedTime)}";
            }

            if (ElapsedTime >= MaxTime || IfEarlyEndButtonPressed)
                EndGameGeneralMethod();
        }

        private void EndGameGeneralMethod()
        {
            PanelForCases.SetActive(false);
            EnergyCounterPanel.SetActive(false);
            TimeCounterPanel.SetActive(false);
            EndGamePanel.SetActive(true);
            EarlyEndButton.SetActive(false);
            EndGameOutput();
        }

        /// <summary>
        /// Отвечает за вывод текста в окне "Конец игры"
        /// </summary>
        private void EndGameOutput()
        {
            var text = GameObject.FindWithTag("ConclusionInfo").GetComponentInChildren<Text>();

            text.text = $"Ваше затраченное время: {TimeToString(ElapsedTime)}\n" +
                $"Разница с оптимальным временем: {TimeToString(Mathf.Abs((float)ElapsedTime - (float)OptimalTime))}\n" +
                $"Процент успешности выполнения: {GetPercentValue(Mathf.Abs((float)ElapsedTime - (float)OptimalTime), OptimalTime)}%";
        }
    }
}
