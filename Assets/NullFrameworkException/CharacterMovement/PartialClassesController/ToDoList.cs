using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NullFrameworkException.CharacterMovement.PartialClassesController
{
    /// <summary>
    /// This will just track the to do list of the scene.
    /// </summary>
    public class ToDoList : MonoBehaviour
    {
    #region struct ToDoListItem
        /// <summary>
        /// An task to be done and if completed.
        /// </summary>
        [Serializable] private struct ToDoListItem
        {
            /// <summary>
            /// The item to do.
            /// </summary>
            [TextArea(3,10)]public string toDoItem;
            /// <summary>
            /// Has it been completed.
            /// </summary>
            public bool isItCompleted;
        }
    #endregion
        /// <summary>
        /// A List of all the items to be done.
        /// </summary>
        [SerializeField] private ToDoListItem[] toDoList;
        private void Start() => PrintToDoList(toDoList);

        /// <summary>
        /// Debug.log all the items to be done in the to do list.
        /// </summary>
        /// <param name="_toDoListItems"> The list of items to be desplayed in a Debug.log.</param>
        private void PrintToDoList(ToDoListItem[] _toDoListItems)
        {
            Debug.Log($"---!!!--- TASKS STILL TO DO ---!!!---");
            
            for(int i = 0; i < _toDoListItems.Length; i++)
                Debug.Log(!_toDoListItems[i].isItCompleted ? $"To Do --- {_toDoListItems[i].toDoItem}" : $"COMPLETED --- {_toDoListItems[i].toDoItem}");
        }
    }
}