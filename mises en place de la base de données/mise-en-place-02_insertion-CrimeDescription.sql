
-- ----------------------------------------------------------------------------
-- Insertion dans table 'Crime'
-- ----------------------------------------------------------------------------

INSERT INTO NY_Crimes.Crime_description VALUES (1 , 'grand larceny of motor vehicle' , 'of auto - attem');
INSERT INTO NY_Crimes.Crime_description VALUES (2 , 'grand larceny of motor vehicle' , 'of moped');
INSERT INTO NY_Crimes.Crime_description VALUES (3 , 'grand larceny of motor vehicle' , 'of auto');
INSERT INTO NY_Crimes.Crime_description VALUES (4 , 'grand larceny of motor vehicle' , 'of motorcycle');
INSERT INTO NY_Crimes.Crime_description VALUES (5 , 'grand larceny of motor vehicle' , 'of truck');
INSERT INTO NY_Crimes.Crime_description VALUES (6 , 'grand larceny' , 'by acquiring los');
INSERT INTO NY_Crimes.Crime_description VALUES (7 , 'grand larceny' , 'by acquiring lost credit card');
INSERT INTO NY_Crimes.Crime_description VALUES (8 , 'grand larceny' , 'by bank acct compromise-atm transaction');
INSERT INTO NY_Crimes.Crime_description VALUES (9 , 'grand larceny' , 'by bank acct compromise-reproduced check');
INSERT INTO NY_Crimes.Crime_description VALUES (10 , 'grand larceny' , 'by bank acct compromise-teller');
INSERT INTO NY_Crimes.Crime_description VALUES (11 , 'grand larceny' , 'by bank acct compromise-unauthorized purchase');
INSERT INTO NY_Crimes.Crime_description VALUES (12 , 'grand larceny' , 'by bank acct compromise-unclassified');
INSERT INTO NY_Crimes.Crime_description VALUES (13 , 'grand larceny' , 'by credit card acct compromise-existing acct');
INSERT INTO NY_Crimes.Crime_description VALUES (14 , 'grand larceny' , 'by dishonest emp');
INSERT INTO NY_Crimes.Crime_description VALUES (15 , 'grand larceny' , 'by extortion');
INSERT INTO NY_Crimes.Crime_description VALUES (16 , 'grand larceny' , 'by false promise');
INSERT INTO NY_Crimes.Crime_description VALUES (17 , 'grand larceny' , 'by false promise-in person contact');
INSERT INTO NY_Crimes.Crime_description VALUES (18 , 'grand larceny' , 'by false promise-not in person contact');
INSERT INTO NY_Crimes.Crime_description VALUES (19 , 'grand larceny' , 'by identity theft-unclassified');
INSERT INTO NY_Crimes.Crime_description VALUES (20 , 'grand larceny' , 'by open bank acct');
INSERT INTO NY_Crimes.Crime_description VALUES (21 , 'grand larceny' , 'by open credit card (new acct)');
INSERT INTO NY_Crimes.Crime_description VALUES (22 , 'grand larceny' , 'by open/compromise cell phone acct');
INSERT INTO NY_Crimes.Crime_description VALUES (23 , 'grand larceny' , 'by theft of credit card');
INSERT INTO NY_Crimes.Crime_description VALUES (24 , 'grand larceny' , 'from boat, unattended');
INSERT INTO NY_Crimes.Crime_description VALUES (25 , 'grand larceny' , 'from building (non-residence) unattended');
INSERT INTO NY_Crimes.Crime_description VALUES (26 , 'grand larceny' , 'from coin machin');
INSERT INTO NY_Crimes.Crime_description VALUES (27 , 'grand larceny' , 'from eatery, unattended');
INSERT INTO NY_Crimes.Crime_description VALUES (28 , 'grand larceny' , 'from night club, unattended');
INSERT INTO NY_Crimes.Crime_description VALUES (29 , 'grand larceny' , 'from open areas, unattended');
INSERT INTO NY_Crimes.Crime_description VALUES (30 , 'grand larceny' , 'from person, bag open/dip');
INSERT INTO NY_Crimes.Crime_description VALUES (31 , 'grand larceny' , 'from person,personal electronic device(snatch)');
INSERT INTO NY_Crimes.Crime_description VALUES (32 , 'grand larceny' , 'from person,pick');
INSERT INTO NY_Crimes.Crime_description VALUES (33 , 'grand larceny' , 'from person,purs');
INSERT INTO NY_Crimes.Crime_description VALUES (34 , 'grand larceny' , 'from person,uncl');
INSERT INTO NY_Crimes.Crime_description VALUES (35 , 'grand larceny' , 'from pier, unattended');
INSERT INTO NY_Crimes.Crime_description VALUES (36 , 'grand larceny' , 'from residence, unattended');
INSERT INTO NY_Crimes.Crime_description VALUES (37 , 'grand larceny' , 'from store-shopl');
INSERT INTO NY_Crimes.Crime_description VALUES (38 , 'grand larceny' , 'from truck, unattended');
INSERT INTO NY_Crimes.Crime_description VALUES (39 , 'grand larceny' , 'from vehicle/motorcycle');
INSERT INTO NY_Crimes.Crime_description VALUES (40 , 'grand larceny' , 'grand larceny-check from mailb');
INSERT INTO NY_Crimes.Crime_description VALUES (41 , 'grand larceny' , 'larceny,grand person,neck chai');
INSERT INTO NY_Crimes.Crime_description VALUES (42 , 'grand larceny' , 'of bicycle');
INSERT INTO NY_Crimes.Crime_description VALUES (43 , 'grand larceny' , 'of boat');
INSERT INTO NY_Crimes.Crime_description VALUES (44 , 'grand larceny' , 'of vehicular/motorcycle accessories');
INSERT INTO NY_Crimes.Crime_description VALUES (45 , 'burglary' , 'commercial,day');
INSERT INTO NY_Crimes.Crime_description VALUES (46 , 'burglary' , 'commercial,night');
INSERT INTO NY_Crimes.Crime_description VALUES (47 , 'burglary' , 'commercial,unknown ti');
INSERT INTO NY_Crimes.Crime_description VALUES (48 , 'burglary' , 'residence,day');
INSERT INTO NY_Crimes.Crime_description VALUES (49 , 'burglary' , 'residence,night');
INSERT INTO NY_Crimes.Crime_description VALUES (50 , 'burglary' , 'residence,unknown tim');
INSERT INTO NY_Crimes.Crime_description VALUES (51 , 'burglary' , 'truck day');
INSERT INTO NY_Crimes.Crime_description VALUES (52 , 'burglary' , 'truck night');
INSERT INTO NY_Crimes.Crime_description VALUES (53 , 'burglary' , 'unclassified,day');
INSERT INTO NY_Crimes.Crime_description VALUES (54 , 'burglary' , 'unclassified,night');
INSERT INTO NY_Crimes.Crime_description VALUES (55 , 'burglary' , 'unclassified,unknown');
INSERT INTO NY_Crimes.Crime_description VALUES (56 , 'burglary' , 'unknown time');

select "nombre de tuples = ",count(*) from NY_Crimes.Crime_description;
