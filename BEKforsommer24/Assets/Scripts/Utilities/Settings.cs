using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace Utilities {
    public class Settings : MonoBehaviour {
        [SerializeField] private TMP_InputField targetFrameRate;
        [SerializeField] private TMP_Dropdown displays;
        [SerializeField] private TMP_Dropdown screenModes;
        [SerializeField] private TMP_Dropdown aspectRatio;
        [SerializeField] private TMP_Dropdown resolution;
        
        [SerializeField] private SerializedDictionary<string, Vector2Int[]> aspectRatioResolutions = new() {
            {
                "16:9", new Vector2Int[] {
                    new(3840, 2160),
                    new(2560, 1440),
                    new(1920, 1080),
                    new(1600, 900),
                    new(1366, 768)
                }
            }, {
                "16:10", new Vector2Int[] {
                    new(2560, 1600),
                    new(1920, 1200),
                    new(1680, 1050),
                    new(1440, 900)
                }
            }, {
                "4:3", new Vector2Int[] {
                    new(1600, 1200),
                    new(1280, 1024),
                    new(1024, 768)
                }
            }, {
                "21:9", new Vector2Int[] {
                    new(3440, 1440),
                    new(2560, 1080)
                }
            }
        };
        
        private string _aspectRatio;
        
        private void Start() {
            if (displays != null) {
                displays.options = new List<TMP_Dropdown.OptionData>();
                
                for (var i = 0; i < Display.displays.Length; i++) {
                    displays.options.Add(new TMP_Dropdown.OptionData("Display " + i));
                }
                
                var savedDisplayIndex = PlayerPrefs.GetInt("UnitySelectMonitor", 0);
                displays.value = savedDisplayIndex;
                
                displays.onValueChanged.AddListener(displayIndex => {
                    if (displayIndex < Display.displays.Length) {
                        PlayerPrefs.SetInt("UnitySelectMonitor", displayIndex);
                        PlayerPrefs.Save();
                    }
                });
                
                displays.RefreshShownValue();
            }
            
            if (targetFrameRate) {
                targetFrameRate.contentType = TMP_InputField.ContentType.IntegerNumber;
                
                var targetFPS = PlayerPrefs.GetInt("TargetFrameRate");
                targetFrameRate.text = targetFPS.ToString();
                
                targetFrameRate.onValueChanged.AddListener(value => {
                    if (value != "") SetTargetFPS(int.Parse(value));
                });
            }
            
            if (screenModes) {
                screenModes.options = new List<TMP_Dropdown.OptionData> {
                    new("FullScreen"),
                    new("Borderless Window"),
                    new("Maximized Window"),
                    new("Windowed")
                };
                
                screenModes.onValueChanged.AddListener(value => {
                    Screen.fullScreenMode = value switch {
                        0 => FullScreenMode.ExclusiveFullScreen,
                        1 => FullScreenMode.FullScreenWindow,
                        2 => FullScreenMode.MaximizedWindow,
                        3 => FullScreenMode.Windowed,
                        _ => Screen.fullScreenMode
                    };
                    
                    PlayerPrefs.SetInt("FullScreenMode", value);
                });
                
                screenModes.value = PlayerPrefs.GetInt("FullScreenMode", 0);
            }
            
            if (aspectRatio) {
                aspectRatio.options = new List<TMP_Dropdown.OptionData> {
                    new("16:9"),
                    new("16:10"),
                    new("4:3"),
                    new("21:9")
                };
                
                aspectRatio.onValueChanged.AddListener(value => {
                    _aspectRatio = aspectRatio.options[value].text;
                    
                    resolution.options = new List<TMP_Dropdown.OptionData>();
                    aspectRatioResolutions.TryGetValue(_aspectRatio, out var i);
                    if (i != null) {
                        foreach (var variable in i) {
                            resolution.options.Add(new TMP_Dropdown.OptionData(variable.ToString()));
                        }
                    }
                    
                    resolution.RefreshShownValue();
                    
                    resolution.onValueChanged.Invoke(resolution.value);
                });
                
                var ratio = GetAspectRatio(new Vector2Int(Screen.width, Screen.height));
                ratio = ratio switch {
                    "8:5" => "16:10",
                    "7:3" => "21:9",
                    _ => ratio
                };
                
                var indexOfAspectRatio = new List<string>(aspectRatioResolutions.Keys).IndexOf(ratio);
                
                aspectRatio.value = indexOfAspectRatio != -1 ? indexOfAspectRatio : 0;
                aspectRatio.onValueChanged.Invoke(aspectRatio.value);
            }
            
            if (resolution) {
                resolution.onValueChanged.AddListener(value => {
                    aspectRatioResolutions.TryGetValue(_aspectRatio, out var i);
                    if (i != null) Screen.SetResolution(i[value].x, i[value].y, Screen.fullScreenMode);
                });
                

                var resolutions = aspectRatioResolutions[_aspectRatio];
                
                var currentResolution = new Vector2Int(Screen.width, Screen.height);
                var resolutionIndex = -1;
                for (var i = 0; i < resolutions.Length; i++) {
                    if (!resolutions[i].Equals(currentResolution)) continue;
                    
                    resolutionIndex = i;
                    break;
                }
                
                resolution.value = resolutionIndex != -1 ? resolutionIndex : 0;
                resolution.onValueChanged.Invoke(resolution.value);
            }
        }

        public void LoadScene(string sceneName) {
            SceneManager.LoadScene(sceneName);
        }
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        private static void Initialize() {
            var refreshRate = (int)Screen.currentResolution.refreshRateRatio.value;
            var targetFPS = PlayerPrefs.GetInt("TargetFrameRate", refreshRate);
            SetTargetFPS(targetFPS);
        }
        
        public static void SetTargetFPS(int targetFPS) {
            Application.targetFrameRate = targetFPS;
            PlayerPrefs.SetInt("TargetFrameRate", targetFPS);
        }
        
        public static string GetAspectRatio(Vector2Int resolution) {
            var gcd = GetGreatestCommonDivisor(resolution.x, resolution.y);
            var aspectWidth = resolution.x / gcd;
            var aspectHeight = resolution.y / gcd;
            return $"{aspectWidth}:{aspectHeight}";
        }
        
        public static int GetGreatestCommonDivisor(int a, int b) {
            while (a != 0 && b != 0) {
                if (a > b) a %= b;
                else b %= a;
            }
            
            return a | b;
        }
    }
}