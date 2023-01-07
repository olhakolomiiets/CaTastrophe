     using UnityEngine;
     using UnityEngine.UI;
     
     public class triggerButtonWithKey : MonoBehaviour
     {
         public KeyCode key;
         private Button _buttonDo;

         private void Start()
         {
            _buttonDo = GetComponent<Button>();
         }
     
         void Update()
         {
            if (Input.GetKeyDown(key))
             {
                 _buttonDo.onClick.Invoke();
             }
         }
     }