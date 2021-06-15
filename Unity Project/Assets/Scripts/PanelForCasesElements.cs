using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LearnNamespace
{
    public static class PanelForCasesElements
    {
        /// <summary>
        /// хранит в себе панель, содержащее кнопки задач
        /// </summary>
        public static GameObject PanelForCases;

        //просто метод, при помощи которого можно с панели считать все кнопки.
        //опять же, ключ - ключевое слово, характеризующее тип данного дела
        //по ключу получаем лист, хранящий элементы типа gameObject
        public static Dictionary<string, List<GameObject>> GetAllButtons(GameObject PanelForCases)
        {
            var result = new Dictionary<string, List<GameObject>>();

            var listForBigCases = new List<GameObject>();
            var listForMiddleCase = new List<GameObject>();
            var listForSimpleCase = new List<GameObject>();

            for (int i = 0; i < PanelForCases.transform.childCount; i++)
            {
                var currentObject = PanelForCases.transform.GetChild(i).gameObject;
                var currentName = currentObject.name;

                if (currentName.Contains(TypeCase.BigCase))
                {
                    listForBigCases.Add(currentObject);
                    continue;
                }

                if (currentName.Contains(TypeCase.MiddleCase))
                {
                    listForMiddleCase.Add(currentObject);
                    continue;
                }

                if (currentName.Contains(TypeCase.SimpleCase))
                {
                    listForSimpleCase.Add(currentObject);
                    continue;
                }
            }

            result.Add(TypeCase.BigCase, listForBigCases);
            result.Add(TypeCase.MiddleCase, listForMiddleCase);
            result.Add(TypeCase.SimpleCase, listForSimpleCase);

            return result;
        }
    }
}
