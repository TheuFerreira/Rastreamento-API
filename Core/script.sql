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