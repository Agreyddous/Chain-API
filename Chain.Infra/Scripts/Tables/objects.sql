CREATE TABLE IF NOT EXISTS `chaindb`.`object` (
  `idobject` CHAR(38) NOT NULL,
  `title` VARCHAR(255) NOT NULL,
  `description` VARCHAR(255) NOT NULL,
  `creationdate` TIMESTAMP NOT NULL,
  `creator` CHAR(38) NOT NULL,
  `path_object` VARCHAR(255) NOT NULL,
  `father_object` CHAR(38) NOT NULL,
  `type_object` CHAR(38) NOT NULL,
  `status` CHAR(38) NOT NULL,
  PRIMARY KEY (`idobject`),
  INDEX `creator_key_idx` (`creator` ASC),
  INDEX `father_key_idx` (`father_object` ASC),
  INDEX `type_key_idx` (`type_object` ASC),
  INDEX `status_key_idx` (`status` ASC),
  CONSTRAINT `creator_key`
    FOREIGN KEY (`creator`)
    REFERENCES `chaindb`.`users` (`iduser`),
  CONSTRAINT `father_key`
    FOREIGN KEY (`father_object`)
    REFERENCES `chaindb`.`object` (`idobject`),
  CONSTRAINT `status_key`
    FOREIGN KEY (`status`)
    REFERENCES `chaindb`.`status_object` (`id_status`),
  CONSTRAINT `type_key`
    FOREIGN KEY (`type_object`)
    REFERENCES `chaindb`.`type_object` (`id_type`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;