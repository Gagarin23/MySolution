# MySolution
Интернет магазины часто предоставляют информацию о своем товарном каталоге в виде специального xml-файла в yml-формате.

Требуется написать приложение, которое способно выполнить 2 команды

    1) Запуск dotnet run -- save <shopId> <url> приводит к скачиванию yml файла по указанному url, его парсингу и сохранению данных о товарах(поля id и name) в БД для магазина с указанным shopId.

    2) Запуск dotnet run -- print <shopId> приводит к выводу в консоль текущего каталога магазина shopId (произвольная строка до 50 символов) из БД в csv формате (требования к формату).

Для примера можно использовать следующие yml файлы:

http://static.ozone.ru/multimedia/yml/facet/div_soft.xml
http://static.ozone.ru/multimedia/yml/facet/business.xml
http://static.ozone.ru/multimedia/yml/facet/mobile_catalog/1133677.xml


В задании создал 3 таблицы:
1. Shops - магазины. Ключ - ShopId (string, произвольный, айдишник и он же название магазина. Сделал как было написано в задании.)
2. Offers - товары(назвал offer как в xml). Ключ - Offer (int, по умолчанию назначаемый автоматически, но при добавлении из XML брал id из тега offer, переключал способ назначения с помощью "Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Offer ON/OFF;")"), Поля - Name (string, имя продукта).
3. AvalibatyInShop - нахождение товара в конкретном магазине. - Ключ(int, назначаемый базой). Поля - ShopId (string, айди/название), OfferId (int, айдишник продукта). В коде ещё добавил зависимость, поля Shop и Offer.

Прибег к отношению многие ко многим, создав промежуточную таблицу AvalibatyInShop. Иначе при добавлении товара, если таковой уже имеется в базе и назначен магазин, придётся либо перезаписывать старые данные о магазине либо оставлять без изменений.

ВАЖНЫЙ UDP! Создан ExtendedProject для расширенной модели БД. Пока что отличие только в способе записи в базу. Так же добавлен проект PerfomenceProject для демострации превозобладания нового способа записи.
