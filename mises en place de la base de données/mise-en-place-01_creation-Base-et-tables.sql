-- IMPORTANT : à exécuter en tant que ROOT !

-- 

-- ----------------------------------------------------------------------------
-- RAZ en cas de besoin
-- ----------------------------------------------------------------------------
DROP TABLE IF EXISTS NY_Crimes.Crime;
DROP TABLE IF EXISTS NY_Crimes.Crime_description ;
DROP TABLE IF EXISTS NY_Crimes.Jurisdiction ;

DROP USER 'esilvs6' ;
DROP DATABASE IF EXISTS NY_Crimes ;


-- ----------------------------------------------------------------------------
-- Base de Données & Utilisateur y ayant accès (en plus de l'admin 'root')
-- ----------------------------------------------------------------------------
CREATE DATABASE IF NOT EXISTS NY_Crimes ;
USE NY_Crimes;

-- Normalement, cet utilisateur existe déjà sur votre ordinateur (sinon décommenter)
CREATE USER 'esilvs6' IDENTIFIED BY 'esilvs6';
GRANT ALL ON NY_Crimes.* TO 'esilvs6'@'localhost';
FLUSH PRIVILEGES ;

-- ----------------------------------------------------------------------------
-- Pour purger
-- ----------------------------------------------------------------------------
-- DROP USER 'esilvs6' ;
-- DROP DATABASE NY_Crimes ;


-- ----------------------------------------------------------------------------
-- Création des Tables
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS NY_Crimes.Crime_description (
	id INT NOT NULL AUTO_INCREMENT ,
	description VARCHAR( 35 ) NOT NULL ,
	desc_specificity VARCHAR( 50 ) NOT NULL ,
	PRIMARY KEY (id)
) ENGINE = MYISAM ;


CREATE TABLE IF NOT EXISTS NY_Crimes.Jurisdiction (
	id INT NOT NULL AUTO_INCREMENT ,
	name VARCHAR( 25 ) NOT NULL ,
	PRIMARY KEY (id)
) ENGINE = MYISAM ;


CREATE TABLE IF NOT EXISTS NY_Crimes.Crime (
	id INT NOT NULL AUTO_INCREMENT ,
	date VARCHAR( 10 ) NOT NULL ,
	borough VARCHAR( 15 ) NOT NULL ,
	coord_X INT NOT NULL ,
	coord_Y INT NOT NULL ,
	crime_description_id INT( 3 ) NOT NULL ,
	jurisdiction_id INT( 3 ) NOT NULL ,
	PRIMARY KEY (id) ,
	FOREIGN KEY (crime_description_id) REFERENCES NY_Crimes.Crime_description(id) ,
	FOREIGN KEY (jurisdiction_id) REFERENCES NY_Crimes.Jurisdiction(id)
) ENGINE = MYISAM ;

