<h1 align="center">Drone Harvesting — Тестовое задание Unity C# Developer</h1>

<p align="center"><b>Проект тестового задания — симуляция сбора ресурсов дронами на 3D сцене.</b></p>

## 🎯 Задание

Две команды дронов (красная и синяя) соревнуются за сбор ресурсов, которые спавнятся в случайных местах сцены.

Каждый дрон:
1. Находит ближайший свободный ресурс.
2. Двигается к нему, избегая столкновений с другими дронами.
3. Собирает ресурс (2 секунды).
4. Возвращается на свою базу.
5. Выгружает ресурс (визуальный эффект).
6. Повторяет цикл.

- Визуальные эффекты:
  - Вспышка при выгрузке ресурса.
  - Пульсация/масштаб дрона.
  - Отображение пути.
    
## ▶️ Видео демонстрация

<p align="Left">  
<b>Видео на RuTube</b><br/>
</p>
<p align="center">
https://rutube.ru/video/1dd4cf6f527fadd8cb7f167819b7f3fd/
</p>

<p align="Left">  
<b>Или посмотреть на Google Drive</b><br/>
</p>
<p align="center">
https://drive.google.com/file/d/1n0vKd-tU5XcfrAmWUYY658zPdWG-H0vQ/view?usp=sharing


## 🎥 Скриншоты
<p align="center">
  <img src="https://redleggames.com/Games/DroneHarvesting/New/Screen_01.jpg" width="300"/>
  <img src="https://redleggames.com/Games/DroneHarvesting/New/Screen_02.jpg" width="300"/>
  <img src="https://redleggames.com/Games/DroneHarvesting/New/Screen_03.jpg" width="300"/>
  <img src="https://redleggames.com/Games/DroneHarvesting/New/Screen_04.jpg" width="300"/>
  <img src="https://redleggames.com/Games/DroneHarvesting/New/Screen_05.jpg" width="300"/>
  <img src="https://redleggames.com/Games/DroneHarvesting/New/Screen_06.jpg" width="300"/>
  <img src="https://redleggames.com/Games/DroneHarvesting/New/Screen_06.jpg" width="300"/>
</p>

##  🧾 Пояснения
Я решил не реализовывать два пункта из списка дополнительных задач:

1. Изменение скорости симуляции
   
