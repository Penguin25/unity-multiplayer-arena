# 🎓 УРОК 1: Основы C# на примере GameManager

## 1. ЧТО ТАКОЕ КЛАСС?

Класс — это как чертёж дома. Он описывает, что у дома есть (стены, двери) и что оно может делать (открывать дверь).

В Unity каждый скрипт — это класс, который становится **компонентом** на объекте.

```csharp
public class GameManager : MonoBehaviour
```

- `public` — значит этот класс виден всем (другим скриптам)
- `class GameManager` — имя класса
- `: MonoBehaviour` — **наследование**. Мы говорим: "GameManager — это особый тип MonoBehaviour"
  - MonoBehaviour даёт нам `Start()`, `Update()`, `SerializeField` и другие Unity-фичи

---

## 2. ПЕРЕМЕННЫЕ И ТИПЫ ДАННЫХ

```csharp
[SerializeField] private Transform[] spawnPoints;
```

Разберём по частям:

### `Transform[]` — это массив (array)
- `Transform` — компонент Unity, хранит позицию, поворот, размер объекта
- `[]` — значит это **массив**, т.е. много Transform'ов
- Массив — как шкаф с пронумерованными полками: `[0]`, `[1]`, `[2]`...

### `private` — модификатор доступа
- `private` = только этот класс может видеть переменную
- `public` = все могут видеть (из других скриптов)
- В Unity лучше делать всё `private` и открывать доступ через `[SerializeField]`

### `[SerializeField]` — атрибут Unity
- Говорит Unity: "покажи эту private переменную в инспекторе"
- **Почему не public?** 
  - `public` — другие скрипты могут менять (плохо для инкапсуляции)
  - `[SerializeField] private` — видно в инспекторе, но другие скрипты не могут менять напрямую

**ПРАВИЛО:** Всегда используй `[SerializeField] private` вместо `public` для переменных, которые нужно видеть в инспекторе!

---

## 3. МЕТОДЫ (ФУНКЦИИ)

```csharp
void Start()
{
    int index = Random.Range(0, spawnPoints.Length);
    player.position = spawnPoints[index].position;
}
```

### `void Start()`
- `void` — метод ничего не возвращает
- `Start()` — имя метода, вызывается ОДИН раз при старте сцены
- `()` — скобки, могут содержать параметры

### Что делает код внутри:

```csharp
int index = Random.Range(0, spawnPoints.Length);
```

- `int` — целое число (integer)
- `index` — имя переменной
- `=` — присваивание (НЕ равенство!)
- `Random.Range(0, spawnPoints.Length)` — случайное число от 0 до длины массива

**Пример:** Если `spawnPoints.Length = 5`, то `index` будет 0, 1, 2, 3 или 4.

```csharp
player.position = spawnPoints[index].position;
```

- Берём случайную точку спавна
- Ставим игрока на эту позицию

---

## 4. ПУБЛИЧНЫЕ МЕТОДЫ

```csharp
public void Respawn()
{
    // ...
}
```

- `public` — другие скрипты могут вызывать этот метод
- `void` — ничего не возвращает
- `Respawn()` — имя метода

**Пример вызова из другого скрипта:**
```csharp
// В Snowball.cs при попадании:
gameManager.Respawn();  // ❌ Неправильно — если gameManager null!

// Правильно:
if (gameManager != null)
{
    gameManager.Respawn();
}
```

---

## 5. COMPONENT ACCESS (ПОЛУЧЕНИЕ КОМПОНЕНТОВ)

```csharp
PlayerHealth ph = player.GetComponent<PlayerHealth>();
ph.ResetHealth();
```

- `GetComponent<T>()` — находит компонент типа T на объекте
- `<PlayerHealth>` — это **дженерик** (обобщённый тип), уточняем ЧТО ищем
- Сохраняем в переменную `ph`, чтобы не вызывать дважды

**Почему не так:**
```csharp
// ❌ Плохо — два раза ищем компонент!
player.GetComponent<PlayerHealth>().ResetHealth();
player.GetComponent<PlayerHealth>().health = 100;

// ✅ Хорошо — один раз сохраняем
PlayerHealth ph = player.GetComponent<PlayerHealth>();
ph.ResetHealth();
ph.health = 100;
```

---

## 6. Time.timeScale

```csharp
Time.timeScale = 1f;  // Нормальная скорость
Time.timeScale = 0f;  // Пауза (всё замирает)
```

- `1f` — нормальная скорость (f = float, дробное число)
- `0f` — время остановлено (пауза)
- `0.5f` — всё в замедлении (слоу-мо)

**Где используется:**
- В `Respawn()` — возвращаем время после паузы
- В `PlayerHealth.TakeDamage()` — ставим на паузу при смерти

---

## 📝 ЗАПОМНИ:

| Концепция | Пример | Зачем |
|-----------|--------|-------|
| Класс | `class GameManager` | Чертёж/шаблон |
| Наследование | `: MonoBehaviour` | Расширяем функционал |
| Переменная | `int index` | Хранение данных |
| Массив | `Transform[]` | Много элементов |
| Модификатор | `private`, `public` | Контроль доступа |
| Атрибут | `[SerializeField]` | Показать в инспекторе |
| Метод | `void Start()` | Действие |
| GetComponent | `GetComponent<PlayerHealth>()` | Найти компонент |

---

## 🎯 ДОМАШНЕЕ ЗАДАНИЕ

### Задание 1: Добавь логирование
В `Start()` добавь вывод в консоль, сколько точек спавна:

```csharp
void Start()
{
    Debug.Log("Точек спавна: " + spawnPoints.Length);
    int index = Random.Range(0, spawnPoints.Length);
    player.position = spawnPoints[index].position;
}
```

### Задание 2: Добавь счётчик смертей
1. Создай переменную `private int deathCount = 0;`
2. В `Respawn()` увеличивай: `deathCount++;`
3. Выводи в лог: `Debug.Log("Смерть #" + deathCount);`

### Задание 3: Сделай метод публичным
Добавь метод, который возвращает количество смертей:

```csharp
public int GetDeathCount()
{
    return deathCount;
}
```

`return` — метод ВОЗВРАЩАЕТ значение (не `void`!)

---

## ❓ ВОПРОСЫ ДЛЯ ПРОВЕРКИ

1. Чем `public` отличается от `[SerializeField] private`?
2. Что такое массив и как получить его длину?
3. Почему лучше сохранять `GetComponent` в переменную?
4. Что делает `Time.timeScale = 0f`?

---

**Ответь на вопросы и покажи код с домашкой — разберём! 🚀**
