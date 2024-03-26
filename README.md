The purpose of the exercise is to recreate a management system for the Municipal Police using ASP.NET MVC and the use of a database.
The application allows: registering all offenders, having a list of violations that can be contested, and compiling a report.
Additionally, there are some pages that display the result of certain queries requested by the task.

Lo scopo dell'esercizio è quello di ricreare un gestionale per la Polizia Municipale tramite ASP.NET MVC e l'uso di un database. 
L'applicazione permette di: anagrafare tutti i trasgressori, avere l'elenco delle violazioni che è possibile contestare e compilare un verbale.
Inoltre sono presenti alcune pagine che visualizzano il risultato di alcune Query richieste dal compito.

Questo repository contiene le query SQL utilizzate per creare le tabelle nel database "Esercizio be-s2l5".
### ANAGRAFICA
CREATE TABLE ANAGRAFICA (
    IDanagrafica INT PRIMARY KEY,
    Cognome NVARCHAR(20) not null,
    Nome NVARCHAR(20) not null,
    Indirizzo NVARCHAR(100),
    Città NVARCHAR(20) not null,
    CAP NVARCHAR(5) not null,
    CodiceFiscale NVARCHAR(16)
);

CREATE TABLE TIPO_VIOLAZIONE (
    IDviolazione INT PRIMARY KEY,
    Descrizione NVARCHAR(100) NOT NULL
);

### VERBALE
CREATE TABLE VERBALE (
    IDverbale INT PRIMARY KEY,
    DataViolazione DATETIME NOT NULL,
    IndirizzoViolazione NVARCHAR(80),
    NominativoAgente NVARCHAR(45) NOT NULL,
    DataTrascrizioneVerbale DATETIME NOT NULL,
    Importo MONEY NOT NULL,
    DecurtamentoPunti INT NOT NULL,
    IDanagrafica INT NOT NULL,
    IDviolazione INT NOT NULL,
    FOREIGN KEY (IDanagrafica) REFERENCES ANAGRAFICA(IDanagrafica),
    FOREIGN KEY (IDviolazione) REFERENCES TIPO_VIOLAZIONE(IDviolazione)
);

###
Qui sotto sono presenti le query per popolare le tabelle

INSERT INTO ANAGRAFICA (Cognome, Nome, Città, CAP, CodiceFiscale)
VALUES ('Bianchi', 'Maria', 'Napoli', '80100', 'BNCMRA01A01F205C'),
       ('Rizzo', 'Giuseppe', 'Palermo', '90100', 'RZZGPP01A01H123B'),
       ('Ferrari', 'Anna', 'Torino', '10100', 'FRRNNA01A01H456D'),
       ('Esposito', 'Luca', 'Bologna', '40100', 'SPSLCA01A01H789E'),
       ('Romano', 'Giovanna', 'Firenze', '50100', 'RMNGVN01A01F321G'),
       ('Costa', 'Marco', 'Genova', '16100', 'CSTMRC01A01F654I'),
       ('Gallo', 'Alessia', 'Catania', '95100', 'GLOLSA01A01H987L'),
       ('Conti', 'Paolo', 'Milano', '20100', 'CNTPLA01A01F654R'),
       ('De Luca', 'Laura', 'Bari', '70100', 'DLCLRA01A01H321S'),
       ('Greco', 'Roberto', 'Venezia', '30100', 'GRCRBTO01A01F987T');

INSERT INTO VERBALE (DataViolazione, IndirizzoViolazione, NominativoAgente, DataTrascrizioneVerbale, Importo, DecurtamentoPunti, IDanagrafica, IDviolazione)
VALUES ('2023-01-01', 'Via Roma 1', 'Mario Rossi', '2023-01-02', 100.00, 2, 1, 1),
       ('2023-02-01', 'Via Napoli 2', 'Luca Bianchi', '2023-02-02', 150.00, 3, 2, 2),
       ('2023-03-01', 'Via Palermo 3', 'Giovanna Verdi', '2023-03-02', 200.00, 4, 3, 3),
       ('2023-04-01', 'Via Milano 4', 'Alessandro Esposito', '2023-04-02', 250.00, 5, 4, 4),
       ('2023-05-01', 'Via Firenze 5', 'Paola Romano', '2023-05-02', 300.00, 6, 5, 5),
       ('2023-06-01', 'Via Torino 6', 'Giovanni Costa', '2023-06-02', 350.00, 7, 6, 1),
       ('2023-07-01', 'Via Bologna 7', 'Sofia Gallo', '2023-07-02', 400.00, 8, 7, 2),
       ('2023-08-01', 'Via Genova 8', 'Marco Conti', '2023-08-02', 450.00, 9, 8, 3),
       ('2023-09-01', 'Via Catania 9', 'Laura De Luca', '2023-09-02', 500.00, 10, 9, 4),
       ('2023-10-01', 'Via Venezia 10', 'Roberta Greco', '2023-10-02', 550.00, 11, 10, 5),
       ('2023-11-01', 'Via Napoli 11', 'Mario Rossi', '2023-11-02', 100.00, 16, 1, 1),
       ('2023-12-01', 'Via Roma 12', 'Luca Bianchi', '2023-12-02', 150.00, 3, 2, 2),
       ('2024-01-01', 'Via Palermo 13', 'Giovanna Verdi', '2024-01-02', 200.00, 4, 3, 3),
       ('2024-02-01', 'Via Milano 14', 'Alessandro Esposito', '2024-02-02', 250.00, 5, 4, 4),
       ('2024-03-01', 'Via Firenze 15', 'Paola Romano', '2024-03-02', 300.00, 6, 5, 5);

INSERT INTO TIPO_VIOLAZIONE (Descrizione)
VALUES ('Eccesso di velocità'),
       ('Guida in stato di ebbrezza'),
       ('Mancato rispetto del segnale di stop'),
       ('Guida senza cintura di sicurezza'),
       ('Utilizzo del cellulare durante la guida');
