# VotieAPI

## 1. Sprendžiamo uždavinio aprašymas

### 1.1. Sistemos paskirtis

Projekto tikslas – palengvinti rinkimų balsavimo ir pagreitinti jų skaičiavimo sistemą. Veikimo principas – pačią kuriamą platformą sudaro dvi dalys: internetinė aplikacija, kuria naudosis balsuotojai, administratorius bei aplikacijų programavimo sąsaja. Balsuotojas, norėdamas naudotis šia platforma turės prisiregistruos prie internetinės aplikacijos. Tuomet prisijungęs galės pasirinkti atvirus rinkimus ir atiduoti savo balso pasirinktam kandidatui. Administratorius galės peržiūrėti rinkimų rezultatus, juos patvirtinti arba atmesti, jeigu yra įtarimas dėl galimo balsų klastojimo.

### 1.2. Funkciniai reikalavimai

#### Neregistruotas sistemos naudotojas galės:

1. Peržiūrėti platformos pagrindinį puslapį.
2. Prisijungti prie internetinės aplikacijos.

#### Registruotas sistemos naudotojas galės:

1. Atsijungti nuo internetinės aplikacijos.
2. Prisijungti (užsiregistruoti) prie platformos.
3. Matyti dabar vykstančius rinkimus.
4. Išeiti iš rinkimų lango.
5. Pasirinkti kandidatą.
6. Paspaudus matyti kandidato aprašymą.
7. Atiduoti balsą.

#### Administratorius galės:

1. Patvirtinti naudotojo registraciją.
2. Patvirtinti balsavimo rezultatus.
3. Šalinti naudotojus.
4. Šalinti netinkamus balsus.

## 2. Sistemos architektūra

Sistemos sudedamosios dalys:
- Kliento pusė (ang. Front-End) – Blazor, Javascript.
- Serverio pusė (angl. Back-End) – .NET.
- Duomenų bazė – MySQL.

### 2.1 pav. Sistemos diegimo diagrama

Sistemos talpinimui yra naudojamas Azure serveris. Kiekviena sistemos dalis yra diegiama tame pačiame serveryje. Internetinė aplikacija yra pasiekiama per HTTP protokolą. Šios sistemos veikimui (pvz., duomenų manipuliavimui su duomenų baze) yra reikalingas Voty API, kuris pasiekiamas per aplikacijų programavimo sąsają. Pats Voty API vykdo duomenų mainus su duomenų baze - tam naudojama TCP/IP sąsaja.

![Sistemos Voty diegimo diagrama]([link-to-image](https://imgur.com/a/uvd6BGJ)https://imgur.com/a/uvd6BGJ)
