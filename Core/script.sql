CREATE SCHEMA tracking;

USE tracking;

CREATE TABLE users(
	id_user INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
	name VARCHAR(150) NOT NULL,
	email VARCHAR(100) NOT NULL,
	password VARCHAR(256) NOT NULL,
	birth_date DATETIME NOT NULL, 
	created_at DATETIME NOT NULL,
	updated_at DATETIME NOT NULL
);

CREATE TABLE delivery (
	id_delivery BIGINT NOT NULL PRIMARY KEY AUTO_INCREMENT,
	id_user INT NOT NULL,
	description VARCHAR(200) NOT NULL,
	origin VARCHAR(200) NOT NULL,
	destiny VARCHAR(200) NOT NULL,
	observation VARCHAR(200) NULL,
	code VARCHAR(16) NOT NULL,
	created_at DATETIME NOT NULL,
	last_update_date DATETIME NOT NULL
);

CREATE TABLE user_has_delivery(
	id_delivery INT NOT NULL,
	id_user INT NOT NULL,
	created_at DATETIME NOT NULL,

	PRIMARY KEY (id_delivery, id_user)
);


ALTER TABLE `tracking`.`delivery` 
CHANGE COLUMN `code` `code` VARCHAR(64) NOT NULL ;

ALTER TABLE delivery
ADD COLUMN status INT NOT NULL DEFAULT 0 AFTER last_update_date;

CREATE TABLE delivery_position (
	id_delivery_position INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
	id_delivery INT NOT NULL,
	latitude DECIMAL NOT NULL,
	longitude DECIMAL NOT NULL,
	created_at DATETIME NOT NULL
);

ALTER TABLE `tracking`.`delivery_position` 
CHANGE COLUMN `latitude` `latitude` DECIMAL(8,6) NOT NULL ,
CHANGE COLUMN `longitude` `longitude` DECIMAL(9,6) NOT NULL ;

CREATE TABLE address(
	id_address INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
    cep VARCHAR(8) NOT NULL,
    uf VARCHAR(2) NOT NULL,
    city VARCHAR(64) NOT NULL,
    district VARCHAR(128),
    street VARCHAR(128),
    number VARCHAR(16),
    complement VARCHAR(128)
);

ALTER TABLE delivery
DROP COLUMN origin,
DROP COLUMN destiny,
ADD COLUMN address_origin_id INT NOT NULL AFTER id_user,
ADD COLUMN address_destiny_id INT NOT NULL AFTER address_origin_id;

ALTER TABLE delivery_position
ADD COLUMN id_address INT NULL AFTER id_delivery_position;