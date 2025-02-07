using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson3Controller : MonoBehaviour
{

    [SerializeField] private string _value;
    [SerializeField] private List<string> _list;


    [ContextMenu("Print")]//Here is my 1 block - I added "Print" option to the Context Menu
    private void Print()
    {
        if (_list.Count == 0)
        {
            Debug.Log("The List is empty");
            return;
        }

        string msg = "List: ";
        for (int i = 0; i < _list.Count; ++i)
        {
            msg += $"\n{_list[i]}";
        } 

        Debug.Log(msg);
    }



    [ContextMenu("Add Item")]//Here is my 2 block - I added "Add Item" option to the Context Menu
    private void AddItem()
    {
        if(!string.IsNullOrEmpty(_value))
        {
            _list.Add(_value);
            Debug.Log("Added: " + _value);
        }
        else
        {
            Debug.Log("Error: I need a value");
        }
    }



    [ContextMenu("Remove Last Item")]//Here is my 3 block - I added "Remove Last Item" option to the Context Menu
    private void RemoveLastItem()
    {
        if (_list.Count > 0)
        {
            string removedLastItem = _list[_list.Count - 1];
            _list.RemoveAt(_list.Count - 1);
            Debug.Log("Removed: " + removedLastItem);

        }
        else
        {
            Debug.Log("The List is Empty");
        }

    }


    [ContextMenu("Remove Value")]//Here is my 4 block - I added "Remove Value" option to the Context Menu
    private void RemoveValue()
    {
       if (_list.Remove(_value))
        {
            Debug.Log("Removed: " + _value);
        }
        else
        {
            Debug.Log("Error: no Value here!");
        }

    }

    
      [ContextMenu("Sort List")]//Here is my 5 block - I added "Sort List" option to the Context Menu
    private void SortList()
    {
        if (_list.Count == 0)
        {
            Debug.Log("The list is empty. I can't sort it");
            return;
        }

        _list.Sort();
        Debug.Log("The list is sorted!");
        Print();
    }
    
    [ContextMenu("Clear List")]//Here is my 6 block - I added "Clear List" option to the Context Menu
    private void ClearList()
    {
        _list.Clear();
        Debug.Log("The List is cleared!");
    }

}