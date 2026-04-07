# DESIGN.md — Visual Constitution for AI-Generated UI

## 1. Philosophy — Visual DNA (Linear Style)

Bu ürünün tasarım dili, modern SaaS ürünlerinin zirvesini temsil eder. Referans stil: Linear.app.

Temel prensipler:

* Less is more → Her şey gereksiz detaylardan arındırılmış olmalı
* Clarity over decoration → Görsel efekt değil, netlik önceliklidir
* Function-driven aesthetics → Tasarım, fonksiyonun doğal sonucu olmalı
* Calm UI → Kullanıcıyı yormayan, göz dinlendiren arayüz

❗ AI için kural:
Eğer bir UI elementi “gösterişli” görünüyorsa, muhtemelen yanlış yapılmıştır.

---

## 2. Layout System — Mathematical Spacing (8px Rule)

Tüm boşluklar matematiksel bir sistemle kontrol edilir.

### Zorunlu Spacing Sistemi:

Sadece şu değerler kullanılabilir:
8px, 16px, 24px, 32px, 40px, 48px, 64px

### Kurallar:

* Padding ve margin ASLA rastgele verilmez
* Tüm spacing değerleri 8’in katı olmak zorundadır
* Component içi spacing → genellikle 16px veya 24px
* Section arası spacing → 48px veya 64px

Örnek:

```css
padding: 16px;
margin-bottom: 24px;
gap: 8px;
```

❗ AI için kural:
8px grid sistemine uymayan her spacing değeri hatalı üretim kabul edilir.

---

## 3. Color Engineering — Dark Mode First

### Temel Yaklaşım:

Saf siyah (#000) kullanılmaz. Derinlik hissi için tonlar kullanılır.

### Primary Palette:

```css
--bg-primary: #0C0C0D;
--bg-secondary: #111113;
--bg-tertiary: #1A1A1D;

--text-primary: #FFFFFF;
--text-secondary: #A1A1AA;
--text-muted: #71717A;
```

### Accent Colors:

```css
--accent-primary: #6366F1;
--accent-hover: #4F46E5;
--accent-soft: rgba(99, 102, 241, 0.1);
```

### Border System (Subtle Depth Rule):

Border rengi, arka plandan sadece 1-2 ton farklı olmalı.

```css
--border-subtle: #1F1F23;
--border-strong: #2A2A2E;
```

❗ AI için kural:
Kontrast aşırı olmayacak. Ama okunabilirlik asla düşmeyecek.

---

## 4. Typography Guidelines

### Font Ailesi:

Primary: Inter
Alternative: Geist

```css
font-family: 'Inter', sans-serif;
```

### Font Kuralları:

* Line-height: 1.6
* Letter-spacing: minimal (0 veya -0.01em)

### Tipografi Ölçeği:

```css
h1 → 32px
h2 → 24px
h3 → 20px
body → 16px
small → 14px
```

### Renk Kullanımı:

* Başlıklar → text-primary
* Paragraflar → text-secondary
* Yardımcı metin → text-muted

❗ AI için kural:
Büyük font + küçük line-height kombinasyonu YASAK.

---

## 5. Component System

### Buttons

#### Primary Button

```css
background: var(--accent-primary);
color: white;
padding: 12px 16px;
border-radius: 8px;
```

Hover:

```css
background: var(--accent-hover);
```

#### Secondary Button

```css
background: var(--bg-secondary);
color: var(--text-primary);
border: 1px solid var(--border-subtle);
```

#### Ghost Button

```css
background: transparent;
color: var(--text-secondary);
```

Hover:

```css
background: rgba(255,255,255,0.05);
```

---

### Cards

```css
background: var(--bg-secondary);
border: 1px solid var(--border-subtle);
border-radius: 12px;
padding: 16px;
```

Kurallar:

* Shadow minimum kullanılmalı
* Depth hissi border ile verilmeli

---

### Inputs

```css
background: var(--bg-tertiary);
border: 1px solid var(--border-subtle);
border-radius: 8px;
padding: 12px;
color: var(--text-primary);
```

Focus:

```css
border-color: var(--accent-primary);
outline: none;
```

---

## 6. Micro-Interactions

Genel Kurallar:

* Animasyon süresi: 150ms – 250ms
* Easing: ease-out
* Hover efektleri çok hafif olmalı

Örnek:

```css
transition: all 0.2s ease-out;
```

Yasaklar:

* Bounce animasyonları ❌
* Abartılı scale efektleri ❌
* Uzun animasyonlar ❌

❗ AI için kural:
Kullanıcı animasyonu fark etmemeli, sadece hissetmeli.

---

## 7. Layout Composition

Grid:

* 12 column grid sistemi
* Maksimum genişlik: 1200px

Alignment:

* Sol hizalama tercih edilir
* Center sadece özel durumlarda

Whitespace:

* UI'nın minimum %30’u boşluk olmalı

❗ AI için kural:
Sıkışık UI = başarısız UI

---

## 8. Do & Don't

### Do:

* Minimal tasarım
* Net hiyerarşi
* Tutarlı spacing
* Subtle border kullanımı

### Don't:

* Rastgele padding/margin
* Aşırı renk kullanımı
* Shadow bağımlılığı
* Bootstrap default görünümü

---

## 9. System Prompt for AI Agents (KRİTİK)

Bu doküman, UI üretimi sırasında en yüksek öncelikli kural setidir.

Zorunlu kurallar:

* Bu tasarım sisteminin dışına çıkmak teknik hata kabul edilir
* 8px spacing sistemine uymamak geçersiz üretimdir
* Renk paleti dışına çıkmak yasaktır
* Whitespace bırakmamak amatör tasarım hatasıdır

AI davranış talimatı:
Sen sadece çalışan UI üretmezsin.
Sen, modern, premium, milyon dolarlık SaaS kalitesinde arayüz üretirsin.
Her component, her spacing, her renk bilinçli seçilmelidir.

Son kural:
Eğer ortaya çıkan tasarım “Bootstrap gibi” görünüyorsa, başarısız olmuşsun demektir.

