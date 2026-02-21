using System;
using System.Collections.Generic;
using UnityEngine;
using Oxide.Game.Rust.Cui;

namespace Oxide.Plugins
{
    [Info("Speedometer", "jerky", "1.0.0")]
    [Description("Displays vehicle speed in KTS and km/h (Rounded to integers with wave-jitter prevention)")]
    class Speedometer : RustPlugin
    {
        private Dictionary<ulong, Timer> playerTimers = new Dictionary<ulong, Timer>();

        private const float UpdateInterval = 0.4f; 
        private const float Deadzone = 0.35f;      
        private const string UiName = "SpeedometerUI";

        void OnEntityMounted(BaseMountable mountable, BasePlayer player)
        {
            if (player == null || mountable == null) return;
            StopPlayerTimer(player.userID);
            playerTimers[player.userID] = timer.Every(UpdateInterval, () => UpdateSpeedUI(player, mountable));
        }

        void OnEntityDismounted(BaseMountable mountable, BasePlayer player)
        {
            if (player == null) return;
            StopPlayerTimer(player.userID);
            CuiHelper.DestroyUi(player, UiName);
        }

        void UpdateSpeedUI(BasePlayer player, BaseMountable mountable)
        {
            if (player == null || !player.IsConnected || mountable == null)
            {
                StopPlayerTimer(player?.userID ?? 0);
                return;
            }

            var vehicle = mountable.GetParentEntity();
            if (vehicle == null) return;

            float speedMetric = vehicle.GetWorldVelocity().magnitude;
            string displayText;

            if (speedMetric < Deadzone)
            {
                displayText = "0 <size=14>KTS</size>\n<size=12>0 km/h</size>";
            }
            else
            {
                double knots = Math.Round(speedMetric * 1.94384, 0);
                double kmh = Math.Round(speedMetric * 3.6, 0);
                
                displayText = $"{knots} <size=14>KTS</size>\n<size=12>{kmh} km/h</size>";
            }

            ShowUI(player, displayText);
        }

        void ShowUI(BasePlayer player, string text)
        {
            CuiHelper.DestroyUi(player, UiName);
            var elements = new CuiElementContainer();

            elements.Add(new CuiLabel
            {
                Text = { 
                    Text = text, 
                    FontSize = 24, 
                    Align = TextAnchor.MiddleCenter, 
                    Color = "1 1 1 0.9",
                    FadeIn = 0.05f 
                },
                RectTransform = { 
                    AnchorMin = "0.45 0.12", 
                    AnchorMax = "0.55 0.22" 
                }
            }, "Hud", UiName);

            CuiHelper.AddUi(player, elements);
        }

        private void StopPlayerTimer(ulong userId)
        {
            if (playerTimers.TryGetValue(userId, out Timer t))
            {
                t.Destroy();
                playerTimers.Remove(userId);
            }
        }

        void Unload()
        {
            foreach (var player in BasePlayer.activePlayerList) CuiHelper.DestroyUi(player, UiName);
            foreach (var t in playerTimers.Values) t.Destroy();
            playerTimers.Clear();
        }
    }
}