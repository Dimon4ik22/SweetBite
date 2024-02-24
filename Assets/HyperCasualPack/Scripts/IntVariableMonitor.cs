using HyperCasualPack.ScriptableObjects;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace HyperCasualPack
{
	public class IntVariableMonitor : MonoBehaviour
	{
		[SerializeField] SaveableRuntimeIntVariable _monitorVariable;
		[SerializeField] TextMeshProUGUI _monitorText;

		void OnEnable()
		{
			_monitorVariable.ValueChanged += MonitorVariableOnValueChanged;
		}
		
		void OnDisable()
		{
			_monitorVariable.ValueChanged -= MonitorVariableOnValueChanged;
		}
		
		void MonitorVariableOnValueChanged(int obj)
		{
			_monitorText.text = obj.ToString();
		}

		[Button]
		private void SetGoldZero()
		{
			_monitorVariable.RuntimeValue = 0;
		}
        [Button]
        private void SetGoldPlus1000()
        {
            _monitorVariable.RuntimeValue += 1000;
        }
    }
}
