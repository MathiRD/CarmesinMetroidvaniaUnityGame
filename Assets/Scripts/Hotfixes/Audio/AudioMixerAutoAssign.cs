
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Audio;

namespace Metroidvania.Hotfixes.Audio
{
    /// <summary>
    /// Se o ScriptableObject GameAudioSettings estiver sem referência ao AudioMixer,
    /// este patch tenta atribuir automaticamente o primeiro AudioMixer encontrado no projeto.
    /// </summary>
    public static class AudioMixerAutoAssign
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Patch()
        {
            try
            {
                var mixers = Resources.FindObjectsOfTypeAll<AudioMixer>();
                var mixer = mixers.FirstOrDefault();
                if (mixer == null) return;

                var asm = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.GetName().Name == "Assembly-CSharp");
                if (asm == null) return;

                var t = asm.GetType("Metroidvania.Audio.GameAudioSettings");
                if (t == null) return;

                var assets = Resources.FindObjectsOfTypeAll(t);
                foreach (var asset in assets)
                {
                    var f = t.GetField("audioMixer", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    if (f != null && f.GetValue(asset) == null)
                    {
                        f.SetValue(asset, mixer);
                        Debug.Log("[Hotfix] AudioMixer atribuído automaticamente ao GameAudioSettings.");
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning($"[Hotfix] AudioMixerAutoAssign falhou: {e.Message}");
            }
        }
    }
}
