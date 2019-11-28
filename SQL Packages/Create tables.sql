CREATE TABLE
IF
	NOT EXISTS `vehicles` (
		`id` BIGINT UNSIGNED NOT NULL AUTO_INCREMENT,
		`make` VARCHAR ( 64 ) NOT NULL DEFAULT "Unknown",
		`model` VARCHAR ( 128 ) NOT NULL,
		`make-Year` INTEGER ( 4 ) UNSIGNED ZEROFILL NOT NULL DEFAULT 1,
		`registration` VARCHAR ( 16 ) NOT NULL,
		`fuel` VARCHAR ( 8 ) NOT NULL DEFAULT "Unknown",
		`tank-Size` DECIMAL ( 9, 1 ) UNSIGNED NULL,
		`initals` VARCHAR ( 4 ) NOT NULL DEFAULT "xxx",
		`created-At` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ( 0 ),
		`updated-At` datetime NULL,
		PRIMARY KEY ( `id` ) 
	) CREATE TABLE
IF
	NOT EXISTS `services` (
		`id` BIGINT UNSIGNED NOT NULL AUTO_INCREMENT,
		`vehicle_id` BIGINT UNSIGNED ZEROFILL NOT NULL,
		`odometer` DECIMAL ( 9, 1 ) NOT NULL,
		`serviced_at` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ( 0 ),
		`updated_at` datetime NOT NULL,
		PRIMARY KEY ( `id` ) 
	) CREATE TABLE
IF
	NOT EXISTS `journey` (
		`id` BIGINT UNSIGNED NOT NULL AUTO_INCREMENT,
		`vehicle_id` BIGINT UNSIGNED ZEROFILL NOT NULL DEFAULT 0,
		`distance` DECIMAL ( 9, 1 ) UNSIGNED NOT NULL,
		`journey_at` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ( 0 ),
		`created_at` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ( 0 ),
		`updated_at` datetime NULL,
		PRIMARY KEY ( `id` ) 
	) CREATE TABLE
IF
	NOT EXISTS `fuel` (
		`id` BIGINT UNSIGNED NOT NULL AUTO_INCREMENT,
		`vehicle_id` BIGINT ( 64 ) UNSIGNED ZEROFILL NOT NULL,
		`amount` DECIMAL ( 65 ) NOT NULL,
		`cost` DECIMAL ( 4 ) NOT NULL,
		`serviced_at` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ( 0 ),
		`updated_at` datetime NOT NULL,
	PRIMARY KEY ( `id` ) 
	) -- CREATE TABLE `ajc_car_asses1` . `fuel`;