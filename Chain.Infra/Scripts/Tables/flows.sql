CREATE TABLE IF NOT EXISTS `chaindb`.`flows` (
  `idflow` CHAR(38) NOT NULL,
  `idobject` CHAR(38) NOT NULL,
  `idowner` CHAR(38) NOT NULL,
  `ownerType` INT NOT NULL,
  `date_creation` TIMESTAMP NOT NULL,
  `path_object` VARCHAR(800) NOT NULL,
  PRIMARY KEY (`idflow`),
  INDEX `object_key_idx` (`idobject` ASC),
  INDEX `group_key_idx` (`idowner` ASC),
  CONSTRAINT `object_key`
    FOREIGN KEY (`idobject`)
    REFERENCES `chaindb`.`object` (`idobject`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;