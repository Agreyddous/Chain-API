CREATE TABLE IF NOT EXISTS `chaindb`.`status_object` (
  `id_status` CHAR(38) NOT NULL,
  `status_name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id_status`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;