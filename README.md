

Exchange rate provider:

Amaç: Projelerin ihtiyaç duyacağı anlık kur bilgisi sağlayan bir eklenti yazmak.

İçerik: Kütüphane plugin mantığın geliştirildi. Şuan için sadece TCMB provider entegrasyonu gerçekleştirildi.
		Farklı  bir kur sağlayıcısı entegre edilip geliştirilebilir.
		Genel provider mantığı arka planda çalışan threadler ile sağlandı. Kur bilgisi anlık çekilmiyor. Uygulama start olduğu an kur bilgisi ilgili providerdan alınır ardından belirli aralıklar ile güncelleme yapılır.
		Bu süre parametrik yapılabilir veya buna provider kurallarına göre karar verilbilir.
		Böyle bir mantık oluşturmamın sebebi yüksek trafikli uygulamarda anlık kur bilgisi çekmek ciddi performans sorunlarına yol açabilir. Bundan dolayı güncel olduğunu bilgiğimiz memory de tuttuğumuz kur bilgisini dönüyoruz.
		
Teknik detay:
			1. Kur bilgisi alınabilecek sağlayıcılar için bir arayüz oluşturuldu.
			2. İstenilen veriyi çıktı alabilmeyi sağlayacak export providerlar geliştirdi.
			3. Sistemin ihtiyaç duyduğu serileştirme yardımcı sınıfları oluşturuldu.
			4. Providerlar ayrı library e alındı. Amaç uygulama tüm providerlara ihtiyaç duymayabilir veya paylaşılmayabilir,
				register işlemi referans üzerinden yapılabilir ... vb senaryolardan kaynaklı harici library oluşturuldu.
			5. Sistmein ihtiyaç duyduğu extension metotlar yazıldı.
				Lock,String,Decimal,Thead... bv
			6. Servis çağrımları izlenebilmesi için bir proxy oluşturuldu.
			7. Kur bigileri üzerinde arama,sıralama gibi operasyonları sağlayan api geliştirdi.
			8. Çıktı alınmak istenilen kur bilgileri için arama ve sıralama operasyonları sağlayan api geliştirdi.
			9. Temel test senaryoları yazıldı.
			

					


 


