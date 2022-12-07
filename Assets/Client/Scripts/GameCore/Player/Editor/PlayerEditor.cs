using UnityEditor;
using UnityEngine;

namespace Client.Editor
{
    [CustomEditor(typeof(PlayerBehaviour))]
    public class PlayerEditor : UnityEditor.Editor
    {
        private PlayerBehaviour _playerBehaviour;

        private void OnEnable()
        {
            _playerBehaviour = target as PlayerBehaviour;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("ApplyDamage"))
            {
                _playerBehaviour.Health -= 10;
            }
            if (GUILayout.Button("Heal"))
            {
                _playerBehaviour.Health += 10;
            }
            if (GUILayout.Button("ApplyEnergy"))
            {
                _playerBehaviour.Energy -= 10;
            }
            if (GUILayout.Button("HealEnergy"))
            {
                _playerBehaviour.Energy += 10;
            }
            
        }
        
        
        
    }
}