CREATE TABLE IF NOT EXISTS `chaindb`.`groups` (
  `idgroup` CHAR(38) NOT NULL,
  `group_name` VARCHAR(45) NOT NULL,
  `description` VARCHAR(255) NOT NULL,
  PRIMARY KEY (`idgroup`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;