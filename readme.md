Credentials:
admin@gmail.com - AdminPass
manager@gmail.com - ManagerPass
moder@gmail.com - ModerPass
user@gmail.com - UserPass

Home Controller:

http://ecsd00300935.epam.com:50001 - Welcome page

Account Controller:

http://ecsd00300935.epam.com:50001/account/register - Registration form

http://ecsd00300935.epam.com:50001/account/login - Login 

http://ecsd00300935.epam.com:50001/account/accessDenied - Acces denied page

http://ecsd00300935.epam.com:50001/account/logout - Logout form

Admimistration Controller: 

http://ecsd00300935.epam.com:50001/manage/users - Get all users

http://ecsd00300935.epam.com:50001/manage/roles - Get all roles

http://ecsd00300935.epam.com:50001/manage/role/change - Change role select page

http://ecsd00300935.epam.com:50001/manage/role/delete- Change role delete page

http://ecsd00300935.epam.com:50001/manage/role/edit - Manage role edit page

http://ecsd00300935.epam.com:50001/manage/user/delete - Manage user delete page

Basket Controller:

http://ecsd00300935.epam.com:50001/basket - Get all basket items page

http://ecsd00300935.epam.com:50001/basket/item/increment - Increment basket item

http://ecsd00300935.epam.com:50001/basket/item/remove - Remove basket item

http://ecsd00300935.epam.com:50001/game/{gameKey}/buy - Add game to basket

http://ecsd00300935.epam.com:50001/basket/remove - Remove game from basket

Comment Controller:

http://ecsd00300935.epam.com:50001/comments/{gameKey}/newcomment - Add new comment

http://ecsd00300935.epam.com:50001/comments/{gameKey}/reply - Add reply to comment

http://ecsd00300935.epam.com:50001/comments/{gameKey}/quote - Add quote to comment

http://ecsd00300935.epam.com:50001/comments/{gameKey}/delete - Delete comment

http://ecsd00300935.epam.com:50001/comments/ban - Ban user

Game Controller:

http://ecsd00300935.epam.com:50001/games - Get all games

http://ecsd00300935.epam.com:50001/games/list - Get all games partial view

http://ecsd00300935.epam.com:50001/games/new - Add new game

http://ecsd00300935.epam.com:50001/games/update - Update existing game

http://ecsd00300935.epam.com:50001/games/{gameKey} - Get game by key

http://ecsd00300935.epam.com:50001/games/remove - Remove game by key

http://ecsd00300935.epam.com:50001/games/{gameKey}/download - Download game by key

http://ecsd00300935.epam.com:50001/games/notFound - Game not found page

http://ecsd00300935.epam.com:50001/games/publish - Set publish date

Genre Controller:

http://ecsd00300935.epam.com:50001/genres - Get all genres

http://ecsd00300935.epam.com:50001/genres/update - Update genre

http://ecsd00300935.epam.com:50001/genres/new - Create new genre

http://ecsd00300935.epam.com:50001/genres/remove - Set genre is deleted flag

Order Controller

http://ecsd00300935.epam.com:50001/order - Process payment

http://ecsd00300935.epam.com:50001/order/card - Get order by card

http://ecsd00300935.epam.com:50001/order/edit - Edit order

http://ecsd00300935.epam.com:50001/order/edit - Pay order

http://ecsd00300935.epam.com:50001/order/history - History orders

http://ecsd00300935.epam.com:50001/order/manage - Manage orders

http://ecsd00300935.epam.com:50001/order/manageShipped - Manage shipepd orders

Publisher Controller:

http://ecsd00300935.epam.com:50001/publisher - Get all publishers

http://ecsd00300935.epam.com:50001/publisher/{companyName} - Get publisher by name

http://ecsd00300935.epam.com:50001/publisher/new - Create new publisher

http://ecsd00300935.epam.com:50001/publisher/remove - Remove publisher

http://ecsd00300935.epam.com:50001/publisher/edit - Edit publisher

AuthApi Controler:

http://ecsd00300935.epam.com:50001/api/login - api authentication

GameApi Controller:

http://ecsd00300935.epam.com:50001/api/games - Get all games

SortType
integer($int32)
(formData)	
Available values : 0, 1, 2, 3, 4, 5

PriceFrom
number($double)
(formData)	
PriceTo
number($double)
(formData)	
SearchString
string
(formData)	
TotalPages
integer($int32)
(formData)	
CurrentPage
integer($int32)
(formData)	
ItemsPerPage
integer($int32)
(formData)	
Publishers
array[string]
(formData)	
ReleaseDate
integer($int32)
(formData)	
Available values : 0, 1, 2, 3, 4, 5

Genres
array[string]
(formData)	
Platforms
array[string]
(formData)


http://ecsd00300935.epam.com:50001/api/games/{gameKey} - Get game by key

Name	Description
gameKey *required
string
(path)

http://ecsd00300935.epam.com:50001/api/games/create - Create new game

Key *
string
(formData)	
Name *
string
(formData)	
NameRu
string
(formData)	
Description *
string
(formData)	
DescriptionRu
string
(formData)	
PublishDate
string
(formData)	
Price *
number($double)
(formData)	
UnitsInStock *
integer($int32)
(formData)	
Publisher
string
(formData)	
Discontinued
boolean
(formData)	
Rating
number($double)
(formData)	
IsDeleted
boolean
(formData)	
GamePlatforms
array[string]
(formData)	
GameGenres
array[string]
(formData)	
Publishers
array[string]
(formData)

