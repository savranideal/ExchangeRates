

<h3>Exchange rate provider:</h3>

<b>Amaç:</b> Projelerin ihtiyaç duyacağı anlık kur bilgisi sağlayan bir eklenti yazmak.

<b>İçerik:</b>
<ul>
		<li>Kütüphane plugin mantığında geliştirildi. TCMB provider entegrasyonu gerçekleştirildi.</li>
		<li>Farklı  bir kur sağlayıcısı entegre edilip geliştirilebilir.</li>
		<li>Genel provider mantığı arka planda çalışan threadler ile sağlandı. Kur bilgisi anlık çekilmiyor. Uygulama start olduğu an kur bilgisi ilgili providerdan alınır ardından belirli aralıklar ile güncelleme yapılır.</li>
		<li>Bu süre parametrik yapılabilir veya buna provider kurallarına göre karar verilbilir.</li>
		<li>Yüksek trafikli uygulamarda anlık kur bilgisi çekmek ciddi performans sorunlarına yol açabilir. Bundan dolayı güncel olduğunu bildiğimiz ve memoryde tuttuğumuz kur bilgisi kullanılır.</li>
</ul>		
<b>Teknik detay:</b>
<ul>
	<li> Kur bilgisi alınabilecek sağlayıcılar için bir arayüz oluşturuldu.</li>
		<li> İstenilen veriyi çıktı alabilmeyi sağlayacak export providerlar geliştirdi.</li>
		<li> Sistemin ihtiyaç duyduğu serializer yardımcı sınıfları oluşturuldu.</li>
		<li> Providerlar ayrı kütüphaneye alındı. Buradaki amaç, uygulama tüm providerlara ihtiyaç duymayabilir veya paylaşılmayabilir,
		   register işlemi referans üzerinden yapılabilir ... vb senaryolardan kaynaklı harici library oluşturuldu.</li>
		<li> Sistemin ihtiyaç duyduğu extension metotlar yazıldı.
		   Lock,String,Decimal,Thead... vb </li>
		<li> Servis çağrımlarının izlenebilmesi için bir proxy oluşturuldu. </li>
		<li> Kur bigileri üzerinde arama,sıralama gibi operasyonları sağlayan api geliştirdi. </li>
		<li> Çıktı alınmak istenilen kur bilgileri için arama ve sıralama operasyonları sağlayan api geliştirdi. </li>
		<li> Temel test senaryoları yazıldı. </li>
</ul>
			

					


 


