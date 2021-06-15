using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LearnNamespace
{
    public static class PanelForCasesElements
    {
        /// <summary>
        /// ������ � ���� ������, ���������� ������ �����
        /// </summary>
        public static GameObject PanelForCases;

        //������ �����, ��� ������ �������� ����� � ������ ������� ��� ������.
        //����� ��, ���� - �������� �����, ��������������� ��� ������� ����
        //�� ����� �������� ����, �������� �������� ���� gameObject
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