Изменение скорости дронов уже позволяет влиять на темп игры. Кроме того, можно было бы использовать Time.timeScale для глобального ускорения/замедления, как это было реализовано в моём предыдущем проекте *[Ultimate Admiral: Dreadnoughts](https://store.steampowered.com/app/1069660/Ultimate_Admiral_Dreadnoughts/)* — там можно было ускорять бой или ставить его на паузу. При ускорении быстрее летели пули, двигались корабли и происходили все анимации. 
<p align="Left">
<img src="https://redleggames.com/Games/DroneHarvesting/UAD_TimeScale.png" width="150"/>
</p>

2. Миникарта / навигация по карте
   
В том же проекте с кораблями я реализовал полноценную миникарту на глобальной карте, где игрок мог перемещать видимую область камеры. Это была востребованная механика, которая понравилась игрокам. В данном проекте я решил не дублировать функциональность, чтобы сфокусироваться на логике дронов и архитектуре.
<p align="Left"> 
<img src="https://redleggames.com/Games/DroneHarvesting/UAD_MiniMap_01.png" width="200"/>
<img src="https://redleggames.com/Games/DroneHarvesting/UAD_MiniMap_02.png" width="200"/>
</p>

*Описание проекта и сцены*

Старт игры осуществляется через Services/EntryPoint.cs — он сначала инициализирует сервис спавна ресурсов, затем запускает процесс появления дронов на сцене.
Каждый компонент в проекте строго отвечает за свою зону ответственности.

В иерархии сцены, в Services/SceneContext, расположены все Zenject-инсталлеры (GameplayInstaller, SignalsInstaller, FXInstaller), которые внедряют зависимости во все системы.

*DronService и ResourceService*

Services/DronService содержит спавнер дронов и систему выделения дронов (по клику).
Services/ResourceService управляет не только спавном ресурсов, но и поиском ближайшего ресурса по заданной позиции. Я не стал называть его «спавнером», так как он выполняет больше логики, чем просто генерация.

*Логика дронов*

Сами дроны организованы по принципу разделения ответственности:
Все компоненты, отвечающие за движение, визуальное отображение и навигацию, находятся в префабе дрона и его дочерних объектах.
Класс Drone.cs управляет инициализацией, деспавном и активацией состояния дрона (FSM).
Также он предоставляет доступ к зависимым сервисам для текущего состояния.

## 🛠 Используемые технологии

| Система           | Использование                                                                                       |
|-------------------|-----------------------------------------------------------------------------------------------------|
| `Unity`           | 2022.3.23f1 (URP)                                                                                   |
| `Паттерны`        | Bootstrap, EntryPoint, State Machine паттерн для логики дронов                                      |
| `Zenject`         | Зависимости между классами, Пул дронов, Ресурса, эффектов и сигналы для общения между классами в UI |
| `DoTween`         | Анимации сбора и выгрузки ресурсов                                                                  |
| `NavMesh`         | Unity NavMesh (AI) - Поиск пути и движение дронов и избегании столкновений                          |
| `ScriptableObject`| Настройки вынесены в файл (подробности ниже)                                                        |
| `LineRenderer`    | Визуализации пути дронов                                                                            |


## 📁 Структура проекта
<pre> ```Assets/
├── Materials/             # Тестовые материалы и текстуры. QuickOutline материалы.
│
├── Prefab/                # Префабы Дронов, Собираемого ресурса и эффекта выгрузки. А также DroneHarvestingGameSettings - настройки игры (подробности чуть ниже)
│
├── Resources/             # DOTweenSettings и URP assets
│
├── Scenes/                # Все сцены игры 
│   ├── Bootstrap          # Разгоночная сцена, содержит только загрузочный экран, с неё запускаются все остальные сцены
│   └── Gameplay           # Сцена с геймплеем
│
├── Scripts/
│   ├── Base/              # Скрипт Базы дронов
│   ├── Drone/             # Скрипты принадлежащие Дронам: Данные, Пул дронов, Сервис создания и удаления, Выбор внешнего вида от команды
│   │  └── UI              # Отображения состояния Дрона и Выделение Дрона
│   │  └── DroneStateMachi # State Состояния поведения Дронов
│   ├── EntryPoint/        # Bootstrap и EntryPoint скрипты
│   ├── FX/                # Пул эффектов и сам эффект
│   ├── Installers/        # Zenject Installer'ы
│   ├── Resources/         # Скрипты для объектов Собираемых ресурсов и их пула
│   ├── Settings/          # Scriptable Object настроек
│   ├── Signals/           # Zenject сигналы для реагирования на тустовый UI, для примера
└── └── UI/                # Управление тестовым UI
``` </pre>
---

# Scriptable Object - DroneHarvestingGameSettings
Содержит настройки для кастомизации игры:

| Поле                    | Описание                                      |
|-------------------------|-----------------------------------------------|
| `TotalDroneCount`       | Выбор максимального количества дронов в сцене |
| `DronePrefab`           | Префаб Дрона - общий для обеих команд         |
| `ResourcePrefab`        | Префаб собираемого ресурса                    |
| `BlueTeamMaterial`      | Материал с цветом для Синей команды           |
| `RedTeamMaterial`       | Материал с цветом для Красной команды         |

<img src="https://redleggames.com/Games/DroneHarvesting/DroneHarvestingGameSettings.png"/>


## Как запустить

Скачай архив, распакуй и запусти готовый Build:

https://github.com/DjKarp/DroneHarvesting/releases

скачать с моего сайта -> 
https://redleggames.com/Games/DroneHarvesting/DroneHarvesting.zip

скачать с Google Drive -> 
https://drive.google.com/file/d/1xciQAumFeCiucPvHKV17yqReFyZrr9yI/view?usp=sharing


Склонируй проект:

git clone https://github.com/DjKarp/DroneHarvesting.git

Открыть в Unity 2022.3+ (URP)

Установить Zenject, DOTween, TMPro, NavMesh

Сцена запуска: Bootstrap, Gameplay

Играй! 🎉
