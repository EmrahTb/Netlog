Netlog Case Çalışması.

Proje .Net Core 3.1 framework sürümünde çalışmaktadır.

Proje çalışırken Web Uygulaması ve Api Uygulaması olarak ayrı ayrı ayağa kalkmaktadır.

Projenin testleri yazılmıştır.

Web tarafında Crud işlemleri yapılmaktadır.

Api uygulamasında ise mobil,web vs. uygulamalara api desteği verilmektedir.

Api uygulaması swagger üzerinden api'lere erişim altyapısı sunmaktadır.

Api uygulamasına Jwt Token güvenlik altyapısıyla giriş yapılmaktadır.

Swagger üzerinden test yapabilmek için ApiUser altındaki ​/api​/apiUsers​/authenticate endpointinden token almanız gerekiyor.
Test bilgileri: 
"username": "test",
"password": "test"
Token aldıktan sonra swagger üzerinden Authorize buttonundan Bearer {ALDIĞINIZ TOKEN} şeklinde giriş yaparsanız tüm apilere erişim sağlayabilirsiniz.


Anlık 20K-30K kullanıcı geleceğini düşünürsekte;

Projedeki tüm metotlar asenkron olarak yazılmıştır.

Gelen kullanıcı sayısına göre ölçeklendirme yapmak içinde Kubernetes altyapısını kullanırdım.

Load testlerimide Selenium uygulaması üzerinden yapardım.
