using BepInEx;
using BepInEx.Unity.IL2CPP;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProximityChat
{
    [BepInPlugin("net.devante.sotf.proximitychat", "ProximityChat", "1.0.0")]
    public class Plugin : BasePlugin
    {
        mumblelib.MumbleLinkFile mumbleLink;
        private Timer gameStateCheckTimer;
        private Timer reportingTaskTimer;

        public override void Load()
        {
            // Plugin startup logic
            Log.LogInfo($"Plugin is loaded!");

            // Set up a timer to periodically check the game state every 5 seconds
            gameStateCheckTimer = new Timer(CheckGameState, null, TimeSpan.Zero, TimeSpan.FromSeconds(3));
        }

        private unsafe void CheckGameState(object state)
        {
            // var cState = Sons.Save.GameState.ToString;   -   OLD CODE

            if (TheForest.Utils.LocalPlayer.IsInWorld == true)
            {
                // await Task.Delay(1000);
                Log.LogInfo("Player now registered to be in the world.");

                // Run Mumble Setup
                mumbleLink = mumblelib.MumbleLinkFile.CreateOrOpen();
                mumblelib.Frame* frame = mumbleLink.FramePtr();
                frame->SetName("SoTF");
                frame->uiVersion = 2;
                string id = randomString(16);
                Log.LogInfo($"Setting Mumble ID to {id}");
                frame->SetID(id);
                Log.LogInfo($"Setting context to InWorld");
                frame->SetContext("InWorld");

                Log.LogInfo("Mumble Shared Memory Initialized");


                // Stop the game state check timer
                gameStateCheckTimer.Change(Timeout.Infinite, Timeout.Infinite);

                // Start the ReportingTask every second
                reportingTaskTimer = new Timer(FixedUpdated, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(12));
            }
            else
            {
                // Important for debugging in development builds.
                Log.LogInfo($"Currently not in a level. Reattempting.. (false)");
            }
        }

        private static System.Random random = new System.Random();
        private string randomString(int len)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, len)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        private unsafe void FixedUpdated(object state)
        {

            // Check if Player left the expedition to prevent game crashing.
            if (TheForest.Utils.LocalPlayer.IsInWorld == false && SceneManager.GetSceneByName("SonsMain").IsValid() == false)
            {
                Log.LogInfo($"Expedition Aborted, Closing Link Connection.");
                // Stop sending data to Mumble
                reportingTaskTimer.Change(Timeout.Infinite, Timeout.Infinite);

                // Start checking Gamestate again
                gameStateCheckTimer = new Timer(CheckGameState, null, TimeSpan.Zero, TimeSpan.FromSeconds(3));

                // Close Mumble Link Connection
                mumbleLink.Dispose();
                mumbleLink = null;

                return;
            }

            // Execute the code to get player variables and output them to the console
            var character = TheForest.Utils.LocalPlayer.Transform;
            var position = character.position - new Vector3(0, 1, 0);
            var rotation = character.rotation;

            // OldCamera
            //    Transform camera = GameObject.Find("FPSCameraHolder_PlayerLocal(Clone)")?.transform;

            // Convert Vector3 components to strings - Only needed if debug outputs are uncommented.
            //    string positionString = $"({position.x}, {position.y}, {position.z})";
            //    Log.LogInfo($"Player Position: {positionString}");

            if (character != null)
            {
                //   Log.LogInfo($"Everything is set. (!= null).");
                if (mumbleLink == null)
                {
                    Log.LogInfo($"Initializing Load(). (mumbleLink == null).");
                    Load();
                }

                mumblelib.Frame* frame = mumbleLink.FramePtr();

                frame->fCameraPosition[0] = position.x;
                frame->fCameraPosition[1] = position.y;
                frame->fCameraPosition[2] = position.z;

                frame->fCameraFront[0] = character.forward.x;
                frame->fCameraFront[1] = character.forward.y;
                frame->fCameraFront[2] = character.forward.z;

                frame->fAvatarPosition[0] = position.x;
                frame->fAvatarPosition[1] = position.y;
                frame->fAvatarPosition[2] = position.z;

                frame->fAvatarFront[0] = character.forward.x;
                frame->fAvatarFront[1] = character.forward.y;
                frame->fAvatarFront[2] = character.forward.z;

                frame->uiTick++;
            }
            else
            {
                if (mumbleLink != null)
                {
                    Log.LogInfo($"Closing Link Connection.");
                    mumbleLink.Dispose();
                    mumbleLink = null;
                    return;
                }
                Log.LogInfo($"An error has occurred.");
            }
        }
        public interface LinkFileFactory
        {
            LinkFile Open();
        }

        public interface LinkFile : IDisposable
        {
            uint UIVersion { set; }
            void Tick();
            Vector3 CharacterPosition { set; }
            Vector3 CharacterForward { set; }
            Vector3 CharacterTop { set; }
            string Name { set; }
            Vector3 CameraPosition { set; }
            Vector3 CameraForward { set; }
            Vector3 CameraTop { set; }
            string ID { set; }
            string Context { set; }
            string Description { set; }
        }
    }
}
