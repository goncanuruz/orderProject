Proje Bilgileri

Framework: .NET 6

Dil: C#

Veritabanı: MySQL


Docker ile Çalıştırma

Projeyi çalıştırmadan önce, Docker Compose dosyasını çalıştırmanız gerekmektedir. Bu dosya, projenin ihtiyaç duyduğu tüm altyapıyı (örneğin, Seq, Redis ve RabbitMQ) beraberinde getirmek amacıyla eklenmiştir.
Docker Compose dosyasının bulunduğu dizine gidin:

cd OrderProject

Aşağıdaki komutu çalıştırın:

docker-compose down && docker-compose build --no-cache && docker-compose up -d

Bu komutlar ile Docker ortamında Seq, Redis ve RabbitMQ kurulumu gerçekleştirilecektir.

