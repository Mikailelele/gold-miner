using System;
using _Project.ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace _Project.Core.Level
{
    public interface ILevelService
    {
        GameObject CurrentLevel { get; }

        int CurrentLevelIndex { get; }
        
        void LoadLevel(int levelIndex);
        void RestartLevel();
        
        event Action OnLevelLoadedAction; 
    }
    
    public sealed class LevelService : ILevelService, IStartable
    {
        public GameObject CurrentLevel { get; private set; }
        
        private LevelsSettingsSo _levelsSettingsSo;
        
        private IObjectResolver _objectResolver;
        
        public int CurrentLevelIndex { get; private set; }

        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public event Action OnLevelLoadedAction = delegate { };
        
        [Inject]
        private void Construct(LevelsSettingsSo levelsSettingsSo)
        {
            _levelsSettingsSo = levelsSettingsSo;
            
            this.LogInjectSuccess();
        }
        
        public void Start()
        {
            LoadLevel(0);
        }
        
        public void LoadLevel(int levelIndex)
        {
            CurrentLevelIndex = levelIndex;
            
            CurrentLevel = Object.Instantiate(_levelsSettingsSo.Data[CurrentLevelIndex].LevelPrefab);
            
            OnLevelLoadedAction.Invoke();
        }
    }
}