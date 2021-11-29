#Bilgilendirme
- Servislerin kullanýmý için swagger default olarak var fakat test için postman api koleksiyonunu proje dizininde bulabilirsiniz.
- Projeyi çalýþtýrmadan önce appsettings.json dosyasýndaki connection stringi kendinize göre ayarlayýnýz.
- EF Code First olarak geliþtirildi. Bunun için update-database yapmanýza gerek yok startup içerisinde uygulama ayaða kalkarken yapýlmaktadýr.
- Validasyon iþlemleri için FluentValidation kullanýldý.
- MediatR ve CQRS paternler kullanýldý.
- Pipeline sürecinde Log yapýsý oluþturuldu.

#Eksiklikler
- Unit test  (mevcut iþimdeki yoðunlumdan dolayý vakit ayýramadým)

#Ýyileþtirilebilecek adýmlar
- Generic bir repository pattern uygulayabilirdim.
- Pipeline içerisinde Error handling yapabilirdim.
- Linear paterni daha verimli uygulayabilirdim.
- Mapping iþlemleri için AutoMapper vb. kullanabilirdim.

