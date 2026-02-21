# Speedometer for Rust
![202602~4](https://github.com/user-attachments/assets/17642c58-7b58-4b0f-8039-79890de2e633)

A lightweight Oxide (uMod) plugin that displays real-time speed while operating any vehicle in Rust.

## Overview
Speedometer provides players with an on-screen HUD showing their current velocity. This is particularly useful for naval navigation, flight control, and land vehicle testing.

## Verified with Tugboat
The calculation logic of this plugin is specifically calibrated and verified using the official Rust **Tugboat**.
- **Accuracy:** When the Tugboat's internal analog gauge indicates **22 KTS**, this plugin accurately displays **22 KTS / 41 km/h**.
- **Consistency:** Ensures that the digital HUD matches the immersive physical instruments in the game.

## Key Features
- **Dual Unit Display:** Shows speed in both KTS (Knots) and km/h (Kilometers per hour) simultaneously.
- **Integer Precision:** Values are rounded to the nearest integer (四捨五入) for a clean, flicker-free display.
- **Wave-Jitter Prevention:** Includes a built-in "Deadzone" (0.35m/s). This ensures the speedometer stays at "0" when docked or stationary, ignoring minor movements caused by ocean waves.
- **Automatic HUD:** The display automatically appears when you mount a vehicle/horse and disappears when you dismount.

## UI Layout
The information is displayed at the bottom-center of the player's HUD:  
22 KTS  
41 km/h

## Installation
1. Download `Speedometer.cs`.
2. Place the file into your server's `oxide/plugins` directory.
3. The plugin will compile and load automatically. No configuration is required.

---

# Speedometer (日本語)

Rustの乗り物に乗っている間、現在の速度を画面に表示する軽量なプラグインです。

## 概要
船、ヘリコプター、車両、馬などに乗っている際、現在の速度をリアルタイムでHUDに表示します。航行や飛行の管理、車両の性能測定に最適です。

## タグボートでの検証済み
本プラグインの計算ロジックは、Rust公式の**タグボート（Tugboat）**に搭載されているアナログ速度計を用いて正確に検証されています。
- **正確性:** タグボートの計器が **22 KTS** を指しているとき、本プラグインでも **22 KTS / 41 km/h** と正確に一致するように調整されています。
- **整合性:** ゲーム内の視覚的な計器とデジタル表示が完全に同期します。

## 主な機能
- **二重単位表示:** KTS（ノット）と km/h（キロメートル毎時）を上下に併記します。
- **整数表示:** 数値を四捨五入して表示することで、コンマ単位のガタつきを抑え、高い視認性を確保しています。
- **波の揺れ防止機能:** デッドゾーン（0.35m/s）を設定しているため、停車中に波で船体が揺れても表示が「0」から動かないよう最適化されています。
- **自動表示切替:** 乗り物に乗ると自動で表示され、降りると自動で消去されます。

## インストール方法
1. `Speedometer.cs` をダウンロードします。
2. サーバーの `oxide/plugins` フォルダに配置します。
3. プラグインが自動的に読み込まれ、設定不要ですぐに使用可能です。
