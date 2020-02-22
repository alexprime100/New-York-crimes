
-- ----------------------------------------------------------------------------
-- Insertion dans table 'Jurisdiction'
-- ----------------------------------------------------------------------------

INSERT INTO NY_Crimes.Jurisdiction VALUES (1, 'N.Y. POLICE DEPT');
INSERT INTO NY_Crimes.Jurisdiction VALUES (2, 'N.Y. HOUSING POLICE');
INSERT INTO NY_Crimes.Jurisdiction VALUES (3, 'N.Y. TRANSIT POLICE');
INSERT INTO NY_Crimes.Jurisdiction VALUES (4, 'PORT AUTHORITY');
INSERT INTO NY_Crimes.Jurisdiction VALUES (5, 'HEALTH & HOSP CORP');
INSERT INTO NY_Crimes.Jurisdiction VALUES (6, 'AMTRACK');
INSERT INTO NY_Crimes.Jurisdiction VALUES (7, 'POLICE DEPT NYC');
INSERT INTO NY_Crimes.Jurisdiction VALUES (8, 'N.Y. STATE POLICE');
INSERT INTO NY_Crimes.Jurisdiction VALUES (9, 'TRI-BORO BRDG TUNNL');
INSERT INTO NY_Crimes.Jurisdiction VALUES (10, 'LONG ISLAND RAILRD');
INSERT INTO NY_Crimes.Jurisdiction VALUES (11, 'OTHER');
INSERT INTO NY_Crimes.Jurisdiction VALUES (12, 'METRO NORTH');
INSERT INTO NY_Crimes.Jurisdiction VALUES (13, 'STATN IS RAPID TRANS');
INSERT INTO NY_Crimes.Jurisdiction VALUES (14, 'U.S. PARK POLICE');
INSERT INTO NY_Crimes.Jurisdiction VALUES (15, 'DEPT OF CORRECTIONS');

select "nombre de tuples = ",count(*) from NY_Crimes.Jurisdiction;