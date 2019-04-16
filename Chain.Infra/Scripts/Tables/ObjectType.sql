CREATE TABLE IF NOT EXISTS `chaindb`.`type_object` (
  `id_type` CHAR(38) NOT NULL,
  `type_name` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`id_type`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;