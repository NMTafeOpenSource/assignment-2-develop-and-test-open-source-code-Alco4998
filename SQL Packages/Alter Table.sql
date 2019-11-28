ALTER TABLE `services` ADD CONSTRAINT `services_ibfk_1` FOREIGN KEY ( `vehicle_id` ) REFERENCES `vehicles` ( `id` ) ON UPDATE CASCADE;
ALTER TABLE `journey` ADD CONSTRAINT `journey_ibfk_1` FOREIGN KEY ( `vehicle_id` ) REFERENCES `vehicles` ( `id` ) ON UPDATE CASCADE;
ALTER TABLE `fuel` ADD CONSTRAINT `fuel_ibfk_1` FOREIGN KEY ( `vehicle_id` ) REFERENCES `vehicles` ( `id` ) ON UPDATE CASCADE;