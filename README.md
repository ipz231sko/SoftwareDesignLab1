#Лабораторна робота #1. Принципи програмування. DRY, KISS, SOLID, YAGNI та ін.
1. **Принцип єдиної відповідальності.** Кожен клас відповідає за одну конкретну дію, що допомагає зменшити залежність між різними частинами коду.
  -  клас Money - відповідає відповідає за обробку грошових сум, включаючи додавання, віднімання та форматування;
  -  клас Product - управляє інформацією про товар, включаючи ціну і знижки;
  -  клас Category - представляє категорію товару;
  -  клас Warehouse - виконує функції управління запасами (додавання, видалення, оновлення);
  -  клас Reporting - відповідає за ведення звітності та реєстрацію операцій;
  -  клас Cart - управляє корзиною покупок, включаючи додавання та видалення товарів;
2. **Принцип відкритості/закритості (OCP).** Класи можуть бути розширені без зміни існуючого коду:
  - класи [USD](https://github.com/ipz231sko/SoftwareDesignLab1/blob/main/WarehouseApp/Classes/Money.cs#L74-L79), [EUR](https://github.com/ipz231sko/SoftwareDesignLab1/blob/main/WarehouseApp/Classes/Money.cs#L80-L85), і [UAH](https://github.com/ipz231sko/SoftwareDesignLab1/blob/main/WarehouseApp/Classes/Money.cs#L86-L92) розширюють базовий клас Money для підтримки різних валют. кщо потрібно додати нову валюту, можна створити новий клас, який успадковується від Money, не змінюючи логіку вже реалізованих класів;
  - Category дозволяє розширювати список категорій без зміни класу Product;
  - Reporting може бути розширений для створення нових видів звітів;
3. **Принцип заміщенн Лісков (LSP).** Дочірні класи можуть заміняти батьківські без порушення логіки програми:
  - [USD](https://github.com/ipz231sko/SoftwareDesignLab1/blob/main/WarehouseApp/Classes/Money.cs#L74-L79), [EUR](https://github.com/ipz231sko/SoftwareDesignLab1/blob/main/WarehouseApp/Classes/Money.cs#L80-L85), [UAH](https://github.com/ipz231sko/SoftwareDesignLab1/blob/main/WarehouseApp/Classes/Money.cs#L86-L92) успадковують Money і можуть використовуватися в будь-якому місці, де очікується Money.
  - Product працює з будь-якими категоріями [Category](https://github.com/ipz231sko/SoftwareDesignLab1/blob/main/WarehouseApp/Classes/Product.cs#L14);
4. **Принцип розділення інтерфейсу (ISP).** Інтерфейси чітко розділяють різні аспекти системи:
  - [IStockManager](github.com/ipz231sko/SoftwareDesignLab1/blob/main/WarehouseApp/Classes/Warehouse.cs#L9-L14) містить методи для управління запасами, без зайвих обов'язків, таких як генерація звітів;
  - [IInventoryReporter](github.com/ipz231sko/SoftwareDesignLab1/blob/main/WarehouseApp/Classes/Warehouse.cs#L16-L20) відповідає виключно за відображення інформації про запаси, що дозволяє легко змінювати реалізації без впливу на інші частини системи;
5. **Принцип інверсії залежності (DIP).** Класи залежать від абстракцій, а не від конкретних реалізацій:
  - Клас Reporting залежить від інтерфейсів IStockManager та IInventoryReporter, що дозволяє легко підміняти реалізації.
6. **Принцип DRY (Don't Repeat Yourself).** Код не містить повторюваних фрагментів:
  - Всі перевірки для грошових сум (наприклад, діапазон копійок) виконуються в одному місці в класі `Money`, що зменшує ризик помилок.
  - Логіка обробки товарів в класі Warehouse використовує спільні методи для додавання, видалення та оновлення запасів, що робить код більш чистим.
7. **Принцип інкапсуляції (Encapsulation Principle).** Дані в класах приховані від зовнішнього доступу, що зменшує ризик їх неправильного використання:
  -  У класі Money поля [wholeMoney](github.com/ipz231sko/SoftwareDesignLab1/blob/main/WarehouseApp/Classes/Money.cs#L11) та [fractionalMoney](github.com/ipz231sko/SoftwareDesignLab1/blob/main/WarehouseApp/Classes/Money.cs#L12) є приватними, що захищає їх від прямого доступу та модифікацій ззовні.
  -  Метод [SetMoney](https://github.com/ipz231sko/SoftwareDesignLab1/blob/main/WarehouseApp/Classes/Money.cs#L29-L36) дозволяє змінювати грошові суми, але виконує перевірки, щоб забезпечити коректність даних.

**UML-діаграму** можна преглнути [тут](https://github.com/ipz231sko/SoftwareDesignLab1/blob/main/UML-діаграма.png)
