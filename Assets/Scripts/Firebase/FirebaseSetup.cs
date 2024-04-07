using Firebase;
using UnityEngine;

namespace FirebaseSystem
{
    public class FirebaseSetup : MonoBehaviour
    {
        private FirebaseApp app;
        
        private void Start()
        {
            Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                var dependencyStatus = task.Result;
                if (dependencyStatus == Firebase.DependencyStatus.Available)
                {
                    app = Firebase.FirebaseApp.DefaultInstance;
                }
                else
                {
                    Debug.LogError(System.String.Format("Cloud not resolve all Firebase dependencies: {0}", dependencyStatus));
                }
            });
        }
    }
}