http://ecsd00300935.epam.com:50001/api/games/delete/{gameKey} - Delete game by key

Name	Description
gameKey *
string
(path)

http://ecsd00300935.epam.com:50001/api/games/update/{gameKey} - Update game by key

Name	Description
Key *
string
(formData)	
Name *
string
(formData)	
NameRu
string
(formData)	
Description *
string
(formData)	
DescriptionRu
string
(formData)	
PublishDate
string
(formData)	
Price *
number($double)
(formData)	
UnitsInStock *
integer($int32)
(formData)	
Publisher
string
(formData)	
Discontinued
boolean
(formData)	
Rating
number($double)
(formData)	
IsDeleted
boolean
(formData)	
GamePlatforms
array[string]
(formData)	
GameGenres
array[string]
(formData)	
Publishers
array[string]
(formData)	
gameKey *
string
(path)

http://ecsd00300935.epam.com:50001/api/games/{gameKey}/comments - Get game comments

gameKey *
string
(path)

http://ecsd00300935.epam.com:50001/api/games/{gameKey}/comments/{commentId} - Get specific game comment

Name	Description
gameKey *
string
(path)	
commentId *
string($uuid)
(path)

http://ecsd00300935.epam.com:50001/api/games/{gameKey}/genres - Get game genres

Name	Description
gameKey *
string
(path)

http://ecsd00300935.epam.com:50001/api/games/{gameKey}/platforms - Get game platforms

gameKey *
string
(path)

http://ecsd00300935.epam.com:50001/api/games/{gameKey}/publisher - Get game publisher

gameKey *
string
(path)	

GenreApi Controller:

http://ecsd00300935.epam.com:50001/api/genres - Get all genres

http://ecsd00300935.epam.com:50001/api/genres/{genreName} - Get genre by name

Name	Description
genreName *
string
(path)

http://ecsd00300935.epam.com:50001/api/genres/delete/{genreName} - Delete genre by name

Name	Description
genreName *
string
(path)

http://ecsd00300935.epam.com:50001/api/genres/create - Create new genre

Name	Description
Id
string($uuid)
(formData)	
Name *
string
(formData)	
NameRu
string
(formData)	
ParentGenre
string
(formData)	
AllGenres
array[string]
(formData)

http://ecsd00300935.epam.com:50001/api/genres/update/{genreName} - Update genre by name

Id
string($uuid)
(formData)	
Name *
string
(formData)	
NameRu
string
(formData)	
ParentGenre
string
(formData)	
AllGenres
array[string]
(formData)	
genreName *
string
(path)

OrderApi Controller:

http://ecsd00300935.epam.com:50001/api/orders/{orderId} - Get orer by order id

orderId *
string($uuid)
(path)

http://ecsd00300935.epam.com:50001/api/orders/current - Get current active order

gameKey
string
(query)

http://ecsd00300935.epam.com:50001/api/orders/create - Create new order

gameKey
string
(query)

http://ecsd00300935.epam.com:50001/api/orders/remove - Remove order

gameKey
string
(query)

http://ecsd00300935.epam.com:50001/api/orders/update/{gameKey}/{quantity} - Update order game quantity

gameKey *
string
(path)	
quantity *
integer($int32)
(path)

PlatformApi Controller:

http://ecsd00300935.epam.com:50001/api/platforms - Get all platforms

http://ecsd00300935.epam.com:50001/api/platforms/{platformName} - Get platform by name

platformName *
string
(path)

http://ecsd00300935.epam.com:50001/api/platforms/delete/{platformName} - Delete platform by name

platformName *
string
(path)

http://ecsd00300935.epam.com:50001/api/platforms/create - Create platform

Name *
string
(formData)	
NameRu
string
(formData)	
OldName
string
(formData)

http://ecsd00300935.epam.com:50001/api/platforms/update/{platformName} - Update platform by name

Name *
string
(formData)	
NameRu
string
(formData)	
OldName
string
(formData)	
genreName *
string
(path)

PublisherApi Controller:

http://ecsd00300935.epam.com:50001/api/publishers - Get all publishers

http://ecsd00300935.epam.com:50001/api/publishers/{companyName} - Get publisher by company name

companyName *
string
(path)

http://ecsd00300935.epam.com:50001/api/publishers/delete/{companyName} - Delete publisher by company name

companyName *
string
(path)

http://ecsd00300935.epam.com:50001/api/publishers/create - Create publisher

CompanyName *
string
(formData)	
CompanyNameRu
string
(formData)	
Description *
string
(formData)	
DescriptionRu
string
(formData)	
HomePage
string
(formData)	
HomePageRu
string
(formData)	
OldCompanyName
string
(formData)

http://ecsd00300935.epam.com:50001/api/publishers/update/{companyName} - Update publisher by company name

CompanyName *
string
(formData)	
CompanyNameRu
string
(formData)	
Description *
string
(formData)	
DescriptionRu
string
(formData)	
HomePage
string
(formData)	
HomePageRu
string
(formData)	
OldCompanyName
string
(formData)	
companyName *
string
(path)