using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace JasonRPG
{
    public class Idle : IState
    {
        public void Tick()
        {
            Debug.Log( "Idle: Tick() called.");
        }

        public void OnEnter()
        {
            Debug.Log("Idle: Entered");
        }

        public void OnExit()
        {
            Debug.Log( "Idle: Exited");
        }
    }
}