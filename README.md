# Task Distribution App

## Proje Açıklaması 
- Projede, analist kullanıcılarının belirlenen taskları, zorluk seviyelerine göre adil bir şekilde developer kullanıcılarına ataması hedeflenmiştir. 
- Proje ASP.NET MVC ve .NET Framework 4.7.2 kullanılarak geliştirilmiştir.
- MSSQL veritabanını kullanmaktadır. 
- Entity Framework ve Db-First yaklaşımı kullanılarak ORM altyapısı oluşturulmuştur. 
- Repository pattern ve katmanlı mimari prensipleri ile SOLID prensiplerine uyulması sağlanmıştır. 
- Ekran tasarımları için Bootstrap framework'ü kullanılmıştır.

## Kullanıcı Rolleri

Projede üç farklı kullanıcı rolü tanımlanmıştır:

1. **Analist**: Analistler, taskları oluşturabilir, düzenleyebilir ve silebilir.
2. **Yönetici**: Yöneticiler, taskların atandığı developerları düzenleyebilir ve taskları silebilir.
3. **Developer**: Developerlar, kendilerine atanan taskları görüntüleyebilir ve tamamlandı olarak işaretleyebilir.

## Uygun Developerı Bulma Algoritması

Aşağıda, analistin bir taskı kaydederken uygun developerı bulmak için kullanılan algoritma adımları verilmiştir:

1. Analist, taskı zorluk seviyesiyle birlikte kaydeder.
2. Girilen zorluk seviyesinde daha önce hiç task almamış bir developer varsa:
   - Eğer sadece 1 adet developer varsa, task bu developera atanır.
   - Birden fazla developer varsa, alfabetik olarak önce gelen developer taska atanır.
3. Girilen zorluk seviyesinde task almamış bir developer yoksa:
   - Tasklar oluşturulma tarihine göre sıralanır.
   - Girilen zorluk seviyesindeki en eski atama bulunur ve bu atamadaki developera task atanır.
   - Bu aşama veritabanında yazılan Scalar-Valued fonksiyon kullanılarak gerçekleştirilmiştir.

## Projeye Ait Teknolojik Yapı

- Projede ASP.NET MVC ve .NET Framework 4.7.2 kullanılmıştır.
- Veritabanı olarak MSSQL tercih edilmiştir.
- ORM altyapısı olarak Entity Framework ve Db-First yaklaşımı kullanılmıştır.
- Repository pattern kullanılmıştır.
- Projede katmanlı mimari prensipleri ve SOLID prensiplerine uyulması hedeflenmiştir.
- Dependency injection için Autofac kütüphanesi kullanılmıştır.
- Ekran tasarımları için Bootstrap framework'ü kullanılmıştır.

## Kurulum ve Çalıştırma

Aşağıdaki adımları izleyerek projeyi yerel ortamınızda

 çalıştırabilirsiniz:

1. Repository'i klonlayın veya ZIP olarak indirin.
2. Proje klasöründe `DatabaseScript.sql` adlı dosyayı kullanarak MSSQL veritabanını oluşturun.
3. Visual Studio'da projeyi açın.
4. Bağımlılıkları yüklemek için NuGet Package Manager Console'u açın ve `Update-Package` komutunu çalıştırın.
5. `Web.config` dosyasında veritabanı bağlantı dizesini güncelleyin.
6. Projeyi derleyin ve çalıştırın.

Bu adımları tamamladıktan sonra, projenin yerel ortamınızda başarıyla çalıştığını görebilirsiniz.

---
