# ПРОГРЕСС ОБУЧЕНИЯ - Unity Multiplayer Arena

## ИНФОРМАЦИЯ ОБ УЧЕНИКЕ

- **Имя:** SergeyKushnarev
- **Уровень C#:** Начальный -> Базовый (прошёл 5 уроков)
- **Цели:** 
  - Изучить C# на примерах проекта (Фаза 1 завершена)
  - Добавить мультиплеер (Фаза 2 — следующий шаг)
  - Билд на Android

---

## РОЛЬ CLAUDE В ПРОЕКТЕ

Сеньор Unity C# разработчик с 15 лет опыта, **team lead**.
- Обучает и объясняет всё по ходу работы
- Направляет архитектурные решения
- Помогает довести игру до мультиплеера на Android
- Формат: теория -> домашка -> проверка -> следующий урок

---

## ФАЗА 1 — ОБУЧЕНИЕ C# (ЗАВЕРШЕНА 2026-04-09)

### Урок 1: Основы C# (GameManager) — ЗАЧЁТ
- Классы, наследование (`: MonoBehaviour`)
- Переменные и типы (`int`, `Transform`, массивы `[]`)
- Модификаторы доступа (`public`, `private`)
- `[SerializeField]` — инкапсуляция
- Методы, `GetComponent<T>()`, `Time.timeScale`
- Домашка: `Debug.Log`, `deathCount`, `GetDeathCount()` — выполнена

### Урок 2: PlayerMovement — ЗАЧЁТ
- `Start()` vs `Update()`
- `Time.deltaTime` — независимость от FPS
- `Input.GetAxis`, `Input.GetButtonDown`
- `Rigidbody`, `Vector3`, физика движения
- `Raycast` для проверки земли, `LayerMask`
- Домашка: исправить `public` на `[SerializeField] private` — выполнена

### Урок 3: PlayerHealth + Корутины — ЗАЧЁТ
- Корутины (`IEnumerator`, `yield return`)
- `WaitForSecondsRealtime` vs `WaitForSeconds`
- UI Slider, `SetActive`, курсор
- Домашка: модификаторы + порядок переменных + вопросы — выполнена

### Урок 4: PlayerThrowing — ЗАЧЁТ
- `[Header()]` — группировка в инспекторе
- `Mathf.Lerp(a, b, t)`, `Mathf.Clamp01`
- `Instantiate`, `isKinematic`, `Physics.IgnoreCollision`
- LineRenderer, формула параболы `S = S0 + Vt + 0.5gt^2`
- Домашка: модификаторы + вопросы по Lerp — выполнена

### Урок 5: Snowball + Target + TurretShooting — ЗАЧЁТ
- `Destroy(gameObject)` vs `Destroy(gameObject, delay)`
- `OnCollisionEnter` — обработка столкновений
- Паттерн "таймер" в Update
- Лишние `using` — удалять если не используются
- `Vector3.Distance` для проверки расстояния
- Домашка: модификаторы + удалить лишние using — выполнена

### Все 9 скриптов разобраны и исправлены:
- GameManager.cs, PlayerMovement.cs, PlayerHealth.cs, PlayerThrowing.cs
- Snowball.cs, Target.cs, TurretShooting.cs, CameraFollow.cs, MainMenu.cs

---

## ФАЗА 2 — МУЛЬТИПЛЕЕР (СЛЕДУЮЩИЙ ШАГ)

Выбран фреймворк: **Photon PUN 2**

### План:
1. Установка Photon PUN 2 SDK
2. Lobby — подключение к серверу, создание/вход в комнату
3. Спавн игроков по сети
4. Синхронизация движения, бросков, HP
5. Мобильное управление для Android
6. Билд и тестирование

---

**Последнее обновление:** 9 апреля 2026
